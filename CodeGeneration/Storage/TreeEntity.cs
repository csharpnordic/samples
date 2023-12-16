namespace CodeGeneration.Storage;

/// <summary>
/// Иерархическая сущность
/// <para>Сущность образовывает лес деревьев</para>
/// </summary>
public class TreeEntity : NamedEntity
{
    /// <summary>
    /// Идентификатор родителя
    /// <para>null - корневой элемент</para>
    /// </summary>
    public Guid? ParentID { get; set; }
}
