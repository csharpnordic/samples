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
    public partial class TriangleControl : UserControl
    {
        /// <summary>
        /// Треугольник направлен вверх (Z = 0)
        /// </summary>
        public bool Up { get; set; }

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
            var brush = new SolidBrush(Up ? Color.Orange : Color.DarkOrange);
            var pen = new Pen(Color.DarkBlue, Width / 20);
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
                    new Point(0,0),
                    new Point(Width, 0),
                    new Point (Width/2, Height),
                };
            }
            e.Graphics.FillPolygon(brush, points);
            e.Graphics.DrawPolygon(pen, points);

            //  base.OnPaint(e);
        }
    }
}
