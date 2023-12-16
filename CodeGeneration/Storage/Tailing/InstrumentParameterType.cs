using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cenguru.Common.Enums;
using CodeGeneration.Attributes;
using CodeGeneration.Interfaces;
using CodeGeneration.Storage;
using CodeGeneration.Storage.Administration;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Параметры для типов КИА
/// </summary>
[DisplayName("{'ru':'Параметры для типа КИА','en':'Instrument types parameters'}")]
[Comment("Параметры для типов КИА")]
[Table("InstrumentParameterTypes", Schema = DB.SchemaName)]
[MasterData(SubSystem.GTS)]
[UpLink(typeof(InstrumentType))]
public class InstrumentParameterType : NamedEntity, ILinkedEntity
{
    /// <summary>
    /// Идентификатор типа КИА
    /// </summary>
    [DisplayName("{'ru':'Идентификатор типа КИА','en':'Instrument type id'}")]
    [Comment("Идентификатор типа КИА")]
    [Column("InstrumentType_ID")]
    public Guid InstrumentTypeID { get; set; }

    /// <summary>
    /// Тип КИА
    /// </summary>
    [DisplayName("{'ru':'Тип КИА','en':'Instrument type'}")]
    [Comment("Тип КИА")]
    [ForeignKey(nameof(InstrumentTypeID))]
    [FilteredField]
    [RequiredField]
    public virtual InstrumentType InstrumentType { get; set; }

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
    /// Описание 
    /// </summary>       
    [DisplayName("{'ru':'Описание','en':'Description'}")]
    [Comment("Описание")]
    [LocalizedField]
    [MaxLength(1000)]
    public string? Description { get; set; }

    /// <summary>
    /// Тип данных (Value в InstrumentParameters). Возможные значения:  0 - не указан; 1 - string; 2 - double
    /// </summary>
    [DisplayName("{'ru':'Тип данных','en':'Data type'}")]
    [Comment("Тип данных. Возможные значения:  0 - не указан; 1 - string; 2 - double")]
    public ParamDataType DataType { get; set; }

    /// <summary>
    /// Порядок для отображения
    /// </summary>
    [DisplayName("{'ru':'Порядок для отображения','en':'Order'}")]
    [Comment("Порядок для отображения")]
    public int Order { get; set; }

    /// <summary>
    /// Символ (в формулах) 
    /// </summary>       
    [DisplayName("{'ru':'Символ','en':'Symbol'}")]
    [Comment("Символ (в формулах)")]
    [MaxLength(15)]
    public string? Symbol { get; set; }

    /// <inheritdoc/>
    [Display(AutoGenerateField = false)]
    public string LinkedID => nameof(InstrumentTypeID);
}