using System;
using System.IO;

namespace PracticalWork_6
{
    internal class Program
    {
        static void Main()
        {
            MenuSelection();
        }

        /// <summary>
        /// Управление выбором в меню
        /// </summary>
        static void MenuSelection()
        {
            bool menuActive = true;
            string path = "Workers.txt";

            while (menuActive)
            {
                FileInfo fileInfo = new FileInfo(path);

                Console.WriteLine("\nУправление : " +
                                  "\n1 - Показать всех сотрудников" +
                                  "\n2 - Добавить нового сотрудника" +
                                  "\n0 - Выход");

                string input = Console.ReadLine().Trim();
                int inputSelection = 0;

                if(!int.TryParse(input, out inputSelection) )
                {
                    inputSelection = 3;
                }

                switch (inputSelection)
                {
                    case 1:

                        if(!fileInfo.Exists)
                        {
                            FileStream fileStream = new FileStream(path, FileMode.Create);
                            fileStream.Close();
                        }

                        OutputInformations(path);
                        break;
                    case 2:
                        NewWorker(path);
                        break;
                    case 0:
                        menuActive = false;
                        break;
                    default:
                        Console.WriteLine("Некоректный ввод");
                        break;
                }
            }
        }

        /// <summary>
        /// Добавление нового сотрудника
        /// </summary>
        /// <param name="path"> Путь к файлу </param>
        static void NewWorker(string path)
        {
            string[] dataArray = ArrayFilling();
            string data = string.Join("#", dataArray);

            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(data);
            }
        }

        /// <summary>
        /// Создание массива данных сотрудника
        /// </summary>
        /// <returns></returns>
        static string[] ArrayFilling()
        {
            string [] dataArray = new string[7];

            for (int i = 0; i < dataArray.Length; i++)
            {
                if (i != 1)
                {
                    dataArray[i] = InputData(i);
                }
                else
                {
                    dataArray[i] = DateTime.Now.ToString();
                }
            }

            return dataArray;
        }

        /// <summary>
        /// Ввод данных пользователем
        /// </summary>
        /// <param name="ID"> Номер заполняемой строки </param>
        /// <returns></returns>
        static string InputData(int ID)
        {
            string title = VariableName(ID);

            Console.WriteLine($"Введите {title}: ");
            string inputText = Console.ReadLine().Trim();
            while(true)
            {
                if (inputText != "") 
                {
                    return inputText;
                }
                else 
                {
                    inputText = "Данных нет";
                    return inputText;
                }
            }
        }

        /// <summary>
        /// Обработка номера строки
        /// </summary>
        /// <param name="ID"></param>
        /// <returns> Текстовое обозначение строки </returns>
        static string VariableName(int ID)
        {
            string title = "";
            switch (ID)
            {
                case 0:
                    title = "ID:";
                    break;
                case 1:
                    title = "Дата добавления:";
                    break;
                case 2:
                    title = "Ф.И.О.:";
                    break;
                case 3:
                    title = "Возраст:";
                    break;
                case 4:
                    title = "Рост:";
                    break;
                case 5:
                    title = "Дата рождения:";
                    break;
                case 6:
                    title = "Место рождения:";
                    break;
            }
            return title;
        }

        /// <summary>
        /// Вывод информации о сотрудниках
        /// </summary>
        /// <param name="path"> Путь к файлу </param>
        static void OutputInformations(string path)
        {
            using(StreamReader sr = new StreamReader(path))
            {
                string line;
                Console.WriteLine("Сотрудники:");

                while ((line = sr.ReadLine()) != null)
                {
                    string[] dataArray = line.Split('#');

                    Console.WriteLine();

                    for (int i = 0; i < dataArray.Length; i++)
                    {
                        string title = VariableName(i);
                        Console.WriteLine($"{title} {dataArray[i]}");
                    }
                }

            }
        }
    }
}
