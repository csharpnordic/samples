using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles
{
    /// <summary>
    /// Сторона плитки / направление движения 
    /// </summary>
    public enum Side
    {
        /// <summary>
        /// Левая сторона
        /// </summary>
        Left = 0,
        /// <summary>
        /// Правая сторона
        /// </summary>
        Right = 1,
        /// <summary>
        /// Верхняя сторона
        /// </summary>
        Up = 2,
        /// <summary>
        /// Нижняя сторона
        /// </summary>
        Down = 3,
        /// <summary>
        /// Сторона не задана
        /// </summary>
        None
    }
}
