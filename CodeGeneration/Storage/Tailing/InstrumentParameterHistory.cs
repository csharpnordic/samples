using CodeGeneration.Attributes;
using CodeGeneration.Storage;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Журнал параметров КИА
/// </summary>
[DisplayName("{'ru':'Журнал параметров КИА','en':'Instrument parameter history'}")]
[Comment("Журнал параметров КИА")]
[Table("ParameterHistory", Schema = DB.SchemaName)]
public class InstrumentParameterHistory : Entity
{
    /// <summary>
    /// Идентификатор параметра КИА
    /// </summary>
    [DisplayName("{'ru':'Идентификатор параметра КИА','en':'Instruments parameter ID'}")]
    [Comment("Идентификатор параметра КИА")]
    [Column("InstrumentParameter_ID")]
    public Guid InstrumentParameterID { get; set; }

    /// <summary>
    /// Параметры КИА
    /// </summary>
    [DisplayName("{'ru':'Параметры КИА','en':'Instrument parameters'}")]
    [Comment("Параметры КИА")]
    [ForeignKey(nameof(InstrumentParameterID))]
    [FilteredField]
    [Required()]
    public virtual InstrumentParameter InstrumentParameter { get; set; }

    /// <summary>
    /// Строковое значение 
    /// </summary>       
    [DisplayName("{'ru':'Строковое значение','en':'String Value'}")]
    [Comment("Строковое значение")]
    [MaxLength(500)]
    public string? StrValue { get; set; }

    /// <summary>
    /// Числовое значение 
    /// </summary>       
    [DisplayName("{'ru':'Числовое значение','en':'Numeric Value'}")]
    [Comment("Числовое значение")]
    public double? DblValue { get; set; }

    /// <summary>
    /// Метка времени начала действия значения параметра
    /// </summary>
    [DisplayName("{'ru':'Начало действия','en':'Start time'}")]
    [Comment("Метка времени начала действия значения параметра")]
    public DateTime StartTime { get; set; } = DateTime.Now;
}
