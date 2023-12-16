using Cenguru.Common.Enums;
using CodeGeneration.Attributes;
using CodeGeneration.Enums;
using CodeGeneration.Interfaces;
using CodeGeneration.Storage;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Состояние безопасности значения измерения
/// </summary>
[DisplayName("{'ru':'Состояния безопасности','en':'Safety states'}")]
[Comment("Состояния безопасности")]
[Table("SafetyStates", Schema = DB.SchemaName)]
[MasterData(CodeGeneration.Storage.Administration.SubSystem.GTS)]
public class SafetyState : NamedEntity, IComparable<SafetyState>, IState
{
    /// <summary>
    /// Цвет состояния для интерфейса, #RGB
    /// </summary>
    [FrontType(ColumnBrowseDataType.Color)]
    [DisplayName("Цвет")]
    [Comment("Цвет для интерфейса, #RRGGBB")]
    [MaxLength(MaxColorLength)]
    [Required()]
    public string ColorCode { get; set; } = "#000000"; // по умолчанию - чёрный цвет

    /// <summary>
    /// Уровень серьёзности
    /// </summary>
    [DisplayName("{'ru':'Уровень серьёзности','en':'Severity level'}")]
    [Comment("Уровень серьёзности, начиная с 0, чем больше - тем серьёзнее")]
    [Required()]
    public int Level { get; set; } = 0;

    /// <summary>
    /// Тип состояния КИА
    /// </summary>
    [DisplayName("{'ru':'Тип состояния','en':'State type'}")]
    [Comment("Тип состояния")]
    public StateType StateType { get; set; }

    /// <summary>
    /// Сравнение по уровню
    /// </summary>
    /// <param name="lang">код языка</param>
    /// <param name="entity">Сравниваемая сущность</param>
    /// <returns></returns>
    public override int CompareTo(string lang, IEntity entity) => Level.CompareTo((entity as SafetyState)!.Level);

    /// <summary>
    /// Сравнение по уровню
    /// </summary>
    /// <param name="other">Сравниваемая сущность</param>
    /// <returns></returns>
    public int CompareTo(SafetyState? other) => Level.CompareTo(other?.Level);

    /// <summary>
    /// Возвращает состояние безопасности с более высоким уровнем серьезности
    /// </summary>
    /// <param name="state">Сравниваемая сущность</param>
    /// <returns></returns>
    public SafetyState MoreCriticalState(SafetyState state) => state.CompareTo(this) == 0 ? this : state;
}
