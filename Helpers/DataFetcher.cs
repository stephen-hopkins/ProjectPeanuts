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

        public DataFetcher() {
            tvListingsApiKey = "zpcmbxsgmebm3g9vb4xuhasq";
            searchApiKey = "zpcmbxsgmebm3g9vb4xuhasq";
            searchSecretKey = "88nsXYtbPX";
        }

        public async Task<TVServiceCollection> getTVServices() {

            TVServiceCollection tvServices = new TVServiceCollection();

            String address = "http://api.rovicorp.com/TVlistings/v9/listings/services/postalcode/0/info?locale=en-GB&countrycode=GB&apikey=4xgjm3mcyfy97m7g3fusap2t";

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(address);
            string jsonString = await response.Content.ReadAsStringAsync();

            JsonObject json = JsonObject.Parse(jsonString);
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
