using PuzzleSolver.Puzzles;
using PuzzleSolver.Puzzles.Routing;
using System.Data;

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
        private readonly static Brush[] Brush = new Brush[2] 
        { 
            new SolidBrush(Color.Cyan),
            new SolidBrush(Color.Blue)
        };

        /// <summary>
        /// Возможное количество цветов
        /// </summary>
        public int Colors { get; set; }

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

                UpdateColor();
                Invalidate();
            }
        }

        /// <summary>
        /// Признак отрисовки зданий для задачи маршрута
        /// </summary>
        public bool DrawHouses { get; set; }

        /// <summary>
        /// Беспараметрический конструктор
        /// </summary>
        public TileControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обновление фонового цвета компонента
        /// </summary>
        private void UpdateColor()
        {
            // автоматическое определение цвета
            if (cell.Border)
                BackColor = Color.DarkGray;
            else if (cell.Fixed)
                BackColor = Color.LightGray;
            else
                BackColor = Color.White;
        }

        /// <summary>
        /// Отрисовка домов вокруг дороги
        /// <para>Вся плитка разбивается на квадраты 3*3</para>
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="x">Относительная координата по горизонтали</param>
        /// <param name="y">Относительная координата по вертикали</param>
        private void DrawHouse(Graphics graphics, int x, int y)
        {
            var brush = new SolidBrush(Color.DarkGreen);
            // размеры квадрата
            int sx = Width / 3;
            int sy = Height / 3;
            graphics.FillRectangle(brush, x * sx, y * sy, sx, sy);
        }

        /// <summary>
        /// Визуализауция плитки
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Если плитка не задана, то и рисовать нечего
            if (Cell?.Tile == null) return;

            // Рисуем четыре стороны плитки
            foreach (var side in Enum.GetValues<Side>().Where(x => x != Side.None))
            {
                int x = 0, y = 0;
                int dx = 0, dy = 0;
                // размеры по умолчанию
                int sx = Width / Rate;
                int sy = Height / Rate;

                switch (side)
                {
                    case Side.Up:
                        x = (Width - Width * Colors / Rate) / 2;
                        dx = Width / Rate;
                        y = 0;
                        dy = 0;
                        sy = Cell.Border ? Height / Rate : Height / 2;
                        break;

                    case Side.Down:
                        x = (Width - Width * Colors / Rate) / 2;
                        dx = Width / Rate;
                        y = Cell.Border ? Height * (Rate - 1) / Rate : Height / 2;
                        dy = 0;
                        sy = Cell.Border ? Height / Rate : Height / 2;
                        break;

                    case Side.Left:
                        x = 0;
                        dx = 0;
                        y = (Height - Height * Colors / Rate) / 2;
                        dy = Height / Rate;
                        sx = Cell.Border ? Width / Rate : Width / 2;
                        break;

                    case Side.Right:
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

            if (DrawHouses)
            {
                DrawHouse(e.Graphics, 0, 0);
                DrawHouse(e.Graphics, 0, 2);
                DrawHouse(e.Graphics, 2, 0);
                DrawHouse(e.Graphics, 2, 2);
                if (Cell.Tile[Side.Up] == 0)
                    DrawHouse(e.Graphics, 1, 0);
                if (Cell.Tile[Side.Down] == 0)
                    DrawHouse(e.Graphics, 1, 2);
                if (Cell.Tile[Side.Left] == 0)
                    DrawHouse(e.Graphics, 0, 1);
                if (Cell.Tile[Side.Right] == 0)
                    DrawHouse(e.Graphics, 2, 1);
                if (cell.Tile.Pipe.Max() == 0)
                    DrawHouse(e.Graphics, 1, 1);
            }

            // Помеченная клетка
            if (Cell.Mark)
            {
                var blue = new SolidBrush(Color.Navy);
                e.Graphics.FillRectangle(blue, Width / 3, Height / 3, Width / 3, Height / 3);
            }

            // Стартовая/финишная точка
            Color color;
            switch (Cell.Type)
            {
                case CellType.Start: color = Color.Orange; break;
                case CellType.Finish: color = Color.Red; break;
                default: color = Color.Transparent; break;
            }
            if (color != Color.Transparent)
            {
                var brush = new SolidBrush(color);
                e.Graphics.FillEllipse(brush, Width / 3, Height / 3, Width / 3, Height / 3);
            }

            base.OnPaint(e);
        }

        /// <summary>
        /// Обработка щелчка мышкой
        /// </summary>
        /// <param name="me"></param>
        protected override void OnMouseClick(MouseEventArgs me)
        {
            bool border = Cell?.Border ?? false; // граничная клетка
            switch (me.Button)
            {
                case MouseButtons.Left:
                    Side side;
                    if (border) // координаты щелчка неважны
                    {
                        side = Cell.Tile.Side;
                    }
                    else
                    {
                        // правый верхний треугольник
                        bool rT = me.X > me.Y;
                        // левый верхний треугольник
                        bool lT = me.X + me.Y < Math.Min(Width, Height);

                        if (lT)
                            side = rT ? Side.Up : Side.Left;
                        else
                            side = rT ? Side.Right : Side.Down;
                    }
                    Cell.Tile[side] = (Cell.Tile[side] + 1) % (int)Math.Pow(2, Colors);
                    break;

                case MouseButtons.Right: // изменение фиксированности клетки
                    if (!border)
                    {
                        Cell.Fixed = !Cell.Fixed;
                        UpdateColor();
                    }
                    break;

                case MouseButtons.Middle: // задание стартовой / финишной точки
                    if (!border)
                    {
                        Cell.Type = (CellType)(((int)Cell.Type + 1) % Enum.GetValues<CellType>().Length);
                    }
                    break;
            }

            Invalidate();
        }
    }
}
