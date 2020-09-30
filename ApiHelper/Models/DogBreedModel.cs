using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ApiHelper.Models
{
    class DogBreedModel
    {
        [JsonProperty("message")]
        public Dictionary<string, List<string>> Breeds { get; set; }
    }
}
