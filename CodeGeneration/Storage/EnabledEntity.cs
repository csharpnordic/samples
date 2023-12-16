using System.ComponentModel;

namespace CodeGeneration.Storage;

/// <summary>
/// Сущность с признаком активности
/// </summary>
public abstract class EnabledEntity : Entity
{
    /// <summary>
    /// Признак активности сущности
    /// </summary>
    [DisplayName("{'ru':'Вкл.','en':'Enabled'}")]
    [Comment("{'ru':'Признак доступности объекта. Доступный объект доступен для выбора пользователями, недоступный объект виден только при редактировании мастер-данных',"
        + "'en':'Enabled object is available for all users, disabled object is available only for users allowed to change master data'}")]
    [Attributes.FilteredField()]
    public bool Enabled { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    public EnabledEntity()
    {
        // По умолчанию любой объект является активным
        Enabled = true;
    }
}
