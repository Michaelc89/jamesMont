using System;
using System.Collections.Generic;
using System.Text;

namespace jamesMont.Model
{
    public class TheBookingTable
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("BookingName")]
        public string BookingName { get; set; }

        [Newtonsoft.Json.JsonProperty("Date")]
        public DateTime Date { get; set; }

        [Newtonsoft.Json.JsonProperty("Slot")]
        public int Slot { get; set; }

        [Newtonsoft.Json.JsonProperty("Length")]
        public float Length { get; set; }
    }
}
