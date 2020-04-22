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
        //логотип компании
        public string LogoLink { get; }
        public Image(string coverlink, string logoLink)
        {
            CoverImage = coverlink;
            LogoLink = logoLink;
        }
    }
}
