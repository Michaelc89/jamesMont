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

        [Newtonsoft.Json.JsonProperty("Length")]
        public float Length { get; set; }

        public Categories()
        {

        }
        public Categories(string id, string category, float len)
        {
            this.Id = id;
            this.CategoryName = category;
            this.Length = len;
        }
    }
}
