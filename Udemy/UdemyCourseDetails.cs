using AngleSharp.Html.Parser;
using CourseLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Udemy
{
    [Serializable]
    public class UdemyCourseDetails : CourseDetails
    {
        public UdemyCourseDetails(string headline, string content_info, string description, Meta requirements_data)
             : base(headline, DescriptionFix(description),GetItem(requirements_data), "Онлайн-курс (Online-Course)", content_info)
        {}
       
        private static string DescriptionFix(string description)
        {
            HtmlParser domparser = new HtmlParser();
            var document = domparser.ParseDocument($"<html> {description} </html>");
            return document.QuerySelector("html").TextContent.Replace("\n", "");
        }
        private static string GetItem(Meta data)
        {
            if (data == null || data.items == null)
                return "No additional knowledge required";
            return data.items[data.items.Count-1];
        }
    }

    public class Meta
    {
        [JsonProperty("items")]
        public List<string> items { get; set; }
    }
}
