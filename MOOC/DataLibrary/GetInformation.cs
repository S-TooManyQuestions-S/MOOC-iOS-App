using System;
using System.Collections.Generic;
using System.Net;
using MOOC.JSONOptions;
using Newtonsoft.Json;

namespace MOOC.DataLibrary
{
    public class GetInformation
    {
        //для запроса курсов
        static string getCourses = @"http://194.87.236.137/getCourses?keyword=";
        //для запроса деталей по курсу
        static string getDetails = @"http://194.87.236.137/getDetails?link=";

        //получении курсов по ключевому слову
        public static List<Course> GetCourses(string keyword)
         => JsonConvert.DeserializeObject<List<Course>>(GetSource(getCourses + keyword)) ?? null;

        //Получение деталей по курсу (по ссылке)
        public static CourseDetails GetDetails(string link)
         => JsonConvert.DeserializeObject<CourseDetails>(GetSource(getDetails + link)) ?? null;
            
       
        /// <summary>
        /// Получение информации со страницы как строки
        /// </summary>
        /// <param name="url">Ссылка на страницу</param>
        /// <returns>Строка информации</returns>
        private static string GetSource(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                return new WebClient().DownloadString(url);
            }
            catch (WebException e)
            {
                Console.WriteLine("При получении информации страницы произошла ошибка!" + e.Message);
                return "";
            }
        }
        static GetInformation()
        => important = JsonMethods.JsonReader();

        
        //попудярное
        public static List<Course> popular { get; set; }

        //получаем список избранного из нашего файла
        public static List<Course> important { get; set; }

        //результаты по запросу c#
        public static List<Course> sharp { get; set; }
        //результаты по запросу python
        public static List<Course> python { get; set; }
        //результаты по запросу Data Science
        public static List<Course> data { get; set; }
        //результаты по запросу Xamarin
        public static List<Course> xamarin { get; set; }
        //результаты русской подборки
         public static List<Course> russian { get; set; }
    }
}
