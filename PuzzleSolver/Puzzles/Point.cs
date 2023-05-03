using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles
{
    /// <summary>
    /// Точка на плоскости
    /// </summary>
    public class Point
    {
        /// <summary>
        /// Абсцисса
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Ордината
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Беспараметрический конструктор
        /// </summary>
        public Point() { }

        /// <summary>
        /// Конструктор по координатам
        /// </summary>
        /// <param name="x">Абсцисса</param>
        /// <param name="y">Ордината</param>
        public Point(int x, int y) { X = x; Y = y; }
    }
}
