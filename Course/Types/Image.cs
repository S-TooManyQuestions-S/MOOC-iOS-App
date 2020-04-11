using System;
using System.Collections.Generic;
using System.Text;

namespace CourseLib.Types
{
    public class Image
    {
        public string CoverImage { get; }//обложка курса
        public string LogoLink { get; }//логотип компании
        public Image(string coverlink, string logoLink)
        {
            CoverImage = coverlink;
            LogoLink = logoLink;
        }
    }
}
