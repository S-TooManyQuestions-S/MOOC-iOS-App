using System;
using System.Collections.Generic;
using System.IO;
using MOOC.DataLibrary;
using Newtonsoft.Json;

namespace MOOC.JSONOptions
{
    public static class JsonMethods
    {
        /// <summary>
        /// Сериализация JSON
        /// </summary>
        /// <param name="courses"></param>
        public static void JsonWriter(List<Course> courses)
        {
            //получение пути к папке
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //совмещаем путь и название папки (если папки не существует - система создает ее)
            string filePath = Path.Combine(path, "important1.txt");
            //конвертация в json
            var jsonInformation = JsonConvert.SerializeObject(courses, Formatting.Indented);
            //пишем информацию в файл
            using (StreamWriter sw = new StreamWriter(filePath,false))
                sw.Write(jsonInformation);
        }

        /// <summary>
        /// Десериализация JSON
        /// </summary>
        /// <returns></returns>
        public static List<Course> JsonReader()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            string filePath = Path.Combine(path, "important1.txt");

            string information;
            using (FileStream file = File.Open(filePath,FileMode.OpenOrCreate))
            using (StreamReader reader = new StreamReader(file,true))
                information = reader.ReadToEnd();

            var listOfCourses = JsonConvert.DeserializeObject<List<Course>>(information) ?? new List<Course>();

            return listOfCourses;

        }

    }
}
