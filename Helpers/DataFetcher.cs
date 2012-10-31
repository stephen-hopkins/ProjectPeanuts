using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Data.Json;
using Windows.Foundation.Collections;

namespace Peanuts.Helpers
{
    public class DataFetcher : Peanuts.Interfaces.IDataFetcher
    {
        public async Task<Dictionary<string, string>> getTVServices() {

            Dictionary<string, string> tvServices = new Dictionary<string,string>();

            String address = "http://api.rovicorp.com/TVlistings/v9/listings/services/postalcode/0/info?locale=en-GB&countrycode=GB&apikey=4xgjm3mcyfy97m7g3fusap2t";

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(address);
            string jsonString = await response.Content.ReadAsStringAsync();

            JsonObject json = JsonObject.Parse(jsonString);
            JsonArray jArray = json.GetNamedObject("ServicesResult").GetNamedObject("Services").GetNamedArray("Service");

            for (uint n = 0; n < jArray.Count; n++) {
                string serviceName = jArray.GetObjectAt(n).GetNamedString("Name");
                string serviceID = jArray.GetObjectAt(n).GetNamedString("ServiceId");
                tvServices.Add(serviceName, serviceID);
            }

            int hello = 5;
     

            return tvServices;

        }
    }
}
