using AngleSharp.Html.Parser;
using CourseLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Udemy
{
    public class UdemyCourseDetails : CourseDetails
    {
        public UdemyCourseDetails(string _class, string headline, string content_info, string description, Meta requirements_data)
             : base(headline, DescriptionFix(description),requirements_data.items[0], _class, content_info)
        {}
       
        private static string DescriptionFix(string description)
        {
            HtmlParser domparser = new HtmlParser();
            var document = domparser.ParseDocument($"<html> {description} </html>");
            return document.QuerySelector("html").TextContent.Replace("\n", "");
        }
    }

    public class Meta
    {
        [JsonProperty("items")]
        public List<string> items { get; set; }
    }
}
