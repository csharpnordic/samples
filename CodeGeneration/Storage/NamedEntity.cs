namespace CodeGeneration.Storage;

/// <summary>
/// Именованная сущность
/// </summary>
public abstract class NamedEntity : EnabledEntity
{
    /// <summary>
    /// Наименование сущности
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Строковое представление объекта
    /// <para>Отсекаем начальные и конечные пробелы</para>
    /// </summary>
    /// <returns></returns>
    public override string ToString() => Name?.Trim();
}
