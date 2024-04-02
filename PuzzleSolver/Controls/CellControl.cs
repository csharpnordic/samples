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
        /// <summary>
        /// Формат вывода текста в середине ячейки
        /// </summary>
        private readonly static StringFormat stringFormat = new StringFormat()
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        /// <summary>
        /// Режим размещения фигуры
        /// </summary>
        public static bool FigureMode { get; set; }

        public Puzzles.Coverage.Cell? Cell { get; set; }

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
            Color color = Color.Gray; // цвет по умолчанию
            if (Cell != null)
            {
                if (Cell.Mark > 0) color = Color.SandyBrown;
                else if (Cell.Available) color = Color.White;
            }
            var brush = new SolidBrush(color);
            var pen = new Pen(Color.Black);
            e.Graphics.FillRectangle(brush, 0, 0, Width, Height);
            e.Graphics.DrawRectangle(pen, 0, 0, Width, Height);
            if (Cell != null)
            {
                var font = new Font(FontFamily.GenericSansSerif, Height / 4);
                e.Graphics.DrawString(Cell.ToString(), font, new SolidBrush(ForeColor), Width / 2, Height / 2, stringFormat);
            }
        }

        /// <summary>
        /// Реакция на щелчок мышью
        /// </summary>
        /// <param name="e"></param>       
        protected override void OnMouseClick(MouseEventArgs e)
        {
            // Проверка на корректность вызова
            if (Tag is not Puzzles.Coverage.State state || Cell == null) return;

            switch (e.Button)
            {
                case MouseButtons.Left:
                    // только если клетка не принадлежит фигуре и приналдежит полю
                    if (Cell.Mark == 0 && Cell.Available) 
                    {
                        Cell.Index = (Cell.Index + 1) % (state.Chars + 1);
                        Invalidate();
                    }
                    break;

                case MouseButtons.Middle:
                    // только если клетка принадлежит полю
                    if (Cell.Available)
                    {
                        Cell.Mark = (Cell.Mark + 1) % (state.Chars + 1);
                        Invalidate();
                    }
                    break;

                case MouseButtons.Right:
                    Cell.Available = !Cell.Available;
                    Invalidate();
                    break;
            }           
        }
    }
}