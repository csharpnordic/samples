using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Routing
{
    /// <summary>
    /// Параметры головоломки
    /// </summary>
    public class BaseState : RectangularState
    {
              /// <summary>
        /// Количество цветов маршрутов
        /// </summary>
        [DisplayName("Количество цветов")]
        public int Colors { get; set; }

        /// <summary>
        /// Разрешение нескольких цветов
        /// </summary>
        [DisplayName("Несколько цветов сразу")]
        public bool MultiColor { get; set; }

        /// <summary>
        /// Контроль границ
        /// </summary>
        [DisplayName("Контроль границ")]
        public bool CheckBorders { get; set; }
    }
}
