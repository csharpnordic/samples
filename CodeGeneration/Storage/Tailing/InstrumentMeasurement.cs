using CodeGeneration.Attributes;
using CodeGeneration.Storage;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Измерения конкретного КИА
/// </summary>
[DisplayName("{'ru':'Измерения конкретного КИА','en':'Instrument measurement'}")]
[Comment("Измерения конкретного КИА")]
[Table("InstrumentMeasurements", Schema = DB.SchemaName)]
public class InstrumentMeasurement : Entity
{
    /// <summary>
    /// Метка времени
    /// </summary>
    [DisplayName("{'ru':'Метка времени','en':'Time stamp'}")]
    [Comment("Метка времени")]
    [Column("Timestamp")]
    public DateTime Timestamp { get; set; }

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
    public virtual Instrument Instrument { get; set; }

    /// Статус
    /// </summary>
    [DisplayName("{'ru':'Идентификатор агрегированного состояния безопасности','en':'Aggregated Safety State Identifier'}")]
    [Comment("Идентификатор агрегированного состояния безопасности")]
    [Column("Status_ID")]
    public Guid StateID { get; set; }

    /// <summary>
    /// Safety State
    /// </summary>
    [DisplayName("{'ru':'Агрегированное cостояние безопасности','en':'Aggregated Safety State'}")]
    [Comment("Агрегированное состояние безопасности")]
    [ForeignKey(nameof(StateID))]
    [FilteredField]
    public virtual SafetyState SafetyState { get; set; }

    /// <summary>
    /// Значения показателей
    /// </summary>
    public virtual HashSet<InstrumentMeasurementDetail> Details { get; set; }

    /// <summary>
    /// Получение результатов текущих измерений для списка КИА на конкретную дату.
    /// Если измерения по КИА нет, формируется специальное состояние "Нет данных"
    /// </summary>
    /// <param name="db">Контекст базы данных</param>
    /// <param name="instruments">Список КИА</param>
    /// <param name="dateTime">Метка времени</param>
    /// <returns></returns>
    public static IEnumerable<InstrumentMeasurement> GetStatesOnDate(DB db, IEnumerable<Instrument> instruments, DateTime dateTime)
    {
        SafetyState noDataState = db.GetState(Enums.StateType.NoData);
        IQueryable<InstrumentMeasurement> measurementsQuery = db.InstrumentMeasurements.Where(x => x.Timestamp.Date < dateTime.Date.AddDays(1));

        var measList = (from instrument in instruments
                        join measurements in measurementsQuery on instrument.ID equals measurements.InstrumentID into meas
                        from measurementsResult in meas.DefaultIfEmpty()
                        select measurementsResult == null
                            ? new InstrumentMeasurement() // создание виртуального объекта (его нет в БД) - как следствие, его не отредактировать...
                            {
                                ID = Guid.Empty, // признак синтетического виртуального объекта, который не хранится в БД
                                InstrumentID = instrument.ID,
                                Instrument = instrument,
                                SafetyState = noDataState,
                                StateID = noDataState.ID,
                                Timestamp = DateTime.MinValue
                            }
                            : measurementsResult).ToList();

        IEnumerable<InstrumentMeasurement?> measQuery = measList.GroupBy(x => x.InstrumentID)
            .Select(x => new { Meas = x.Select(y => y), MaxDate = x.Select(y => y.Timestamp).Max() })
            .SelectMany(x => x.Meas.Where(y => y.Timestamp == x.MaxDate)).GroupBy(x => x.InstrumentID)
            .Select(x => new { Meas = x.Select(y => y), MaxState = x.Select(y => y.SafetyState.Level).Max() })
            .Select(x => x.Meas.FirstOrDefault(y => y.SafetyState.Level == x.MaxState));

        var measStateList = measQuery.ToList();

        bool dbSaveNeeded = false;

        if (measStateList.Any())
        {
            // Цикл по последним измерениям КИА на дату        
            foreach (InstrumentMeasurement? meas in measStateList)
            {
                // Состояние "Нет данных" присваивается, если у КИА отсутствуют измерения и их пересчитывать не надо
                if (meas?.SafetyState == noDataState)
                {
                    continue;
                }
                // Пересчет всех показателей последнего измерения КИА
                // Измерение тоже будет пересчитано
                meas?.CalculateSafetyState(db);
                dbSaveNeeded = true;
            }
        }

        // После пересчета сохраним статусы
        if (dbSaveNeeded)
        {
            db.SaveChanges();
        }

        return measStateList.AsEnumerable()!;
    }

    /// <summary>
    ///  Обновление состояния безопасности
    /// </summary>
    /// <returns></returns>
    /// <param name="normalState">Нормальное состояние</param>
    public void UpdateSafetyState(SafetyState normalState)
    {
        SafetyState? calcState = Details?.Select(x => x.SafetyState).Max();
        SafetyState = calcState ?? normalState;
    }

    /// <summary>
    /// Получение актуального состояния безопасности (с пересчетом статусов безопасности)
    /// </summary>
    /// <param name="db">Контекст базы данных</param>
    public void CalculateSafetyState (DB db)
    {
        if (Details.Any())
        {
            SafetyState normalState = db.GetState(Enums.StateType.Normal);

            foreach (InstrumentMeasurementDetail detail in Details)
            {
                detail.CalculateSafetyState(normalState);
            }
        }
    }
}