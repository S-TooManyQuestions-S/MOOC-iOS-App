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
             : base(headline, DescriptionFix(description),GetItem(requirements_data), "Online-Course",GetCorrectWorkLoad(content_info))
        {}
       
        /// <summary>
        /// При парсинге Udemy необходимо избавиться от html тегов, тут нам и помогает данный метод
        /// </summary>
        /// <param name="description">Описание (не форматированное)</param>
        /// <returns>Отформатированное описание (без лишних знаков и тегов)</returns>
        private static string DescriptionFix(string description)
        => new HtmlParser().ParseDocument($"<html> {description} </html>")
            .QuerySelector("html")
            .TextContent
            .Replace("\n", "")
            .Replace("&nbsp", "");


        /// <summary>
        /// Вычленение рекомендаций из сложного АПИ Udemy (массива значений)
        /// </summary>
        /// <param name="data">Json представление</param>
        /// <returns>Рекоммендация (требование)</returns>
        private static string GetItem(Meta data)
        {
            var m = data?.items ?? new List<string>();
            string requirements = String.Empty;

            foreach (var item in m)
                requirements += "->"+item +"\n";

            if (string.IsNullOrEmpty(requirements))
                return "No additional knowledge required!";
            else
                return requirements;
        }
        /// <summary>
        /// Извлекаем ненужное "total" из нашего значения
        /// </summary>
        /// <param name="data">Входная необработанная строка </param>
        /// <returns>Строка без total</returns>
        private static string GetCorrectWorkLoad(string data)
        {
            if (data.Contains("total"))
                return data.Replace("total","");
            else
                return data;
        }
        
    }
    /// <summary>
    /// Класс-контроллер для конвертации из JSON
    /// </summary>
    public class Meta
    {
        [JsonProperty("items")]
        public List<string> items { get; set; }
    }
}
