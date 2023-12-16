using Cenguru.Common.Enums;
using Cenguru.Common.Exceptions;
using Remeslo.Common.Extensions;
using Remeslo.Common.Storage;
using Remeslo.Common.Storage.Administration;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Контекст базы данных подсистемы GTS
/// </summary>
/// EntityFrameworkCore\Add-Migration nnn -Context CodeGeneration.Storage.Tailing.DB
public class DB : RootDB
{
    /// <summary>
    /// Имя схемы подсистемы
    /// </summary>
    public const string SchemaName = "gts";

    /// <inheritdoc cref="DbContextOptions"/>
    private readonly DbContextOptions<DB> options;

    /// <summary>
    /// .ctor
    /// </summary>
    public DB() { }

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="noTracking">
    /// Параметр для включения/отключения asNoTracking
    /// <list type="noTracking">
    ///     <item>
    ///         <description> false - выключено(по умолчанию) </description>
    ///     </item>
    ///     <item>
    ///         <description> true - включено </description>
    ///     </item>
    /// </list>
    /// </param>
    public DB(bool noTracking = false) : base(noTracking) { }

    /// <summary>
    /// Конструктор для контекста базы данных Cenguru(GTS)
    /// </summary>
    /// <param name="options">Опции для Контекста</param>
    /// <param name="noTracking">
    /// Параметр для включения/отключения asNoTracking
    /// <list type="noTracking">
    ///     <item>
    ///         <description> false - выключено(по умолчанию) </description>
    ///     </item>
    ///     <item>
    ///         <description> true - включено </description>
    ///     </item>
    /// </list>
    /// </param>
    public DB(DbContextOptions<DB> options, bool noTracking = false) : base(options, noTracking)
    {
        this.options = options;
    }

    /// <summary>
    /// Настройка базы данных
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.Configure(nameof(Cenguru), DB.SchemaName);

    /// <summary>
    /// Событие создания модели
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Прячем таблицы схемы adm от миграции (таблицы не создаются, но внешние ключи на них добавляются)
        // Чтобы исключить и внешние ключи нужно использовать запись вида
        /// modelBuilder.Ignore<Feature>();        
        modelBuilder.Entity<Feature>().ToTable(nameof(Feature), t => t.ExcludeFromMigrations(false));
        modelBuilder.Entity<FeatureGroup>().ToTable(nameof(FeatureGroup), t => t.ExcludeFromMigrations(false));
        modelBuilder.Entity<FeatureOfGroup>().ToTable(nameof(FeatureOfGroup), t => t.ExcludeFromMigrations(false));
        modelBuilder.Entity<Icon>().ToTable(nameof(Icon), t => t.ExcludeFromMigrations(false));
        modelBuilder.Entity<OkeiUnit>().ToTable(nameof(OkeiUnit), t => t.ExcludeFromMigrations(false));
        modelBuilder.Entity<Plant>().ToTable(nameof(Plant), t => t.ExcludeFromMigrations(false));
        modelBuilder.Entity<PlantOfRole>().ToTable(nameof(PlantOfRole), t => t.ExcludeFromMigrations(false));
        modelBuilder.Entity<Role>().ToTable(nameof(Role), t => t.ExcludeFromMigrations(false));
        modelBuilder.Entity<RoleOfUser>().ToTable(nameof(RoleOfUser), t => t.ExcludeFromMigrations(false));
        modelBuilder.Entity<SubSystem>().ToTable(nameof(SubSystem), t => t.ExcludeFromMigrations(false));
        modelBuilder.Entity<User>().ToTable(nameof(User), t => t.ExcludeFromMigrations(false));
        modelBuilder.Entity<UserProfile>().ToTable(nameof(UserProfile), t => t.ExcludeFromMigrations(false));
        modelBuilder.Entity<UserTable>().ToTable(nameof(UserTable), t => t.ExcludeFromMigrations(false));
        modelBuilder.Entity<UserSettingsOfSubsystem>().ToTable(nameof(UserSettingsOfSubsystem), t => t.ExcludeFromMigrations(false));
    }

    /// <summary>
    /// Начальная инициализация данных
    /// </summary>
    public override void Seed()
    {
        base.Seed();

        // Протоколирование            
        log.Info($"Выполнено подключение к СУБД: {Database.GetConnectionString()}");
    }

    #region "Прикладная логика"

    /// <summary>
    /// Получение состояния безопасности значения измерения, а если их нет, то создание
    /// </summary>
    /// <param name="stateType">Тип состояния безопасности</param>
    /// <returns></returns>
    public SafetyState GetState(StateType stateType)
    {
        SafetyState? result = SafetyStates.FirstOrDefault(x => x.StateType == stateType);

        if (result == null)
        {
            switch (stateType)
            {                
                case StateType.NoData:
                    result = new SafetyState
                    {
                        ColorCode = "#9c9c9c",
                        Level = 0,
                        Name = StateType.NoData.ToDescription()!.NormalizeJson(),
                        StateType = StateType.NoData
                    };
                    break;
                case StateType.Disabled:
                    result = new SafetyState
                    {
                        ColorCode = "#000000",
                        Level = -1,
                        Name = StateType.Disabled.ToDescription()!.NormalizeJson(),
                        StateType = StateType.Disabled
                    };
                    break;
                case StateType.Normal:
                    result = new SafetyState
                    {
                        ColorCode = "#2aa646",
                        Level = 1,
                        Name = StateType.Normal.ToDescription()!.NormalizeJson(),
                        StateType = StateType.Normal
                    };
                    break;
                default:
                    break;
            }

            if (result != null)
            {
                SafetyStates.Add(result);
                SaveChanges();
            }
            else
            {
                throw new CenguruException($"Не удалось получить состояние безопасности");
            }
        }

        return result!;
    }    

    /// <summary>
    /// Инструмент по идентификатору инструмента или его измерения
    /// </summary>
    /// <param name="id">Идентификатор инструмента или измерения. Если не задан или нулевой, возвращается null и осуществляется протоколирование</param>
    /// <param name="instrument">Инструмент</param>
    /// <returns>null - если инструмент не найден</returns>
    public Instrument? GetInstrument(Guid? id)
    {
        if (id == null || id == Guid.Empty)
        {
            return null;
        }
        Instrument? instrument = Instruments.Find(id);
        if (instrument == null)
        {
            instrument = InstrumentMeasurements.Find(id)?.Instrument;
            if (instrument == null)
            {
                log.Warn($"Не найдено КИА/измерение с идентификатором: {id}");
            }
        }
        return instrument;
    }


    #endregion
}