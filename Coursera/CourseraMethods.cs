using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using CourseLib.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Globalization;

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
        public static IHtmlDocument LoadPage(string path)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add("Accept-Language", "ru-ru"); //меняем язык 
                return new HtmlParser().ParseDocument(webClient.DownloadString(path));
            }
            catch (WebException)
            {
                return null;
            }

        }


        /// <summary>
        /// Получить курсы по ключевому слову
        /// </summary>
        /// <param name="keyword">ключевое слово</param>
        /// <returns>список сформированных курсов</returns>
        public static List<CourseraCourse> GetCourses(string keyword)
        {
            List<CourseraCourse> listOfCourses = new List<CourseraCourse>();
            var searchPage = LoadPage(course_search + keyword);

            if (searchPage == null)//если есть ошибка чтения страницы - объект создаваться не будет
                return null;

            List<string> ratings = GetValueInfo(searchPage, "span", "ratings-text");

            if (ratings.Count == 0)
                return listOfCourses;

            List<string> hrefs = GetAttributeInfo(searchPage, "a", "rc-DesktopSearchCard anchor-wrapper", 7);
            List<string> cover_photos = GetAttributeInfo(searchPage, "img", "product-photo", 0);
            List<string> course_names = GetValueInfo(searchPage, "h2", "color-primary-text card-title headline-1-text");
            if (hrefs == null || cover_photos == null || course_names == null)
                return null;

            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            for (int i = 0; i < ratings.Count; i++)
                listOfCourses.Add(new CourseraCourse(course_names[i], double.Parse(ratings[i]), cover_photos[i], "https://www.coursera.org" + hrefs[i]));
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");

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
                doc.QuerySelectorAll(selector).Where(x =>
            x.ClassName != null && x.ClassName == className).ToList().ForEach(x =>
            context.Add(x.Attributes[attribute].Value));
                return context;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }
        /// <summary>
        /// Получить информацию из значения узла (с классом)
        /// </summary>
        /// <param name="doc">документ</param>
        /// <param name="selector">тег для выбора</param>
        /// <param name="className">конкретизация тега именем класса</param>
        /// <returns>Список информации</returns>
        public static List<string> GetValueInfo(IHtmlDocument doc, string selector, string className)
        {
            try
            {
                List<string> data = new List<string>();
                doc.QuerySelectorAll(selector).Where(x =>
                x.ClassName != null && x.ClassName == className).ToList().ForEach(x =>
                data.Add(x.TextContent));
                return data;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetValueInfo(IHtmlDocument doc, string selector)
        => doc.QuerySelector(selector).TextContent;



        public static CourseraCourseDetails GetDetails(string url)
        {
            IHtmlDocument doc = CourseraMethods.LoadPage("https://www.coursera.org/learn/data-visualization-tableau");
            return new CourseraCourseDetails(GetSummary(doc), GetWorkLoad(doc), GetCourseRecommendations(doc), GetCourseFormat(doc), GetDescription(doc)); 
        }

        /// <summary>
        /// Получить рекомендации по конкретному курсу 
        /// Рекомендации к уровню знаний перед выполнением
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static string GetCourseRecommendations(IHtmlDocument doc)
        {
            try
            {
                var m = doc.QuerySelectorAll("div").Where(x => x.ClassName == "_y1d9czk m-b-2 p-t-1s").ToList();
                return (m[m.Count - 3].QuerySelectorAll("p").FirstOrDefault().TextContent);
            }
            catch (NullReferenceException)
            {
                return "Дополнительных знаний не требуется";
            }
            catch (Exception)
            {
                return "Дополнительных знаний не требуется";
            }
        }

        /// <summary>
        /// Получить краткое описание курса, если есть
        /// </summary>
        /// <param name="doc">страница</param>
        /// <returns>Строка краткой информации о курсе</returns>
        public static string GetSummary(IHtmlDocument doc)
        {
            try
            {
                return doc.QuerySelectorAll("p").Where(x => x.ClassName == "max-text-width m-b-0").FirstOrDefault().TextContent;
            }
            catch (NullReferenceException)
            {
                return "None";
            }
            catch (Exception)
            {
                return "None";
            }
        }
        /// <summary>
        /// Получение описания курса
        /// </summary>
        /// <param name="doc">Страница курса</param>
        /// <returns>Описание (полное)</returns>
        public static string GetDescription(IHtmlDocument doc)
        {
            try
            {
                return doc.QuerySelectorAll("div").Where(x => x.ClassName == "description").FirstOrDefault().TextContent;
            }
            catch (NullReferenceException)
            {
                return "None";
            }
            catch (Exception)
            {
                return "None";
            }
        }
        /// <summary>
        /// Получение затрат по времени (рассчет приблизеительный)
        /// </summary>
        /// <param name="doc">страница курса</param>
        /// <returns>Расчет по временных затрат</returns>
        public static string GetWorkLoad(IHtmlDocument doc)
        {
            try
            {
                var m = doc.QuerySelectorAll("div").Where(x => x.ClassName == "ProductGlance").FirstOrDefault().QuerySelectorAll("div").Where(x => x.ClassName == "_y1d9czk m-b-2 p-t-1s").ToList();
                var list =  m[m.Count - 2].QuerySelectorAll("span").ToList();
                string workload = "";
                foreach(var item in list)
                    workload += item.TextContent+"\n";
                return workload;
            }
            catch(NullReferenceException)
            {
                return "None";
            }
            catch(Exception)
            {
                return "None";
            }
        }
        /// <summary>
        /// Получение формата курса
        /// </summary>
        /// <param name="doc">Страница курса</param>
        /// <returns>Формат</returns>

        public static string GetCourseFormat(IHtmlDocument doc)
        {
            try
            {
                var m = doc.QuerySelectorAll("div").Where(x => x.ClassName == "ProductGlance").FirstOrDefault().QuerySelectorAll("div").Where(x => x.ClassName == "_y1d9czk m-b-2 p-t-1s").ToList();
                var key = m[m.Count - 5].QuerySelector("h4").TextContent;
                return key;
            }
            catch (NullReferenceException)
            {
                return "Онлайн курс";
            }
            catch (Exception)
            {
                return "Онлайн курс";
            }
        }
    }
}
