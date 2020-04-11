using System;
using System.Collections.Generic;
using System.Text;

namespace CourseLib.Types
{
    public class Rating
    {
        public double MyRating { get; } //преобразованный рейтинг
        public Rating(double localrating)
        {
            MyRating = localrating;
        }
    }
}
