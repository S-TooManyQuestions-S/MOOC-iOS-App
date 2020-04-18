using CourseLib;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace MySQL
{
    /// <summary>
    /// Набор методов для работы с конкретной базой данных
    /// </summary>
    public static class MySQLMethods
    {
        //строка подключения к созданной локально базе данных
        private const string connStr = "server=localhost;user=root;database=course_details;password=Busytofuck098765;";

        /// <summary>
        /// ?? Метод позволяющий получить строку из базы данных
        /// по ключевому значению (ссылке)
        /// </summary>
        /// <param name="link">Ключевое значение (ссылка)</param>
        /// <returns>Дополнительная информация о курсе</returns>
        public static CourseDetails GetFromSQL(string link)
        {
            List<string> data = new List<string>();
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string sql = $"SELECT * FROM details WHERE CourseLink = '{link}'";
                    using (MySqlCommand command = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader MyDataReader = command.ExecuteReader())
                        {
                            DataTable schemaTable = MyDataReader.GetSchemaTable();
                            while (MyDataReader.Read())
                            {
                                for (var i = 0; i < MyDataReader.FieldCount; i++)
                                {
                                    data.Add(MyDataReader.GetString(i));
                                }
                            }
                        }
                    }
                    conn.Close();
                }
                if (data.Count == 0)
                    return null;
                return new CourseDetails(data[1], data[2], data[3], data[4], data[5]);
            }
            catch(Exception e)
            {
                Console.WriteLine($"Произошла ошибка при получении значений из базы данных!\n{e.Message}");
                return null;
            }
        }

        /// <summary>
        /// Метод добавляющий значение в базу данных
        /// </summary>
        /// <param name="details">Детали для помещения в базу данных</param>
        /// <param name="link">Ссылка на страницу курса (уникальный идентификатор)</param>
        public static void InsertInSQL(CourseDetails details, string link)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string sql_registrUser = $"INSERT INTO details VALUES('{link}','{details.ShortDescriprion}','{details.LongDescription}','{details.TargetAudience}','{details.Format}','{details.WorkLoad}');";
                    MySqlCommand comm_registr = new MySqlCommand(sql_registrUser, conn);
                    comm_registr.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"Данные с идентификатором: {link} не были помещены в базу!\n{e.Message}");
            }
        }
        
    }
}
