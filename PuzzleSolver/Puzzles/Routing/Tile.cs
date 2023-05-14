using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Routing
{
    /// <summary>
    /// Плитка игрового поля (включая граничные плитки)
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// Наличие путей по всем сторонам плитки (<seealso cref="Side"/>)
        /// </summary>
        public int[] Pipe { get; set; } = new int[4];

        /// <summary>
        /// Для граничной плитки - сторона, которой она граничит с игровым полем. Для игровой плитки - <seealso cref="Side.None"/>
        /// </summary>
        public Side Side { get; set; }       

        /// <summary>
        /// Индексатор по стороне плитки
        /// </summary>
        /// <param name="side">Сторона плитки</param>
        /// <returns></returns>
        public int this[Side side]
        {
            get
            {
                return Pipe[(int)side];
            }
            set
            {
                Pipe[(int)side] = value;
            }
        }
    }
}
