using PuzzleSolver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Coverage
{
    /// <summary>
    /// Состояние головоломки покрытия
    /// </summary>
    public class State : BaseState, IState
    {
        /// <summary>
        /// Символы по умолчанию, включая начальный пробел
        /// </summary>
        private const string DefaultImage = " OX";

        /// <summary>
        /// Массив отображаемых символов размера <seealso cref="BaseState.Chars"/>+1
        /// </summary>
        public char[] Image { get; set; }

        /// <summary>
        /// Игровое поле
        /// </summary>
        public Cell[][] Field { get; set; }

        /// <summary>
        /// Беспараметрической конструктор для сериализации
        /// </summary>
        public State() { }

        /// <summary>
        /// Конструктор по параметрам
        /// </summary>
        /// <param name="state">Начальные параметры головоломки</param>
        public State(BaseState state)
        {
            SizeX = state.SizeX;
            SizeY = state.SizeY;
            Chars = state.Chars;
            Image = new char[Chars+1];

            for (int i = 0; i <= Chars; i++)
            {
                Image[i] = DefaultImage[i];
            }

            Field = Core.Array2<Cell>(SizeX, SizeY, true);
        }

        /// <inheritdoc/>
        public bool Done()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerable<IMove> GetMoves()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Log()
        {
        }

        /// <inheritdoc/>
        public void Move(IMove move)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void UndoMove(IMove move)
        {
            throw new NotImplementedException();
        }
    }
}
