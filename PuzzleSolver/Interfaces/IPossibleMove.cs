using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Interfaces
{
    /// <summary>
    /// Проверка хода на корректность
    /// </summary>
    public interface IPossibleMove
    {
        /// <summary>
        /// Проверка потенциального хода на корректность
        /// </summary>
        /// <param name="imove">Проверяемый ход</param>
        /// <returns></returns>
        bool PossibleMove(IMove imove);
    }
}
