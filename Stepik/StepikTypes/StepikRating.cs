using CourseLib.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stepik.StepikTypes
{
    public class StepikRating : Rating
    {
        public StepikRating(double localrating)
            :base(GetRating(localrating))
        { }
        private static double GetRating(double localrating)
        {
            return Math.Round(localrating / 10000, 2);
        }
    }
}
