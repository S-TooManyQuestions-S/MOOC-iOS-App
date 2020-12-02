using System;
using System.Collections.Generic;
using System.Text;

namespace CourseLib
{
    [Serializable]
    public class CourseDetails
    {
        //краткое описание (если доступно)
        public string ShortDescriprion { get; }
        //более полное описание (если доступно)
        public string LongDescription { get; }
        //целевая аудитория (если доступно)
        public string TargetAudience { get; }
        //формат курса (если доступно)
        public string Format { get; }
        //продолжительность выполнения курса (если доступно)
        public string WorkLoad { get; } 

        public CourseDetails(string short_descr, string long_descr, string audience, string format, string time)
        {
            ShortDescriprion = short_descr;
            LongDescription = long_descr;
            TargetAudience = audience;
            Format = format;
            WorkLoad = time;
        }
    }
}
