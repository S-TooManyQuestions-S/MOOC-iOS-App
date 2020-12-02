using System;
namespace MOOC.DataLibrary.Types
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
        public Image(string CoverImage, string LogoLink)
        {
            this.CoverImage = CoverImage;
            this.LogoLink = LogoLink;
        }
    }
}
