using System;
namespace MOOC.DataLibrary.Types
{
    /// <summary>
    /// Контейнер для рейтинга
    /// </summary>
    public class Rating
    {
        //преобразованный рейтинг (мой локальный рейтинг)
        public double MyRating { get; }
        public Rating(double MyRating)
        {
            this.MyRating = MyRating;
        }
    }
}
