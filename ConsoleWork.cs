using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Homework_07
{
    class ConsoleWork
    {
        /// <summary>
        /// Работа с ежедневником через консоль
        /// </summary>
        public ConsoleWork ()
        {
            Console.Clear();
            Diary CurrentDiary = null; //Ежедневник с которым ведется работа

            while (true)
            {
                bool pass = true;

                //Если ни один ежедневник не открыт
                if (CurrentDiary == null)
                {
                    switch (StartMenu())
                    {
                        case 1: //Создание нового ежедника
                            Console.Clear();
                            CurrentDiary = new Diary();
                            Console.Write("Введите имя нового ежедневника: ");
                            CurrentDiary.Name = Console.ReadLine();
                            if (CurrentDiary.Name.Replace(" ", "").Equals(""))
                            {
                                CurrentDiary.Name = "NO_NAME";
                            }
                            Console.Clear();
                            switch (CreateDiaryMenu())
                            {
                                case 1: //Создание новой страницы
                                    CurrentDiary.AddPage(CreatePage());
                                    Console.Clear();
                                    break;
                                case 2: //Загрузка существующей страницы
                                    CurrentDiary.AddPage(LoadPage());
                                    Console.Clear();
                                    break;
                                case 0: //Возврат в главное меню
                                    CurrentDiary = null;
                                    Console.Clear();
                                    break;
                            }
                            break;
                        case 2: //Загрузка существующего ежедневника
                            CurrentDiary = LoadDiary();
                            Console.Clear();
                            break;
                        case 3: //TEST
                            Console.Clear();
                            break;
                        case 0: //Выход
                            pass = false;
                            break;
                        default: //Ничего
                            Console.Clear();
                            break;
                    }
                } else
                {
                    switch (DiaryMenu(CurrentDiary))
                    {
                        case 0: //Назад в главное меню
                            while (true)
                            {
                                Console.Clear();
                                Console.Write("Хотите сохранить данный перед выходом? Все несохраненные данные будут потеряны (Y/N): ");
                                ConsoleKeyInfo key = Console.ReadKey();
                                if (key.Key == ConsoleKey.N)
                                {
                                    break;
                                } else if (key.Key == ConsoleKey.Y)
                                {
                                    SaveDiary(CurrentDiary);
                                    break;
                                }

                                Console.Clear();
                            }
                            CurrentDiary = null;
                            break;
                        default:
                            //DiaryMenu(CurrentDiary);
                            break;

                    }
                }

                if (!pass)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Главное меню
        /// </summary>
        /// <returns>Код следующего шага</returns>
        public int StartMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ГЛАВНОЕ МЕНЮ");
            Console.ResetColor();

            Console.WriteLine("\n1. Создать ежедневник" +
                "\n2. Загрузить ежедневник" +
                //"\n3. TEST" +
                "\n0. Выход");

            ConsoleKey key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.D1:
                    return 1;
                case ConsoleKey.NumPad1:
                    return 1;
                case ConsoleKey.D2:
                    return 2;
                case ConsoleKey.NumPad2:
                    return 2;
                //case ConsoleKey.D3:
                //    return 3;
                //case ConsoleKey.NumPad3:
                //    return 3;
                case ConsoleKey.D0:
                    return 0;
                case ConsoleKey.NumPad0:
                    return 0;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Меню создания нового ежедневника
        /// </summary>
        /// <returns>Код следующего шага</returns>
        public int CreateDiaryMenu()
        {
            Console.Clear();
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("СОЗДАНИЕ НОВОГО ЕЖЕДНЕВНИКА");
                Console.ResetColor();

                Console.WriteLine("\n1. Создать страницу" +
                    "\n2. Загразить страницу" +
                    "\n0. Назад");

                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                        return 1;
                    case ConsoleKey.NumPad1:
                        return 1;
                    case ConsoleKey.D2:
                        return 2;
                    case ConsoleKey.NumPad2:
                        return 2;
                    case ConsoleKey.D0:
                        return 0;
                    case ConsoleKey.NumPad0:
                        return 0;
                    default:
                        break;
                }

                Console.Clear();
            }
        }

        /// <summary>
        /// Основное меню Ежедневника
        /// </summary>
        /// <param name="diary">Текущий ежедневник</param>
        /// <returns>Код возврата</returns>
        public int DiaryMenu( Diary diary)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("МЕНЮ ЕЖЕДНЕВНИКА \"{0}\"", diary.Name);
            Console.ResetColor();

            Console.WriteLine("\n1. Просмотр страниц" +
                "\n2. Сохранить ежедневник" +
                "\n3. Изменить имя ежедневника" +
                "\n4. Сортировка страница по датам указанным на них" +
                "\n5. Сортировка страниц по номерам (датам создания)" +
                "\n0. В главное меню");

            ConsoleKey key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.D1: //Просмотр страниц
                case ConsoleKey.NumPad1:
                    PageOverviewMenu(diary, 0);
                    return 1;

                case ConsoleKey.D2: //Сохранить ежедневник
                case ConsoleKey.NumPad2:
                    SaveDiary(diary);
                    DiaryMenu(diary);
                    return 2;

                case ConsoleKey.D3: //Изменить название ежедневника
                case ConsoleKey.NumPad3:
                    Console.Clear();
                    Console.Write("Введите новое название ежедневника");
                    diary.Name = Console.ReadLine();
                    diary.Name = diary.Name.Replace(" ", "").Equals("") ? "NO_NAME" : diary.Name;
                    DiaryMenu(diary);
                    return 3;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    diary.SortByDate();
                    return 4;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    diary.SortByNumber();
                    return 5;
                case ConsoleKey.D0: //Назад в главное меню
                case ConsoleKey.NumPad0:
                    return 0;
                default:
                    return -1;
            }
        }

        /// <summary>
        /// Просмотр страниц
        /// </summary>
        /// <param name="diary">Просматриваемый ежедневник</param>
        /// <param name="pageIndex">Индекс страницы</param>
        /// <returns>Код возврата</returns>
        public int PageOverviewMenu(Diary diary, int pageIndex)
        {
            Console.Clear();
            if (pageIndex < 0) pageIndex = 0;
            if (pageIndex >= diary.ListOfPages.Count) pageIndex = diary.ListOfPages.Count - 1;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("ПРОСМОТР СТРАНИЦ");
            Console.ResetColor();

            if (diary.ListOfPages.Count != 0)
            {
                ShowPage(diary.ListOfPages[pageIndex]);
                if (pageIndex != 0)
                {
                    Console.WriteLine("<Стрелка влево> Для движение влево по страницам");
                }
                if (pageIndex != diary.ListOfPages.Count - 1)
                {
                    Console.WriteLine("<Стрелка вправо> Для движения вправо по страницам");
                }
                
            }else
            {
                Console.WriteLine("\nЕжедненик пуст, добвьте страницы");
            }

            Console.WriteLine("\n1. Добавить страницу" +
                "\n2. Удалить страницу" +
                "\n3. Сохранить страницу как отдельный файл" +
                "\n4. Загрузить страницу из файла" +
                "\n5. Меню списка дел" +
                "\n6. Меню расписания" +
                "\n0. НАЗАД");
            Console.WriteLine();
            ConsoleKey key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    diary.AddPage(CreatePage());
                    PageOverviewMenu(diary, diary.ListOfPages.Count - 1);
                    return 1;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    diary.DeletePage(pageIndex);
                    PageOverviewMenu(diary, pageIndex);
                    return 2;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    SavePage(diary.ListOfPages[pageIndex]);
                    PageOverviewMenu(diary, pageIndex);
                    return 3;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    diary.AddPage(LoadPage());
                    PageOverviewMenu(diary, diary.ListOfPages.Count - 1);
                    return 4;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    ToDolistMenu(diary.ListOfPages[pageIndex]);
                    PageOverviewMenu(diary, pageIndex);
                    return 5;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    SchedualMenu(diary.ListOfPages[pageIndex]);
                    PageOverviewMenu(diary, pageIndex);
                    return 6;
                case ConsoleKey.LeftArrow:
                    PageOverviewMenu(diary, pageIndex - 1);
                    return 44;
                case ConsoleKey.RightArrow:
                    PageOverviewMenu(diary, pageIndex + 1);
                    return 66;
                case ConsoleKey.D0:
                    return 0;
                case ConsoleKey.NumPad0:
                    return 0;
                default:
                    PageOverviewMenu(diary, pageIndex);
                    return -1;
            }
        }

        /// <summary>
        /// Показать одну станицу
        /// </summary>
        /// <param name="page">показываемая страница</param>
        public void ShowPage(Page page)
        {
            Console.WriteLine(page.ToString());
        }

        
        /// <summary>
            /// Загрузка ежедневника из файла
            /// </summary>
            /// <returns>Ежедненвник, загруженный из файла</returns>
        public Diary LoadDiary()
        {
            Console.Clear();
            while (true)
            {
                Console.Write("Введите путь загружаемого файла или оставьте пустым для отмены: ");
                string path = Console.ReadLine();
                if (!path.Equals("")){
                    try
                    {
                        return Diary.LoadDiary(path);
                    } catch (Exception e)
                    {
                        Console.WriteLine(e.Message + " Повторите ввод!");
                    }
                } else
                {
                    return null;
                }
            }
        }

        /// <summary>
            /// Создание новой страницы ежедневника
            /// </summary>
            /// <returns>Новая страница ежедневника</returns>
        public Page CreatePage()
        {
            Console.Clear();

            DateTime pageDate;
            List<string> dayNames = new List<string>();
            Point point = null;
            Schedule schedule = null;
            

            while (true)
            {
                Console.Write("Введите дату в формате dd.mm.yyyy или оставьте поле пустым, для автоматического ввода текущей даты: ");
                string date = Console.ReadLine();
                if (date.Replace(" ", "").Equals(""))
                {
                    pageDate = DateTime.Now;
                    break;
                }
                else
                {
                    string[] dateArr = date.Split('.');
                    try
                    {
                        if (dateArr.Length == 3)
                        {
                            pageDate = new DateTime(int.Parse(dateArr[2]), int.Parse(dateArr[1]), int.Parse(dateArr[0]));
                            break;
                        } else
                        {
                            throw (new Exception("Введено неверное количество аргументов"));
                        }
                    } catch (Exception e)
                    {
                        Console.WriteLine(e.Message + "\nНажмите любую клавишу для повторного ввода");
                        Console.ReadKey();
                        Console.Clear();
                    }
                }
            }

            Console.Clear();

            Console.WriteLine($"Введите название для страницы датированную {pageDate.Day:00}.{pageDate.Month:00}.{pageDate.Year:0000} (возможно наименование праздника). " +
                $"После ввода одного из наименований нажмите Enter и продолжите ввод следующего наименования." +
                "Если наимнование не требуется, или ввод завершен, нажмите Enter при пустой строке: ");
            while (true)
            {
                string temp = Console.ReadLine();
                if (temp.Replace(" ", "").Equals(""))
                {
                    break;
                } else
                {
                    dayNames.Add(temp);
                }
            }

            Console.Clear();

            Console.WriteLine($"Введите список дел на {pageDate.Day:00}.{pageDate.Month:00}.{pageDate.Year:0000} (Возможно список покупок или напоминания). " +
                $"После ввода одного из пунктов нажмите Enter и продолжайте ввод следующего." +
                "Если список дел не требуется, или ввод завершен, нажмите Enter при пустой строке: ");
            while (true)
            {
                string temp = Console.ReadLine();
                if (temp.Replace(" ", "").Equals(""))
                {
                    break;
                }
                else
                {
                    if (point == null)
                    {
                        point = new Point(temp);
                    } else
                    {
                        point.AddPoint(temp);
                    }
                }
            }

            Console.Clear();

            Console.WriteLine($"Введите расписание на {pageDate.Day:00}.{pageDate.Month:00}.{pageDate.Year:0000} (Сначала вводится наименование мероприятия, потом время начала и время конца). " +
                $"После ввода одного из пунктов нажмите Enter и продолжайте ввод следующего." +
                "Если расписание не требуется, или ввод завершен, нажмите Enter при пустой строке: ");
            while (true)
            {
                Console.Write("Введите наименование мероприятия: ");
                string temp = Console.ReadLine();
                if (temp.Replace(" ", "").Equals(""))
                {
                    break;
                } else
                {
                    byte H1, M1, H2, M2;
                    while (true) {
                        
                        Console.Write("Введите время начала в формате hh:mm : ");
                        string tempH1 = Console.ReadLine();
                        string[] arrT1 = tempH1.Split(':');
                        if (arrT1.Length != 2)
                        {
                            Console.WriteLine("Неверное количество аргументов, повторите ввод!");
                        } else
                        {
                            try
                            {
                                H1 = byte.Parse(arrT1[0]);
                                M1 = byte.Parse(arrT1[1]);
                                break;

                            } catch (Exception e)
                            {
                                Console.WriteLine(e.Message + "Повторите ввод!");
                            }
                        }
                    }

                    while (true)
                    {
                        Console.Write("Введите время окончания в формате hh:mm : ");
                        string tempH1 = Console.ReadLine();
                        string[] arrT1 = tempH1.Split(':');
                        if (arrT1.Length != 2)
                        {
                            Console.WriteLine("Неверное количество аргументов, повторите ввод!");
                        }
                        else
                        {
                            try
                            {
                                H2 = byte.Parse(arrT1[0]);
                                M2 = byte.Parse(arrT1[1]);
                                break;

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message + "Повторите ввод!");
                            }
                        }
                    }

                    if (schedule == null)
                    {
                        schedule = new Schedule(temp, H1, M1, H2, M2);
                    } else
                    {
                        schedule = Schedule.AddSchedule(schedule, new Schedule(temp, H1, M1, H2, M2));
                    }
                }
            }

            return new Page(pageDate, point, schedule, dayNames.ToArray());
        }

        /// <summary>
        /// Загрузка страницы из файла
        /// </summary>
        /// <returns>Загруженная страница из указанного пути</returns>
        public Page LoadPage()
        {
            Console.Clear();
            while (true)
            {
                Console.Write("Введите путь к загружаемой странице или оставьте пустым для отмены: ");
                string path = Console.ReadLine();
                if (!path.Equals(""))
                {
                    try
                    {
                        return Page.LoadPage(path);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message + "Повторите ввод!");
                    }
                } else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Сохранение Днекника как отдельного файла МЕНЮ
        /// </summary>
        /// <param name="diary">Сохраняемый ежедневник</param>
        public void SaveDiary(Diary diary)
        {
            Console.Clear();
            Console.Write("Введите путь сохранения или оставьте пустую строку для сохранения по-умолчанию: ");
            string path = Console.ReadLine();
            diary.SaveDiary(path);
        }

        /// <summary>
        /// Сохраенение страницы как отдельного файла МЕНЮ
        /// </summary>
        /// <param name="page">Сохраняемая страница</param>
        public void SavePage(Page page)
        {
            Console.Clear();
            Console.Write("Введите путь сохранения или оставьте пустую строку для сохранения по-умолчанию: ");
            string path = Console.ReadLine();
            page.SavePage(path);
        }

        /// <summary>
        /// Вывод списка дел
        /// </summary>
        /// <param name="page">Страница список дел которой выводят</param>
        public void ToDolistMenu(Page page)
        {
            Console.Clear();
            if (page.ToDoList != null)
            {
                Console.WriteLine(page.ToString());
                Console.WriteLine("\n1. Отметить сделанным пункт списка" +
                    "\n2. Добавить новый пункт списка" +
                    "\n0. Назад");

                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        while (true)
                        {
                            Console.Write("Введите номер пункта, который необходимо отметить как выполненный (Максимальное значение {0})", page.ToDoList.ToDoList.Count == 0? 1 : page.ToDoList.ToDoList.Count);
                            try
                            {
                                int tryNum = int.Parse(Console.ReadLine());
                                if (tryNum >0 && tryNum <= (page.ToDoList.ToDoList.Count == 0? 1 : page.ToDoList.ToDoList.Count))
                                {
                                    if (page.ToDoList.ToDoList.Count > 0)
                                    {
                                        page.ToDoList[tryNum - 1].Checked();
                                    } else
                                    {
                                        page.ToDoList.Checked();
                                    }
                                    //ToDolistMenu(page);
                                    break;
                                } else
                                {
                                    Console.WriteLine("Некоректно введено значение. Повторите ввод!");
                                }
                            }catch (Exception e)
                            {
                                Console.WriteLine(e.Message + " Повторите ввод!");
                            }
                        }
                        ToDolistMenu(page);
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        Console.WriteLine($"Введите новые пункты списка дел (Возможно список покупок или напоминания). " +
                            "После ввода одного из пунктов нажмите Enter и продолжайте ввод следующего." +
                            "Если список дел не требуется, или ввод завершен, нажмите Enter при пустой строке: ");
                        while (true)
                        {
                            string temp = Console.ReadLine();
                            if (temp.Replace(" ", "").Equals(""))
                            {
                                break;
                            }
                            else
                            {
                                page.ToDoList.AddPoint(temp);
                            }
                        }
                        ToDolistMenu(page);
                        break;
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        break;
                    default:
                        ToDolistMenu(page);
                        break;

                }
            } else
            {
                Console.WriteLine("Список дел пуст\n" +
                    "\n1. Создать новый список дел" +
                    "\n0. Назад");

                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine($"Введите список дел на {page.Date.Day,00}.{page.Date.Month,00}.{page.Date.Year,0000} (Возможно список покупок или напоминания). " +
                            "После ввода одного из пунктов нажмите Enter и продолжайте ввод следующего." +
                            "Если список дел не требуется, или ввод завершен, нажмите Enter при пустой строке: ");
                        while (true)
                        {
                            string temp = Console.ReadLine();
                            if (temp.Replace(" ", "").Equals(""))
                            {
                                break;
                            }
                            else
                            {
                                if (page.ToDoList == null)
                                {
                                    page.ToDoList = new Point(temp);
                                }
                                else
                                {
                                    page.ToDoList.AddPoint(temp);
                                }
                            }
                        }
                        break;
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        break;
                    default:
                        ToDolistMenu(page);
                        break;
                }
            }
        }

        /// <summary>
        /// Меню расписания
        /// </summary>
        /// <param name="page">Страница с выводимым расписанием</param>
        public void SchedualMenu (Page page)
        {
            Console.Clear();

            if (page.Schedule != null)
            {
                Console.WriteLine(page.Schedule.ToString());
                Console.WriteLine("\n1. Добавить пункт к расписанию" +
                    "\n0. Назад");
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        Console.WriteLine($"Введите новый пункт расписания на {page.Date.Day:00}.{page.Date.Month:00}.{page.Date.Year:0000} " +
                            $"(Сначала вводится наименование мероприятия, потом время начала и время конца). " +
                            "Если новый пункт не требуется нажмите Enter при пустой строке названия пункта: ");

                        Console.Write("Введите наименование мероприятия: ");
                        string temp = Console.ReadLine();
                        if (temp.Replace(" ", "").Equals(""))
                        {
                            SchedualMenu(page);
                            break;
                        }
                        else
                        {
                            byte H1, M1, H2, M2;
                            while (true)
                            {

                                Console.Write("Введите время начала в формате hh:mm : ");
                                string tempH1 = Console.ReadLine();
                                string[] arrT1 = tempH1.Split(':');
                                if (arrT1.Length != 2)
                                {
                                    Console.WriteLine("Неверное количество аргументов, повторите ввод!");
                                }
                                else
                                {
                                    try
                                    {
                                        H1 = byte.Parse(arrT1[0]);
                                        M1 = byte.Parse(arrT1[1]);
                                        break;

                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message + "Повторите ввод!");
                                    }
                                }
                            }

                            while (true)
                            {
                                Console.Write("Введите время окончания в формате hh:mm : ");
                                string tempH1 = Console.ReadLine();
                                string[] arrT1 = tempH1.Split(':');
                                if (arrT1.Length != 2)
                                {
                                    Console.WriteLine("Неверное количество аргументов, повторите ввод!");
                                }
                                else
                                {
                                    try
                                    {
                                        H2 = byte.Parse(arrT1[0]);
                                        M2 = byte.Parse(arrT1[1]);
                                        break;

                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine(e.Message + "Повторите ввод!");
                                    }
                                }
                            }

                            if (page.Schedule == null)
                            {
                                page.Schedule = new Schedule(temp, H1, M1, H2, M2);
                            }
                            else
                            {
                                page.Schedule = Schedule.AddSchedule(page.Schedule, new Schedule(temp, H1, M1, H2, M2));
                            }
                        }
                        SchedualMenu(page);
                        break;
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        break;
                    default:
                        ToDolistMenu(page);
                        break;
                }

            } else
            {
                Console.WriteLine("Расписание пустое\n" +
                    "\n1. Создать новое расписание" +
                    "\n2. НАЗАД");

                ConsoleKey key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.WriteLine($"Введите расписание на {page.Date.Day:00}.{page.Date.Month:00}.{page.Date.Year:0000} (Сначала вводится наименование мероприятия, потом время начала и время конца). " +
                            "После ввода одного из пунктов нажмите Enter и продолжайте ввод следующего." +
                            "Если расписание не требуется, или ввод завершен, нажмите Enter при пустой строке: ");
                        while (true)
                        {
                            Console.Write("Введите наименование мероприятия: ");
                            string temp = Console.ReadLine();
                            if (temp.Replace(" ", "").Equals(""))
                            {
                                break;
                            }
                            else
                            {
                                byte H1, M1, H2, M2;
                                while (true)
                                {

                                    Console.Write("Введите время начала в формате hh:mm : ");
                                    string tempH1 = Console.ReadLine();
                                    string[] arrT1 = tempH1.Split(':');
                                    if (arrT1.Length != 2)
                                    {
                                        Console.WriteLine("Неверное количество аргументов, повторите ввод!");
                                    }
                                    else
                                    {
                                        try
                                        {
                                            H1 = byte.Parse(arrT1[0]);
                                            M1 = byte.Parse(arrT1[1]);
                                            break;

                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message + "Повторите ввод!");
                                        }
                                    }
                                }

                                while (true)
                                {
                                    Console.Write("Введите время окончания в формате hh:mm : ");
                                    string tempH1 = Console.ReadLine();
                                    string[] arrT1 = tempH1.Split(':');
                                    if (arrT1.Length != 2)
                                    {
                                        Console.WriteLine("Неверное количество аргументов, повторите ввод!");
                                    }
                                    else
                                    {
                                        try
                                        {
                                            H2 = byte.Parse(arrT1[0]);
                                            M2 = byte.Parse(arrT1[1]);
                                            break;

                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message + "Повторите ввод!");
                                        }
                                    }
                                }

                                if (page.Schedule == null)
                                {
                                    page.Schedule = new Schedule(temp, H1, M1, H2, M2);
                                }
                                else
                                {
                                    page.Schedule = Schedule.AddSchedule(page.Schedule, new Schedule(temp, H1, M1, H2, M2));
                                }
                            }
                        }
                        break;
                    case ConsoleKey.D0:
                    case ConsoleKey.NumPad0:
                        break;
                    default:
                        ToDolistMenu(page);
                        break;
                }
            }
        }
    }
}
