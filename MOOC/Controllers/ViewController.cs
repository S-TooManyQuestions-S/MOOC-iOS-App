using Foundation;
using System;
using System.Collections.Generic;
using UIKit;
using MOOC.DataLibrary;
using MOOC.Controllers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Linq;
using System.Text.RegularExpressions;

namespace MOOC
{
    public partial class ViewController : UIViewController
    {


        public ViewController(IntPtr handle) : base(handle)
        {

        }

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

        public override void ViewDidLoad()
        {

            base.ViewDidLoad();
            //если ничего е найдено - пустой лист
            GetInformation.popular = GetInformation.GetCourses(string.Empty) ?? new List<Course>();
            //обновляем страницу
            DataTable.Source = new ListTableSource(GetInformation.popular);

            //устанавливаем размеры таблицы (высота ячейки)
            DataTable.RowHeight = 156;

            DataTable.ReloadData();//перезагружаем таблицу
            //событие, если работает поиск (пользователь начал поиск)

            CourseSearch.SearchButtonClicked += (sender, e)
               =>
            {
                string message = CourseSearch.Text; //вычленяем сообщение
                                                    //выполняем задачу в потоковом режиме, чтобы не тормозило приложение
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
        {
            //получение информации по ключевому слову
            GetInformation.courses = GetInformation.GetCourses(message);
            InvokeOnMainThread(() =>
            {
                //заполнение DataTable
                DataTable.Source = new ListTableSource(RatingSort(GetInformation.courses));
                DataTable.ReloadData();
            });
        })).Start();
            };
            //добавляем обновление списка
            #region update
            RefreshAsync();

            AddRefreshControl();

