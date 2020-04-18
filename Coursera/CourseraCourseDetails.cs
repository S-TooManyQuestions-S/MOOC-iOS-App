using CourseLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coursera
{
    public class CourseraCourseDetails : CourseDetails
    {
        public CourseraCourseDetails(string summary, string workload, string target_audience, string course_format, string description)
           : base(summary, description, target_audience, course_format, workload) { }
        
    }
}
