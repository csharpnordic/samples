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
    public class State : BaseState, IState, ILoad
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
        /// Список фигур
        /// </summary>
        public List<Figure> Figures { get; set; } = new();

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
            Image = new char[Chars + 1];

            for (int i = 0; i <= Chars; i++)
            {
                Image[i] = DefaultImage[i];
            }

            Field = Core.Array2<Cell>(SizeX, SizeY, true);
            InitAfterLoad();
        }

        /// <inheritdoc/>
        public void InitAfterLoad()
        {
            // Восстановление ссылки на состояние
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    Field[x][y].State = this;
                }
            }
        }

        /// <summary>
        /// Сброс помеченной фигуры
        /// </summary>
        public void ResetFigure()
        {
            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {   // Сброс помеченной фигуры
                    Field[x][y].Mark = 0;
                }
            }
        }

        /// <summary>
        /// Добавление нарисованной фигуры в набор фигур
        /// </summary>
        public Figure? AddFigure()
        {
            var figure = new Figure();

            for (int x = 0; x < SizeX; x++)
            {
                for (int y = 0; y < SizeY; y++)
                {
                    // Пропуск всех непомеченных клеток
                    if (Field[x][y].Mark == 0) continue;

                    var tile = new Tile()
                    {
                        X = x,
                        Y = y,
                        Index = Field[x][y].Mark
                    };
                    figure.Tiles.Add(tile);

                    // Сброс помеченной фигуры
                    Field[x][y].Mark = 0;
                }
            }

            // Если ни одной плитки не нашли, то фигура не сформирована
            if (figure.Tiles.Count() == 0) return null;

            // Определение верхнего левого угла фигуры
            var mx = figure.Tiles.Min(tile => tile.X);
            var my = figure.Tiles.Min(tile => tile.Y);

            // Нормализация координат фигуры (перемещение в верхний левый угол поля)
            figure.Tiles.ForEach(tile =>
            {
                tile.X -= mx;
                tile.Y -= my;
            });

            // Нумерация фигур
            figure.Number = Figures.Count() + 1;

            Figures.Add(figure);
            return figure;
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
