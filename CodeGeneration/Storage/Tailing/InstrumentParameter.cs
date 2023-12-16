using CodeGeneration.Attributes;
using CodeGeneration.Interfaces;
using CodeGeneration.Storage;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Параметры КИА
/// </summary>
[DisplayName("{'ru':'Параметры КИА','en':'Instrument parameters'}")]
[Comment("Параметры КИА")]
[Table("InstrumentParameters", Schema = DB.SchemaName)]
[UpLink(typeof(Instrument))]
public  class InstrumentParameter : Entity, ILinkedEntity
{
    /// <summary>
    /// Идентификатор типа параметра
    /// </summary>
    [DisplayName("{'ru':'Идентификатор типа параметра','en':'Instruments type parameter ID'}")]
    [Comment("Идентификатор типа параметра")]
    [Column("InstrumentParameterType_ID")]
    public Guid? InstrumentParameterTypeID { get; set; }

    /// <summary>
    /// Тип параметра КИА
    /// </summary>
    [DisplayName("{'ru':'Тип параметра КИА','en':'Instruments type parameter'}")]
    [Comment("Тип параметра КИА")]
    [ForeignKey(nameof(InstrumentParameterTypeID))]
    [FilteredField]
    [RequiredField]
    public virtual InstrumentParameterType InstrumentParameterType { get; set; }

    /// Идентификатор КИА
    /// </summary>
    [DisplayName("{'ru':'Идентификатор КИА','en':'Instrument ID'}")]
    [Comment("Идентификатор КИА")]
    [Column("Instrument_ID")]
    public Guid InstrumentID { get; set; }

    /// <summary>
    /// КИА
    /// </summary>
    [DisplayName("{'ru':'КИА','en':'Instrument'}")]
    [Comment("КИА")]
    [ForeignKey(nameof(InstrumentID))]
    [FilteredField]
    [RequiredField]
    public virtual Instrument Instrument { get; set; }

    /// <summary>
    /// Журналы параметров КИА
    /// </summary>
    public virtual HashSet<InstrumentParameterHistory> ParameterHistory { get; set; } = new HashSet<InstrumentParameterHistory>();

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

    /// <inheritdoc/>
    [Display(AutoGenerateField = false)]
    public string LinkedID => nameof(InstrumentID);
}
