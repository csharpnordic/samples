using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Coverage
{
    /// <summary>
    /// Клетка поля
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Значение клетки поля
        /// </summary>
        public char Value { get; set; } = ' '; // по умолчанию пробел

        /// <summary>
        /// Клетка поля доступна для размещения фигуры
        /// <para>Для поддержки полей с вырезами или полей непрямоугольной формы</para>
        /// </summary>
        public bool Available { get; set; } = true; // по умолчанию клетка доступная
    }
}
