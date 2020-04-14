using AngleSharp.Html.Dom;
using CourseLib;
using Coursera;
using Stepik;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            var m = CourseraMethods.GetCourses("Python");
            List<CourseDetails> m1 = new List<CourseDetails>();
            int i = 0;
            foreach (var item in m)
            {
                m1.Add(CourseraMethods.GetDetails(item.Info.InformationPath));
                Console.WriteLine(m[i].CourseName);
                Console.WriteLine(m1[i].ShortDescriprion);
                Console.WriteLine(m1[i].LongDescription);
                Console.WriteLine(m1[i].TargetAudience);
                Console.WriteLine(m1[i].WorkLoad);
                Console.WriteLine();
                i++;
            }
                
            
        }
    }
}
