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
    public class DataFetcher : IDataFetcher
    {
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

        public async Task<List<SeriesSummary>> searchSeries(string input) {
            
            List<SeriesSummary> result = new List<SeriesSummary>();
            string address = "http://api.rovicorp.com/search/v2.1/video/search?apikey=" + searchApiKey + "&sig=" + getRoviSearchSig() + "&query=" + input + "&entitytype=tvseries" + "&format=json";

            HttpClient httpClient = new HttpClient();
            string response = await httpClient.GetStringAsync(address);

            JsonObject json = JsonObject.Parse(response);
            JsonArray jArray = json.GetNamedObject("searchResponse").GetNamedArray("Results");

            // masterTitle is inside a json object annoyingly - need to amend

            for (uint n = 0; n < jArray.Count; n++) {
                string title = jArray.GetObjectAt(n).GetNamedString("masterTitle");
                string id = jArray.GetObjectAt(n).GetNamedString("id");
            }


            int bint = 5;
            return result;

        }




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
