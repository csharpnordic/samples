using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleThreads;

static class Program
{
    /// <summary>
    /// Список потоков
    /// </summary>
    internal static readonly List<Worker> list = [];

    static int count = 8;

    /// <summary>
    /// Главный метод
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args)
    {
        try
        {
            for (int i = 0; i < count; i++)
            {
                // Создать объект и поместить в список
                list.Add(new Worker());
            }

            // Если удалить эту строку, то не будет реализован
            // синхронный запуск всех потоков одновременно
            // (такое же изменение надо сделать в классе Worker)
            lock (Worker.locker)
            {
                // Запуск потоков
                foreach (Worker w in list)
                {
                    w.thread.Start();
                }
            }
            // Обработчик Ctrl+C
            Console.CancelKeyPress += Console_CancelKeyPress;

            // Ожидание завершения вычисления
            foreach (Worker w in list)
            {
                w.thread.Join();
            }

            // Вывод результата
            Console.WriteLine();
            foreach (Worker w in list)
            {
                Console.WriteLine(w);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Нажатие Ctrl+C
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
    {
        // Запрос на завершение потока
        list[--count].cts.Cancel();
        // Отмена завершения приложения
        e.Cancel = true;
    }
}
