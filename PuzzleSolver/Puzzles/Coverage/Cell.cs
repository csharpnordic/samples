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
        /// Состояние, к которому привязана клетка
        /// </summary>
        internal State State { get; set; }

        /// <summary>
        /// Индекс отображаемого символа в составе фигуры, начиная с 0
        /// <para>Только для режима размещения фигур</para>
        /// </summary>
        internal int Mark { get; set; }

        /// <summary>
        /// Индекс отображаемого символа в составе поля, начиная с 0
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// Клетка поля доступна для размещения фигуры
        /// <para>Для поддержки полей с вырезами или полей непрямоугольной формы</para>
        /// </summary>
        public bool Available { get; set; } = true; // по умолчанию клетка доступная

        /// <summary>
        /// Строковое представление клетки
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Mark > 0)
                return State?.Image[Mark].ToString() ?? string.Empty;
            else
                return State?.Image[Index].ToString() ?? string.Empty;
        }
    }
}
