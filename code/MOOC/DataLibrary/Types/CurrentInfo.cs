using System;
namespace MOOC.DataLibrary.Types
{
    
        /// <summary>
        /// Мета-информация по курсу
        /// </summary>
        public class CurrentInfo
        {
            //название компании
            public string CompanyName { get; }
            //идентификатор внутри базы данных)
            public string APIpath { get; }
            //ссылка на курс (для перехода пользователем)
            public string CoursePath { get; }
            /// <summary>
            /// Стандартный конструктор
            /// </summary>
            /// <param name="company">Наименование компании</param>
            /// <param name="id">Айди</param>
            /// <param name="informationPath">Ссылка для парсинга</param>
            /// <param name="coursePath">Cсылка на курс</param>
            public CurrentInfo(string company,string apIpath, string coursePath)
            {
                CompanyName = company;
                APIpath = apIpath;
                CoursePath = coursePath;
            }
        }
    
    
}
