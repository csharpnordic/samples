using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;


namespace Queens
{
    internal class Program
    {
        /// <summary>
        /// Размер шахматного поля
        /// </summary>
        const int Size = 8;

        /// <summary>
        /// Ширина клетки шахматного поля в символах
        /// </summary>
        const int Width = 2;

        /// <summary>
        /// Изображение ферзя (королевы) на доске
        /// </summary>
        const string Queen = "()";

        /// <summary>
        /// Проверка на то, бьют ли два ферзя друг друга
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        static bool IsHit(int x1, int y1, int x2, int y2)
        {
            // Если ферзи находятся на одной горизонтали 
            if (y1 == y2) return true;
            // Если ферзи находятся на одной вертикали
            if (x1 == x2) return true;
            // Если ферзи находятся на такой диагонали: \
            if ((x2 - x1) == (y2 - y1)) return true;
            // Если ферзи находятся на такой диагонали: /
            if ((x2 - x1) == (y1 - y2)) return true;

            return false;
        }

        /// <summary>
        /// Массив горизонтальных позиций ферзей от 0 до <seealso cref="Size"/>-1
        /// </summary>
        static int[] q = new int[Size];

        static int counter = 0;
        static int iterations = 0;

        /// <summary>
        /// Движение заданного ферзя по горизонтали 
        /// </summary>
        /// <param name="n">Номер ферзя</param>
        static IEnumerable<int[]> Move(int n)
        {
            iterations++;
            // движение ферзя
            for (int x = 0; x < Size; x++)
            {
                // поставить ферзя в заданную позицию
                q[n] = x;

                // Обработка всех предыдущих ферзей
                bool hit = false;
                for (int i = 0; i < n; i++)
                {
                    // проверка по вертикали
                    if (q[i] == q[n])
                    {
                        hit = true;
                        break;
                    }
                    // проверка по диагоналям
                    if (Math.Abs(q[i] - q[n]) == (n - i))
                    {
                        hit = true;
                        break;
                    }
                }
                // ферзь под боем, комбинация некорректна
                if (hit) continue;

                if (n == Size - 1)
                {
                    // Копирование массива - обязательно!
                    int[] copy = new int[q.Length];
                    Array.Copy(q, copy, q.Length);
                    yield return copy;
                    counter++;
                    // System.Threading.Thread.Sleep(500);
                    // Console.ReadLine();
                }
                else
                {
                    foreach (var soluton in Move(n + 1))
                    {
                        yield return soluton;
                    }
                }
            }
        }

        /// <summary>
        /// Вывод доски на экран
        /// </summary>
        /// <exception cref="Exception"></exception>
        static void Print(int[] q)
        {
            // Очистка экрана
            // Console.Clear();

            // Подпись доски
            StringBuilder sb = new StringBuilder();
            // string label = string.Empty;
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            // char letter = 'A';
            for (int x = 0; x < Size; x++)
            {
                sb.Append($"{chars.Substring(x, 1),Width}");
                // letter++;
            }
            Console.WriteLine(sb.ToString().ToUpper());

            // Вывод доски на экран
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    switch ((x + y) % 2)
                    {
                        case 0:
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                            break;
                        case 1:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        default: // так быть не должно! 
                            throw new Exception("Так быть не должно");
                    }
                    if (q[y] == x)
                    {
                        Console.Write($"{Queen,Width}");
                    }
                    else
                    {
                        Console.Write($"{string.Empty,Width}");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            foreach (var solution in Move(0))
            {
                Print(solution);
            }
            Console.WriteLine($"Всего {counter} решений");
            Console.WriteLine($"Всего {iterations} итераций");
            Console.ReadLine();
        }
    }
}