﻿using PuzzleSolver.Puzzles.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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
                if (value >= 0) Brush[0] = new SolidBrush(Color.Cyan);
                if (value >= 1) Brush[1] = new SolidBrush(Color.Blue);
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
                Invalidate();
            }
        }

        /// <summary>
        /// Беспараметрический конструктор
        /// </summary>
        public TileControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Визуализауция плитки
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
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

            base.OnPaint(e);
        }

        /// <summary>
        /// Изменение значения плитки
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            if (Cell.Border)
            {
                // координаты щелчка неважы
                Cell.Tile[Cell.Tile.Side] = (Cell.Tile[Cell.Tile.Side] + 1) % (int)Math.Pow(2, Colors);
            }
            else if (e is MouseEventArgs me)
            {
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
            }
            Invalidate();
        }
    }
}
