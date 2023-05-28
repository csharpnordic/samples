using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Coverage
{
    /// <summary>
    /// Фигура
    /// </summary>
    public class Figure
    {
        /// <summary>
        /// Порядковый номер фигуры, начиная с 1
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Абсцисса фигуры на поле
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Ордината фигуры на поле
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Плитки в составе фигуры
        /// </summary>
        public List<Tile> Tiles { get; set; } = new();

        /// <summary>
        /// Строковое представление объекта
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"Фигура {Number}";
    }
}
