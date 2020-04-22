using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Globalization;
using CourseLib;

namespace Coursera
{
    public static class CourseraMethods
    {
        public static string course_search = "https://www.coursera.org/search?query=";

        /// <summary>
        /// Получаем Html документ-страницу
        /// </summary>
        /// <param name="path">Ссылка на страницу</param>
        /// <returns>Html-документ</returns>
        private static IHtmlDocument LoadPage(string path)
        {
            try
            {
                string info;
                using (WebClient webClient = new WebClient())
                {
                    webClient.Headers.Add("Accept-Language", "en-us");
                    info = webClient.DownloadString(path);
                }
                return new HtmlParser().ParseDocument(info);
            }
            catch (WebException e)
            {
                Console.WriteLine("Ошибка при загрузке страницы для парсинга![Coursera]" +
                    "Information: Coursera \n" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Непредвиденная ошибка при загрузке страницы для парсинга![Coursera]" +
                    "Information: Coursera \n" + e.Message);
            }
            return null;
        }

        /// <summary>
        /// 0 Получить курсы по ключевому слову
        /// </summary>
        /// <param name="keyword">ключевое слово</param>
        /// <returns>список сформированных курсов</returns>
        public static List<CourseraCourse> GetCourses(string keyword)
        {
            List<CourseraCourse> listOfCourses = new List<CourseraCourse>();
            var searchPage = LoadPage(course_search + keyword);
            if (searchPage != null)
            {
                List<string> ratings = GetValueInfo(searchPage, "span", "ratings-text");
                if (ratings.Count == 0)
                    return listOfCourses;//курсы не были найдены

                //ссылки на курсы
                List<string> hrefs = GetAttributeInfo(searchPage, "a", "rc-DesktopSearchCard anchor-wrapper", 7);
                //ссылки на обложки курсов
                List<string> cover_photos = GetAttributeInfo(searchPage, "img", "product-photo", 0);
                //названия курсов
                List<string> course_names = GetValueInfo(searchPage, "h2", "color-primary-text card-title headline-1-text");

                if (hrefs.Count == 0 || cover_photos.Count == 0 || course_names.Count == 0)
                    return listOfCourses; //недостаточно информации для парсинга 

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                for (int i = 0; i < ratings.Count; i++)
                    listOfCourses.Add(new CourseraCourse(course_names[i], double.Parse(ratings[i]), cover_photos[i], "https://www.coursera.org" + hrefs[i]));
                Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            }
            return listOfCourses;
        }
        /// <summary>
        /// Получить информацию из аттрибутов
        /// </summary>
        /// <param name="doc">документ</param>
        /// <param name="selector">тег для выбора</param>
        /// <param name="className">класс для конкретизации тега</param>
        /// <param name="attribute">номер аттрибута</param>
        /// <returns>Список информации</returns>
        private static List<string> GetAttributeInfo(IHtmlDocument doc, string selector, string className, int attribute)
        {
            List<string> context = new List<string>();
            try
            {
                doc.QuerySelectorAll(selector).Where(x => x.ClassName == className).ToList().ForEach(x =>
            context.Add(x.Attributes[attribute].Value));
                return context;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine($"Ошибка при парсинге значения [Coursera] [атрибут]\n<тег>: {selector}\n<название класса>: {className}\nномер атрибута: {attribute}\n" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Непредвиденная ошибка при парсинге значения [Coursera] [атрибут]\n<тег>: {selector}\n<название класса>: {className}\nномер атрибута: {attribute}\n" + e.Message);
            }
            return context;
        }
        /// <summary>
        /// Получить информацию из значения узла (с классом)
        /// </summary>
        /// <param name="doc">документ</param>
        /// <param name="selector">тег для выбора</param>
        /// <param name="className">конкретизация тега именем класса</param>
        /// <returns>Список информации</returns>
        private static List<string> GetValueInfo(IHtmlDocument doc, string selector, string className)
        {
            List<string> data = new List<string>();
            try
            {
                doc.QuerySelectorAll(selector).Where(x =>
                x.ClassName != null && x.ClassName == className).ToList().ForEach(x =>
                data.Add(x.TextContent));
                return data;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine($"Ошибка при парсинге значения [Coursera] [по значению]\n<тег>: {selector}\n<название класса>: {className}\n" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Непредвиденная ошибка при парсинге значения [Coursera] [по значению]\n<тег>: {selector}\n<название класса>: {className}\n" + e.Message);
            }
            return data;
        }
          
        /// <summary>
        /// ??Получение информации о конкретном курсе
        /// </summary>
        /// <param name="url">ссылка на курс</param>
        /// <returns>Информация о курсе</returns>
        public static CourseraCourseDetails GetDetails(string url)
        {
            IHtmlDocument doc = LoadPage(url);
            if(doc != null)
                return new CourseraCourseDetails(GetSummary(doc), GetWorkLoad(doc), GetCourseRecommendations(doc), GetCourseFormat(doc), GetDescription(doc));
            return null;
        }

        /// <summary>
        /// ""Получить рекомендации по конкретному курсу 
        /// Рекомендации к уровню знаний перед выполнением
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private static string GetCourseRecommendations(IHtmlDocument doc)
        {
            try
            {
                var m = doc.QuerySelectorAll("div").Where(x => x.ClassName == "_y1d9czk m-b-2 p-t-1s").ToList();
                return (m[m.Count - 3].QuerySelectorAll("p").FirstOrDefault().TextContent);
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Произошла ошибка при парсинге значения [Coursera] [значение не найдено]\n<название класса>: _y1d9czk m-b-2 p-t-1s\n"+e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Произошла непредвиденная ошибка при парсинге значения [Coursera]\n < название класса >: _y1d9czk m-b-2 p-t-1s\n"+e.Message);
            }
            return "";
        }

        /// <summary>
        /// ""Получить краткое описание курса, если есть
        /// </summary>
        /// <param name="doc">страница</param>
        /// <returns>Строка краткой информации о курсе</returns>
        private static string GetSummary(IHtmlDocument doc)
        {
            try
            {
                return doc.QuerySelectorAll("p").Where(x => x.ClassName == "max-text-width m-b-0").FirstOrDefault().TextContent;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Произошла ошибка при парсинге значения [Coursera] [значение не найдено]\n<название класса>: max-text-width m-b-0\n" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла непредвиденная ошибка при парсинге значения [Coursera]\n < название класса >: max-text-width m-b-0\n" + e.Message);
            }
            return "";
        }
        /// <summary>
        /// ""Получение описания курса
        /// </summary>
        /// <param name="doc">Страница курса</param>
        /// <returns>Описание (полное)</returns>
        private static string GetDescription(IHtmlDocument doc)
        {
            try
            {
                return doc.QuerySelectorAll("div").Where(x => x.ClassName == "description").FirstOrDefault().TextContent;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Произошла ошибка при парсинге значения [Coursera][значение не найдено]\n < название класса >: description\n" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла непредвиденная ошибка при парсинге значения [Coursera]\n < название класса >: description\n" + e.Message);
            }

