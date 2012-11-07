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
        private string apiKey;
        private string secretKey;

        public DataFetcher() {
            apiKey = "zpcmbxsgmebm3g9vb4xuhasq";
            secretKey = "88nsXYtbPX";
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

        private string getRoviSig() {
            long unixTime = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            string toBeHashed = apiKey + secretKey + unixTime;

            IBuffer binaryBuffer = CryptographicBuffer.ConvertStringToBinary(toBeHashed, BinaryStringEncoding.Utf8);
            HashAlgorithmProvider hasher = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            IBuffer hashedBinaryBuffer = hasher.HashData(binaryBuffer);
            if (hashedBinaryBuffer.Length != hasher.HashLength) {
                throw new PeanutsException("Error creating MD5 hash in DataFetcher");
            }

            return CryptographicBuffer.EncodeToBase64String(hashedBinaryBuffer);
        }



    }
}
