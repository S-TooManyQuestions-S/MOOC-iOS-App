using CourseLib;
using CourseLib.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Udemy
{
    public class UdemyCourse : Course
    {
        public UdemyCourse(string name, double localrating, string course_cover, string detailsPath)
            : base(name, new Rating(localrating), new Image(course_cover, logoPath), new CurrentInfo("Udemy", 0, detailsPath))
        { }
        private static string logoPath = "https://www.udemy.com/staticx/udemy/images/v6/default-meta-image.png";
    }
}
