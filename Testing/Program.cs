using CourseLib;
using Coursera;
using Stepik;
using System;
using System.Collections.Generic;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            List<CourseraCourse> m = CourseraMethods.GetCourses("Python");
            foreach (var item in m)
            {
                Console.WriteLine(item.CourseName);
                Console.WriteLine(item.CourseRating.MyRating);
                Console.WriteLine(item.CourseImages.CoverImage);
                Console.WriteLine(item.Info.InformationPath);
                Console.WriteLine();
            }

        }
    }
}
