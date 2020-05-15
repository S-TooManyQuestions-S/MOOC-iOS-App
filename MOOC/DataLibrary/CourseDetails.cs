using System;
namespace MOOC.DataLibrary
{
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

        public CourseDetails(string shortDescriprion, string longDescription, string targetAudience, string format, string workLoad)
        {
            ShortDescriprion = shortDescriprion;
            LongDescription = longDescription;
            TargetAudience = targetAudience;
            Format = format;
            WorkLoad = workLoad;
        }
    }
}
