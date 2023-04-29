using PuzzleSolver.Extenders;
using PuzzleSolver.Puzzles.Sudoku;
using System;

namespace PuzzleSolver
{
    /// <summary>
    /// Главная форма
    /// </summary>
    public partial class MainForm : Form
    {
        private State state;

        private Dictionary<Cell, Button> buttons = new();

        private string[] images = new string[] {
            string.Empty,
            "\U0001f467", // кукла
            "\U0001f99c", // птица
            "\U0001f9F8", // мишка
            "\U0001f697", // машина
            "\U0001f460", // женская туфля
            "\U0001fa91", // кресло
            "\U0001f382", // торт
            "\U00002615", // чашка (чайник)
            "\U0001f3a9", // шапка (ведро)
        };

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Выход из приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Переключение в режим судоку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sudokuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabSudoku.Select();

            tabSudoku.Controls.Clear();
            buttons.Clear();

            var parent = tabSudoku;

            state = new(9, 9, 3);

            // генерация интерфейса
            // количество больших ячеек
            int panelCountX = state.SizeX / state.Size;
            int panelCountY = state.SizeY / state.Size;
            // размер больших ячеек
            int panelSize = Math.Min(parent.Width / panelCountX, parent.Height / panelCountY);
            // размер клеток
            int cellSize = panelSize / state.Size;
            for (int x = 0; x < panelCountX; x++)
            {
                for (int y = 0; y < panelCountY; y++)
                {
                    var panel = new Panel()
                    {
                        Left = x * panelSize,
                        Top = y * panelSize,
                        Width = panelSize,
                        Height = panelSize,
                        BorderStyle = BorderStyle.FixedSingle,
                        ForeColor = Color.Black
                    };
                    for (int dx = 0; dx < state.Size; dx++)
                    {
                        for (int dy = 0; dy < state.Size; dy++)
                        {
                            var cell = state.Cells[x * state.Size + dx][y * state.Size + dy];
                            var button = new Button()
                            {
                                Left = dx * cellSize,
                                Top = dy * cellSize,
                                Width = cellSize,
                                Height = cellSize,
                                Tag = cell
                            };
                            button.Font = new Font(FontFamily.GenericSansSerif, cellSize / 2);
                            state.Cells[x * state.Size + dx][y * state.Size + dy].ValueChanged += CellValueChanged;
                            button.Click += SudokuButtonClick;
                            panel.Controls.Add(button);
                            buttons.Add(cell, button);
                        }
                    }
                    parent.Controls.Add(panel);
                }
            }
        }

        /// <summary>
        /// Изменение значения клетки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="number"></param>
        private void CellValueChanged(object sender, int number)
        {
            if (sender is Cell cell)
            {
                Button button = buttons[cell];
                if (button != null)
                {
                    button.Text = images[number];
                }
            }
        }

        /// <summary>
        /// Нажатие кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void SudokuButtonClick(object? sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is Cell cell)
            {
                cell.Number = (cell.Number + 1) % (state.Size * state.Size + 1);
                cell.Fixed = initButton.Checked;
            }
        }

        /// <summary>
        /// Сохранить состояние
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "Сохранение головоломки",
                Filter = "Файлы (*.json)|*.json|Все файлы (*.*)|*.*"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                state.SaveJson(dialog.FileName);
            }
        }
    }
}