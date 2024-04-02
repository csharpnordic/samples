﻿using PuzzleSolver.Interfaces;
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
        /// Сторона, противоположная заданной
        /// </summary>
        /// <param name="side">Сторона плитки</param>
        /// <returns>Сторона, противоположная заданной</returns>
        /// <exception cref="Exception"></exception>
        internal static Side OppositeSide(Side side)
        {
            switch (side)
            {
                case Side.Left: return Side.Right;
                case Side.Right: return Side.Left;
                case Side.Up: return Side.Down;
                case Side.Down: return Side.Up;
                default: throw new Exception();
            }
        }

        /// <summary>
        /// Универсальный алгоритм рекурсивного спуска по ходам головоломки
        /// </summary>
        /// <param name="state">Изменяемое состояние головоломки</param>
        /// <returns></returns>
        public static bool Solve(this IState state, bool oneStep)
        {
            // Проверка на корректность запуска
            if (state == null) return false;

            state.Log();
            var moves = state.GetMoves();
            foreach (var move in moves)
            {
                // Выполняем очередной возможный ход
                state.Move(move);

                // Для отладки - делает только один ход
                if (oneStep) return true;

                // Проверка нахождения решения
                if (state.Done())
                {
                    return true;
                }
                if (Solve(state, oneStep)) // если же решение не найдено, рекурсивно попробуем следующий ход
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
