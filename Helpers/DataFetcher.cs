using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Data.Json;

namespace Peanuts.Helpers
{
    class DataFetcher : Peanuts.Interfaces.IDataFetcher
    {
        public async Task<Dictionary<String, int>> getTVServices() {

            Dictionary<String, int> tvServices = new Dictionary<string,int>();

            String address = "http://api.rovicorp.com/TVlistings/v9/listings/services/postalcode/0/info?locale=en-GB&countrycode=GB&apikey=4xgjm3mcyfy97m7g3fusap2t";

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(address);
            string jsonString = await response.Content.ReadAsStringAsync();

            JsonObject json = JsonObject.Parse(jsonString); 


        }
    }
}
