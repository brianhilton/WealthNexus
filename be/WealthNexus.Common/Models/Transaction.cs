using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WealthNexus.Common.Models {
    public class Transaction
    {
        [JsonProperty(PropertyName = "id")]
        public string? Id { get; set; }

        public double Amount { get; set; }
        
        public string AccountId { get; set; }

        public DateTime Date { get; set; }
    }


}
