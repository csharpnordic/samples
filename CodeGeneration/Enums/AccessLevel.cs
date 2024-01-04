namespace CodeGeneration.Enums;

/// <summary>
/// Уровень доступа
/// </summary>
public enum AccessLevel
{
    /// <summary>
    /// Нет доступа
    /// </summary>
    None,
    /// <summary>
    /// Чтение
    /// </summary>
    ReadOnly,
    /// <summary>
    /// Чтение и запись
    /// </summary>
    ReadWrite,
    /// <summary>
    /// Чтение, запись, удаление
    /// </summary>
    FullAccess
}