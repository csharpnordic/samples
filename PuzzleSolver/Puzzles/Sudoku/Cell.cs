using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Sudoku
{
    /// <summary>
    /// Клетка игрового поля
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Метод обработки изменения значения 
        /// </summary>
        /// <param name="number">Новое значение клетки</param>
        public delegate void ChangeValue(object sender, int number);

        /// <summary>
        /// Событие изменения значения
        /// </summary>
        public event ChangeValue? ValueChanged;

        /// <summary>
        /// Значение клетки игрового поля
        /// </summary>
        private int number;

        /// <summary>
        /// Значение клетки игрового поля
        /// </summary>
        public int Number
        {
            get
            {
                return number;
            }
            set
            {
                number = value;
                ValueChanged?.Invoke(this, number);
            }
        }

        /// <summary>
        /// Является ли клетка фиксированной (заданной в начальном условии)
        /// </summary>
        public bool Fixed { get; set; }

        /// <summary>
        /// Строковое представление обьекта
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Number.ToString("#");
    }
}
