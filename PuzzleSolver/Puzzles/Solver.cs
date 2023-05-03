using PuzzleSolver.Interfaces;
using PuzzleSolver.Puzzles.Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles
{
    /// <summary>
    /// Решатель головоломок
    /// </summary>
    public static class Solver
    {
        /// <summary>
        /// Универсальный алгоритм рекурсивного спуска по ходам головоломки
        /// </summary>
        /// <param name="state">Изменяемое состояние головоломки</param>
        /// <returns></returns>
        public static bool Solve(this IState state)
        {
            state.Log();
            var moves = state.GetMoves();
            foreach (var move in moves)
            {
                // Выполняем очередной возможный ход
                state.Move(move);              
                // Проверка нахождения решения
                if (state.Done())
                {
                    return true;
                }               
                if (Solve(state)) // если же решение не найдено, рекурсивно попробуем следующий ход
                {
                    return true;
                }
                // Отмена последнего сделанного хода
                state.UndoMove(move);
            }
            return false;
        }
    }
}
