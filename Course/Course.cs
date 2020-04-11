using CourseLib.Types;

namespace CourseLib
{
    public class Course
    {
        public string CourseName { get; } //название курса
        public Rating CourseRating { get; } //рейтинг
        public Image CourseImages { get; }//все ссылки на картинки
        public CurrentInfo Info { get; }//метаданные по конкретному курсу

        public Course(string name, Rating rating, Image images, CurrentInfo info)
        {
            CourseName = name;
            CourseRating = rating;
            Info = info;
            CourseImages = images;
        }
    }
}
