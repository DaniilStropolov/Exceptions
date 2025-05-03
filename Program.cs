using System.Runtime.InteropServices;

namespace Exceptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Домашняя работа. \n");

            //Выбор задания
            int taskNumber = 0;
            while (taskNumber != 1 && taskNumber != 2)
            {
                //Проверка на две ошибки:
                //1) собственная ошибка InvalidChoiceException для проверки на ввод только двух цифр: 1 или 2
                //2) FormatException проверка на неверный формат (вводится должны только числа из-за Convert.ToInt32(...))
                try
                {
                    Console.Write("Выберите задачу (1 или 2): ");
                    taskNumber = Convert.ToInt32(Console.ReadLine());
                    if (taskNumber != 1 && taskNumber != 2)
                    {
                        throw new InvalidChoiceException("введено неверное значение. Нужно ввести 1 или 2.\n");
                    }
                }
                catch (InvalidChoiceException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: введено не число.\n");
                }

                //Если пользователь выбрал первое задание
                if (taskNumber == 1) 
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("\nЗадача 1.");
                    Console.BackgroundColor = ConsoleColor.Black;

                    Exception[] exceptions = new Exception[5];
                    exceptions[0] = new ArithmeticException("Арифметическая ошибка");
                    exceptions[1] = new IndexOutOfRangeException("Ошибка индекса, выходящего за пределы массива");
                    exceptions[2] = new FormatException("Ошибка формата");
                    exceptions[3] = new OutOfMemoryException("Ошибка нехватки памяти");
                    exceptions[4] = new CustomException();

                    int count = 1;
                    foreach (Exception ex in exceptions) //Прогон каждого исключения
                    {
                        try
                        {
                            throw ex;
                        }
                        catch
                        {
                            Console.WriteLine($"Ошибка {count} из 5:");
                            Console.WriteLine(ex.Message + "\n");
                        }
                        count++;
                    }
                }

                //Если пользователь выбрал второе задание
                else if (taskNumber == 2)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nЗадача 2.");
                    Console.BackgroundColor = ConsoleColor.Black;

                    string[] surnames = { "Миллер", "Уилльямс", "Андерсен", "Альварез", "Легасов" }; 
                    Sorter sorter = new Sorter();

                    int sortChoice = 0;
                    bool validInput = false;
                    while (!validInput)
                    {
                        try
                        {
                            Console.Write("Введите 1 для сортировки в алфавитном порядке или 2 для сортировки в обратном алфавитном порядке: ");
                            sortChoice = Convert.ToInt32(Console.ReadLine());
                            if (sortChoice != 1 && sortChoice != 2)
                            {
                                throw new InvalidChoiceException("введено неверное значение. Нужно ввести 1 или 2.\n");
                            }
                            validInput = true;
                        }
                        catch (InvalidChoiceException ex)
                        {
                            Console.WriteLine($"Ошибка: {ex.Message}");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Ошибка: введено не число.\n");
                        }
                    }
                    //Подписка на событие в зависимости от выбора пользователя
                    if (sortChoice == 1)
                    {
                        sorter.SortPerformed += sorter.SortArray;
                    }
                    else if (sortChoice == 2)
                    {
                        sorter.SortPerformed += sorter.ReverseArray;
                    }
                    else
                    {
                        Console.WriteLine("Некорректный выбор.");
                        return;
                    }

                    // Вызов события
                    sorter.PerformSort(surnames);
                }
            }
        }

        //Класс для всех действий сортировок, в том числе создания с
        class Sorter
        {
            //Делегат, определяющий сигнатуру метода сортировки
            public delegate void SortPerformedDelegate(string[] array);
            // Событие, которое будет вызываться при сортировке
            public event SortPerformedDelegate SortPerformed;

            //Метод, вызывающий событие SortPerformed, если есть подписчики
            public void PerformSort(string[] array)
            {
                SortPerformed?.Invoke(array);
            }

            public void SortArray(string[] arr)
            {
                Array.Sort(arr);
                Console.WriteLine("\nОтсортированный в алфавитном порядке массив:");
                foreach (var name in arr)
                {
                    Console.WriteLine(name);
                }
            }

            public void ReverseArray(string[] arr)
            {
                Array.Sort(arr);
                Array.Reverse(arr);
                Console.WriteLine("\nОтсортированный в обратном алфавитном порядке массив:");
                foreach (var name in arr)
                {
                    Console.WriteLine(name);
                }
            }
        }

        class CustomException : Exception
        {
            public CustomException() : base("Кастомная ошибка") { }
        }
        class InvalidChoiceException : Exception
        {
            public InvalidChoiceException(string message) : base(message) { }
        }
    }
}
