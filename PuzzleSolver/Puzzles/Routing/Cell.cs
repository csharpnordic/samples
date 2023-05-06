using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Routing
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
        public delegate void ChangeValue(object sender, Tile? tile);

        /// <summary>
        /// Событие изменения значения
        /// </summary>
        public event ChangeValue? ValueChanged;

        /// <summary>
        /// Плитка
        /// </summary>
        private Tile? tile;

        /// <summary>
        /// Индекс плитки в наборе плиток
        /// </summary>
        public Tile? Tile
        {
            get
            {
                return tile;
            }
            set
            {
                tile = value;
                ValueChanged?.Invoke(this, tile);
            }
        }

        /// <summary>
        /// Является ли клетка фиксированной (заданной в начальном условии)
        /// </summary>
        public bool Fixed { get; set; }

        /// <summary>
        /// Является ли клетка граничной
        /// </summary>
        public bool Border { get; set; }
    }
}
