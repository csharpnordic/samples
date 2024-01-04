namespace CodeGeneration.Enums;

/// <summary>
/// Возможные варианты даты для отчёта
/// </summary>
public enum DateType
{
    /// <summary>
    /// Не задано
    /// </summary>
    None,
    /// <summary>
    /// Вчера
    /// </summary>
    Yesterday ,
    /// <summary>
    /// Сегодня
    /// </summary>
    Today,
    /// <summary>
    /// Начало текущей недели
    /// </summary>
    BeginOfWeek,
    /// <summary>
    /// Начало предыдущей недели
    /// </summary>
    BeginOfPreviousWeek,
    /// <summary>
    /// Начало текущего месяца
    /// </summary>
    BeginOfMonth,
    /// <summary>
    /// Начало предыдущего месяца
    /// </summary>
    BeginOfPreviousMonth
}
