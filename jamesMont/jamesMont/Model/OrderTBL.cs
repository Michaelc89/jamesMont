using System;
using System.Collections.Generic;
using System.Text;

namespace jamesMont.Model
{
    public class OrderTBL
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public string id { get; set; }

        [Newtonsoft.Json.JsonProperty("OrderDate")]
        public DateTime OrderDate { get; set; }

        [Newtonsoft.Json.JsonProperty("ProductID")]
        public string ProductID { get; set; }

        [Newtonsoft.Json.JsonProperty("Quantity")]
        public float Quantity { get; set; }

        [Newtonsoft.Json.JsonProperty("UserID")]
        public string UserID { get; set; }


    }
}
