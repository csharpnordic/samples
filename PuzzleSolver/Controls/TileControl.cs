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
        /// Плитка, с которой связан
        /// </summary>
        public Puzzles.Routing.Cell Cell { get; set; }

        /// <summary>
        /// Беспараметрический конструктор
        /// </summary>
        public TileControl()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Cell.Border)
            {
                int x = 0, y = 0;
                int dx = 0, dy = 0;
                switch (Cell.Tile.Side)
                {
                    case Puzzles.Routing.Side.Top:
                        x = (Width - Width * Colors / Rate) / 2;
                        dx = Width / Rate;
                        y = 0;
                        dy = 0;
                        break;

                    case Puzzles.Routing.Side.Bottom:
                        x = (Width - Width * Colors / Rate) / 2;
                        dx = Width / Rate;
                        y = Height * (Rate - 1) / Rate;
                        dy = 0;
                        break;

                    case Puzzles.Routing.Side.Left:
                        x = 0;
                        dx = 0;
                        y = (Height - Height * Colors / Rate) / 2;
                        dy = Height / Rate;
                        break;

                    case Puzzles.Routing.Side.Right:
                        x = Width * (Rate - 1) / Rate;
                        dx = 0;
                        y = (Height - Height * Colors / Rate) / 2;
                        dy = Height / Rate;
                        break;
                }
                for (int i = 0; i < Colors; i++)
                {
                    if ((1 << i & Cell.Tile[Cell.Tile.Side]) > 0)
                    {
                        Rectangle rect = new Rectangle(x, y, Width / Rate, Height / Rate);
                        e.Graphics.FillRectangle(Brush[i], rect);
                    }
                    x += dx;
                    y += dy;
                }
            }

            base.OnPaint(e);
        }

        protected override void OnClick(EventArgs e)
        {
            Cell.Tile[Cell.Tile.Side] = (Cell.Tile[Cell.Tile.Side] + 1) % (int)Math.Pow(2, Colors);
            Invalidate();
        }
    }
}
