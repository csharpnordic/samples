using CodeGeneration.Attributes;
using CodeGeneration.Storage;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Источник данных (внешняя система)
/// </summary>
[DisplayName("{'ru':'Источники данных','en':'Data sources'}")]
[Comment("Источники данных")]
[Table("SourceSystems", Schema = DB.SchemaName)]
[MasterData(CodeGeneration.Storage.Administration.SubSystem.GTS)]
public class SourceSystem : NamedEntity
{
    /// <summary>
    /// Драйвер
    /// </summary>
    [DisplayName("{'ru':'Драйвер','en':'Driver'}")]
    [Comment("Драйвер")]
    [ForeignKey(nameof(DriverID))]
    [FilteredField]
    [RequiredField]
    public virtual Driver Driver { get; set; }

    /// <summary>
    /// Идентификатор драйвера
    /// </summary>
    [DisplayName("{'ru':'Идентификатор драйвера','en':'Driver id'}")]
    [Comment("Идентификатор драйвера")]
    [Column("Driver_ID")]
    public Guid DriverID { get; set; }

    /// <summary>
    /// Сервер 
    /// </summary>       
    [DisplayName("{'ru':'Сервер','en':'Host name'}")]
    [Comment("Сервер")]
    [MaxLength(100)]
    [RequiredField]
    public string HostName { get; set; }

    /// <summary>
    /// Учётное имя 
    /// </summary>       
    [DisplayName("{'ru':'Учётное имя','en':'Username'}")]
    [Comment("Учётное имя")]
    [MaxLength(100)]
    [RequiredField]
    public string Username { get; set; }

    /// <summary>
    /// Пароль 
    /// </summary>       
    [DisplayName("{'ru':'Пароль','en':'Password'}")]
    [Comment("Пароль")]
    [MaxLength(100)]
    public string? Password { get; set; }
}