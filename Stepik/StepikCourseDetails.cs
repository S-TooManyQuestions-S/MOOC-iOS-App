using AngleSharp.Html.Parser;
using CourseLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stepik
{
    public class StepikCourseDetails : CourseDetails
    {
        public StepikCourseDetails(string summary, string workload, string target_audience, string course_format, string description)
            : base(summary, workload, target_audience, course_format, DescriptionFix(description))
        { }
        private static string DescriptionFix(string description)
        {
            HtmlParser domparser = new HtmlParser();
            var document = domparser.ParseDocument($"<html> {description} </html>");
            return document.QuerySelector("html").TextContent;
        }
    }
}
