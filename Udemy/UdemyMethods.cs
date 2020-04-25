using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using Udemy.APIser;

namespace Udemy
{
    public static class UdemyMethods
    {
        private static string course_search = "https://www.udemy.com/api-2.0/courses/?search=";
        private static string course_details = "https://www.udemy.com/api-2.0/courses/{0}?fields%5Bcourse%5D=description,headline,content_info,requirements_data,_class";

        /// <summary>
        /// ??Получение информации страницы по ссылке
        /// </summary>
        /// <param name="path">Ссылка на json файл</param>
        /// <returns>Информация</returns>
        private static string LoadPage(string path)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Accept: application/json, text/plain, */*");
                webClient.Headers.Add("Authorization: Basic TVRFV2VOcjFPMVFsTGVTbGl3MDI0d3NsZFh3clNKYmt4V09pMjdVNTpBY1RYeE1QQUdORlBBd1hVMnA4YUprVTlQbDZiTEYwOFVIelRHRHhFdkFmRlNrRVliYmpteWE0OFN3Yjd6WnZVM3dKS0l5aTBVOVZMbjRzS3pTR0VZNWMweTk5UE5jN1NXd2R1eTdwbjZZOWdSNUxvejQxYVBBT0ZYNGNjUDg5ZA==");
                webClient.Headers.Add("Content-Type: application/json;charset=utf-8");
                return webClient.DownloadString(path);
            }
            catch (WebException e)
            {
                Console.WriteLine("При получении информации страницы произошла ошибка [Udemy] [LoadPage]\n"+ e.Message);
                return null;
            }
        }
        /// <summary>
        /// 0 Получение курсов Udemy по ключевому слову 
        /// </summary>
        /// <param name="keyword">Ключевому слову</param>
        /// <returns>Список полученных курсов</returns>
        public static List<UdemyCourse> GetCourses(string keyword)
        {
            List<UdemyCourse> list = new List<UdemyCourse>();
            try
            {
                return JsonConvert.DeserializeObject<MainDataController>(LoadPage(course_search + keyword)).results;
            }
            catch(Exception e)
            {
                Console.WriteLine("При JSON десериализации произошла ошибка [Udemy] [GetCourses]\n" + e.Message);
                return list;
            }
        }
        /// <summary>
        /// Получение конкретной информации о курсе
        /// </summary>
        /// <param name="id">Айди курса на сайте Udemy</param>
        /// <returns>Информация о Udemy курсе</returns>
        /// https://www.udemy.com/api-2.0/courses/{id}?fields%5Bcourse%5D=description,headline,content_info,requirements_data,_class
        public static UdemyCourseDetails GetDetails(string link)
        {
            string page = LoadPage(link);
            try
            {
                return JsonConvert.DeserializeObject<UdemyCourseDetails>(page);
            }
           catch(Exception e)
            {
                Console.WriteLine("При JSON десериализации произошла ошибка [Udemy] [GetCourses]\n"+e.Message);
                return null;
            }

        }

    }
}
