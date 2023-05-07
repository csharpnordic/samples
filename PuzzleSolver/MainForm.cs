using PuzzleSolver.Controls;
using PuzzleSolver.Extenders;
using PuzzleSolver.Interfaces;
using PuzzleSolver.Puzzles;
using PuzzleSolver.Puzzles.Routing;
using PuzzleSolver.Puzzles.Sudoku;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace PuzzleSolver
{
    /// <summary>
    /// Главная форма
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Состояние головоломки
        /// </summary>
        private Puzzles.Sudoku.State state;
        private Puzzles.Routing.State stater;
        private Puzzles.Sudoku.State3 state3;

        /// <summary>
        /// Отображение клеток судоку на кнопки
        /// </summary>
        private Dictionary<Puzzles.Sudoku.Cell, Button> buttons = new();

        /// <summary>
        /// Отображение клеток маршрутов на элементы управления
        /// </summary>
        private Dictionary<Puzzles.Routing.Cell, TileControl> dict = new();

        /// <summary>
        /// Отображение треугольных клеток судоку на треугольники
        /// </summary>
        private Dictionary<Puzzles.Sudoku.Cell, TriangleControl> triangles = new();

        /*
        private string[] images = new string[] {
            string.Empty,
            "\U0001f9F8", // мишка
            "\U0001f99c", // птица
            "\U0001f382", // торт
            "\U0001f697", // машина
            "\U0001fa91", // кресло
            "\U0001f467", // кукла
            "\U0001f460", // женская туфля
            "\U00002615", // чашка (чайник)
            "\U0001f3a9", // шапка (ведро)
        };
        */

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        #region "Пункт меню 'Игра'"

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
                switch (tabs.SelectedIndex)
                {
                    case 0:
                        state?.SaveJson(dialog.FileName);
                        break;
                    case 1:
                        stater?.SaveJson(dialog.FileName);
                        break;
                    case 2:
                        state3?.SaveJson(dialog.FileName);
                        break;
                }
            }
        }

        /// <summary>
        /// Загрузка файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Title = "Загрузка набора фигур",
                Filter = "Файлы (*.json)|*.json|Все файлы (*.*)|*.*"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Puzzles.State s = Core.LoadJson<Puzzles.State>(dialog.FileName);
                if (s.ClassName == typeof(Puzzles.Sudoku.State).FullName)
                {
                    state = Core.LoadJson<Puzzles.Sudoku.State>(dialog.FileName);
                    state.InitLines();
                    InitSudokuPanel(tabSudoku, state);
                }
                else if (s.ClassName == typeof(Puzzles.Routing.State).FullName)
                {
                    stater = Core.LoadJson<Puzzles.Routing.State>(dialog.FileName);
                    InitRoutingPanel(tabRouting, stater);
                }
                else if (s.ClassName == typeof(Puzzles.Sudoku.State3).FullName)
                {
                    state3 = Core.LoadJson<Puzzles.Sudoku.State3>(dialog.FileName);
                    InitTrianglePanel(tabSudoku, state3);
                }
                else
                {
                    throw new Exception();
                }
                string name = System.IO.Path.GetFileName(dialog.FileName);
                statusLabel.Text = $"Файл {name} загружен успешно";
            }
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

        #endregion

        #region "Начало игры"

        /// <summary>
        /// Переключение в режим судоку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sudokuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabs.SelectTab(tabSudoku);
            state = new Puzzles.Sudoku.State(9, 9, 3);
            InitSudokuPanel(tabSudoku, state);
        }

        /// <summary>
        /// Переключение в режим маршрутизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void routingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabs.SelectTab(tabRouting);
            stater = new Puzzles.Routing.State(6, 4, 2);
            InitRoutingPanel(tabRouting, stater);
        }

        /// <summary>
        /// Переключение в режим треугольного судоку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabs.SelectTab(tabTriangle);
            state3 = new Puzzles.Sudoku.State3(5, 9);
            InitTrianglePanel(tabTriangle, state3);
        }

        #endregion

        #region "Инициализация интерфейса"

        /// <summary>
        /// Инициализация интерфейса игрового поля судоку
        /// </summary>
        /// <param name="parent"></param>
        private void InitSudokuPanel(Control parent, Puzzles.Sudoku.State state)
        {
            // Инициализация глобальных переменных
            parent.Controls.Clear();
            buttons.Clear();
            parent.Tag = state;

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
                            cell.ValueChanged += CellValueChanged;
                            button.Click += SudokuButtonClick;
                            panel.Controls.Add(button);
                            buttons.Add(cell, button);
                            // для обновления интерфейса после добавления в словарь
                            cell.Number = cell.Number;
                        }
                    }
                    parent.Controls.Add(panel);
                }
            }

            tabs.SelectTab(tabSudoku);
        }

        /// <summary>
        /// Инициализация интерфейса маршрутизации
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="state"></param>
        private void InitRoutingPanel(Control parent, Puzzles.Routing.State state)
        {
            // Инициализация глобальных переменных
            parent.Controls.Clear();
            dict.Clear();
            parent.Tag = state;

            // количество ячеек
            int cellCountX = state.SizeX;
            int cellCountY = state.SizeY;
            // размер ячеек, учитывая граничные ячейки
            int cellSize = Math.Min(parent.Width / (cellCountX + 2), parent.Height / (cellCountY + 2));

            TileControl button;
            for (int x = 0; x < cellCountX; x++)
            {
                // Верхняя граница игрового поля
                button = new TileControl()
                {
                    Left = (x + 1) * cellSize,
                    Top = 0,
                    Width = cellSize,
                    Height = cellSize,
                    Cell = state[Side.Top, x],
                    Colors = state.Colors
                };
                parent.Controls.Add(button);

                // Нижняя граница игрового поля
                button = new TileControl()
                {
                    Left = (x + 1) * cellSize,
                    Top = (cellCountY + 1) * cellSize,
                    Width = cellSize,
                    Height = cellSize,
                    Cell = state[Side.Bottom, x],
                    Colors = state.Colors

                };
                parent.Controls.Add(button);

                // Игровое поле
                for (int y = 0; y < cellCountY; y++)
                {
                    button = new TileControl()
                    {
                        Left = (x + 1) * cellSize,
                        Top = (y + 1) * cellSize,
                        Width = cellSize,
                        Height = cellSize,
                        Cell = state.Field[x][y],
                        Colors = state.Colors
                    };
                    dict.Add(button.Cell, button);
                    state.Field[x][y].ValueChanged += RoutingTile_ValueChanged;
                    parent.Controls.Add(button);
                }
            }

            for (int y = 0; y < cellCountY; y++)
            {
                // Левая граница игрового поля
                button = new TileControl()
                {
                    Left = 0,
                    Top = (y + 1) * cellSize,
                    Width = cellSize,
                    Height = cellSize,
                    Cell = state[Side.Left, y],
                    Colors = state.Colors
                };
                parent.Controls.Add(button);

                // Правая граница игрового поля
                button = new TileControl()
                {
                    Left = (cellCountX + 1) * cellSize,
                    Top = (y + 1) * cellSize,
                    Width = cellSize,
                    Height = cellSize,
                    Cell = state[Side.Right, y],
                    Colors = state.Colors
                };
                parent.Controls.Add(button);
            }

            tabs.SelectTab(tabRouting);
        }

        /// <summary>
        /// Инициализация интерфейса треугольного судоку
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="state"></param>
        private void InitTrianglePanel(Control parent, Puzzles.Sudoku.State3 state)
        {
            // Инициализация глобальных переменных
            parent.Controls.Clear();
            triangles.Clear();
            parent.Tag = state;

            double rate = Math.Sqrt(3) / 2;

            // размер ячеек, учитывая граничные ячейки
            int width = parent.Width / state.Size;
            int height = (int)(parent.Height / state.Size / rate);
            width = Math.Min(width, height);
            height = (int)(width * rate);
            for (int x = 0; x < state.Size; x++)
            {
                for (int y = 0; y < state.Size - x; y++)
                {
                    for (int z = 0; z <= 1; z++)
                    {
                        // пропуск верхнего треугольника за границей
                        if (x + y + z >= state.Size) continue;

                        var control = new TriangleControl()
                        {
                            BackColor = Color.Transparent,
                            Left = (int)((x + y / 2.0 + z / 2.0) * width),
                            Top = height * (state.Size - y - 1),
                            Width = width,
                            Height = height,
                            Up = z == 0,
                            Tag = state3.Cells?[x][y][z]
                        };
                        state3.Cells[x][y][z].ValueChanged += TriangleValueChanged;
                        control.Click += SudokuTriangleClick;
                        triangles.Add(state3.Cells[x][y][z], control);
                        parent.Controls.Add(control);
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Нажатие кнопки судоку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void SudokuButtonClick(object? sender, EventArgs e)
        {
            if (sender is Control control && control.Tag is Puzzles.Sudoku.Cell cell && tabs.SelectedTab.Tag is Puzzles.Sudoku.State state)
            {
                int number = (cell.Number + 1) % state.Images.Length;
                control.BackColor = Control.DefaultBackColor;
                if (!initButton.Checked && number > 0) // проверка на корректность хода
                {
                    var move = new Puzzles.Sudoku.Move(cell, number);
                    control.BackColor = state.PossibleMove(move) ? Color.LightGreen : Color.Orange;
                }
                cell.Fixed = initButton.Checked;
                cell.Number = number;
            }
        }

        /// <summary>
        /// Нажатие треугольника судоку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void SudokuTriangleClick(object? sender, EventArgs e)
        {
            if (sender is Control control && control.Tag is Puzzles.Sudoku.Cell cell && tabs.SelectedTab.Tag is Puzzles.Sudoku.State3 state)
            {
                int number = (cell.Number + 1) % state.Images.Length;
                if (!initButton.Checked && number > 0) // проверка на корректность хода
                {
                    var move = new Puzzles.Sudoku.Move(cell, number);
                    // state.PossibleMove(move) ? Color.LightGreen : Color.Orange;
                }
                cell.Fixed = initButton.Checked;
                cell.Number = number;
            }
        }

        /// <summary>
        /// Перерисовка клетки маршрутизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="tile"></param>
        private void RoutingTile_ValueChanged(object sender, Tile? tile)
        {
            if (sender is Puzzles.Routing.Cell cell && dict.TryGetValue(cell, out TileControl control))
            {
                control.Invalidate();
            }
        }

        /// <summary>
        /// Изменение значения клетки судоку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="number"></param>
        private void CellValueChanged(object sender, int number)
        {
            if (sender is Puzzles.Sudoku.Cell cell && buttons.TryGetValue(cell, out Button? button))
            {
                if (button != null)
                {
                    button.Text = state.Images[number];
                    // разрешаем редактирование клетки при начальной расстановке
                    // или если клетка не фиксированная (не условие задачи)
                    button.Enabled = initButton.Checked || !cell.Fixed;
                }
            }
        }

        /// <summary>
        /// Изменение значения треугольной клетки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="number"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void TriangleValueChanged(object sender, int number)
        {
            if (sender is Puzzles.Sudoku.Cell cell && triangles.TryGetValue(cell, out TriangleControl control))
            {
                control.Text = state3.Images[cell.Number];
            }
        }

        /// <summary>
        /// Решение головоломки 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void solveButton_Click(object sender, EventArgs e)
        {
            switch (tabs.SelectedIndex)
            {
                case 0:
                    state.Solve();
                    break;
                case 1:
                    if (stater.TileSet.Count == 0)
                    {
                        stater.InitTileSet();
                    }
                    stater.Solve();
                    break;
                case 2:
                    // state3.Solve();
                    break;
            }
        }

        /// <summary>
        /// Проверка судоку на корректность
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validateButton_Click(object sender, EventArgs e)
        {
            foreach (var kv in buttons)
            {
                var move = new Puzzles.Sudoku.Move(kv.Key, kv.Key.Number);
                kv.Value.BackColor = state.PossibleMove(move) ? Color.LightGreen : Color.Orange;
            }
        }
    }
}