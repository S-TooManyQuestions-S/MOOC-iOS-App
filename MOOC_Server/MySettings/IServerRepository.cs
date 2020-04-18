using CourseLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOOC_Server.MySettings
{
    public interface IServerRepository
    {
        /// <summary>
        /// Получить курсы по ключевому слову
        /// </summary>
        /// <param name="keyword">Ключевое слово</param>
        /// <returns>Набор информации о каждом курсе</returns>
        List<Course> GetByKeyWord(string keyword);
        /// <summary>
        /// Получить информацию по курсу
        /// </summary>
        /// <param name="link">Ссылка на страницу курса
        /// уникальный идентификатор</param>
        /// <returns>Конкретные данные по курсу</returns>
        CourseDetails GetDetailsByCourse(string link);
    }
}
