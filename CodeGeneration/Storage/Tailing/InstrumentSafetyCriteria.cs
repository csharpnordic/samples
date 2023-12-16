using Cenguru.Common.Enums;
using CodeGeneration.Attributes;
using CodeGeneration.Extensions;
using CodeGeneration.Interfaces;
using CodeGeneration.Storage;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Настройки критериев безопасности для типа КИА
/// </summary>
[DisplayName("{'ru':'Настройки критериев безопасности для типа КИА','en':'Safety criteria settings for instrument types'}")]
[Comment("Настройки критериев безопасности для типа КИА")]
[Table("InstrumentSafetyCriterias", Schema = DB.SchemaName)]
[MasterData(CodeGeneration.Storage.Administration.SubSystem.GTS)]
[DownLink(typeof(SafetyCriteria))]
[UpLink(typeof(InstrumentTypeDetail))]
public class InstrumentSafetyCriteria : Entity, ILinkedEntity, INamed
{
    /// <summary>
    /// Измерение 
    /// </summary>
    [DisplayName("{'ru':'Измерение','en':'Instrument type detail'}")]
    [Comment("Измерение")]
    [ForeignKey(nameof(InstrumentTypeDetailID))]
    [FilteredField]
    [RequiredField]
    public virtual InstrumentTypeDetail InstrumentTypeDetail { get; set; }

    /// <summary>
    /// Идентификатор измерения
    /// </summary>
    [DisplayName("{'ru':'Идентификатор измерения','en':'Instrument type detail id'}")]
    [Comment("Идентификатор измерения")]
    [Column("InstrumentTypeDetail_ID")]
    public Guid InstrumentTypeDetailID { get; set; }

    /// <summary>
    /// Идентификатор статуса
    /// </summary>
    [DisplayName("{'ru':'Идентификатор агрегированного состояния безопасности','en':'Aggregated Safety State Identifier'}")]
    [Comment("Идентификатор агрегированного состояния безопасности")]
    [Column("Status_ID")]
    public Guid StatusID { get; set; }

    /// <summary>
    /// Агрегированное cостояние безопасности
    /// </summary>
    [DisplayName("{'ru':'Агрегированное cостояние безопасности','en':'Aggregated Safety State'}")]
    [Comment("Агрегированное состояние безопасности")]
    [ForeignKey(nameof(StatusID))]
    [FilteredField]
    [RequiredField]
    public virtual SafetyState SafetyState { get; set; }

    /// <summary>
    /// Тип операции сравнения
    /// </summary>
    [DisplayName("{'ru':'Тип операции сравнения','en':'Operator comparison type'}")]
    [Comment("Тип операции сравнения")]
    [RequiredField]
    public OperatorType OperatorType { get; set; }

    /// <summary>       
    /// Имя свойства, содержащего идентификатор связанной родительской сущности
    /// </summary>
    [Display(AutoGenerateField = false)]
    public string LinkedID => nameof(InstrumentTypeDetailID);

    /// <summary>
    /// Критерии
    /// </summary>
    public virtual HashSet<SafetyCriteria> SafetyCriterias { get; set; }

    /// <summary>
    /// Наименование объекта. Используется язык по умолчанию
    /// </summary>
    [NotMapped]
    [Display(AutoGenerateField = false)]
    public string Name
    {
        get => $"{InstrumentTypeDetail.Name.Localize(NamedEntity.DefaultLanguage)}: {SafetyState.Name.Localize(NamedEntity.DefaultLanguage)}";
        set
        { // сохранение не предусмотрено}         
        }
    }
}