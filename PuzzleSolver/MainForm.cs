using PuzzleSolver.Controls;
using PuzzleSolver.Extenders;
using PuzzleSolver.Interfaces;
using PuzzleSolver.Puzzles;
using PuzzleSolver.Puzzles.Routing;
using System.Reflection;

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
        private Interfaces.IState istate;

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
                istate?.SaveJson(dialog.FileName);
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
                // Предварительное чтение файла для определения имени класса
                Puzzles.State s = Core.LoadJson<Puzzles.State>(dialog.FileName);
                // Поиск типа данных по названию
                var type = Assembly.GetExecutingAssembly().GetType(s.ClassName);
                // Загрузка данных
                istate = Core.LoadJson(dialog.FileName, type) as Interfaces.IState;

                if (istate is Puzzles.Sudoku.State state1)
                {
                    state1.InitLines();
                    InitSudokuPanel(panel, state1);
                    checkMenuItem(sudokuToolStripMenuItem);
                }
                else if (istate is Puzzles.Routing.State state2)
                {
                    InitRoutingPanel(panel, state2);
                    checkMenuItem(routingToolStripMenuItem);
                }
                else if (istate is Puzzles.Sudoku.State3 state3)
                {
                    state3.InitLines();
                    InitTrianglePanel(panel, state3);
                    checkMenuItem(triangleToolStripMenuItem);
                }
                else if (istate is Puzzles.Routing.StateT state4)
                {
                    InitTrafficPanel(panel, state4);
                    checkMenuItem(trafficToolStripMenuItem);
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

        /// <summary>
        /// Выделить заданный пункт меню
        /// <para>Снять отметку с остальных пунктов</para>
        /// </summary>
        /// <param name="checkItem"></param>
        private void checkMenuItem(object checkItem)
        {
            foreach (ToolStripMenuItem item in puzzleToolStripMenuItem.DropDownItems)
            {
                item.Checked = item == checkItem;
            }
        }

        #region "Начало игры"

        /// <summary>
        /// Переключение в режим судоку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sudokuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            istate = new Puzzles.Sudoku.State(9, 9, 3);
            InitSudokuPanel(panel, istate as Puzzles.Sudoku.State);
            sudokuToolStripMenuItem.Checked = true;
            checkMenuItem(sender);
        }

        /// <summary>
        /// Переключение в режим маршрутизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void routingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            istate = new Puzzles.Routing.State(6, 4, 2);
            InitRoutingPanel(panel, istate as Puzzles.Routing.State);
            checkMenuItem(sender);
        }

        /// <summary>
        /// Переключение в режим треугольного судоку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            istate = new Puzzles.Sudoku.State3(5, 9);
            InitTrianglePanel(panel, istate as Puzzles.Sudoku.State3);
            checkMenuItem(sender);
        }

        /// <summary>
        /// Переключение в режим построения маршрута
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trafficToolStripMenuItem_Click(object sender, EventArgs e)
        {
            istate = new Puzzles.Routing.StateT(19, 15);
            InitTrafficPanel(panel, istate as Puzzles.Routing.StateT);
            checkMenuItem(sender);
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

                        var cell = state.Cells[x][y][z];
                        var control = new TriangleControl()
                        {
                            BackColor = Color.Transparent,
                            Left = (int)((x + y / 2.0 + z / 2.0) * width),
                            Top = height * (state.Size - y - 1),
                            Width = width,
                            Height = height,
                            Up = z == 0,
                            Tag = cell,
                            Text = state.Images[cell.Number]
                        };
                        cell.ValueChanged += TriangleValueChanged;
                        control.Click += SudokuTriangleClick;
                        triangles.Add(cell, control);
                        parent.Controls.Add(control);
                    }
                }
            }
        }

        /// <summary>
        /// Инициализация интерфейса поиска маршрута
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="state"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void InitTrafficPanel(Panel parent, Puzzles.Routing.StateT state)
        {
            // Инициализация глобальных переменных
            parent.Controls.Clear();
            dict.Clear();

            // размер ячеек
            int cellSize = Math.Min(parent.Width / state.SizeX, parent.Height / state.SizeY);

            TileControl button;

            // Игровое поле
            for (int x = 0; x < state.SizeX; x++)
            {
                for (int y = 0; y < state.SizeY; y++)
                {
                    button = new TileControl()
                    {
                        Left = x * cellSize,
                        Top = y * cellSize,
                        Width = cellSize,
                        Height = cellSize,
                        Cell = state.Field[x][y],
                        Colors = 1,
                        DrawHouses = true

                    };
                    dict.Add(button.Cell, button);
                    state.Field[x][y].ValueChanged += RoutingTile_ValueChanged;
                    parent.Controls.Add(button);
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
            if (sender is Control control && control.Tag is Puzzles.Sudoku.Cell cell && istate is Puzzles.Sudoku.State state)
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
            if (sender is Control control && control.Tag is Puzzles.Sudoku.Cell cell && istate is Puzzles.Sudoku.State3 state)
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
                    button.Text = ((Puzzles.Sudoku.State)istate).Images[number];
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
                control.Text = ((Puzzles.Sudoku.State3)istate).Images[cell.Number];
            }
        }

        /// <summary>
        /// Решение головоломки 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void solveButton_Click(object sender, EventArgs e)
        {
            if (istate is Puzzles.Routing.State state)
            {
                state.InitTileSet();
            }
            istate.Solve();
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
                kv.Value.BackColor = ((Puzzles.Sudoku.State)istate).PossibleMove(move) ? Color.LightGreen : Color.Orange;
            }
        }
    }
}