using System;
using System.Globalization;
using AngleSharp.Html.Parser;
using CourseLib;
using CourseLib.Types;


namespace Stepik
{
    public class StepikCourse : Course
    { //доделать score
        public StepikCourse(double course, string course_title, string course_cover)
             : base(course_title, new Rating(StepikMethods.GetStepikRating(course)), new Image(course_cover), new CurrentInfo("Stepik", APIpath + course, Coursepath + course + "/"))
        {
        }
        private static string APIpath = "https://stepik.org/api/courses/";
        private static string Coursepath = "https://stepik.org/course/";

    }
}
