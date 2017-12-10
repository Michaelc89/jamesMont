using System;
using System.Collections.Generic;
using System.Text;

namespace jamesMont.Model
{
    public class ShopTBL
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("ProductName")]
        public string ProductName { get; set; }

        public ShopTBL()
        {

        }
        public ShopTBL(string prod)
        {
            this.ProductName = prod;
        }
    }
}


