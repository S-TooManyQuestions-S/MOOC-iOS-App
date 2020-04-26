using CourseLib;
using Coursera;
using MySQL;
using Stepik;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Udemy;

namespace MOOC_Server.MySettings
{
    /// <summary>
    /// Класс реализующий (собирающий все методы по сбору данных)
    /// </summary>
    public class ServerRepository : IServerRepository
    {
        public List<Course> GetByKeyWord(string keyword)
        {
            try
            {
                List<Course> AllCourses = new List<Course>();
                AllCourses.AddRange(StepikMethods.GetCourses(keyword));
                AllCourses.AddRange(CourseraMethods.GetCourses(keyword));
                AllCourses.AddRange(UdemyMethods.GetCourses(keyword));
                Console.WriteLine("111");
                return AllCourses;
            }
            catch(NullReferenceException e)
            {
                return null;
            }
            
        }
        public CourseDetails GetDetailsByCourse(string link)
        {
            try
            {
                if (MySQLMethods.GetFromSQL(link) == null)
                {
                    if (link.Contains("stepik"))
                    {
                        CourseDetails details = StepikMethods.GetDetails(link);
                        MySQLMethods.InsertInSQL(details, link);
                        return details;
                    }
                    else if (link.Contains("coursera"))
                    {
                        CourseDetails details = CourseraMethods.GetDetails(link);
                        MySQLMethods.InsertInSQL(details, link);
                        return details;
                    }
                    else if(link.Contains("udemy")) //обработка через id
                    {
                        CourseDetails details = UdemyMethods.GetDetails(link);
                        MySQLMethods.InsertInSQL(details, link);
                        return details;
                    }
                }
                return MySQLMethods.GetFromSQL(link);
            }
            catch(NullReferenceException e)
            {
                Console.WriteLine(e.Message);
                
            }
            catch(Exception e)
            {
                Console.WriteLine("Непредвиденная ошибка: \n" +e.Message);
            }
            return null;
        }
    }
}
