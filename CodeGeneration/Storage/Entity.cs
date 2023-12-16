using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGeneration.Storage;

/// <summary>
/// Корневая сущность базы данных
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Уникальный идентификатор записи
    /// </summary>
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public Guid ID { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Метка времени создания объекта
    /// </summary>
    public DateTime Created { get; set; } = DateTime.Now;

    /// <summary>
    /// Метка времени обновления объекта
    /// </summary>
    public DateTime Modified { get; set; } = DateTime.Now;
}
