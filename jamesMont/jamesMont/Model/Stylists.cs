using System;
using System.Collections.Generic;
using System.Text;

namespace jamesMont.Model
{
    public class Stylists
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public string id { get; set; }

        [Newtonsoft.Json.JsonProperty("StylistName")]
        public string StylistName { get; set; }

        public Stylists(string name)
        {
            this.StylistName = name;
        }
    }
}
