using System;
using System.Collections.Generic;
using System.Text;

namespace CourseLib.Types
{
    /// <summary>
    /// Мета-информация по курсу
    /// </summary>
    public class CurrentInfo
    {
        //название компании
        public string CompanyName { get; }
        //айди внутри компании (может использоваться для поиска данных)
        public double CompanyId { get; }
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
        public CurrentInfo(string company, double id, string api, string coursePath)
        {
            CompanyId = id;
            CompanyName = company;
            APIpath = api;
            CoursePath = coursePath;
        }
    }
}
