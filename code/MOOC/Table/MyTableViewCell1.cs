using System;
using System.Drawing;
using System.Text.RegularExpressions;
using CoreAnimation;
using CoreGraphics;
using FFImageLoading;
using Foundation;
using MOOC.DataLibrary;
using UIKit;

namespace MOOC.Controllers
{
    public partial class MyTableViewCell1 : UITableViewCell
    {
        public static readonly NSString Key = new NSString("MyTableViewCell1");
        public static readonly UINib Nib;

        static MyTableViewCell1()
        {
            Nib = UINib.FromName("MyTableViewCell1", NSBundle.MainBundle);
        }

        protected MyTableViewCell1(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        public void UpdateData(Course course)
        {
            

            var blur = UIBlurEffect.FromStyle(UIBlurEffectStyle.Regular);
            var blurView = new UIVisualEffectView(blur)
            {
                Frame = new RectangleF(0, (float)(CourseTitle.Frame.Height*0.9), (float)CourseTitle.Frame.Width+30, (float)CourseTitle.Frame.Height)
               
            };
            CourseImage.Add(blurView);

            try
            {
                ImageService.Instance.LoadUrl(course.CourseImages.CoverImage).Into(CourseImage);
            }
            catch (Exception)
            {
                if (course.Info.APIpath.Contains("stepik"))
                    CourseImage.Image = UIImage.FromFile("DefaultImages/Stepik.jpg");
                else if (course.Info.APIpath.Contains("coursera"))
                    CourseImage.Image = UIImage.FromFile("DefaultImages/Coursera.jpg");
                else
                    CourseImage.Image = UIImage.FromFile("DefaultImages/Udemy.png");
            }
            if (Regex.IsMatch(course.CourseName, "[А-Яа-я]+"))
            {
                Language.Image = UIImage.FromFile("DefaultImages/rus.png");
                Language.ContentMode = UIViewContentMode.ScaleAspectFill;
            }
            else
            {
                Language.Image = default;
            }  

            SelectionStyle = UITableViewCellSelectionStyle.None;
            CourseImage.ClipsToBounds = true;
            
            CourseImage.ContentMode = UIViewContentMode.ScaleAspectFill;
            CourseTitle.Text = course.CourseName;
        }

        
        
    }
}
