using System.ComponentModel;

namespace PuzzleSolver.Puzzles.Coverage
{
    /// <summary>
    /// Параметры головоломки
    /// </summary>
    public class BaseState : RectangularState
    {
        [DisplayName("Количество символов")]
        public int Chars { get; set; }
    }
}
