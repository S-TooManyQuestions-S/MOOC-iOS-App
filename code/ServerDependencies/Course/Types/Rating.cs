using System;
using System.Collections.Generic;
using System.Text;

namespace CourseLib.Types
{
    /// <summary>
    /// Контейнер для рейтинга
    /// </summary>
    public class Rating
    {
        //преобразованный рейтинг (мой локальный рейтинг)
        public double MyRating { get; } 
        public Rating(double localrating)
        {
            MyRating = localrating;
        }
    }
}