            DataTable.Add(RefreshControl);
            #endregion
        }


        /// <summary>
        /// Переключатель между Популярным и избранным
        /// </summary>
        /// <param name="sender"></param>
        partial void ValueChanged(UISegmentedControl sender)
        {
            if (IsWifiConnected())
            {
                //индекс который выбрал пользователь
                var index = Specification.SelectedSegment;
                //определяем какой сегмент выбран и действуем в соответствии с выбранным сегментом
                if (index == 0)
                {
                    //если популярное еще не загружено - загружаем, иначе
                    //чтобы не подгружать постоянно - отображаем подгруженное
                    if (GetInformation.popular == null)
                        //выполняем подгрузку в другом потоке
                        new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                        {
                            //ищем популярные
                            GetInformation.popular = GetInformation.GetCourses(string.Empty);
                            InvokeOnMainThread(() =>
                            {
                                DataTable.Source = new ListTableSource(RatingSort(GetInformation.popular));
                                DataTable.ReloadData();
                            });
                        })).Start();
                    else
                    {
                        DataTable.Source = new ListTableSource(RatingSort(GetInformation.popular));
                        DataTable.ReloadData();
                    }
                }
                else if (index == 2)
                {

                    //аналогично популярному, но уже важное (избранное)

                    DataTable.Source = new ListTableSource(GetInformation.important);
                    DataTable.ReloadData();


                }
                else if (index == 1)
                {
                    GetInformation.russian = new List<Course>();
                    if (GetInformation.popular == null)
                        new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                        {
                            //ищем популярные
                            GetInformation.popular = GetInformation.GetCourses(string.Empty);
                            InvokeOnMainThread(() =>
                            {

                                GetInformation.popular.ForEach(x =>
                                {
                                    if (Regex.IsMatch(x.CourseName, "[А-Яа-я]+"))
                                        GetInformation.russian.Add(x);
                                });
                                DataTable.Source = new ListTableSource(RatingSort(GetInformation.russian));
                                DataTable.ReloadData();
                            });
                        })).Start();
                    else
                    {

                        GetInformation.popular.ForEach(x =>
                        {
                            if (Regex.IsMatch(x.CourseName, "[А-Яа-я]+"))
                                GetInformation.russian.Add(x);
                        });
                        DataTable.Source = new ListTableSource(RatingSort(GetInformation.russian));
                        DataTable.ReloadData();
                    }
                }
            }
            else
            {//Если нет соединения с интернетом - отображаем ошибку
                CreateAlert();
            }
        }


        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {

            if (segue.Identifier == "CellSegue")
            {
                if (!IsWifiConnected())
                { CreateAlert(); }

                var controller = segue.DestinationViewController as DetailsController;

                int row = DataTable.IndexPathForSelectedRow.Row;

                Course course = ((ListTableSource)DataTable.Source).courses[row];

                controller.details = GetInformation.GetDetails(course.Info.APIpath);
               
                controller.course = course;
            }

        }
        /// <summary>
        /// Поиск по тегу c#
        /// </summary>
        /// <param name="sender"></param>
        partial void Sharp_TouchUpInside(UIButton sender)
        {
            if (!IsWifiConnected())
            { CreateAlert(); }
            if (GetInformation.sharp == null)
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    GetInformation.sharp = GetInformation.GetCourses("c%23");
                    InvokeOnMainThread(() =>
                    {
                        DataTable.Source = new ListTableSource(RatingSort(GetInformation.sharp));
                        DataTable.ReloadData();
                    });
                })).Start();
            }
            else
            {
                DataTable.Source = new ListTableSource(RatingSort(GetInformation.sharp));
                DataTable.ReloadData();
            }

        }
        /// <summary>
        /// Поиск по тегу Python
        /// </summary>
        /// <param name="sender"></param>
        partial void Python_TouchUpInside(UIButton sender)
        {
            if (!IsWifiConnected())
            { CreateAlert(); }
            if (GetInformation.python == null)
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    GetInformation.python = GetInformation.GetCourses("python");
                    InvokeOnMainThread(() =>
                    {
                        DataTable.Source = new ListTableSource(RatingSort(GetInformation.python));
                        DataTable.ReloadData();
                    });
                })).Start();
            }
            else
            {
                DataTable.Source = new ListTableSource(RatingSort(GetInformation.python));
                DataTable.ReloadData();
            }

        }
        /// <summary>
        /// Поиск по тегу Data Science
        /// </summary>
        /// <param name="sender"></param>
        partial void DataScience_TouchUpInside(UIButton sender)
        {
            if (!IsWifiConnected())
            { CreateAlert(); }
            if (GetInformation.data == null)
            {
                new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                {
                    GetInformation.data = GetInformation.GetCourses("Data%20Science");
                    InvokeOnMainThread(() =>
                    {
                        DataTable.Source = new ListTableSource(RatingSort(GetInformation.data));
                        DataTable.ReloadData();
                    });
                })).Start();
            }
            else
            {
                DataTable.Source = new ListTableSource(GetInformation.data);
                DataTable.ReloadData();
            }



        }
        /// <summary>
        /// Поиск по тегу Xamarin
        /// </summary>
        /// <param name="sender"></param>
        partial void Xamarin_TouchUpInside(UIButton sender)
        {
            if (IsWifiConnected())
            {
                if (GetInformation.xamarin == null)
                {
                    new System.Threading.Thread(new System.Threading.ThreadStart(() =>
                    {
                        GetInformation.xamarin = GetInformation.GetCourses("xamarin");
                        InvokeOnMainThread(() =>
                        {
                            DataTable.Source = new ListTableSource(RatingSort(GetInformation.xamarin));
                            DataTable.ReloadData();
                        });
                    })).Start();
                }
                else
                {
                    DataTable.Source = new ListTableSource(RatingSort(GetInformation.xamarin));
                    DataTable.ReloadData();
                }
            }
        }

        /// <summary>
        /// Сортировка курсов по убыванию рейтинга
        /// </summary>
        /// <param name="listToSort"></param>
        /// <returns></returns>
        private static List<Course> RatingSort(List<Course> listToSort)
        {
            listToSort?.Sort((x, y) => x.CompareTo(y));
            return listToSort;
        }


        bool useRefreshControl = false;
        UIRefreshControl RefreshControl;

        void RefreshAsync()
        {
            if (useRefreshControl)
                RefreshControl.BeginRefreshing();

            if (useRefreshControl)
                RefreshControl.EndRefreshing();

            DataTable.ReloadData();
        }

        void AddRefreshControl()
        {
            RefreshControl = new UIRefreshControl();
            RefreshControl.ValueChanged += (sender, e) =>
           {
               RefreshAsync();
           };
            useRefreshControl = true;
        }

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

    }
}