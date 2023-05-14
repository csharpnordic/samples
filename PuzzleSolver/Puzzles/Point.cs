using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        /// <summary>
        /// Перемещение точки на один шаг в заданном направлении
        /// </summary>
        /// <param name="source">Исходная точка</param>
        /// <param name="side">Направление перемещения</param>
        /// <returns></returns>
        public Point Move(Side side)
        {
            var delta = Neighbour.Neighbours.First(x => x.Side == side);
            var point = new Point(X + delta.DX, Y + delta.DY);
            return point;
        }

        /// <summary>
        /// Строковое представление точки
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"({X},{Y})";
    }
}
