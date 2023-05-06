using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Routing
{
    /// <summary>
    /// Соседняя клетка
    /// </summary>
    public struct Neighbour
    {
        /// <summary>
        /// Смещение по горизонтали
        /// </summary>
        public int DX;
        /// <summary>
        /// Смещение по вертикали
        /// </summary>
        public int DY;
        /// <summary>
        /// Граничащая сторона
        /// </summary>
        public Side Side;

        /// <summary>
        /// Конструктор со значениями
        /// </summary>
        /// <param name="dx">Смещение по горизонтали</param>
        /// <param name="dy">Смещение по вертикали</param>
        /// <param name="side">Граничащая сторона</param>
        public Neighbour(int dx, int dy, Side side)
        { 
            this.DX = dx;   
            this.DY = dy;
            this.Side = side;
        }
    }
}
