using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Udemy.APIser
{
    public class MainDataController
    {
        [JsonProperty("results")]
        public List<UdemyCourse> results { get; set; } //результаты  поиска
    }
}
