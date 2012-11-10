using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Globalization;
using System.Text.RegularExpressions;

namespace Peanuts
{
    public class UserInfo
    {
        private string countryCode;
        private string locale;
        private Windows.Storage.ApplicationDataContainer roamingSettings;


        public string Locale { get { return locale; } }
        public string Country { get { return countryCode; } }


        // initialise should be run after constructor, in try block (catch NoListingsAvailableException)
        public UserInfo() {
            roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
        }


        public void initialise() {

            // try to get countryCode from roamingSettings, if not there get from Windows API and save in roamingSettings
            if (roamingSettings.Values.ContainsKey("countryCode")) {
                countryCode = roamingSettings.Values["countryCode"].ToString();
            } else {
                GeographicRegion region = new GeographicRegion();
                string countryDetected = region.CodeTwoLetter;
                if (tvListingsAvailable(countryDetected)) {
                    roamingSettings.Values["countryCode"] = countryDetected;
                    countryCode = countryDetected;
                } else {
                    countryCode = "Not Supported";
                    throw new NoListingsAvailableException("No TV Listings for CountryCode detected from Windows", countryDetected);
                }
            }

            // same for this
            if (roamingSettings.Values.ContainsKey("locale")) {
                locale = roamingSettings.Values["locale"].ToString();
            } else {
                IReadOnlyList<string> languages = Windows.Globalization.ApplicationLanguages.Languages;
                locale = "NOT SET";
                foreach (string lang in languages) {
                    if (validLocale(lang)) {
                        roamingSettings.Values["locale"] = lang;
                        locale = lang;
                        break;
                    }
                }
                if (locale == "NOT SET") {
                    locale = "en-US";
                }
            }
        }

        private bool tvListingsAvailable(string country) {
            string regex = "AR|SV|NI|AT|FI|NO|BE|FR|PA|BM|DE|PE|BO|GT|PL|CA|HN|PT|CL|IE|ES|CO|IT|SE|CR|JM|CH|DK|LU|GB|DO|MX|US|EC|NL|VE";
            Match match = Regex.Match(country, regex);
            return match.Success;
        }

        private bool validLocale(string loc) {
            string regex = "da-DK|no-NO|nl-BE|pl-PL|nl-NL|pt-PT|en-BM|es-AR|en-CA|es-BO|en-IE|es-CL|en-JM|es-CO|en-GB|es-CR|en-US|es-DO|fi-FI|es-EC|fl-BE|es-SV|fr-BE|es-GT|fr-CA|es-HN|fr-FR|es-MX|fr-LU|es-NI|fr-CH|es-PA|de-AT|es-PE|de-DE|es-ES|de-LU|es-US|de-CH|es-VE|it-IT|sv-SE";
            Match match = Regex.Match(loc, regex);
            return match.Success;
        }

    }
}
