using CourseLib;
using Coursera;
using Microsoft.Extensions.Logging;
using MySQL;
using NLog;
using Stepik;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Udemy;

namespace MOOC_Server.MySettings
{
    /// <summary>
    /// Класс реализующий (собирающий все методы по сбору данных)
    /// </summary>
    public class ServerRepository : IServerRepository
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Stopwatch timer = new Stopwatch();

        /// <summary>
        /// Метод собируающий результаты поиска со всех парсеров
        /// </summary>
        /// <param name="keyword">Ключеове слово</param>
        /// <returns>Список результатов</returns>
        public List<Course> GetByKeyWord(string keyword)
        {
            List<Course> AllCourses = new List<Course>();
            try
            {
                //добавление резульатов поиска курсов с платформы Stepik.org
                {
                    timer.Start();
                    AllCourses.AddRange(StepikMethods.GetCourses(keyword));
                    timer.Stop();
                    logger.Info($"[Result:SUCCESS][Process:Parsing][Platform:Stepik]  [Elapsed Time:{timer.Elapsed.TotalMilliseconds}]");
                }
                timer.Reset();
                {
                    timer.Start();
                    AllCourses.AddRange(CourseraMethods.GetCourses(keyword));
                    timer.Stop();
                    logger.Info($"[Result:SUCCESS][Process:Parsing][Platform:Coursera][Elapsed Time:{timer.Elapsed.TotalMilliseconds}]");
                }
                timer.Reset();
                {
                    timer.Start();
                    AllCourses.AddRange(UdemyMethods.GetCourses(keyword));
                    timer.Stop();
                    logger.Info($"[Result:SUCCESS][Process:Parsing][Platform:Udemy]   [Elapsed Time:{timer.Elapsed.TotalMilliseconds}]");
                }
            }
            catch (Exception e)
            {
                logger.Error($"[Result:ERROR][Process:Parsing][Information:{e.Message}]");
            }
            timer.Reset();
            return AllCourses;
        }
        /// <summary>
        /// Получить детали по курсу по уникальному идентификатору (url-ссылке)
        /// </summary>
        /// <param name="link">ссылка на курс</param>
        /// <returns>Детали курса</returns>
        public CourseDetails GetDetailsByCourse(string link)
        {
            timer.Start();
            var result = MySQLMethods.GetFromSQL(link);
            timer.Stop();

            if (result != null)
            {
                logger.Info($"[Result: SUCCESS][Process: GetFromSQL][URL: {link}][Elapsed Time: {timer.ElapsedMilliseconds}]");
                timer.Reset();
                return result;
            }
            logger.Error($"[Result: FAILED][Process: GetFromSQL][URL: {link}][Elapsed Time: {timer.ElapsedMilliseconds}]");
            timer.Reset();

            CourseDetails details = null;
            try
            {
                if (link.Contains("stepik"))
                {
                    {
                        timer.Start();
                        details = StepikMethods.GetDetails(link);
                        timer.Stop();
                        logger.Info($"[Result: SUCCESS][Process: GetDetails][Stepik][URL: {link}][Elapsed Time: {timer.ElapsedMilliseconds}]");
                    }
                }
                else if (link.Contains("coursera"))
                {
                    {
                        timer.Start();
                        details = CourseraMethods.GetDetails(link);
                        timer.Stop();
                        logger.Info($"[Result: SUCCESS][Process: GetDetails][Coursera][URL: {link}][Elapsed Time: {timer.ElapsedMilliseconds}]");
                    }
                }
                else if (link.Contains("udemy")) //обработка через id
                {
                    {
                        timer.Start();
                        details = UdemyMethods.GetDetails(link);
                        timer.Stop();
                        logger.Info($"[Result: SUCCESS][Process: GetDetails][Udemy][URL: {link}][Elapsed Time: {timer.ElapsedMilliseconds}]");
                    }
                }

                if (MySQLMethods.InsertInSQL(details, link))
                    logger.Info($"[Result: SUCCESS][Process: InsertSQL][URL: {link}]");
                else
                    logger.Error($"[Result: FAILED][Process: InsertSQL][URL: {link}]");
            }
            catch (Exception e)
            {
                logger.Error($"[Result: FAILED][Process: GetDetails/Insert][Udemy][URL: { link}][{e.Message}]");
            }
            return details;
        }

    }
}
