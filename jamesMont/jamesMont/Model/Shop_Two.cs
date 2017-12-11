using System;
using System.Collections.Generic;
using System.Text;

namespace jamesMont.Model
{
    public class Shop_Two
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("ProductName")]
        public string ProductName { get; set; }

        [Newtonsoft.Json.JsonProperty("Quantity")]
        public float Quantity { get; set; }

        public Shop_Two()
        {

        }
        public Shop_Two(string prod)
        {
            this.ProductName = prod;
        }
    }
}







