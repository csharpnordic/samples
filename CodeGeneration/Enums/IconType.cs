using System.ComponentModel;

namespace CodeGeneration.Enums;

/// <summary>
/// Значки состояний смены (плана/отчёта)
/// </summary>
public enum IconType
{
    /// <summary>
    /// Не определено - отсутствие символа
    /// </summary>
    None,
    /// <summary>
    /// Сделано - Галочка (v)
    /// </summary>
    Done,
    /// <summary>
    /// В процессе - Шестеренка (*)
    /// </summary>  
    InProcess,
    /// <summary>
    /// Внимание - желтый восклицательный знак (!)
    /// </summary> 
    AttentionYellow,
    /// <summary>
    /// Не начато - красный крестик (x)
    /// </summary>
    CrossRed,
    /// <summary>
    /// Не начато - серый крестик (x)
    /// </summary>
    CrossGray,
    /// <summary>
    ///  Внимание - красный восклицательный знак (!)
    /// </summary>
    AttentionRed
}
