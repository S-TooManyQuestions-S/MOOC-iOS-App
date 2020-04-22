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
        //ссылка на конкретный курс (используется как внутренний уникальный 
        //идентификатор внутри базы данных)
        public string InformationPath { get; }
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        /// <param name="company">Наименование компании</param>
        /// <param name="id">Айди</param>
        /// <param name="informationPath">Ссылка на курс</param>
        public CurrentInfo(string company, double id, string informationPath)
        {
            CompanyId = id;
            CompanyName = company;
            InformationPath = informationPath;
        }
    }
}
