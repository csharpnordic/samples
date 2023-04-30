using PuzzleSolver.Puzzles.Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Puzzles
{
    /// <summary>
    /// Решатель головоломок
    /// </summary>
    public class Solver
    {
        public State? Solve(State state)
        {
            var moves = state.GetMoves();
            foreach (var move in moves)
            {
                state.Log();
                var newState = state.Move(move);
                // return newState;

                if (!newState.Done())
                {
                    return Solve(newState);
                }
                else
                {
                    return newState;
                }
            }
            return null;
        }
    }
}
