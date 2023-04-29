using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Sudoku
{
    public class Move
    {
        /// <summary>
        /// Ячейка игрового поля
        /// </summary>
        public Cell Cell { get; set; }
        /// <summary>
        /// Новое значение ячейки после хода
        /// </summary>
        public int Number { get; set; }
    }
}
