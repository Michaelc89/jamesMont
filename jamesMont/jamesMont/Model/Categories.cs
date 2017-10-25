using System;
using System.Collections.Generic;
using System.Text;

namespace jamesMont.Model
{
    public class Categories
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("CategoryName")]
        public string CategoryName { get; set; }

        public Categories()
        {

        }
        public Categories(string id, string category)
        {
            this.Id = id;
            this.CategoryName = category;
        }
    }
}
