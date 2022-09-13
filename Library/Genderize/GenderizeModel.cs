using Library.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Library.Genderize
{
    public class GenderizeModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("gender")]
        [JsonProperty(NullValueHandling =NullValueHandling.Ignore)]
        public Gender Gender { get; set; }
        [JsonPropertyName("probability")]
        public double Probability { get; set; }
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
