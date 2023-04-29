using PuzzleSolver.Extenders;
using PuzzleSolver.Puzzles;
using PuzzleSolver.Puzzles.Sudoku;
using System;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PuzzleSolver
{
    /// <summary>
    /// ������� �����
    /// </summary>
    public partial class MainForm : Form
    {
        private State state;

        private Dictionary<Cell, Button> buttons = new();

        private string[] images = new string[] {
            string.Empty,
            "\U0001f9F8", // �����
            "\U0001f99c", // �����
            "\U0001f382", // ����
            "\U0001f697", // ������
            "\U0001fa91", // ������
            "\U0001f467", // �����
            "\U0001f460", // ������� �����
            "\U00002615", // ����� (������)
            "\U0001f3a9", // ����� (�����)
        };

        /// <summary>
        /// ����������� �����
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ����� �� ����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// ������������� ���������� �������� ����
        /// </summary>
        /// <param name="parent"></param>
        private void InitPanel(Control parent)
        {
            // ����� ���������� ����������
            parent.Controls.Clear();
            buttons.Clear();

            // ��������� ����������
            // ���������� ������� �����
            int panelCountX = state.SizeX / state.Size;
            int panelCountY = state.SizeY / state.Size;
            // ������ ������� �����
            int panelSize = Math.Min(parent.Width / panelCountX, parent.Height / panelCountY);
            // ������ ������
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
                            cell.ValueChanged += CellValueChanged;
                            button.Click += SudokuButtonClick;
                            panel.Controls.Add(button);
                            buttons.Add(cell, button);
                            // ��� ���������� ���������� ����� ���������� � �������
                            cell.Number = cell.Number;
                        }
                    }
                    parent.Controls.Add(panel);
                }
            }
        }

        /// <summary>
        /// ������������ � ����� ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sudokuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabSudoku.Select();

            state = new(9, 9, 3);

            InitPanel(tabSudoku);
        }

        /// <summary>
        /// ��������� �������� ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="number"></param>
        private void CellValueChanged(object sender, int number)
        {
            if (sender is Cell cell && buttons.TryGetValue(cell, out Button? button))
            {
                if (button != null)
                {
                    button.Text = images[number];
                    // ��������� �������������� ������ ��� ��������� �����������
                    // ��� ���� ������ �� ������������� (�� ������� ������)
                    button.Enabled = initButton.Checked || !cell.Fixed;
                }
            }
        }

        /// <summary>
        /// ������� ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void SudokuButtonClick(object? sender, EventArgs e)
        {
            if (sender is Button button && button.Tag is Cell cell)
            {
                int number = (cell.Number + 1) % (state.Size * state.Size + 1);
                button.BackColor = Control.DefaultBackColor;
                if (!initButton.Checked && number > 0) // �������� �� ������������ ����
                {
                    var move = new Move()
                    {
                        Cell = cell,
                        Number = number
                    };
                    button.BackColor = state.PossibleMove(move) ? Color.LightGreen : Color.Orange;
                }
                cell.Fixed = initButton.Checked;
                cell.Number = number;
            }
        }

        /// <summary>
        /// ��������� ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog
            {
                Title = "���������� �����������",
                Filter = "����� (*.json)|*.json|��� ����� (*.*)|*.*"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                state.SaveJson(dialog.FileName);
            }
        }

        /// <summary>
        /// �������� �����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "�������� ������ �����",
                Filter = "����� (*.json)|*.json|��� ����� (*.*)|*.*"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                state = Core.LoadJson<State>(dialog.FileName);
                state.InitLines();
                InitPanel(tabSudoku);
                string name = System.IO.Path.GetFileName(dialog.FileName);
                statusLabel.Text = $"���� {name} �������� �������";
            }
        }

        /// <summary>
        /// ������� �����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void solveButton_Click(object sender, EventArgs e)
        {
            var solver = new Solver();
            solver.Solve(state);
        }
    }
}