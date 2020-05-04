using CourseLib;
using CourseLib.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Coursera
{
    public class CourseraCourse:Course
    {
        public CourseraCourse(string name, double localrating, string course_cover, string detailsPath)
            :base(name,new Rating(localrating),new Image(course_cover), new CurrentInfo("Coursera",detailsPath,detailsPath))
            { }
        
    }
}
