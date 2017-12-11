using System;
using System.Collections.Generic;
using System.Text;

namespace jamesMont.Model
{
    public class Shop_TBL
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("ProductName")]
        public string ProductName { get; set; }

        [Newtonsoft.Json.JsonProperty("Quantity")]
        public float Quantity { get; set; }

        public Shop_TBL()
        {

        }
        public Shop_TBL(string prod)
        {
            this.ProductName = prod;
        }
    }
}





