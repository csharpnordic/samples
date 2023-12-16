using CodeGeneration.Attributes;
using CodeGeneration.Interfaces;
using CodeGeneration.Storage;
using CodeGeneration.Storage.Administration;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Настройки измерений
/// <para><seealso cref="NamedEntity.Name"/> - Имя тега - источника данных</para>
/// </summary>

public class InstrumentDetail : NamedEntity
{
    /// Идентификатор КИА
    /// </summary>
    [DisplayName("{'ru':'Идентификатор КИА','en':'Instrument id'}")]
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
    public virtual Instrument Instrument { get; set; }

    /// <summary>
    /// Идентификатор типа измерения
    /// </summary>
    [DisplayName("{'ru':'Идентификатор типа измерения','en':'Instrument types detail id'}")]
    [Comment("Идентификатор типа измерения")]
    [Column("InstrumentTypeDetail_ID")]
    public Guid InstrumentTypeDetailID { get; set; }

    /// <summary>
    /// Измерения по типам КИА
    /// </summary>
    [DisplayName("{'ru':'Измерения по типам КИА','en':'Instrument type detail'}")]
    [Comment("Измерения по типам КИА")]
    [ForeignKey(nameof(InstrumentTypeDetailID))]
    [FilteredField]
    [RequiredField]
    public virtual InstrumentTypeDetail InstrumentTypeDetail { get; set; }

    /// <summary>
    /// Идентификатор системы источника
    /// </summary>
    [DisplayName("{'ru':'Идентификатор системы источника','en':'Source system id'}")]
    [Comment("Идентификатор системы источника")]
    [Column("SourceSystem_ID")]
    public Guid? SourceSystemID { get; set; }

    /// <summary>
    /// Cистема-источник
    /// </summary>
    [DisplayName("{'ru':'Cистема-источник','en':'Source system'}")]
    [Comment("Cистема-источник")]
    [ForeignKey(nameof(SourceSystemID))]
    [FilteredField]
    [RequiredField]
    public virtual SourceSystem? SourceSystem { get; set; }

    /// <inheritdoc/>
    [Display(AutoGenerateField = false)]
    public string LinkedID => nameof(InstrumentID);
}