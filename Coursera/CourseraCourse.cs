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
            :base(name,new Rating(localrating),new Image(course_cover, logoPath), new CurrentInfo("Coursera",0,detailsPath))
            { }
        private static string logoPath = "https://www.whizsky.com/wp-content/uploads/2015/02/Coursera-Education-website-in-India-1068x1068.png";
    }
}
