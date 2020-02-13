using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework_07
{
    class Program
    {
        static void Main(string[] args)
        {
            /// Разработать ежедневник.
            /// В ежедневнике реализовать возможность 
            /// - создания
            /// - удаления
            /// - реактирования 
            /// записей
            /// 
            /// В отдельной записи должно быть не менее пяти полей
            /// 
            /// Реализовать возможность 
            /// - Загрузки даннах из файла
            /// - Выгрузки даннах в файл
            /// - Добавления данных в текущий ежедневник из выбранного файла
            /// - Импорт записей по выбранному диапазону дат
            /// - Упорядочивания записей ежедневника по выбранному полю

            ConsoleWork program = new ConsoleWork();

            //DateTime date = new DateTime(2020, 1, 16);
            //Point point = new Point("Купить молока", "Нарисовать корову");
            //Schedule schedule = new Schedule(
            //    new Schedule("Что-то нужное", 06, 00, 12, 00),
            //    new Schedule("Что-то ненужное", 10, 00, 20, 00)
            //    );

            //Page p = new Page(date, point,schedule, "День рождения друга", "Праздник цветов");
            //p.SavePage("test");

            //Page np = Page.LoadPage("test/11612020.page");

            //Console.ReadKey();
        }
    }
}
