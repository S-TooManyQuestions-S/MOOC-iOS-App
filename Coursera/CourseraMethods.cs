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
            => new HtmlParser().ParseDocument(new WebClient().DownloadString(path));


       public static List<CourseraCourse> GetCourses(string keyword)
        {
            List<CourseraCourse> listOfCourses = new List<CourseraCourse>();
            var searchPage = LoadPage(course_search + keyword);
            List<string> hrefs = GetAttributeInfo(searchPage, "a", "rc-DesktopSearchCard anchor-wrapper", 7);
            List<string> cover_photos = GetAttributeInfo(searchPage, "img", "product-photo", 0);
            List<string> course_names = GetValueInfo(searchPage, "h2", "color-primary-text card-title headline-1-text");
            List<string> ratings = GetValueInfo(searchPage, "span", "ratings-text");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            for (int i = 0; i < hrefs.Count; i++)
                listOfCourses.Add(new CourseraCourse(course_names[i], double.Parse(ratings[i]), cover_photos[i], hrefs[i]));
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            return listOfCourses;
        }
        private static List<string> GetAttributeInfo(IHtmlDocument doc, string selector, string className, int attribute)
        {
            List<string> context = new List<string>();
            doc.QuerySelectorAll(selector).Where(x =>
            x.ClassName != null && x.ClassName == className).ToList().ForEach(x =>
            context.Add(x.Attributes[attribute].Value));
            return context;
        }

        private static List<string> GetValueInfo(IHtmlDocument doc, string selector, string className)
        {
            List<string> data = new List<string>();
            doc.QuerySelectorAll(selector).Where(x =>
            x.ClassName != null && x.ClassName == className).ToList().ForEach(x =>
            data.Add(x.TextContent));
            return data;
        }
    }
}
 