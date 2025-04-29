namespace Exceptions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Домашняя работа. \n");
            int taskNumber = 0;
            while (taskNumber != 1 && taskNumber != 2)
            {
                Console.Write("Выберите задачу (1 или 2): ");
                taskNumber = Convert.ToInt32(Console.ReadLine());
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
                    foreach (Exception ex in exceptions)
                    {
                        try
                        {
                            throw ex;
                        }
                        catch
                        {
                            Console.WriteLine($"Ошибка {count} из 5");
                            Console.WriteLine(ex.Message + "\n");
                        }
                        count++;
                    }
                }
                if (taskNumber == 2)
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("\nЗадача 2.");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else
                {
                    Console.WriteLine("\nВы не ввели 1 или 2\n");
                }
            }
            

        }
             class CustomException : Exception
        {
            public CustomException() : base("Кастомная ошибка") { }
        }
    }

}
