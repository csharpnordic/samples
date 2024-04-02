using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Coverage
{
    /// <summary>
    /// Плитка в составе фигуры
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// Индекс отображаемого символа, начиная с 0
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Относительная абсцисса плитки в составе фигуры
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Относительная ордината плитки в составе фигуры
        /// </summary>
        public int Y { get; set; }
    }
}
