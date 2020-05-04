using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Stepik.APIser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;

namespace Stepik
{
    public static class StepikMethods
    {
        private static string course_search = @"https://stepik.org/api/search-results?is_popular=true&is_public=true&page={0}&query={1}&type=course";
        
        public static List<StepikCourse> GetCourses(string keyword)
        {
            string path = "https://stepik.org/api/courses?";
            List<StepikCourse> list = new List<StepikCourse>();
            try
            {
                var results = JsonConvert.DeserializeObject<MainDataController>(GetSource(string.Format(course_search,1,keyword)))?.search_results ?? new List<StepikCourse>();
                results.ForEach(x =>
                {
                    path += $"ids[]={x.id}&";
                });

                var array = JsonConvert.DeserializeObject<bypath>(GetSource(path)).co;


            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при десериализации JSON [Stepik] [GetCourses]\n" + e.Message);
            }
            return list;
        }

        /// <summary>
        /// Получает страницу по ссылке 
        /// </summary>
        /// <param name="url">Ссылка на страницу</param>
        /// <returns>Строка с информацией</returns>
        internal static string GetSource(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Accept-Language", "ru-RU");
                return new WebClient().DownloadString(url);
            }
            catch (WebException e)
            {
                Console.WriteLine("При получении информации страницы произошла ошибка![Stepik] [GetSource]\n" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// ?? Метод возвращающий детали о курсе
        /// </summary>
        /// <param name="link">ссылка на страницу для парсинга</param>
        /// <returns>Объект с соответсвующей информацией</returns>
        public static StepikCourseDetails GetDetails(string link)
        {
            try
            {
                return JsonConvert.DeserializeObject<DetailsDataController>(GetSource(link))?.info[0];
            }
            catch (Exception e)
            {
                Console.WriteLine("При десериализации произошла ошибка![Stepik] [GetDetails]\n" + e.Message);
                return null;
            }
        }


    }
    public class SummaryId
    {
        [JsonProperty("review_summary")]
        public string id { get; set; }
    }

    public class bypath
    {
        [JsonProperty("courses")]
        public List<SummaryId> co { get; set; }
    }
}
