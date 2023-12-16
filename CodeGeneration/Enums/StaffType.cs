namespace CodeGeneration.Enums;

/// <summary>
/// Тип персонала
/// </summary>
public enum StaffType
{
    /// <summary>
    /// Тип не определён
    /// </summary>
    None,
    /// <summary>
    /// Технический персонал линии
    /// </summary>
    Technician,
    /// <summary>
    /// Администратор системы
    /// </summary>
    Administrator,
    /// <summary>
    /// Начальник смены
    /// </summary>
    Supervisor,
    /// <summary>
    /// Оператор линии
    /// </summary>
    Operator,
    /// <summary>
    /// Эксперт
    /// </summary>
    Expert,
    /// <summary>
    /// Начальник планового отдела
    /// </summary>
    SuperPlanner
}
