using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Routing
{
    /// <summary>
    /// Тип клетки
    /// </summary>
    public enum CellType
    {
        /// <summary>
        /// Обычная клетка
        /// </summary>
        None,
        /// <summary>
        /// Стартовая клетка
        /// </summary>
        Start,
        /// <summary>
        /// Финишная клетка
        /// </summary>
        Finish
    }
}
