using CodeGeneration.Attributes;
using CodeGeneration.Storage;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Значения показателей
/// </summary>
[DisplayName("{'ru':'Значения показателей','en':'Instrument measurement detail'}")]
[Comment("Значения показателей")]
[Table("InstrumentMeasurementDetails", Schema = DB.SchemaName)]
public class InstrumentMeasurementDetail : Entity
{
    /// <summary>
    /// Идентификатор измерения
    /// </summary>
    [DisplayName("{'ru':'Идентификатор измерения','en':'Instrument measurement id'}")]
    [Comment("Идентификатор измерения")]
    [Column("InstrumentMeasurement_ID")]
    public Guid InstrumentMeasurementID { get; set; }

    /// <summary>
    /// Измерение конкретного КИА
    /// </summary>
    [DisplayName("{'ru':'Измерение конкретного КИА','en':'Instrument measurement'}")]
    [Comment("Измерение конкретного КИА")]
    [ForeignKey(nameof(InstrumentMeasurementID))]
    [FilteredField]
    [Required()]
    public virtual InstrumentMeasurement InstrumentMeasurement { get; set; }

    /// <summary>
    /// Идентификатор настройки типа измерений
    /// </summary>
    [DisplayName("{'ru':'Идентификатор настройки типа измерений','en':'Instrument detail id'}")]
    [Comment("Идентификатор настройки типа измерений")]
    [Column("InstrumentDetail_ID")]
    public Guid InstrumentDetailID { get; set; }

    /// <summary>
    /// Настройки типов измерений
    /// </summary>
    [DisplayName("{'ru':'Настройки типов измерений','en':'Instrument detail'}")]
    [Comment("Настройки типов измерений")]
    [ForeignKey(nameof(InstrumentDetailID))]
    [FilteredField]
    [Required()]
    public virtual InstrumentDetail InstrumentDetail { get; set; }

    /// <summary>
    /// Значение
    /// </summary>
    [DisplayName("{'ru':'Значение','en':'Value'}")]
    [Comment("Измеренное значение")]
    [Required()]
    public double Value { get; set; }

    /// <summary>
    /// Статус
    /// </summary>
    [DisplayName("{'ru':'Идентификатор состояния безопасности','en':'Safety State Identifier'}")]
    [Comment("Идентификатор состояния безопасности")]
    [Column("Status_ID")]
    public Guid? StatusID { get; set; }

    /// <summary>
    /// Safety State
    /// </summary>
    [DisplayName("{'ru':'Состояние безопасности','en':'Safety State'}")]
    [Comment("Состояние безопасности")]
    [ForeignKey(nameof(StatusID))]
    [FilteredField]
    public virtual SafetyState? SafetyState { get; set; }

    /// <summary>
    ///  Вычисление состояния безопасности
    /// </summary>
    /// <param name="normalState">Нормальное состояние безопасности</param>
    /// <returns></returns>
    public void CalculateSafetyState(SafetyState normalState)
    {
        // Определение актуальных критериев безопасности
        var criterias = InstrumentDetail
             .InstrumentTypeDetail
             .InstrumentCriterias
             .SelectMany(x => x.SafetyCriterias)
             .Where(x => x.ValidFrom <= InstrumentMeasurement.Timestamp.Date &&
                 (x.ValidTo >= InstrumentMeasurement.Timestamp.Date || x.ValidTo == null))
             .ToList();

        SafetyState state = normalState;

        // Если нет критериев, то используется нормальное состояние по умолчанию
        if (state == null || !criterias.Any())
        {
            SafetyState = state; // надо сохранить нормальное состояние, мы же не знаем, какое оно было сейчас
            return;
        }

        // Определение наиболее критического состояния, если есть критерии
        foreach (SafetyCriteria? criteria in criterias)
        {
            state = state.MoreCriticalState(criteria.CompareWithCriteria(Value, normalState));
        }

        SafetyState = state;

        // Нужен пересчет измерения этого показателя
        InstrumentMeasurement.UpdateSafetyState(normalState);
    }

    /// <summary>
    /// Внесение новых показаний
    /// </summary>
    /// <param name="db">Контекст БД</param>
    /// <param name="instrumentDetailID">Идентификатор настройки типов измерения</param>
    /// <param name="timestamp">Дата измерения</param>
    /// <param name="value">Значение измерения</param>
    public static void CreateMeasurement(DB db, Guid instrumentDetailID, DateTime timestamp, double value)
    {
        InstrumentDetail? instrumentDetail = db.InstrumentDetails.Find(instrumentDetailID);

        if (instrumentDetail == null)
            return;

        var measurement = db.InstrumentMeasurements
            .FirstOrDefault(x => x.InstrumentID == instrumentDetail.InstrumentID && x.Timestamp == timestamp);

        if (measurement == null)
        {
            measurement = new InstrumentMeasurement()
            {
                InstrumentID = instrumentDetail.InstrumentID,
                Timestamp = timestamp
            };

            db.InstrumentMeasurements.Add(measurement);
        }

        var measurementDetail = new InstrumentMeasurementDetail()
        {
            InstrumentDetail = instrumentDetail,
            InstrumentDetailID = instrumentDetailID,
            InstrumentMeasurementID = measurement.ID,
            InstrumentMeasurement = measurement,
            Value = value
        };
        db.Add(measurementDetail);

        SafetyState normalState = db.GetState(Enums.StateType.Normal);
        // обновление состояния измеренного значения
        measurementDetail.CalculateSafetyState(normalState);
        // обновление состояния всего измерения
        measurement.UpdateSafetyState(normalState);

        db.SaveChanges();
    }
} 
