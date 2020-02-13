using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_07
{
    [Serializable]
    /// <summary>
    /// Класс времени, в упрощенном виде.
    /// </summary>
    public class TimeSimple
    {
        //Часы от 0 до 23
        private int hour;
        public int Hour
        {
            get { return hour; }
            set
            {
                if (value >= 0 && value <= 23)
                {
                    hour = value;
                }
            }
        }

        //Минуты от 0 до 59
        private int minute;
        public int Minute
        {
            get { return minute; }

            set
            {
                if (value >= 0 && value <= 59)
                {
                    minute = value;
                }
            }
        }

        //Сдвиг по времени относительно 00-00
        public int TimeShift { get { return Hour * 60 + Minute; } }

        //Конструктор "Просого времени"
        public TimeSimple (byte hour, byte minute)
        {
            Hour = hour;
            Minute = minute;
        }

        public override string ToString()
        {
            return $"{Hour:00}:{Minute:00}";
        }

        public static bool operator >(TimeSimple time1, TimeSimple time2)
        {
            return (time1.TimeShift > time2.TimeShift);
        }
        public static bool operator <(TimeSimple time1, TimeSimple time2)
        {
            return (time1.TimeShift < time2.TimeShift);
        }

        public static bool operator ==(TimeSimple time1, TimeSimple time2)
        {
            return (time1.TimeShift == time2.TimeShift);
        }

        public static bool operator !=(TimeSimple time1, TimeSimple time2)
        {
            return (time1.TimeShift != time2.TimeShift);
        }
    }

    [Serializable]
    /// <summary>
    /// Класс расписания. Здесь хранятся названия дел, время их начала и конца
    /// </summary>
    class Schedule
    {
        public List<Schedule> ScheduleList { get; set; } = new List<Schedule>();

        //Название дела
        public string Name { get; set; }

        //Время начала события
        public TimeSimple StartTime { get; set; }
        //Время конца события
        public TimeSimple EndTime { get; set; }

        /// <summary>
        /// Конструктор класса "Расписание"
        /// </summary>
        /// <param name="body">Название события</param>
        /// <param name="starthour">Час начала события</param>
        /// <param name="startmin">Минуты начала события</param>
        /// <param name="endhour">Часы окончания события</param>
        /// <param name="endmin">Минуты окончания события</param>
        //Конструктор расписания
        public Schedule (string body, byte starthour, byte startmin, byte endhour, byte endmin)
        {
            Name = body;
            StartTime = new TimeSimple(starthour, startmin);
            EndTime = new TimeSimple(endhour, endmin);
            //Если время проставлено в неверном порядке
            if (StartTime > EndTime)
            {
                //Меняем местами стартовое и начальное время, используя временную переменную
                TimeSimple temp = EndTime;
                EndTime = StartTime;
                StartTime = temp;
            }
        }

        /// <summary>
        /// Описание списка событий
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <returns>Элемента в списке согласно индексу</returns>
        public Schedule this[int index]
        {
            get
            {
                if (index > 0 && index < ScheduleList.Count)
                {
                    return (ScheduleList[index]);
                }
                else
                {
                    Console.WriteLine("ВЫХОД ЗА ПРДЕЛЫ МАССИВА! (Расписание)");
                    return null;
                }
            }
            set {
                if (index > 0 && index < ScheduleList.Count)
                {
                     ScheduleList[index] = value;
                }
                else
                {
                    Console.WriteLine("ВЫХОД ЗА ПРДЕЛЫ МАССИВА! (Расписание)");
                }
            }

        }

        /// <summary>
        /// Конструктор списка событий
        /// </summary>
        /// <param name="input">Список событий</param>
        public Schedule (params Schedule[] input)
        {
            foreach (Schedule s in input)
            {
                ScheduleList.Add(s);
            }
        }

        /// <summary>
        /// Функция сортировки по времени начала
        /// </summary>
        public void SortByTime ()
        {
            //Элементы сортируются по параметру TimeShift внутри StartTime
            ScheduleList.Sort((a,b) => a.StartTime.TimeShift.CompareTo(b.StartTime.TimeShift));
        }

        /// <summary>
        /// Вывод расписание в строку
        /// </summary>
        /// <returns>Строка с расписанием</returns>
        public override string ToString()
        {
            SortByTime();
            string result = "";
            if (ScheduleList.Count > 0)
            {
                result += "РАСПИСАНИЕ: \n";
                foreach (Schedule s in ScheduleList)
                {
                    result += s.ToString() + "\n";
                }
            } else
            {
                result += StartTime.ToString() + " - " + EndTime.ToString() + "\n   "
                    + Name + "\n";
            }
            return result;
        }

        /// <summary>
        /// Сложение расписний
        /// </summary>
        /// <param name="schedule1">Оснавное расписание</param>
        /// <param name="scheduleAdded">Присоединяемое расписание</param>
        /// <returns>Основное расписание с присоединенным</returns>
        public static Schedule AddSchedule (Schedule schedule1, Schedule scheduleAdded)
        {
            if (schedule1.ScheduleList.Count == 0)
            {
                return schedule1 = new Schedule(schedule1, scheduleAdded);
            } else
            {
                schedule1.ScheduleList.Add(scheduleAdded);
                return schedule1;
            }
        }
    }
}
