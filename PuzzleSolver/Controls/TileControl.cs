using PuzzleSolver.Puzzles.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleSolver.Controls
{
    /// <summary>
    /// Графическое представление плитки
    /// </summary>
    public partial class TileControl : UserControl
    {
        /// <summary>
        /// Масштабный коэффициент для построение интерфейса
        /// </summary>
        private const int Rate = 8;

        /// <summary>
        /// Набор кистей для отрисовки
        /// </summary>
        private Brush[] Brush;

        /// <summary>
        /// Возможное количество цветов
        /// </summary>
        public int Colors
        {
            set
            {
                Brush = new Brush[value];
                if (value >= 1) Brush[0] = new SolidBrush(Color.Cyan);
                if (value >= 2) Brush[1] = new SolidBrush(Color.Blue);
            }
            get
            {
                return Brush?.Length ?? 0;
            }
        }

        /// <summary>
        /// Плитка, с которой связан компонент
        /// </summary>
        private Puzzles.Routing.Cell cell;

        /// <summary>
        /// Плитка, с которой связан компонент
        /// </summary>
        public Puzzles.Routing.Cell Cell
        {
            get
            {
                return cell;
            }
            set
            {
                cell = value;

                // Автоматическое создание объекта при необходимости
                cell.Tile ??= new Tile { Side = Side.None };

                UpdateColor();
                Invalidate();
            }
        }

        /// <summary>
        /// Признак отрисовки зданий для задачи маршрута
        /// </summary>
        public bool DrawHouses { get; set; }

        /// <summary>
        /// Беспараметрический конструктор
        /// </summary>
        public TileControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обновление фонового цвета компонента
        /// </summary>
        private void UpdateColor()
        {
            // автоматическое определение цвета
            if (cell.Border)
                BackColor = Color.DarkGray;
            else if (cell.Fixed)
                BackColor = Color.LightGray;
            else
                BackColor = Color.White;
        }

        private void DrawHouse(Graphics graphics, int x, int y)
        {
            var brush = new SolidBrush(Color.DarkGreen);
            // размеры квадрата
            int sx = Width / 3;
            int sy = Height / 3;
            graphics.FillRectangle(brush, x * sx, y * sy, sx, sy);
        }

        /// <summary>
        /// Визуализауция плитки
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Если плитка не задана, то и рисовать нечего
            if (Cell?.Tile == null) return;

            // Рисуем четыре стороны плитки
            foreach (var side in Enum.GetValues<Side>().Where(x => x != Side.None))
            {
                int x = 0, y = 0;
                int dx = 0, dy = 0;
                // размеры по умолчанию
                int sx = Width / Rate;
                int sy = Height / Rate;

                switch (side)
                {
                    case Puzzles.Routing.Side.Top:
                        x = (Width - Width * Colors / Rate) / 2;
                        dx = Width / Rate;
                        y = 0;
                        dy = 0;
                        sy = Cell.Border ? Height / Rate : Height / 2;
                        break;

                    case Puzzles.Routing.Side.Bottom:
                        x = (Width - Width * Colors / Rate) / 2;
                        dx = Width / Rate;
                        y = Cell.Border ? Height * (Rate - 1) / Rate : Height / 2;
                        dy = 0;
                        sy = Cell.Border ? Height / Rate : Height / 2;
                        break;

                    case Puzzles.Routing.Side.Left:
                        x = 0;
                        dx = 0;
                        y = (Height - Height * Colors / Rate) / 2;
                        dy = Height / Rate;
                        sx = Cell.Border ? Width / Rate : Width / 2;
                        break;

                    case Puzzles.Routing.Side.Right:
                        x = Cell.Border ? Width * (Rate - 1) / Rate : Width / 2;
                        dx = 0;
                        y = (Height - Height * Colors / Rate) / 2;
                        dy = Height / Rate;
                        sx = Cell.Border ? Width / Rate : Width / 2;
                        break;
                }
                for (int i = 0; i < Colors; i++)
                {
                    if ((1 << i & Cell.Tile[side]) > 0)
                    {
                        Rectangle rect = new Rectangle(x, y, sx, sy);
                        e.Graphics.FillRectangle(Brush[i], rect);
                    }
                    x += dx;
                    y += dy;
                }
            }

            if (DrawHouses)
            {
                DrawHouse(e.Graphics, 0, 0);
                DrawHouse(e.Graphics, 0, 2);
                DrawHouse(e.Graphics, 2, 0);
                DrawHouse(e.Graphics, 2, 2);
                if (Cell.Tile[Side.Top] == 0)
                    DrawHouse(e.Graphics, 1, 0);
                if (Cell.Tile[Side.Bottom] == 0)
                    DrawHouse(e.Graphics, 1, 2);
                if (Cell.Tile[Side.Left] == 0)
                    DrawHouse(e.Graphics, 0, 1);
                if (Cell.Tile[Side.Right] == 0)
                    DrawHouse(e.Graphics, 2, 1);
                if (cell.Tile.Pipe.Max() == 0)
                    DrawHouse(e.Graphics, 1, 1);
            }

            base.OnPaint(e);
        }

        /// <summary>
        /// Изменение значения плитки
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            if (Cell?.Border ?? false)
            {
                // координаты щелчка неважны
                Cell.Tile[Cell.Tile.Side] = (Cell.Tile[Cell.Tile.Side] + 1) % (int)Math.Pow(2, Colors);
            }
            else if (e is MouseEventArgs me)
            {
                switch (me.Button)
                {
                    case MouseButtons.Left:
                        // правый верхний треугольник
                        bool rT = me.X > me.Y;
                        // левый верхний треугольник
                        bool lT = me.X + me.Y < Math.Min(Width, Height);
                        Side side;
                        if (lT)
                            side = rT ? Side.Top : Side.Left;
                        else
                            side = rT ? Side.Right : Side.Bottom;

                        Cell.Tile[side] = (Cell.Tile[side] + 1) % (int)Math.Pow(2, Colors);
                        break;

                    case MouseButtons.Right: // изменение фиксированности клетки
                        if (!Cell.Border)
                        {
                            Cell.Fixed = !Cell.Fixed;
                            UpdateColor();
                        }
                        break;
                }
            }
            Invalidate();
        }
    }
}
