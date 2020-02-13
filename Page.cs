using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Homework_07
{
    //enum months { January, February, March, April, May, June, July, August, September, October, November, December}

    [Serializable]
    /// <summary>
    /// Класс страницы ежедневника
    /// </summary>
    class Page
    {
        //Бинарное имя файла страницы
        public string binpath { get { return (Num.ToString() + Date.Day.ToString() 
                    + Date.Month.ToString() + Date.Year.ToString() + ".page"); } }

        //Номер страницы
        public int Num { get; set; }

        //Дата, записанная на странице
        public DateTime Date;

        //Список праздников или других наименований дня, который записан на странице
        public List<string> DayName { get; set; } = new List<string>();

        //Список дел на день, не привязанных к времени
        public Point ToDoList { get; set; }

        //Расписание на день
        public Schedule Schedule { get; set; }

        /// <summary>
        /// Конструктор страницы ежедневника
        /// </summary>
        /// <param name="date">Дата указанная на странице</param>
        /// <param name="todolist">Список дел</param>
        /// <param name="schedule">Расписание на день</param>
        /// <param name="dayNames">Указание праздника или других наименований дня</param>
        public Page(DateTime date, Point todolist, Schedule schedule, params string[] dayNames)
        {
            Date = date;
            ToDoList = todolist;
            Schedule = schedule;
            DayName = dayNames.ToList();
        }

        /// <summary>
        /// Сохранить страницу как файл
        /// </summary>
        /// <param name="path">Путь сохранения</param>
        public void SavePage (string path)
        {
            if (!path.Equals(""))
            {
                Directory.CreateDirectory(path);

                path += "/";
            }
            BinaryFormatter binForm = new BinaryFormatter();

            path += binpath;

            using (var file = new FileStream(path, FileMode.OpenOrCreate))
            {
                binForm.Serialize(file, this);
            }
        }

        /// <summary>
        /// Загрузить страницу из файла
        /// </summary>
        /// <param name="path">Путь к файлу загрузки</param>
        /// <returns>Страница из файла по указанному пути</returns>
        public static Page LoadPage (string path)
        {
            BinaryFormatter binForm = new BinaryFormatter();

            if (path.Substring(path.Length - 5).Equals(".page"))
            {
                try
                {
                    using (var file = new FileStream(path, FileMode.Open))
                    {
                        return (Page)binForm.Deserialize(file);
                    }
                }
                catch (FileNotFoundException fe)
                {
                    return null;
                }
            } else
            {
                throw (new Exception("Неверное разрешение запрашиваемого файла"));
            }
        }

        /// <summary>
        /// Представление класса страницы в виде строки
        /// </summary>
        /// <returns>Строка, содержащая атрибуты класса страницы</returns>
        public override string ToString()
        {
            string result = "";
            result += "Дата: " + $"{Date.Day:00}." + $"{Date.Month:00}." + $"{Date.Year:0000}\n";
            result += "Наименования дня: ";
            foreach (string s in DayName)
            {
                result += s + "; ";
            }
            result += "\n";
            if (ToDoList != null)
                result += "\n" + ToDoList.ToString();
            else result += "\nПустой списко дел\n";
            if (Schedule != null)
                result += "\n" + Schedule.ToString();
            else result += "\nПустое расписание\n";

            return result;
        }
    }
}
