using System;
using System.Collections.Generic;
using System.Text;

namespace jamesMont.Model
{
    public class CupOfCoffee
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }
        
        [Microsoft.WindowsAzure.MobileServices.Version]
        public string AzureVersion { get; set; }

        public string Email { get; set; }

        public DateTime DateUtc { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string DateDisplay { get { return DateUtc.ToLocalTime().ToString("d"); } }

        [Newtonsoft.Json.JsonIgnore]
        public string TimeDisplay { get { return DateUtc.ToLocalTime().ToString("t"); } }

    }
}
