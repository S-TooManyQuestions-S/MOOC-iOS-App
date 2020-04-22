using CourseLib;
using CourseLib.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Udemy
{
    public class UdemyCourse : Course
    {
        public UdemyCourse(string title, double predictive_score, string image_240x135, string url, int id)
            : base(title, new Rating(Math.Round(predictive_score,2)), new Image(image_240x135, logoPath), new CurrentInfo("Udemy", id, detailsPath+url))
        { }
        private static string logoPath = "https://www.udemy.com/staticx/udemy/images/v6/default-meta-image.png";
        private static string detailsPath = "https://www.udemy.com";
    }
}
