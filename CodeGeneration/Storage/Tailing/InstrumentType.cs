using CodeGeneration.Attributes;
using CodeGeneration.Interfaces;
using CodeGeneration.Storage;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Типы КИА
/// </summary>
[DisplayName("{'ru':'Типы КИА','en':'Instrument types'}")]
[Comment("Типы КИА")]
[Table("InstrumentTypes", Schema = DB.SchemaName)]
[MasterData(CodeGeneration.Storage.Administration.SubSystem.GTS)]
[DownLink(typeof(InstrumentTypeDetail), typeof(InstrumentParameterType))]
[ParentEntity(typeof(MonitoringType))]
public class InstrumentType : NamedEntity
{
    /// <summary>
    /// Описание 
    /// </summary>       
    [DisplayName("{'ru':'Описание','en':'Description'}")]
    [Comment("Описание")]
    public string? Description { get; set; }

    /// <summary>
    /// Код типа КИА
    /// </summary>
    [MaxLength(CodeLength)]
    [Required]
    [DisplayName("{'ru':'Код','en':'Code'}")]
    [Comment("Код типа КИА")]
    public string? Code { get; set; }

    /// <summary>
    /// Признак журналирования
    /// </summary>       
    [DisplayName("{'ru':'Признак журналирования','en':'IsLoggable'}")]
    [Comment("Признак журналирования")]
    public bool Loggable { get; set; }

    /// <summary>
    /// Признак возможности нескольких измерений на одну и ту же метку времени
    /// </summary>       
    [DisplayName("{'ru':'Несколько измерений','en':'Multi-Measurement capability'}")]
    [Comment("Признак возможности нескольких измерений на одну и ту же метку времени")]
    public bool MultipleMeasurements  { get; set; }

    /// <summary>
    /// Идентификатор вида наблюдения
    /// </summary>
    [DisplayName("{'ru':'Идентификатор вида наблюдения','en':'Type of observation id'}")]
    [Comment("Идентификатор вида наблюдения")]
    [Column("MonitoringType_ID")]
    public Guid? MonitoringTypeID { get; set; }

    /// <summary>
    /// Вид наблюдения
    /// </summary>
    [DisplayName("{'ru':'Вид наблюдения','en':'Type of observation'}")]
    [Comment("Вид наблюдения")]
    [ForeignKey(nameof(MonitoringTypeID))]
    [FilteredField]
    public virtual MonitoringType? MonitoringType { get; set; }

    /// <summary>
    /// Измерения типа инструмента
    /// </summary>
    public virtual HashSet<InstrumentTypeDetail> Details { get; set; }

    /// <summary>
    /// Параметры типа инструмента
    /// </summary>
    public virtual HashSet<InstrumentParameterType> Parameters { get; set; }
}
