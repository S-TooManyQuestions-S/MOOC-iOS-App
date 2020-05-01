using CourseLib;
using CourseLib.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Udemy
{
    public class UdemyCourse : Course
    {
        public UdemyCourse(string title, double relevancy_score, string image_240x135, string url, int id)
            : base(title, new Rating(relevancy_score), new Image(image_240x135), new CurrentInfo("Udemy", id,string.Format(APIpath,id), detailsPath+url))
        { }
        private static string detailsPath = "https://www.udemy.com";
        private static string APIpath = "https://www.udemy.com/api-2.0/courses/{0}?fields%5Bcourse%5D=description,headline,content_info,requirements_data,_class";

        /*private static double GetScore()
        {

        }*/
    }
}
