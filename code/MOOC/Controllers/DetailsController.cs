using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading;
using Foundation;
using MOOC.DataLibrary;
using SafariServices;
using UIKit;
using Xamarin.Essentials;

namespace MOOC.Controllers
{
    public partial class DetailsController : UIViewController
    {
        public CourseDetails details;
        public Course course;
        private string ShortData;

        #region WIFIConnection
        /// <summary>
        /// Проверка подключения к интернету
        /// </summary>
        /// <returns></returns>
        public bool IsWifiConnected()
        {
            var profiles = Connectivity.ConnectionProfiles;
            return profiles.Contains(ConnectionProfile.WiFi) || profiles.Contains(ConnectionProfile.Ethernet);
        }
        #endregion

        /// <summary>
        ///  Создаем уведомление, если стабильного подключения к интернету нет!
        /// </summary>
        public void CreateAlert()
        {
            var alert = UIAlertController.Create("Ошибка", "Нет интернет-соединения\nПроверьте подключение к сети и перезагрузите приложение!", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Выход", UIAlertActionStyle.Destructive, (action) =>
            { Environment.Exit(0); }));

            this.PresentViewController(alert, true, null);
        }

        public DetailsController(IntPtr handle) : base(handle)
        {


        }


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            if (IsWifiConnected())
            {
                //загрузка кнопки добавления в избранное
                ButtonDidLoad();
                //Загрузка текста
                TextDidLoad();
                //Загрузка изображения и комплектующих
                ImageDidLoad();
            }
            else
            {
                CreateAlert();
            }
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();

            // Release any cached data, images, etc that aren't in use.
        }


        public void ButtonDidLoad()
        {
            if (!JsonNotContains(GetInformation.important, course))
                Like.SetImage(UIImage.GetSystemImage("heart.fill"), UIControlState.Normal);
            else
                Like.SetImage(UIImage.GetSystemImage("heart"), UIControlState.Normal);
        }

        /// <summary>
        /// Метод отвечающий за изменение значения переключателя информации
        /// </summary>
        /// <param name="sender"></param>
        partial void ValueChanged(UISegmentedControl sender)
        {
            var index = DataPicker.SelectedSegment;

            if (index == 1)
            {
                Text.Text = details.LongDescription;
            }
            else
            {
                Text.Text = ShortData;
            }
        }

        partial void UIButton56234_TouchUpInside(UIButton sender)
        {
            if (!IsWifiConnected())
                CreateAlert();
            var sfViewController = new SFSafariViewController(new NSUrl(course.Info.CoursePath));

            PresentViewController(sfViewController, true, null);
        }

        /// <summary>
        /// Весь текст на экране пользователя
        /// </summary>
        public void TextDidLoad()
        {
            NameDidLoad();
            ShortData = $">> Формат: {details.Format}{Environment.NewLine}>> Затраты по времени: {details.WorkLoad}{Environment.NewLine}" +
                $">> Требования:\n{details.TargetAudience}{Environment.NewLine}>> Краткое описание:{details.ShortDescriprion}";

            Text.Text = ShortData;
            Text.TextAlignment = UITextAlignment.Left;

            Text.Frame = new CoreGraphics.CGRect();
            Text.Font = UIFont.BoldSystemFontOfSize(20);
            Text.TextColor = new UIColor(207f / 255, 138f / 255, 17f / 255, 1);

        }

        /// <summary>
        /// Подгружаем все необходимые картинки и все элементы на ней и расставляем на
        /// экране пользователя
        /// </summary>
        public void ImageDidLoad()
        {
            //Подгружаем обложку курса
            try
            {
                ImageService.Instance.LoadUrl(course.CourseImages.CoverImage).Into(InnerImage);
            }
            catch (Exception)
            {
                if (course.Info.APIpath.Contains("stepik"))
                    InnerImage.Image = UIImage.FromFile("DefaultImages/Stepik.jpg");
                else if (course.Info.APIpath.Contains("coursera"))
                    InnerImage.Image = UIImage.FromFile("DefaultImages/Coursera.jpg");
                else
                    InnerImage.Image = UIImage.FromFile("DefaultImages/Udemy.png");
            }
            InnerImage.ClipsToBounds = true;
            InnerImage.ContentMode = UIViewContentMode.ScaleAspectFill;

            //определяем какой логотип вставлять
            if (course.Info.APIpath.Contains("stepik"))
                Logo.Image = UIImage.FromFile("DefaultImages/Stepik.jpg");
            else if (course.Info.APIpath.Contains("coursera"))
                Logo.Image = UIImage.FromFile("DefaultImages/Coursera.jpg");
            else
                Logo.Image = UIImage.FromFile("DefaultImages/Udemy.png");

            {
                Logo.ClipsToBounds = true;
                Logo.ContentMode = UIViewContentMode.ScaleAspectFill;
            }


            //стиль рейтинга 
            Rating.Text = string.Format($"{course.CourseRating.MyRating:F1}");
            Rating.Font = UIFont.BoldSystemFontOfSize(31);
            Rating.TextColor = new UIColor(207f / 255, 138f / 255, 17f / 255, 1);
            Rating.TextAlignment = UITextAlignment.Center;


        }


        public void NameDidLoad()
        {
            Name.Text = course.CourseName;
            Name.Font = UIFont.BoldSystemFontOfSize(20);
            Name.TextColor = new UIColor(207f / 255, 138f / 255, 17f / 255, 1);
            Name.TextAlignment = UITextAlignment.Center;

        }


        partial void Like_TouchUpInside(UIButton sender)
        {
            if (!IsWifiConnected())
                CreateAlert();
            if (!JsonNotContains(GetInformation.important, course))
            {
                Like.SetImage(UIImage.GetSystemImage("heart"), UIControlState.Normal);
                JsonRemove(GetInformation.important, course);
            }
            else
            {
                Like.SetImage(UIImage.GetSystemImage("heart.fill"), UIControlState.Normal);
                if (GetInformation.important != null)
                    GetInformation.important.Add(course);
                else
                    GetInformation.important = new List<Course>() { course };
            }


        }
        /// <summary>
        /// Проверка наличия курса в списке избранных курсов
        /// </summary>
        /// <param name="courses">Избранные курсы</param>
        /// <param name="course">Курс который необходимо проверитб на вхождение</param>
        /// <returns>Не содержит или содержит</returns>
        private static bool JsonNotContains(List<Course> courses, Course course)
         => courses?.TrueForAll(x => x.Info.APIpath != course.Info.APIpath) ?? false;

        /// <summary>
        /// Удаление курса из списка избранных
        /// </summary>
        /// <param name="courses">Список избранных</param>
        /// <param name="course">Курс который необходимо удалить</param>
        private static void JsonRemove(List<Course> courses, Course course)
        {
            courses?.Remove(courses?.Find(x => x.Info.APIpath == course.Info.APIpath));
        }


    }
}

