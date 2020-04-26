﻿using AngleSharp.Html.Dom;
using CourseLib;
using Coursera;
using MySQL;
using Stepik;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
            
            var details = MySQL.MySQLMethods.GetFromSQL("https://www.udemy.com/api-2.0/courses/1298780?fields%5Bcourse%5D=description,headline,content_info,requirements_data,_class");
            Console.WriteLine(details.WorkLoad);
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

            
        }
    }
}
