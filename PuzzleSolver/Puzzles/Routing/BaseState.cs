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
    public class BaseState
    {
        /// <summary>
        /// Размер игрового поля по горизонтали
        /// </summary>
        [DisplayName("Размер игрового поля по горизонтали")]
        public int SizeX { get; set; }

        /// <summary>
        /// Размер игрового поля по вертикали
        /// </summary>
        [DisplayName("Размер игрового поля по вертикали")]
        public int SizeY { get; set; }

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
    }
}
