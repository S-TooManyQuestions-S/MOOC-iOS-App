using System;
using CourseLib;
using CourseLib.Types;
using Stepik.StepikTypes;

namespace Stepik
{
    public class StepikCourse : Course
    {
        public StepikCourse(double score, double course, string course_title, string course_cover)
             : base(course_title, new StepikRating(score), new Image(course_cover), new CurrentInfo("Stepik", course, APIpath + course,Coursepath+course+"/"))
        {
        }
        private static string APIpath = "https://stepik.org/api/courses/";
        private static string Coursepath = "https://stepik.org/course/";
    }
}
