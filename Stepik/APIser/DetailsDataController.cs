using System;
using System.Collections.Generic;
using System.Text;

namespace Stepik.APIser
{
    class DetailsDataController
    {
        public List<StepikCourseDetails> info { get; }
        public DetailsDataController(List<StepikCourseDetails> courses)
        {
            info = courses;
        }
    }
}
