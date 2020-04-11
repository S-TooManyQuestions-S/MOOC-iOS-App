using System;
using CourseLib;
using CourseLib.Types;
using Stepik.StepikTypes;

namespace Stepik
{
    public class StepikCourse : Course
    {
       public StepikCourse(double score, double course, string course_title, string course_cover)
            :base(course_title, new StepikRating(score), new Image(course_cover,logoPath), new CurrentInfo(Company,course,detailsPath+score))
        { }
        private static string logoPath = "https://sun9-8.userapi.com/c834202/v834202874/c592a/vYGGjxSfXvI.jpg";
        private static string Company = "Stepik";
        private static string detailsPath = "https://stepik.org/api/courses/";
    }
}
