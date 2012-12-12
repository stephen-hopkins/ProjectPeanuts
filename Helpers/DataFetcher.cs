using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Data.Json;
using Windows.Foundation.Collections;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace Peanuts
{
    public class DataFetcher 
    {
        private double preferredImageFormatID = 123;

        private string tvListingsApiKey;
        private string searchApiKey;
        private string searchSecretKey;
        private UserInfo userInfo;

        public DataFetcher() {
            tvListingsApiKey = "4xgjm3mcyfy97m7g3fusap2t";
            searchApiKey = "zpcmbxsgmebm3g9vb4xuhasq";
            searchSecretKey = "88nsXYtbPX";
            userInfo = new UserInfo();
            userInfo.initialise();
        }

        public async Task<Series> convertToFullSeries(SeriesSummary basic) {
            
            Series result = new Series(basic);

            HttpClient httpClient = new HttpClient();       
            int season = 1;
            int episode = 1;
            string baseAddress = "http://api.rovicorp.com/data/v1/video/season/{0}/episode/{1}/info?cosmoid=" + basic.CosmoID + "&apikey=" + searchApiKey + "&sig=" + getRoviSearchSig() + "&include=synopsis";
            
            bool noMoreEpisodes = false;
            while (!noMoreEpisodes) {
                string addressToUse = String.Format(baseAddress, season, episode);
                string response = await httpClient.GetStringAsync(addressToUse);
                JsonObject json = JsonObject.Parse(response);
                int responseCode = (int)json.GetNamedNumber("code");

                if (responseCode == 200) {
                    JsonObject videoObject = json.GetNamedObject("video");
                    string cosmoIDString = videoObject.GetNamedObject("ids").GetNamedString("cosmoId");
                    int cosmoID = int.Parse(cosmoIDString);
                    string title = videoObject.GetNamedString("episodeTitle");
                    string synopsis = videoObject.GetNamedObject("synopsis").GetNamedString("synopsis");
                    Episode thisEpisode = new Episode(title, season, episode, synopsis, cosmoID);
                    result.AddEpisode(thisEpisode);
                    episode++;
                } else if (responseCode == 404) {
                    if (episode == 1) {
                        noMoreEpisodes = true;
                    }
                    else {
                        season++;
                        episode = 1;
                    }
                } else {
                    throw new PeanutsException("Unexpected response code when requesting episode information");
                }    
            }
            return result;
        }



        /// <summary>
        /// Gets country, locale, and postcode from UserInfo and returns list of relevant TV services.  Need to do Task.Wait() then check for exceptions before getting result
        /// The function that does this should be async to avoid blocking gui.  Can throw various internet related exceptions.
        /// </summary>
        /// <returns>Task{TVServiceCollection}</returns>
        public async Task<TVServiceCollection> getTVServices() {

            TVServiceCollection tvServices = new TVServiceCollection();
            
            string address = "http://api.rovicorp.com/TVlistings/v9/listings/services/postalcode/" + userInfo.PostCode + "/info?locale=" + userInfo.Locale + "&countrycode=" + userInfo.Country + "&apikey=" + tvListingsApiKey;
            
            HttpClient httpClient = new HttpClient();
            string response = await httpClient.GetStringAsync(address);

            JsonObject json = JsonObject.Parse(response);
            JsonArray jArray = json.GetNamedObject("ServicesResult").GetNamedObject("Services").GetNamedArray("Service");

            for (uint n = 0; n < jArray.Count; n++) {
                string name = jArray.GetObjectAt(n).GetNamedString("Name");
                string id = jArray.GetObjectAt(n).GetNamedString("ServiceId");
                string area = jArray.GetObjectAt(n).GetNamedString("City");
                string provider = jArray.GetObjectAt(n).GetNamedString("MSO");
                string type = jArray.GetObjectAt(n).GetNamedString("Type");
                TVService tvService = new TVService(id, name, area, provider, type);
                tvServices.Add(tvService);
            }
            return tvServices;
        }

        /// <summary>
        /// Searchs for specified search term, returns list of possible Series matches.
        /// </summary>
        /// <param name="Search Term">string</param>
        /// <returns>Task{List{SeriesSummary}}</returns>
        public async Task<List<SeriesSummary>> searchSeries(string input) {
            
            List<SeriesSummary> result = new List<SeriesSummary>();
            string address = "http://api.rovicorp.com/search/v2.1/video/search?apikey=" + searchApiKey + "&sig=" + getRoviSearchSig() + "&query=" + input + "&entitytype=tvseries" + "&include=synopsis,images" + "&format=json";

            HttpClient httpClient = new HttpClient();
            string response = await httpClient.GetStringAsync(address);

            JsonObject json = JsonObject.Parse(response);
            JsonArray resultsArray = json.GetNamedObject("searchResponse").GetNamedArray("results");

            for (uint n = 0; n < resultsArray.Count; n++) {
                JsonObject thisResult = resultsArray.GetObjectAt(n);
                string roviID = thisResult.GetNamedNumber("id").ToString();

                JsonObject videoResult = thisResult.GetNamedObject("video");
                string title = videoResult.GetNamedString("masterTitle");
                string cosmoID = videoResult.GetNamedObject("ids").GetNamedString("cosmoId");

                string year = "";
                if (videoResult.GetNamedValue("releaseYear").ValueType == JsonValueType.Number) {
                    year = videoResult.GetNamedNumber("releaseYear").ToString();
                }

                string synopsis = "";
                if (videoResult.GetNamedValue("synopsis").ValueType == JsonValueType.String) {
                    synopsis = videoResult.GetNamedObject("synopsis").GetNamedString("synopsis");
                }

                // set image to first image, then look for image of preferred type, and replace with this if poss.  set to null if no images at all
                Uri image;
                if (videoResult.GetNamedValue("images").ValueType == JsonValueType.Array) {
                    JsonArray imageArray = videoResult.GetNamedArray("images");
                    image = new Uri(imageArray.GetObjectAt(0).GetNamedString("url"));
                    bool imageFound = false;
                    for (uint arrayVar = 1 ; arrayVar < imageArray.Count ; arrayVar++) {
                        if (!imageFound && imageArray.GetObjectAt(arrayVar).GetNamedNumber("formatid") == preferredImageFormatID) {
                            image = new Uri(imageArray.GetObjectAt(arrayVar).GetNamedString("url"));
                            imageFound = true;
                        }
                    }
                } else {
                    image = null;
                }
                result.Add(new SeriesSummary(title, image, synopsis, year, roviID, cosmoID));
            }
            
            return result;
        }



        // returns Sig needed for Rovi API.  Sig is valid for around 5 minutes.
        private string getRoviSearchSig() {
            string timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds.ToString();
            timestamp = timestamp.Substring(0, timestamp.IndexOf("."));
            string toBeHashed = searchApiKey + searchSecretKey + timestamp;
            
            HashAlgorithmProvider hasher = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer binaryBuffer = CryptographicBuffer.ConvertStringToBinary(toBeHashed, BinaryStringEncoding.Utf8);
            IBuffer hashedBuffer = hasher.HashData(binaryBuffer);
            return CryptographicBuffer.EncodeToHexString(hashedBuffer);
        }
    }
}
