using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using CourseLib;
using Coursera;
using MySQL;
using Stepik;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Udemy;

namespace Testing
{
    class Program
    {
        static void Main(string[] args)
        {
            /*List<Course> list = new List<Course>();
            list.AddRange(  StepikMethods.GetCourses("Data Science"));
            foreach(var item in list)
            {
                var details = StepikMethods.GetDetails(item.Info.InformationPath);
                MySQL.MySQLMethods.InsertInSQL(details,item.Info.);
            }*/

            /* var details = MySQL.MySQLMethods.GetFromSQL("https://www.udemy.com/api-2.0/courses/1298780?fields%5Bcourse%5D=description,headline,content_info,requirements_data,_class");
             Console.WriteLine(details.WorkLoad);*/
            //list.AddRange(UdemyMethods.GetCourses("python"));
            //list.AddRange(StepikMethods.GetCourses("Python"));
            /*foreach (var item in list)
            {
                Console.WriteLine(item.CourseName);
                Console.WriteLine(item.CourseRating.MyRating);
                Console.WriteLine(item.Info.InformationPath);
                Console.WriteLine(item.CourseImages.CoverImage);
                Console.WriteLine(item.CourseImages.LogoLink);
                var details = UdemyMethods.GetDetails(string.Format("https://www.udemy.com/api-2.0/courses/{0}?fields%5Bcourse%5D=description,headline,content_info,requirements_data,_class",item.Info.CompanyId));
                Console.WriteLine(details.Format);
                Console.WriteLine(details.LongDescription);
                Console.WriteLine(details.ShortDescriprion);
                Console.WriteLine(details.TargetAudience);
                Console.WriteLine(details.WorkLoad);
                Console.WriteLine("--------------");
                Console.WriteLine();
                *//* if (item.Info.InformationPath.Contains("coursera"))
                 {
                     var details = CourseraMethods.GetDetails(item.Info.InformationPath);
                     Console.WriteLine($"{details.Format}" +
                         $"{details.LongDescription}" +
                         $"{details.ShortDescriprion}" +
                         $"{details.WorkLoad}" +
                         $"{details.TargetAudience}");
                     Console.WriteLine("--------------------------------------------------");
                 }*/

            /*
            
            Console.WriteLine(StepikCourse.GetRating(list[0].Info.CoursePath).Contains("course-promo-summary__average"));
          Console.WriteLine((new HtmlParser().ParseDocument(StepikCourse.GetRating(list[0].Info.CoursePath)).QuerySelector("span[class='course-promo-summary__average']").TextContent));
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.WriteLine(Udemy.UdemyMethods.LoadPage(@"https://www.udemy.com/api-2.0/courses/258316?fields%5Bcourse%5D=description,headline,content_info,requirements_data,_class ")); */
            
            /*    var list = UdemyMethods.GetCourses("Python" 
                  );
                
              foreach (var item in list)
              {

                  Console.WriteLine(item.CourseRating.MyRating);
                  Console.WriteLine(item.CourseName);
                  Console.WriteLine(item.CourseImages.CoverImage);
                  var m = CourseraMethods.GetDetails(item.Info.APIpath);
                  Console.WriteLine(m.Format);
                  Console.WriteLine(m.LongDescription);
                  Console.WriteLine(m.ShortDescriprion);
                  Console.WriteLine(m.TargetAudience);
                  Console.WriteLine(m.WorkLoad);
                  Console.WriteLine("------------------------------------------");

              }*/
            /*var stopwatch = new Stopwatch();
            stopwatch.Start();
            List<Course> AllCourses = new List<Course>();
            string keyword = "Python";
            var tasks = new List<Task>();
            tasks.Add(Task.Run(() => AllCourses.AddRange(StepikMethods.GetCourses(keyword))));
            tasks.Add(Task.Run(() => AllCourses.AddRange(CourseraMethods.GetCourses(keyword))));
            tasks.Add(Task.Run(() => AllCourses.AddRange(UdemyMethods.GetCourses(keyword))));
            Task t = Task.WhenAll(tasks);

            t.Wait();
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds);
            Console.WriteLine(AllCourses[0].CourseImages.CoverImage);*/

        }
    }
}
