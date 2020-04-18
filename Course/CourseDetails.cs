using System;
using System.Collections.Generic;
using System.Text;

namespace CourseLib
{
    public class CourseDetails
    { 
        public string ShortDescriprion { get; }//краткое описание (если доступно)
        public string LongDescription { get; }//более полное описание (если доступно)
        public string TargetAudience { get; } = "Будет интересно любому";//целевая аудитория (если доступно)
        public string Format { get; } = "Онлайн курс";//формат курса (если доступно)
        public string WorkLoad { get; } = "Свободный график (без ограничений)";//продолжительность выполнения курса (если доступно)

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
