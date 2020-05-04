using System;
using System.Globalization;
using AngleSharp.Html.Parser;
using CourseLib;
using CourseLib.Types;


namespace Stepik
{
    public class StepikCourse : Course
    { 
        private static string APIpath = "https://stepik.org/api/courses/";
        private static string Coursepath = "https://stepik.org/course/";

        public StepikCourse(double score, double course, string course_title, string course_cover)
             : base(course_title, 0, new Image(course_cover), new CurrentInfo("Stepik", APIpath + course, Coursepath + course + "/"))
        => id = course;
       
        //локальное id (course)
       public double id { get; }
    }
}
