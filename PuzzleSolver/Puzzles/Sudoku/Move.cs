using PuzzleSolver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Sudoku
{
    /// <summary>
    /// Игровой ход
    /// </summary>
    public class Move : IMove
    {
        /// <summary>
        /// Ячейка игрового поля
        /// </summary>
        public Cell Cell { get; set; }
        /// <summary>
        /// Новое значение ячейки после хода
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Конструктор со значениями
        /// </summary>
        /// <param name="cell">Ячейка игрового поля</param>
        /// <param name="number">Новое значение ячейки после хода</param>
        public Move(Cell cell, int number)
        {
            Cell = cell;
            Number = number;
        }
    }
}
