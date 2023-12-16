using System.Collections.Generic;

namespace CodeGeneration.Scripts;

/// <summary>
/// Список SQL-ограничений 
/// </summary>
public static class Constraints
{
    /// <summary>
    /// Список SQL-ограничений
    /// </summary>
    /// <returns></returns>
    public static List<string> GetList()
    {
        var list = new List<string>()
        {
                "Constraint_LineShift",
                "Constraint_Operation",
                "Constraint_Plant",
                "Constraint_ShiftType",
        };
        return list;
    }
}
 