using PuzzleSolver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Routing
{
    public class Move : IMove
    {
        /// <summary>
        /// Ячейка игрового поля
        /// </summary>
        public Cell Cell { get; set; }

        /// <summary>
        /// Новая плитка ячейки после хода
        /// </summary>
        public Tile Tile { get; set; }

        /// <summary>
        /// Конструктор со значениями
        /// </summary>
        /// <param name="cell">Ячейка игрового поля</param>
        /// <param name="tile">Новая плитка ячейки после хода</param>
        public Move(Cell cell, Tile tile)
        {
            Cell = cell;
            Tile = tile;
        }
    }
}