            try
            {
                return doc.QuerySelectorAll("div").Where(x => x.ClassName == "AboutCourse").FirstOrDefault().QuerySelectorAll("div").Where(
                    x => x.ClassName == "content-inner").FirstOrDefault().TextContent;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Произошла ошибка при парсинге значения [Coursera][значение не найдено]\n < название класса >: AboutCourse\n" + e.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Произошла непредвиденная ошибка при парсинге значения [Coursera]\n < название класса >: AboutCourse\n" + e.Message);
            }

            return "";
        }

        /// <summary>
        /// ""Получение затрат по времени (рассчет приблизеительный)
        /// </summary>
        /// <param name="doc">страница курса</param>
        /// <returns>Расчет по временных затрат</returns>
        private static string GetWorkLoad(IHtmlDocument doc)
        {
            try
            {
                var m = doc.QuerySelectorAll("div").Where(x => x.ClassName == "ProductGlance").FirstOrDefault().QuerySelectorAll("div").Where(x => x.ClassName == "_y1d9czk m-b-2 p-t-1s").ToList();
                var list = m[m.Count - 2].QuerySelectorAll("span").ToList();
                return list[0].TextContent;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Произошла ошибка при парсинге значения [Coursera][значение не найдено] [span]\n < название класса >: _y1d9czk m-b-2 p-t-1s\n" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла непредвиденная ошибка при парсинге значения [Coursera] [span]\n < название класса >: _y1d9czk m-b-2 p-t-1s\n" + e.Message);
            }
            return "";
        }
        /// <summary>
        /// ""Получение формата курса
        /// </summary>
        /// <param name="doc">Страница курса</param>
        /// <returns>Формат</returns>
        private static string GetCourseFormat(IHtmlDocument doc)
        {
            try
            {
                var m = doc.QuerySelectorAll("div").Where(x => x.ClassName == "ProductGlance").FirstOrDefault().QuerySelectorAll("div").Where(x => x.ClassName == "_y1d9czk m-b-2 p-t-1s").ToList();
                var key = m[m.Count - 5].QuerySelector("h4").TextContent;
                return key;
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine("Произошла ошибка при парсинге значения [Coursera][значение не найдено] [h4]\n < название класса >: _y1d9czk m-b-2 p-t-1s\n" + e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Произошла непредвиденная ошибка при парсинге значения [Coursera] [h4]\n < название класса >: _y1d9czk m-b-2 p-t-1s\n" + e.Message);
            }
            return "";
        }
    }
}
