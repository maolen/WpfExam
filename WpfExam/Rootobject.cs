using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfExam
{
    public class Rootobject
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }
        
    }

}
