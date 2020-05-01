using System;
using System.Collections.Generic;
using System.Text;

namespace CourseLib.Types
{
    /// <summary>
    /// Все изображения, связанные с курсом
    /// </summary>
    public class Image
    {
        //обложка курса
        public string CoverImage { get; }
        public Image(string coverlink)
        {
            CoverImage = coverlink;
        }
    }
}
