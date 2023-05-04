using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Interfaces
{
    /// <summary>
    /// Состояние игры
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Фактическое имя класса состояния
        /// </summary>
        string ClassName { get; }

        /// <summary>
        /// Перечень возможных ходов
        /// </summary>
        /// <returns></returns>
        IEnumerable<IMove> GetMoves();

        /// <summary>
        /// Обновление состояния после хода
        /// </summary>
        /// <param name="move">Возможный ход</param>
        /// <returns></returns>
        void Move(IMove move);

        /// <summary>
        /// Отмена сделанного хода
        /// </summary>
        /// <param name="move">Сделанный ход</param>
        void UndoMove(IMove move);

        /// <summary>
        /// Признак нахождения решения
        /// </summary>
        /// <returns></returns>
        bool Done();

        /// <summary>
        /// Протоколирование состояния
        /// </summary>
        void Log();
    }
}
