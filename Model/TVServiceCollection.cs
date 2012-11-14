using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peanuts
{
    public class TVServiceCollection
    {

        private List<TVService> tvServices;

        // used to cache results from getProviders function - stores all TVServices in previousArea
        private List<TVService> tvServicesInArea;
        private string previousArea;

        public TVServiceCollection() {
            tvServices = new List<TVService>();
            tvServicesInArea = new List<TVService>();
        }

        public void Add(TVService toBeAdded) {
            tvServices.Add(toBeAdded);
        }

 
        /// <summary>
        /// Returns list of areas that TV Services in this collection cover.
        /// </summary>
        /// <returns>List{String}</returns>
        public List<String> getAreas() {
            List<string> areas = new List<string>(tvServices.Count);
            foreach (TVService tvService in tvServices) {
                if (!areas.Contains(tvService.Area)) {
                    areas.Add(tvService.Area);
                }
            }
            return areas;
        }

        /// <summary>
        /// Returns list of TV Service providers in specified area.
        /// </summary>
        /// <param name="area">string</param>
        /// <returns>List{string}</returns>
        public List<string> getProviders(string area) {
            List<string> providers = new List<string>(tvServices.Count);
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

        /// <summary>
        /// Returns list of TV Services in specified area, from specified provider
        /// </summary>
        /// <param name="area">string</param>
        /// <param name="provider">string</param>
        /// <returns>List{string}</returns>
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

 
        /// <summary>
        /// Get serviceID for name of TV Service.
        /// </summary>
        /// <param name="Name of TV Service">string</param>
        /// <returns>string</returns>
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
