using CourseLib;
using Coursera;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using Stepik;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Udemy;

namespace MySQL
{
    /// <summary>
    /// Набор методов для работы с конкретной базой данных
    /// </summary>
    public static class MySQLMethods
    {
        //строка подключения к созданной локально базе данных
        private const string connStr = "server=localhost;port=3306;user=root;database=data_base;password=password;";
       

        public static CourseDetails GetFromSQL(string link)
        {
            try
            {
                string sqlDate = $"SELECT Date FROM datatable WHERE url = '{link}'";
                DateTime _dateOfCreation;
                MySqlCommand command;

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();

                    command = new MySqlCommand(sqlDate, conn);
                    _dateOfCreation = DateTime.Parse(command.ExecuteScalar().ToString());
                }
                
                //если информация не обновлялась более чем 5 дней - происходит обновление данных
                if ((DateTime.Now - _dateOfCreation).Days >= 5)
                    Update(link);

                BinaryFormatter binaryFormatter = new BinaryFormatter();
                string sqlSelect = $"SELECT course FROM datatable WHERE url = '{link}'";
                byte[] data;//информация

                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    command = new MySqlCommand(sqlSelect, conn);
                    data = (byte[])command.ExecuteScalar();
                }

                CourseDetails details;
                using(MemoryStream memoryStream = new MemoryStream(data))
                {
                    details = (CourseDetails)binaryFormatter.Deserialize(memoryStream);
                }
                return details;
            }
            catch(Exception e)
            {
                return null;
                // Console.WriteLine($"Произошла непредвиденная ошибка при получении значений из базы данных!\n{e.Message}");
            }
            
        }

        /// <summary>
        /// Помещает данные в базу данных
        /// </summary>
        /// <param name="details">Данные</param>
        /// <param name="link">Уникальный ключ (ссылка)</param>
        public static bool InsertInSQL(CourseDetails details, string link)
        {
            try
            {
                //Если запись не существует
                //if (!Exist(link))
                {
                    using (MySqlConnection conn = new MySqlConnection(connStr))
                    {
                        conn.Open();//открываем соединение

                        byte[] binData = DataGrab(details);//сериализуем объект для записи

                        string sqlCommand = $"INSERT INTO datatable VALUES (0,'{link}',?information,'{DateTime.Now:yyyy:MM:dd HH:mm:ss}');";//команда для MySql

                        var command = new MySqlCommand(sqlCommand, conn);//инициализация команды

                        command.Parameters.Add("?information", MySqlDbType.Blob).Value = binData;
                        command.ExecuteNonQuery();//добавление информации в бд

                        conn.Close();//закрываем соединение
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                //Console.WriteLine($"Данные с идентификатором: {link} не были помещены в базу!\n{e.Message}");
                return false;
            }
        }

        /// <summary>
        /// Сериализация объекта в массив байтов
        /// </summary>
        /// <param name="details">объект (информация о курсе)</param>
        /// <returns>Преобразованный в байтовый массив объект</returns>
        private static byte[] DataGrab(CourseDetails details)
        {
            BinaryFormatter fb = new BinaryFormatter();
            byte[] data;
            //открываем поток для записи иниформации 
            using (MemoryStream ms = new MemoryStream())
            {
                fb.Serialize(ms, details);
                data = ms.ToArray();
            }
            return data;
        }

        /// <summary>
        /// Проверяет существует ли строка с таким уникальным url в таблице
        /// </summary>
        /// <param name="link">уникальный url</param>
        /// <returns></returns>
        private static bool Exist(string link)
        {
            //команда для выбора значения из таблицы
            var sqlCommand = $"SELECT EXISTS(SELECT url FROM datatable WHERE url = '{link}');";

            var result = String.Empty;

            //открываем соединение
            using (MySqlConnection mySqlConnection = new MySqlConnection(connStr))
            {
                mySqlConnection.Open();
                //инициализация команды
                var command = new MySqlCommand(sqlCommand, mySqlConnection);

                result = command.ExecuteScalar().ToString();
            }
            return result == "1";
        }


        private static void Update(string link)
        {
            //подумать как можно переделать
            CourseDetails details;
            if (link.Contains("udemy"))
                details = UdemyMethods.GetDetails(link);
            else if (link.Contains("stepik"))
                details = StepikMethods.GetDetails(link);
            else details = CourseraMethods.GetDetails(link);

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();//открываем соединение

                byte[] binData = DataGrab(details);//сериализуем объект для записи

                string sqlCommand = $"UPDATE datatable SET course = ?information, Date = {DateTime.Now:yyyy:mm:dd hh:mm:ss}';";//команда для MySql

                var command = new MySqlCommand(sqlCommand, conn);//инициализация команды

                command.Parameters.Add("?information", MySqlDbType.Blob).Value = binData;
                command.ExecuteNonQuery();//добавление информации в бд

            }
        }


    }
}
