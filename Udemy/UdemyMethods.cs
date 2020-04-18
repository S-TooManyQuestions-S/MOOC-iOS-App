using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;

namespace Udemy
{
    public static class UdemyMethods
    {
        private static string course_search = "https://www.udemy.com/courses/search/?q=";

        public static IHtmlDocument LoadPage(string path)
        {
            try
            {
                WebClient webClient = new WebClient();
                webClient.Headers.Add("User-Agent: Chrome/51.0.2704.103");
                webClient.Headers.Add("Accept-Language", "ru-ru");
                return new HtmlParser().ParseDocument(webClient.DownloadString(path));
            }
            catch (WebException)
            {
                return null;
            }
        }

















        public static List<UdemyCourse> GetCourses(string keyword)
        {
            List<UdemyCourse> listOfCourses = new List<UdemyCourse>();
            var searchpage = LoadPage(course_search + keyword);

            if (searchpage == null)
                return listOfCourses;
            List<string> hrefs = GetAttributeInfo(searchpage, "a", "udlite-custom-focus-visible course-card--container--3w8Zm course-card--large--1BVxY", 0);
            List<string> course_names = GetValueInfo(searchpage, "div", "udlite-heading-sm udlite-focus-visible-target course-card--course-title--2f7tE");
            List<string> ratings = GetValueInfo(searchpage, "div", "udlite-heading-sm star-rating--rating-number--3lVe8");
            List<string> cover_photos = GetAttributeInfo(searchpage, "img", "course-card--course-image--2sjYP",3);
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            for (int i = 0; i < ratings.Count; i++)
                listOfCourses.Add(new UdemyCourse(course_names[i], double.Parse(ratings[i]), cover_photos[i], "https://www.udemy.com" + hrefs[i]));
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
            return listOfCourses;
        }

        private static List<string> GetAttributeInfo(IHtmlDocument doc, string selector, string className, int attribute)
        {
            List<string> context = new List<string>();
            try
            {
                doc.QuerySelectorAll("div").Where(x => x.ClassName == "course-list--container--3zXPS").FirstOrDefault().QuerySelectorAll(selector).Where(x =>
            x.ClassName != null && x.ClassName == className).ToList().ForEach(x =>
            context.Add(x.Attributes[attribute].Value));
                return context;
            }
            catch (NullReferenceException)
            {
                return context;
            }
        }

        private static List<string> GetValueInfo(IHtmlDocument doc, string selector, string className)
        {
            try
            {
                List<string> data = new List<string>();
                doc.QuerySelectorAll("div").Where(x => x.ClassName == "course-list--container--3zXPS").ToList()[0].QuerySelectorAll(selector).Where(x =>
                x.ClassName != null && x.ClassName == className).ToList().ForEach(x =>
                data.Add(x.TextContent));
                return data;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}
