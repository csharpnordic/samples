using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Interfaces
{
    /// <summary>
    /// Дополнительная инициализация объекта после десериализации
    /// </summary>
    public interface ILoad
    {
        /// <summary>
        /// Инициализация объекта после десериализации
        /// </summary>
        public void InitAfterLoad();
    }
}
