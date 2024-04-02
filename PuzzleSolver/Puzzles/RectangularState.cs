using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles
{
    /// <summary>
    /// Прямоугольное игровое поле
    /// </summary>
    public class RectangularState
    {
        /// <summary>
        /// Полное имя класса
        /// <para>Для наследования, входит в интерфейс <seealso cref="Interfaces.IState"/></para>
        /// </summary>
        public string ClassName => GetType().FullName;

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
    }
}
