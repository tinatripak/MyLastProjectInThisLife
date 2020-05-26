using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace d
{
    public class ArrayTitle
    {
        [JsonProperty("results")]
        public Titles[] Results { get; set; }
    }
    public class Titles
    {
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
