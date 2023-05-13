using PuzzleSolver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Routing
{
    /// <summary>
    /// Головоломка построения маршрута
    /// </summary>
    public class StateT : IState
    {
        /// <summary>
        /// Полное имя класса
        /// </summary>
        public string ClassName => GetType().FullName;

        /// <summary>
        /// Размер игрового поля по горизонтали
        /// </summary>
        public int SizeX { get; set; }

        /// <summary>
        /// Размер игрового поля по вертикали
        /// </summary>
        public int SizeY { get; set; }

        /// <summary>
        /// Игровое поле
        /// </summary>
        public Cell[][] Field { get; set; }

        /// <summary>
        /// Беспараметрический конструктор для сериализации
        /// </summary>
        public StateT() { }

        /// <summary>
        /// Конструктор по размеру игрового поля
        /// </summary>
        /// <param name="sizeX">Размер игрового поля по горизонтали</param>
        /// <param name="sizeY">Размер игрового поля по вертикали</param>
        public StateT(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;

            // Создание игрового поля
            Field = Solver.Array2<Cell>(sizeX, sizeY, true);
        }

        public bool Done()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMove> GetMoves()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public void Log()
        {
        }

        public void Move(IMove move)
        {
            throw new NotImplementedException();
        }

        public void UndoMove(IMove move)
        {
            throw new NotImplementedException();
        }
    }
}
