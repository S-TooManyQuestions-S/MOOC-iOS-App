using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stepik.APIser
{
    class DetailsDataController
    {
        [JsonProperty("")]
        public List<StepikCourseDetails> info { get; }
        public DetailsDataController(List<StepikCourseDetails> courses)
        {
            info = courses;
        }
    }
}
