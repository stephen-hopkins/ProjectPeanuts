using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts
{
    class TVServiceCollection
    {

        private List<TVService> tvServices;

        // used to cache results from getProviders function - stores all TVServices in previousArea
        private List<TVService> tvServicesInArea;
        private string previousArea;

        public TVServiceCollection() {
            tvServices = new List<TVService>();
            tvServicesInArea = new List<TVService>();
        }

        public void addTVService(TVService toBeAdded) {
            tvServices.Add(toBeAdded);
        }

 

        public List<String> getAreas() {
            List<string> areas = new List<string>();
            foreach (TVService tvService in tvServices) {
                if (!areas.Contains(tvService.Area)) {
                    areas.Add(tvService.Area);
                }
            }
            return areas;
        }

        public List<string> getProviders(string area) {
            List<string> providers = new List<string>();
            previousArea = area;
            tvServicesInArea.Clear();

            foreach(TVService tvService in tvServices) {
                if ( (tvService.Area == area) && (!providers.Contains(tvService.Provider)) ) {
                    providers.Add(tvService.Provider);
                    tvServicesInArea.Add(tvService);
                }
            }
            return providers;
        }

        public List<string> getTVServices(string area, string provider) {

            // check if cache (tvServicesInArea) contains relevant results, if so use it
            if (area == previousArea) {
                List<string> tvServicesToReturn = new List<string>();
                foreach (TVService tvService in tvServicesInArea) {
                    if ( (tvService.Provider == provider) && (!tvServicesToReturn.Contains(tvService.Name)) ) {
                        tvServicesToReturn.Add(tvService.Name);
                    }
                }
                return tvServicesToReturn;
            // if not, run getProviders to make cache correct, then recursively return results
            } else {
                getProviders(area);
                return getTVServices(area, provider);
            }
        }

 

        public string getServiceID(string name) {
            foreach (TVService tvService in tvServices) {
                if (tvService.Name == name) {
                    return tvService.ID;
                }
            }
            throw new PeanutsException("Invalid TVService.name given to getServiceID");
        }
    }
}
