namespace StarSky
{
    internal class Program
    {
        /// <summary>
        /// Потокобезопасная очередь
        /// </summary>
        static internal System.Collections.Concurrent.ConcurrentQueue<int> Queue = new();

        static void Main(string[] args)
        {
            try
            {
                Console.Clear();
                Console.CursorVisible = false;
                List<Star> stars = new(); // список всех звёзд          

                // запуск первой звезды
                var star = new Star();
                stars.Add(star);
                star.Start();

                // Пока не нажата кнопка
                while (!Console.KeyAvailable)
                {
                    if (Queue.TryDequeue(out int n))
                    {
                        if (n == 1) // зажечь новую звезду
                        {
                            if (Star.Count < 20)
                            {
                                star = new Star();
                                stars.Add(star);
                                star.Start();
                            }
                        }
                    }
                }

                // Esc - принудительное досрочное завершение
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                {
                    // Принудительная остановка всех запущенных потоков
                    foreach (var s in stars)
                    {
                        s.Stop();
                    }
                }

                // подождать штатного завершения всех запущенных потоков
                foreach (var s in stars)
                {
                    s.Join();
                }

                Console.ResetColor();
            }
            finally
            {
                Console.CursorVisible = true;
            }
        }
    }
}