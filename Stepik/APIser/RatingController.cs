using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stepik.APIser
{

    public class StepikRating
    {
        public double average { get; }
        public StepikRating(double average)
            => this.average = average;
    }

    public class RatingController
    {
        [JsonProperty("course-review-summaries")]
        public StepikRating [] array { get; set; }
    }
   
}
