using System;
using MOOC.DataLibrary.Types;

namespace MOOC.DataLibrary
{
    /// <summary>
    /// Курс и вся необходимая информация к нему
    /// </summary>
    public class Course : IComparable<Course>
    {
        //название курса
        public string CourseName { get; }
        //рейтинг
        public Rating CourseRating { get; }
        //все ссылки на картинки
        public Image CourseImages { get; }
        //метаданные по конкретному курсу
        public CurrentInfo Info { get; }

        public Course(string CourseName, Rating CourseRating, Image CourseImages, CurrentInfo Info)
        {
            this.CourseName = CourseName;
            this.CourseRating = CourseRating;
            this.CourseImages = CourseImages;
            this.Info = Info;
        }
        /// <summary>
        /// Компортаор для сортировки по рейтингу
        /// </summary>
        /// <param name="other"></param>
        /// <returns>Является ли рейтинг нашего экземпляра больше либо меньше</returns>
        public int CompareTo(Course other)
        => other.CourseRating.MyRating.CompareTo(this.CourseRating.MyRating);
    }
}
