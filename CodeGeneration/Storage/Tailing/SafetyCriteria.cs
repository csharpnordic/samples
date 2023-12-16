using Cenguru.Common.Enums;
using CodeGeneration.Attributes;
using CodeGeneration.Extensions;
using CodeGeneration.Interfaces;
using CodeGeneration.Storage;
using CodeGeneration.Storage.Administration;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Критерии
/// </summary>
[DisplayName("{'ru':'Критерии безопасности','en':'Safety criterias'}")]
[Comment("Критерии безопасности")]
[Table("SafetyCriterias", Schema = DB.SchemaName)]
[MasterData(CodeGeneration.Storage.Administration.SubSystem.GTS)]
[UpLink(typeof(InstrumentSafetyCriteria))]
public class SafetyCriteria : Entity, ILinkedEntity
{
    /// <summary>
    /// Идентификатор настроек критериев безопасности
    /// </summary>
    [DisplayName("{'ru':'Идентификатор настроек критериев безопасности','en':'Safety criteria settings Identifier'}")]
    [Comment("Идентификатор настроек критериев безопасности")]
    [Column("InstrumentSafetyCriteria_ID")]
    public Guid InstrumentSafetyCriteriaID { get; set; }

    /// <summary>
    /// Настройки критериев безопасности
    /// </summary>
    [DisplayName("{'ru':'Настройки критериев безопасности','en':'Safety criteria settings'}")]
    [ForeignKey(nameof(InstrumentSafetyCriteriaID))]
    [FilteredField]
    [RequiredField]
    public virtual InstrumentSafetyCriteria InstrumentSafetyCriteria { get; set; }

    /// <summary>
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
    /// Действителен с
    /// </summary>
    [DisplayName("{'ru':'Действителен c','en':'Valid from'}")]
    [DisplayFormat(DataFormatString = UserProfile.DefaultBackDateFormatString)]
    [Comment("Действителен c")]
    [Column("ValidFrom")]
    [Required()]
    public DateTime ValidFrom { get; set; }

    /// <summary>
    /// Действителен по
    /// </summary>
    [DisplayName("{'ru':'Действителен по','en':'Valid to'}")]
    [DisplayFormat(DataFormatString = UserProfile.DefaultBackDateFormatString)]
    [Comment("Действителен по")]
    [Column("ValidTo")]
    public DateTime? ValidTo { get; set; }

    /// <summary>
    /// Значение
    /// </summary>
    [DisplayName("{'ru':'Значение','en':'Value'}")]
    [Comment("Измеренное значение")]
    [Required()]
    public double Limit { get; set; }

    /// <summary>       
    /// Имя свойства, содержащего идентификатор связанной родительской сущности
    /// </summary>
    [Display(AutoGenerateField = false)]
    public string LinkedID => nameof(InstrumentSafetyCriteriaID);

    /// <summary>
    /// Сравнение значения с критерием безопасности
    /// </summary>
    /// <param name="value">Значение</param>
    /// <param name="normalState">Нормальное состояние безопасности</param>
    /// <returns></returns>
    public SafetyState CompareWithCriteria(double value, SafetyState normalState)
    {
        SafetyState result = normalState;

        if (InstrumentSafetyCriteria.OperatorType.IsFlagSet(OperatorType.Absolute))
        {
            value = Math.Abs(value);
        }

        if (InstrumentSafetyCriteria.OperatorType.IsFlagSet(OperatorType.Equal))
        {
            if (value == Limit)
            {
                result = InstrumentSafetyCriteria.SafetyState;
            }
        }
        else
        {
            if (InstrumentSafetyCriteria.OperatorType.IsFlagSet(OperatorType.Upper))
            {
                if (InstrumentSafetyCriteria.OperatorType.IsFlagSet(OperatorType.Strict) ? value > Limit : value >= Limit)
                {
                    result = InstrumentSafetyCriteria.SafetyState;
                }
            }
            else
            {
                if (InstrumentSafetyCriteria.OperatorType.IsFlagSet(OperatorType.Strict) ? value < Limit : value <= Limit)
                {
                    result = InstrumentSafetyCriteria.SafetyState;
                }
            }
        }

        // Так как значение НЕ удовлетворяет ни критерию, ему присвоено состояние "Нормальное"
        return result;

    }
}
