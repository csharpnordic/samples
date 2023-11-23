using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarSky
{
    internal class Star
    {
        /// <summary>
        /// Генерация случайных чисел
        /// </summary>
        private static Random random = new();

        /// <summary>
        /// Объект для межпоточной блокировки
        /// </summary>
        private static object locker = new();

        /// <summary>
        /// Счётчик активных звёзд
        /// </summary>
        internal static int Count { get; private set; } = 0;

        string chars = ".*X+*. ";
        int x;
        int y;
        ConsoleColor color;
        int index;
        Thread thread;

        internal Star()
        {
            x = random.Next(Console.WindowWidth);
            y = random.Next(Console.WindowHeight);
            color = (ConsoleColor)(1 + random.Next(14)); // все цвета, кроме чёрного
            index = 0;
            thread = new Thread(Show);
        }

        private void ShowChar(char c)
        {
            lock (locker)
            {
                Console.ForegroundColor = color;
                Console.CursorLeft = x;
                Console.CursorTop = y;
                Console.Write(c);
            }
        }

        private void ShowStage()
        {
            if (index < chars.Length)
            {
                ShowChar(chars[index++]);                
            }
        }

        /// <summary>
        /// Жизненный цикл звезды
        /// </summary>
        internal void Show()
        {
            try
            {
                while (index < chars.Length)
                {
                    ShowStage();
                    Thread.Sleep(200);
                    // надо зажечь новую звезду
                    Program.Queue.Enqueue(1);
                }
                // звезда потухла
                Count--;
            }
            catch (ThreadInterruptedException)
            {
                // при принудительном прерывании потока мы очищаем небо за собой
                ShowChar(' ');
            }
        }

        /// <summary>
        /// Запуск отдельного потока
        /// </summary>
        internal void Start()
        {
            thread.Start();
            Count++;
        }

        /// <summary>
        /// Принудительное завершение потока
        /// </summary>
        internal void Stop()
        {
            thread.Interrupt();
            // при принудительном прерывании потока мы очищаем небо за собой
            ShowChar(' ');
        }

        /// <summary>
        /// Ожидание завершения потока
        /// </summary>
        internal void Join()
        {
            thread.Join();
        }
    }
}
