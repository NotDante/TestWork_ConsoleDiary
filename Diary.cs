using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Homework_07
{
    [Serializable]
    /// <summary>
    /// Класс ежедневника, хранящий множество страниц
    /// </summary>
    class Diary
    {
        //Имя пользователя ежедневника
        public string Name { get; set; }

        //Список со страницами ежедневника
        public List<Page> ListOfPages { get; set; } = new List<Page>();

        /// <summary>
        /// Конструктор класса ежедневника
        /// </summary>
        /// <param name="name">Имя ежедневника</param>
        /// <param name="pages">Страницы ежедненвника</param>
        public Diary (string name, params Page[] pages)
        {
            Name = name;
            ListOfPages = pages.ToList();
            for(int i = 0; i< ListOfPages.Count; i++)
            {
                ListOfPages[i].Num = i;
            }
        }

        /// <summary>
        /// Конструктор ежедненвника без входных параметров
        /// </summary>
        public Diary()
        {
            Name = "NO_NAME";
        }

        /// <summary>
        /// Возвращаем страницу по ее внутреннему номеру
        /// </summary>
        /// <param name="num">Номер страницы</param>
        /// <returns>Страницу с указанным номером</returns>
        public Page ReturnPage (int num)
        {
            //Проверка на верность указанного номера
            if (num > 0)
            {
                //Ищем страницу с нужным номером
                for (int i = 0; i<ListOfPages.Count; i++)
                {
                    if (ListOfPages[i].Num == num)
                    {
                        return ListOfPages[i];
                    }
                }
                //Страница не была найдена
                return null;
            } else
            {
                throw (new Exception("Wrong number! Negative Number!"));
            }

            throw (new Exception("Something go wrong! Out of ReturnPage function!"));
        }

        /// <summary>
        /// Добавление новой страницы
        /// </summary>
        /// <param name="newPage">Добавляемая страница</param>
        public void AddPage (Page newPage)
        {
            if (newPage != null)
            {
                ListOfPages.Add(newPage);
                newPage.Num = ListOfPages.IndexOf(newPage);
            }
        }

        /// <summary>
        /// Сохранение Ежедненвника
        /// </summary>
        public void SaveDiary(string path)
        {
            if (path.Equals(""))
            {
                Directory.CreateDirectory(Name);

                BinaryFormatter binForm = new BinaryFormatter();

                using (var file = new FileStream(Name + ".diary", FileMode.OpenOrCreate))
                {
                    binForm.Serialize(file, this);
                }
            } else
            {
                    BinaryFormatter binForm = new BinaryFormatter();

                    using (var file = new FileStream(Name + ".diary", FileMode.OpenOrCreate))
                    {
                        binForm.Serialize(file, this);
                    }
            }

            
        }

        /// <summary>
        /// Загрузить ежедневник из указанного пути
        /// </summary>
        /// <param name="path">Путь к файлу ежедневника</param>
        /// <returns>Ежедненвник по указанному пути</returns>
        public static Diary LoadDiary(string path)
        {
            BinaryFormatter binForm = new BinaryFormatter();

            if (path.Substring(path.Length - 6).Equals(".diary"))
            {
                try
                {
                    using (var file = new FileStream(path, FileMode.Open))
                    {
                        return (Diary)binForm.Deserialize(file);
                    }
                }
                catch (FileNotFoundException fe)
                {
                    return null;
                }
            }
            else
            {
                throw (new Exception("Неверное разрешение запрашиваемого файла"));
            }
        }

        /// <summary>
        /// Удаление страницы по индексу
        /// </summary>
        /// <param name="index">Индекс удаляемой страницы</param>
        public void DeletePage (int index)
        {
            if (index >= 0 && index < ListOfPages.Count)
            {
                ListOfPages.RemoveAt(index);
            }
        }

        /// <summary>
        /// Сортировка страниц по дате
        /// </summary>
        public void SortByDate()
        {
            ListOfPages.Sort((a, b) => a.Date.CompareTo(b.Date));
        }

        /// <summary>
        /// Сортировка страниц по номерам (дате создания)
        /// </summary>
        public void SortByNumber()
        {
            ListOfPages.Sort((a, b) => a.Num.CompareTo(b.Num));
        }
    }
}
