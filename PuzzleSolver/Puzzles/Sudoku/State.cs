using PuzzleSolver.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles.Sudoku
{
    /// <summary>
    /// Состояние игрового поля
    /// </summary>
    public class State : IState
    {
        /// <summary>
        /// Размер игрового поля по горизонтали
        /// </summary>
        public int SizeX { get; set; }
        /// <summary>
        /// Размер игрового поля по вертикали
        /// </summary>
        public int SizeY { get; set; }
        /// <summary>
        /// Размер квадрата
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// Игровое поле
        /// </summary>
        public Cell[][] Cells { get; set; }
        /// <summary>
        /// Группировки клеток поля для проверки уникальности
        /// <para>Горизонтали, диагонали, квадраты <seealso cref="Size"/>*<seealso cref="Size"/></para>
        /// </summary>
        public List<Cell[]> Lines { get; set; }

        /// <summary>
        /// Беспараметрический конструктор для сериализации
        /// </summary>
        public State() { }

        /// <summary>
        /// Конструктор по размеру игрового поля
        /// </summary>
        /// <param name="sizeX">Размер игрового поля по горизонтали</param>
        /// <param name="sizeY">Размер игрового поля по вертикали</param>
        /// <param name="size">Размер квадрата</param>
        public State(int sizeX, int sizeY, int size)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Size = size;
            Cells = new Cell[SizeX][];

            for (int x = 0; x < SizeX; x++)
            {
                Cells[x] = new Cell[SizeY];
                for (int y = 0; y < SizeY; y++)
                {
                    Cells[x][y] = new Cell();
                }
            }

            // Список групп клеток
            Lines = new List<Cell[]>();

            // Горизонтали
            for (int y = 0; y < SizeY; y++)
            {
                Cell[] cells = new Cell[SizeX];
                for (int x = 0; x < SizeX; x++)
                {
                    cells[x] = Cells[x][y];
                }
                Lines.Add(cells);
            }

            // Вертикали
            for (int x = 0; x < SizeX; x++)
            {
                Cell[] cells = new Cell[SizeY];
                for (int y = 0; y < SizeY; y++)
                {
                    cells[y] = Cells[x][y];
                }
                Lines.Add(cells);
            }

            // Группы клеток Size*Size
            for (int x = 0; x < SizeX; x += Size)
            {
                for (int y = 0; y < SizeY; y += Size)
                {
                    Cell[] cells = new Cell[Size * Size];
                    int index = 0;
                    for (int dx = 0; dx < Size; dx++)
                    {
                        for (int dy = 0; dy < Size; dy++)
                        {
                            cells[index++] = Cells[x + dx][y + dy];
                        }
                    }
                    Lines.Add(cells);
                }
            }
        }
    }
}