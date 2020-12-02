using CourseLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

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
                //+ 3 секунды к ожиданию
                // MainDataController newPageOfData = JsonConvert.DeserializeObject<MainDataController>(GetSource(String.Format(course_search, 1, keyword)));

                JObject jObject = JObject.Parse(GetSource(String.Format(course_search, 1, keyword)));
                List<double> courses = new List<double>();//айди курса
                List<string> titles = new List<string>();//наименование курса
                List<string> covers = new List<string>();//обложка курса

                for (int i = 0; i <=9; i++)
                {
                    string course = jObject.SelectToken($"search-results[{i}].course")?.ToString();
                    if (string.IsNullOrEmpty(course))
                        break;
                    else
                        courses.Add(double.Parse(course));

                    titles.Add(jObject.SelectToken($"search-results[{i}].course_title").ToString());
                    covers.Add(jObject.SelectToken($"search-results[{i}].course_cover").ToString());
                }

                if (courses.Count == 0)//если список пуст - дальше идти смысла нет
                    return list;

                //ссылка для получения id для запроса для рейтинга
                string requestPath = "https://stepik.org/api/courses?";
                //формирование ссылки
                courses.ForEach(x =>
                {
                    requestPath += $"ids[]={x}&";
                });
                //получение рейтингов по списку
                List<double> ratings = GetStepikRating(requestPath);
                //формирование объектов типа StepikCourse
                for (int i = 0; i < courses.Count; i++)
                    list.Add(new StepikCourse(courses[i], titles[i], covers[i], ratings[i]));
                return list;
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
                JObject jObject = JObject.Parse(GetSource(link));
                var summary =jObject.SelectToken("courses[0].summary")?.ToString() ?? "";
                var workload = jObject.SelectToken("courses[0].workload")?.ToString() ?? "";
                var target_audience = jObject.SelectToken("courses[0].target_audience")?.ToString() ?? "";
                var course_format = jObject.SelectToken("courses[0].course_format")?.ToString() ?? "";
                var description = jObject.SelectToken("courses[0].description")?.ToString() ?? "";
                return new StepikCourseDetails(summary, workload, target_audience, course_format, description); 
            }
            catch (Exception e)
            {
                Console.WriteLine("При десериализации произошла ошибка![Stepik] [GetDetails]\n" + e.Message);
                return null;
            }
        }

        /// <summary>
        /// Получение листа рейтингов курсов Stepik
        /// </summary>
        /// <param name="link">Ссылка на сами review</param>
        /// <returns>Лист курсов друг заь другом</returns>
        public static List<double> GetStepikRating(string link)
        {
            try
            {
                JObject jObject = JObject.Parse(GetSource(link));

                string path = "https://stepik.org/api/course-review-summaries?";
                for (int i = 0; i <=9; i++)
                {
                    //достаем id для получения рейтинга
                    string review = jObject.SelectToken($"courses[{i}].review_summary")?.ToString();
                    //если больше получить не можем - прекращаем цикл
                    if (string.IsNullOrEmpty(review))
                        break;
                    //генерируем саму ссылку
                    path += $"ids[]={review}&";
                }
                //делаем запрос по ссылке
                jObject = JObject.Parse(GetSource(path));

                List<double> ratings = new List<double>();
                for (int i = 0; i <= 9; i++)
                {//достаем значение самого наконец-то рейтинга
                    string review = jObject.SelectToken($"course-review-summaries[{i}].average")?.ToString();
                    if (string.IsNullOrEmpty(review))
                        break;
                    ratings.Add(double.Parse(review));//добавляем его в наш лист
                }
                return ratings;
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка при при получении рейтинга JSON [Stepik-Rating] [GetCourses]\n" + e.Message);
                return null;
            }
           
        }
    }
}
