using AngleSharp.Html.Parser;
using CourseLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stepik
{
    [Serializable]
    public class StepikCourseDetails : CourseDetails
    {
        public StepikCourseDetails(string summary, string workload, string target_audience, string course_format, string description)
            : base(summary, DescriptionFix(description), string.IsNullOrEmpty(target_audience) ? "Не требуется дополнительных знаний!" : target_audience,
                  string.IsNullOrEmpty(course_format) ? "Онлайн-курc" : course_format,
                  string.IsNullOrEmpty(workload) ? "Свободный график" : workload)
        { }
        
        private static string DescriptionFix(string description)
        {
            HtmlParser domparser = new HtmlParser();
            var document = domparser.ParseDocument($"<html> {description} </html>");
             return document.QuerySelector("html").TextContent.Replace("\n","");
        }
    }
}
