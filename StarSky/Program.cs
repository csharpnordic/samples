namespace StarSky;

internal static class Program
{
    /// <summary>
    /// Потокобезопасная очередь
    /// </summary>
    internal static readonly System.Collections.Concurrent.ConcurrentQueue<int> Queue = new();

    static void Main()
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
                if (Queue.TryDequeue(out int n) && n == 1 && Star.Count < 20)
                {
                    star = new Star();
                    stars.Add(star);
                    star.Start();
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