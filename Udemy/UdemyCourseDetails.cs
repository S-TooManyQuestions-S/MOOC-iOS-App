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
        => new HtmlParser().ParseDocument($"<html> {description} </html>")
            .QuerySelector("html")
            .TextContent
            .Replace("\n", "")
            .Replace("&nbsp", "");
           
        private static string GetItem(Meta data)
       => data?.items?[data.items.Count-1] ?? "No additional knowledge required";
        
    }

    public class Meta
    {
        [JsonProperty("items")]
        public List<string> items { get; set; }
    }
}
