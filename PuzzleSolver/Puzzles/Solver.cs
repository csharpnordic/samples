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
        /// Создание прямоугольного массива массивов заданного типа
        /// </summary>
        /// <typeparam name="T">Тип элемента массиа</typeparam>
        /// <param name="sizeX">Размер массива по горизонтали</param>
        /// <param name="sizeY">Размер массива по вертикали</param>
        /// <param name="initialize">Требуется ли создавать объекты - элементы массива</param>
        /// <returns></returns>
        public static T[][] Array2<T>(int sizeX, int sizeY, bool initialize) where T : new()
        {
            T[][] array = new T[sizeX][];

            for (int x = 0; x < sizeX; x++)
            {
                array[x] = new T[sizeY];
                if (initialize)
                {
                    for (int y = 0; y < sizeY; y++)
                    {
                        array[x][y] = new T();
                    }
                }
            }

            return array;
        }

        /// <summary>
        /// Создание прямоугольного массива массивов заданного типа
        /// </summary>
        /// <typeparam name="T">Тип элемента массиа</typeparam>
        /// <param name="sizeX">Размер массива по горизонтали</param>
        /// <param name="sizeY">Размер массива по вертикали</param>
        /// <param name="initialize">Требуется ли создавать объекты - элементы массива</param>
        /// <returns></returns>
        public static T[][][] Array3<T>(int sizeX, int sizeY, int sizeZ, bool initialize) where T : new()
        {
            T[][][] array = new T[sizeX][][];

            for (int x = 0; x < sizeX; x++)
            {
                array[x] = Array2<T>(sizeY, sizeZ, initialize);
            }

            return array;
        }

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
        public static bool Solve(this IState state)
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
                // return true;
                
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
