using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Routing
{
    /// <summary>
    /// Перемещающаяся по карте города машинка
    /// </summary>
    public class Car : Point
    {
        /// <summary>
        /// Изменение направления движения после поворота
        /// <para>Первый индекс - <seealso cref="Puzzles.Side"/></para>
        /// <para>Второй индекс - <seealso cref="Turn"/></para>
        /// </summary>
        private readonly static Side[,] NextSide = new Side[,]
        {
            // Left, Forward
            { Side.Down, Side.Left }, // Left
            { Side.Up, Side.Right }, // Right
            { Side.Left, Side.Up }, // Up 
            { Side.Right, Side.Down } // Down
        };

        /// <summary>
        /// Текущее направление движения
        /// </summary>
        public Side Side { get; set; }

        /// <summary>
        /// Перемещение в заданном направлении
        /// </summary>
        /// <param name="turn">Направление движения или поворота</param>
        /// <returns></returns>
        public Car Move(Turn turn)
        {
            // первый шаг - движение в заданном направлении
            var point1 = Move(Side);
            // Новое направление движения
            var side = NextSide[(int)Side, (int)turn];
            // второй шаг - движение в новом направлении
            var point2 = point1.Move(side);
            // Новое положение машинки
            var car = new Car()
            {
                X = point2.X,
                Y = point2.Y,
                Side = side
            };
            return car;
        }

        /// <summary>
        /// Строковое представление машинки
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{base.ToString()} {Side}";

        /// <summary>
        /// Сравнение на равенство, включая направление движения
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Car left, Car right)
        {
            return left.X == right.X && left.Y == right.Y && left.Side == right.Side;
        }

        /// <summary>
        /// Сравнение на неравенство, включая направление движения
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Car left, Car right)
        {
            return left.X != right.X || left.Y != right.Y || left.Side != right.Side;
        }
    }
}
