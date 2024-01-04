namespace CodeGeneration.Storage;

/// <summary>
/// Сущность с признаком активности
/// </summary>
public abstract class EnabledEntity : Entity
{
    /// <summary>
    /// Признак активности сущности
    /// </summary>
    public bool Enabled { get; set; } = true;   
}