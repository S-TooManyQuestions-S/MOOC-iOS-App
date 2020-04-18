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
        /// Курсы с первой страницы по запросу
        /// </summary>
        /// <param name="is_popular">По популярности?</param>
        /// <param name="keyword">Слово для поиска</param>
        /// <returns>Лист курсов (если курсов не было найдено - пустой список)</returns>
        public static List<StepikCourse> GetCourses(string keyword)
        {
            MainDataController newPageOfData = JsonConvert.DeserializeObject<MainDataController>(GetSource(String.Format(course_search,1, keyword)));
            if (newPageOfData.IsFound)
                if (newPageOfData.metaresults.HasNext)
                {
                    var m = newPageOfData.search_results;
                    m.AddRange(JsonConvert.DeserializeObject<MainDataController>(GetSource(String.Format(course_search, 2, keyword))).search_results);
                    return m;
                }
            else
                { return newPageOfData.search_results; }    
            return null;
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
                return new WebClient().DownloadString(url);
            }
            catch (WebException)
            {
                return null;
            }
        }

        /// <summary>
        /// Метод возвращающий детали о курсе
        /// </summary>
        /// <param name="link">ссылка на страницу для парсинга</param>
        /// <returns>Объект с соответсвующей информацией</returns>
        public static StepikCourseDetails GetDetails(string link)
        {
            try
            {
                return JsonConvert.DeserializeObject<DetailsDataController>(GetSource(link))?.info[0];
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
    }
}
