using CourseLib;
using Newtonsoft.Json;
using Stepik.APIser;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Stepik
{
    public static class StepikMethods
    {
        private static string course_search = "https://stepik.org/api/search-results?is_popular=true&is_public=true&page={0}&query={1}&type=course";

        /// <summary>
        /// 0 Курсы с первой страницы по запросу
        /// </summary>
        /// <param name="is_popular">По популярности?</param>
        /// <param name="keyword">Слово для поиска</param>
        /// <returns>Лист курсов (если курсов не было найдено - пустой список)</returns>
        public static List<StepikCourse> GetCourses(string keyword)
        {
            List<StepikCourse> list = new List<StepikCourse>();
            try
            {
                MainDataController newPageOfData = JsonConvert.DeserializeObject<MainDataController>(GetSource(String.Format(course_search, 1, keyword)));
                if (newPageOfData.IsFound)
                    return newPageOfData.search_results;
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при десериализации JSON [Stepik] [GetCourses]\n" +e.Message);
            }
            return list;
        }

        /// <summary>
        /// Получает страницу по ссылке
        /// </summary>
        /// <param name="url">Ссылка на страницу</param>
        /// <returns>Строка с информацией</returns>
        private static string GetSource(string url)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Accept-Language", "en-us");
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
}
