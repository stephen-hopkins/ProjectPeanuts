using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Data.Json;
using Windows.Foundation.Collections;

namespace Peanuts
{
    public class DataFetcher : IDataFetcher
    {
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
    }
}
