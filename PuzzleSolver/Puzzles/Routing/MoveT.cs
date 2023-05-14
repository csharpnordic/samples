using PuzzleSolver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Routing
{
    /// <summary>
    /// Движение машинки по городу
    /// </summary>
    public class MoveT : IMove
    {
        /// <summary>
        /// Положение машинки до хода
        /// </summary>
        public Car From { get; set; }

        /// <summary>
        /// Положение машинки после хода
        /// </summary>
        public Car To { get; set; }

        /// <summary>
        /// Направление движения или поворота
        /// </summary>
        public Turn Direction { get; set; }

        /// <summary>
        /// Конструктор со значениями
        /// </summary>
        /// <param name="from">Начальная точка</param>
        /// <param name="to">Конечная точка</param>
        /// <param name="direction">Направление движения</param>
        public MoveT(Car from, Turn direction, Car to)
        {
            From = from;
            To = to;
            Direction = direction;
        }

        /// <summary>
        /// Строковое представление хода
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{From} {Direction} {To}";
    }
}
