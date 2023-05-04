using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Routing
{
    /// <summary>
    /// Сторона плитки 
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
        Top = 2,
        /// <summary>
        /// Нижняя сторона
        /// </summary>
        Bottom = 3,
        /// <summary>
        /// Сторона не задана
        /// </summary>
        None
    }
}
