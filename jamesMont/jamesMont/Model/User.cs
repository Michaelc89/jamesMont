using System;
using System.Collections.Generic;
using System.Text;

namespace jamesMont.Model
{
    public class User
    {
        [Newtonsoft.Json.JsonProperty("Id")]
        public string Id { get; set; }

        [Newtonsoft.Json.JsonProperty("FirstName")]
        public string FirstName { get; set; }

        [Newtonsoft.Json.JsonProperty("Surname")]
        public string Surname { get; set; }

        [Newtonsoft.Json.JsonProperty("Email")]
        public string Email { get; set; }

        [Newtonsoft.Json.JsonProperty("Password")]
        public string Password { get; set; }

        [Newtonsoft.Json.JsonProperty("Phone")]
        public string Phone { get; set; }

        public User()
        {
            
        }
        public User(string first, string second, string email, string password, string phone)
        {
            this.FirstName = first;
            this.Surname = second;
            this.Email = email;
            this.Password = password;
            this.Phone = phone;
        }

    }
}
