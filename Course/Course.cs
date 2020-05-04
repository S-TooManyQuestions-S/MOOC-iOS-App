using CourseLib.Types;

namespace CourseLib
{
    /// <summary>
    /// Курс и вся необходимая информация к нему
    /// </summary>
    public class Course
    {
        //название курса
        public string CourseName { get; }
        //рейтинг
        public  double  CourseRating { get; set; }//пофиксить потом
        //все ссылки на картинки
        public Image CourseImages { get; }
        //метаданные по конкретному курсу
        public CurrentInfo Info { get; }

        public Course(string name, double rating, Image images, CurrentInfo info)
        {
            CourseName = name;
            CourseRating = rating;
            Info = info;
            CourseImages = images;
        }
    }
}
