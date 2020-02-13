using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_07
{
    [Serializable]
    /// <summary>
    /// Класс, в котором находится пункты списка дел
    /// </summary>
    class Point
    {
        #region Локальные переменные
        public List<Point> ToDoList { get; set; } = new List<Point>();

        //Наименование списка
        public string ListName
        {
            get
            {
                //Если список существует, то его наименование будет записано в поле Text
                if (ToDoList.Count > 0)
                {
                    //Возвращаем поле Text
                    return Text;
                }
                //Если списка не существует, то возвращаем null
                else { return null; }
            }
            set
            {
                //Если список сущетвует, то изменяем переменную Text
                if (ToDoList.Count > 0)
                {
                    Text = value;
                }
            }
        }

        //Текст пункта/Заголовок
        public string Text { get; private set; } = "";

        //Выполнен ли пункт. По умолчанию false
        public bool Check { get; private set; } = false;
        #endregion

        #region Функции над пунктами
        /// <summary>
        /// Функция отметки выполнения пункта
        /// </summary>
        public void Checked ()
        {
            //Если пункт не выполнен
            if (!Check)
            {
                Check = true;
            }

            //Если это массив
            if (ToDoList.Count > 0)
            {
                foreach (Point p in ToDoList)
                {
                    p.Checked();
                }
            }
        }

        /// <summary>
        /// Конструктор класса Point
        /// </summary>
        /// <param name="input">Текст пункта</param>
        public Point (string input)
        {
            Text = input;
        }
        #endregion

        #region Функции списка

        /// <summary>
        /// Отмечаем пункты в списке по индексу
        /// </summary>
        /// <param name="indexs">Список индексов, которых нужно отметить</param>
        public void Checked (params int[] indexs)
        {
            if (ToDoList.Count > 0)
            {
                for (int i = 0; i < indexs.Length; i++)
                {
                    if (indexs[i] < ToDoList.Count)
                    {
                        ToDoList[indexs[i]].Checked();
                    } else
                    {
                        Console.WriteLine("Выход за границы списка!");
                    }
                }
            }
        }

        /// <summary>
        /// Конструктор массива пунктов
        /// </summary>
        /// <param name="input">Массив строк, каждый элемент которого является пунктом</param>
        public Point (params string[] input)
        {
            foreach (string s in input)
            {
                ToDoList.Add(new Point(s));
            }
        }

        public Point (params Point[] input)
        {
            foreach (Point p in input)
            {
                ToDoList.Add(p);
            }
        }

        /// <summary>
        /// Индексация класса 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Point this[int index]
        {
            get {
                if (ToDoList.Count > index && index >= 0)
                {
                    return ToDoList[index];
                } else {
                    Console.WriteLine("Выход за границы списка!");
                    return null; } }
            set { if (ToDoList.Count > index && index >= 0)
                {
                    ToDoList[index] = value;
                }
                else {
                    Console.WriteLine("Выход за границы списка!");
                } }
        }

        /// <summary>
        /// Добавление в список нового пункта
        /// </summary>
        /// <param name="addedPoint"></param>
        public void AddPoint (Point addedPoint)
        {
            //Если список пустой
            if (ToDoList.Count == 0)
            {
                //Создаем новый список первым пунктом которого станет этот пункт
                ToDoList.Add(new Point(this.Text) { Check = this.Check ? true : false });
                //Переменные из этого пункта возвращаем в исходные значения
                this.Text = "";
                this.Check = false;
            }
            //Добавляем новый пункт
            ToDoList.Add(addedPoint);
        }

        /// <summary>
        /// Добавление в список нового пункта
        /// </summary>
        /// <param name="addedPoint">Строка обозначающая новый пункт</param>
        public void AddPoint (string addedString)
        {
            //Создаем новый пункт из строки
            Point addedPoint = new Point(addedString);

            //Если список пустой
            if (ToDoList.Count == 0)
            {
                //Создаем новый список первым пунктом которого станет этот пункт
                ToDoList.Add(new Point(this.Text) { Check = this.Check ? true : false });
                //Переменные из этого пункта возвращаем в исходные значения
                this.Text = "";
                this.Check = false;
            }
            //Добавляем новый пункт
            ToDoList.Add(addedPoint);
        }

        #endregion

        /// <summary>
        /// Представление класса в виде строки
        /// </summary>
        /// <returns>
        /// Если класс не список, то возвращает строку, где содержится текст и статус выполнения
        /// Если класс список, то возвращает имя списко (при наличии), текст всех пунктов и статусы их выполнения
        /// </returns>
        public override string ToString()
        {
            //Результирующая строка
            string result = "";
            //Если это список
            if (ToDoList.Count > 0)
            {
                //При наличие имени списка возвращаем его
                result += ((Text.Length > 0) ? $"Имя Списка: {Text}" : "Список: ");
                //Выводим каждый пункт
                foreach (Point p in ToDoList)
                {
                    result += "\n   "+ (ToDoList.IndexOf(p) + 1).ToString() + ". " + p.ToString();
                }
            } else //Если это не список
            {
                //Вывод текста и статуса
                result += "Текст: " + Text + " | Статус: " + ((Check) ? "1" : "0");
            }
            return result;
        }
    }
}
