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
    public partial class CellControl : UserControl
    {
        public Puzzles.Coverage.Cell Cell { get; set; }

        public CellControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Отрисовка ячейки поля
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            var brush = new SolidBrush(Cell?.Available ?? false ?  Color.White : Color.Gray);
            var pen = new Pen(Color.Black);
            e.Graphics.FillRectangle(brush, 0, 0, Width, Height);
            e.Graphics.DrawRectangle(pen, 0, 0, Width, Height);
        }

        /// <summary>
        /// Реакция на щелчок мышью
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    if (Cell!= null)
                    {
                        Cell.Available = !Cell.Available;
                        Invalidate(); // перерисовать
                    }
                    break;
            }
        }
    }
}
