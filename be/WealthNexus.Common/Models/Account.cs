using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WealthNexus.Common.Models {
    public class Account {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double AccountBalance { get; set; }
        public double InterestRate { get; set; }
        public DateTime CreationDate { get; set; }


    }
}
