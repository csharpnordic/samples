using PuzzleSolver.Puzzles.Sudoku;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuzzleSolver.Controls
{
    public partial class TriangleControl : UserControl
    {
        /// <summary>
        /// Треугольник направлен вверх (Z = 0)
        /// </summary>
        private bool up;

        /// <summary>
        /// Треугольник направлен вверх (Z = 0)
        /// </summary>
        public bool Up
        {
            get => up;
            set
            {
                up = value;
                labelValue.TextAlign = up ? ContentAlignment.BottomCenter : ContentAlignment.TopCenter;
            }
        }

        /// <summary>
        /// Отображаемый текст
        /// </summary>
        public new string Text
        {
            set
            {
                labelValue.Text = value;
            }
        }

        public TriangleControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Отрисовка плитки
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            bool flag = false; // признак зафиксированной клетки
            if (Tag is Cell cell)
            {
                flag = cell.Fixed;
            }
            using var brush = new SolidBrush(flag ? Color.DarkOrange : Color.Orange);
            var text = new SolidBrush(Color.Black);
            using var pen = new Pen(Color.DarkBlue, Width / 20);
            Point[] points;
            if (Up)
            {
                points = new Point[]
                {
                    new Point(0, Height),
                    new Point(Width, Height),
                    new Point (Width/2, 0),
                };
            }
            else
            {
                points = new Point[]
                {
                    new Point(0, 0),
                    new Point(Width, 0),
                    new Point (Width/2, Height),
                };
            }
            // Треугольник в виде пути
            var path = new GraphicsPath(points, new byte[] { 0, 1, 1 });
            // Закрашиваемая область - треугольник
            Region = new Region(path);

            // Отрисовка фигуры
            e.Graphics.FillPolygon(brush, points);
            e.Graphics.DrawPolygon(pen, points);
        }

        /// <summary>
        /// Размер шрифта следует за размером треугольника
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            labelValue.Font = new Font(FontFamily.GenericSansSerif, Height / 4);
        }

        /// <summary>
        /// Щелчок по метке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void labelValue_Click(object sender, EventArgs e)
        {
            InvokeOnClick(this, e);
        }
    }
}
