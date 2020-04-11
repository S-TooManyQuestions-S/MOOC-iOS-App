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
        private static string course_search = "https://stepik.org/api/search-results?is_popular=true&is_public=true&page=1&query={1}&type=course";
        /// <summary>
        /// Курсы с первой страницы по запросу
        /// </summary>
        /// <param name="is_popular">По популярности?</param>
        /// <param name="keyword">Слово для поиска</param>
        /// <returns>Лист курсов (если курсов не было найдено - пустой список)</returns>
        public static List<StepikCourse> GetCourses(string keyword)
        {
            MainDataController newPageOfData = JsonConvert.DeserializeObject<MainDataController>(GetSource(String.Format(course_search, keyword)));
            if (newPageOfData.IsFound)
                return newPageOfData.search_results;
            return new List<StepikCourse>();
        }
       
        /// <summary>
        /// Получает страницу по ссылке
        /// </summary>
        /// <param name="url">Ссылка на страницу</param>
        /// <returns>Строка с информацией</returns>
        private static string GetSource(string url)
            => new WebClient().DownloadString(url);
    }
}
