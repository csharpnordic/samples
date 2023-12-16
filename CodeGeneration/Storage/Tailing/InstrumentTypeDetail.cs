using CodeGeneration.Attributes;
using CodeGeneration.Interfaces;
using CodeGeneration.Storage;
using CodeGeneration.Storage.Administration;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Измерения по типам КИА
/// </summary>
[DisplayName("{'ru':'Измерения для типа КИА','en':'Instrument types detail'}")]
[Comment("Измерения по типам КИА")]
[Table("InstrumentTypeDetails", Schema = DB.SchemaName)]
[MasterData(CodeGeneration.Storage.Administration.SubSystem.GTS)]
[DownLink(typeof(InstrumentSafetyCriteria))]
[UpLink(typeof(InstrumentType))]
[ParentEntity(typeof(InstrumentType))]
public class InstrumentTypeDetail : NamedEntity, ILinkedEntity
{
    /// <summary>
    /// Описание 
    /// </summary>       
    [DisplayName("{'ru':'Описание','en':'Description'}")]
    [Comment("Описание")]
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Порядок
    /// </summary>       
    [DisplayName("{'ru':'Порядок','en':'Order'}")]
    [Comment("Порядок для отображения")]
    public int? Order { get; set; }

    #region public OkeiUnit OkeiUnit { get; set; }

    /// <summary>
    /// Идентификатор единицы измерения
    /// </summary>
    [Comment("{'ru':'Идентификатор единицы измерения','en':'Okei unit identifier'}")]
    [Column("OkeiUnit_ID")]
    public Guid OkeiUnitID { get; set; }

    /// <summary>
    /// Техническое поле для работы внешнего ключа
    /// </summary>   
    [Display(AutoGenerateField = false)]
    [Comment("{'ru':'Единица измерения','en':'Okei unit'}")]
    [ForeignKey(nameof(OkeiUnitID))]
    public virtual OkeiUnit OkeiUnitFK { get; set; }

    /// <summary>
    /// Кэш объекта единицы измерения
    /// </summary>
    private OkeiUnit? okeiUnit = null;

    /// <summary>
    /// Название единицы измерения
    /// </summary>
    [DisplayName("{'ru':'Название единицы измерения','en':'Unit of measurement name'}")]
    [NotMapped]
    [Required]
    public OkeiUnit OkeiUnit
    {
        get
        {
            if (okeiUnit == null)
            {
                using var db = new CodeGeneration.Storage.DB();

                okeiUnit = db.OkeiUnits.Find(OkeiUnitID)!;
            }

            return okeiUnit;
        }
        set
        {
            OkeiUnitID = value.ID;
            okeiUnit = null;
        }
    }

    /// <summary>
    /// Условное обозначение единицы измерения
    /// </summary>
     [DisplayName("{'ru':'Единица измерения','en':'Unit of measurement'}")]
    [NotMapped]
    public string OkeiUnitSymbol => OkeiUnit?.Symbol ?? "?";

    #endregion

    /// <summary>
    /// Тип КИА
    /// </summary>
    [DisplayName("{'ru':'Тип КИА','en':'Instrument type'}")]
    [Comment("Тип КИА")]
    [ForeignKey(nameof(InstrumentTypeID))]
    [FilteredField]
    [RequiredField]
    public virtual InstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Идентификатор типа КИА
    /// </summary>
    [DisplayName("{'ru':'Идентификатор типа КИА','en':'Instrument type id'}")]
    [Comment("Идентификатор типа КИА")]
    [Column("InstrumentType_ID")]
    public Guid InstrumentTypeID { get; set; }

    /// <inheritdoc/>
    [Display(AutoGenerateField = false)]
    public string LinkedID => nameof(InstrumentTypeID);

    /// <summary>
    /// Настройки типов измерений
    /// </summary>
    public virtual HashSet<InstrumentDetail> InstrumentDetails { get; set; }

    /// <summary>
    /// Настройки критериев безопасности для типа КИА
    /// </summary>
    public virtual HashSet<InstrumentSafetyCriteria> InstrumentCriterias { get; set; }
}
