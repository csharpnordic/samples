using Microsoft.EntityFrameworkCore;

namespace CodeGeneration.Storage;

/// <summary>
/// Контекст платформенной базы данных
/// </summary>
public class DB : DbContext
{
    /// <summary>
    /// Цвета интерфейса
    /// </summary>
    public virtual DbSet<Administration.Color> Colors { get; set; }

    /// <summary>
    /// Список функциональных возможностей
    /// </summary>
    public virtual DbSet<Administration.Feature> Features { get; set; }

    /// <summary>
    /// Список групп функциональных возможностей
    /// </summary>
    public virtual DbSet<Administration.FeatureGroup> FeatureGroups { get; set; }

    /// <summary>
    /// Список функциональных возможностей групп (N-N)
    /// </summary>
    public virtual DbSet<Administration.FeatureOfGroup> FeaturesOfGroups { get; set; }

    /// <summary>
    /// Список значков
    /// </summary>
    public virtual DbSet<Administration.Icon> Icons { get; set; }

    /// <summary>
    /// Список заданий для запуска по расписанию
    /// </summary>
    public virtual DbSet<Administration.Job> Jobs { get; set; }

    /// <summary>
    /// Список языков
    /// </summary>
    public virtual DbSet<Administration.Language> Languages { get; set; }

    /// <summary>
    /// Список пунктов меню
    /// </summary>
    public virtual DbSet<Administration.MenuItem> MenuItems { get; set; }

    /// <summary>
    /// Список пунктов меню групп (N-N)
    /// </summary>
    public virtual DbSet<Administration.MenuItemOfGroup> MenuItemOfGroups { get; set; }

    /// <summary>
    /// Оповещения
    /// </summary>
    public virtual DbSet<Administration.Notification> Notifications { get; set; }

    /// <summary>
    /// История уведомлений
    /// </summary>
    public virtual DbSet<Administration.NotifiedUser> NotifiedUsers { get; set; }

    /// <summary>
    /// Единицы измерения
    /// </summary>
    public virtual DbSet<Administration.OkeiUnit> OkeiUnits { get; set; }

    /// <summary>
    /// Список предприятий
    /// </summary>
    public virtual DbSet<Administration.Plant> Plants { get; set; }

    /// <summary>
    /// Список заводов для роли (N-N)
    /// </summary>
    public virtual DbSet<Administration.PlantOfRole> PlantsOfRoles { get; set; }

    /// <summary>
    /// Список ролей пользователей
    /// </summary>
    public virtual DbSet<Administration.Role> Roles { get; set; }

    /// <summary>
    /// Список ролей для пользователя (N-N)
    /// </summary>
    public virtual DbSet<Administration.RoleOfUser> RolesOfUsers { get; set; }

    /// <summary>
    /// Список подсистем
    /// </summary>
    public virtual DbSet<Administration.SubSystem> SubSystems { get; set; }

    /// <summary>
    /// Список подсистем завода
    /// </summary>
    public virtual DbSet<Administration.SubSystemOfPlant> SubSystemsOfPlants { get; set; }

    /// <summary>
    /// Список пользователей
    /// </summary>
    public virtual DbSet<Administration.User> Users { get; set; }

    /// <summary>
    /// Список профилей пользователей
    /// </summary>
    public virtual DbSet<Administration.UserProfile> UsersProfiles { get; set; }

    /// <summary>
    /// Настройки пользователей по подсистемам
    /// </summary>
    public virtual DbSet<Administration.UserSettingsOfSubsystem> UserSettingsOfSubsystems { get; set; }

    /// <summary>
    /// Порядок столбцов таблиц для пользователей
    /// </summary>
    public virtual DbSet<Administration.UserTable> UsersTables { get; set; }

    /// <summary>
    /// Перечень драйверов
    /// </summary>
    public virtual DbSet<Tailing.Driver> Drivers { get; set; }

    /// <summary>
    /// КИА
    /// </summary>
    public virtual DbSet<Tailing.Instrument> Instruments { get; set; }

    /// <summary>
    /// Настройки типов измерений
    /// </summary>
    public virtual DbSet<Tailing.InstrumentDetail> InstrumentDetails { get; set; }

    /// <summary>
    /// Измерения конкретного КИА
    /// </summary>
    public virtual DbSet<Tailing.InstrumentMeasurement> InstrumentMeasurements { get; set; }

    /// <summary>
    /// Значения показателей
    /// </summary>
    public virtual DbSet<Tailing.InstrumentMeasurementDetail> InstrumentMeasurementDetails { get; set; }

    /// <summary>
    /// Параметры для типов КИА
    /// </summary>
    public virtual DbSet<Tailing.InstrumentParameter> InstrumentParameters { get; set; }

    /// <summary>
    /// Журналы параметров КИА
    /// </summary>
    public virtual DbSet<Tailing.InstrumentParameterHistory> ParameterHistory { get; set; }

    /// <summary>
    /// Параметры КИА
    /// </summary>
    public virtual DbSet<Tailing.InstrumentParameterType> InstrumentParameterTypes { get; set; }

    /// <summary>
    /// Типы КИА
    /// </summary>
    public virtual DbSet<Tailing.InstrumentType> InstrumentTypes { get; set; }

    /// <summary>
    /// Измерения по типам КИА
    /// </summary>
    public virtual DbSet<Tailing.InstrumentTypeDetail> InstrumentTypeDetails { get; set; }

    /// <summary>
    /// Настройки критериев безопасности
    /// </summary>
    public virtual DbSet<Tailing.InstrumentSafetyCriteria> InstrumentSafetyCriterias { get; set; }

    /// <summary>
    /// Структура хростохранилища
    /// </summary>
    public virtual DbSet<Tailing.Location> Locations { get; set; }

    /// <summary>
    /// Виды наблюдения
    /// </summary>
    public virtual DbSet<Tailing.MonitoringType> MonitoringTypes { get; set; }

    /// <summary>
    /// Критерии
    /// </summary>
    public virtual DbSet<Tailing.SafetyCriteria> SafetyCriterias { get; set; }

    /// <summary>
    /// Состояния безопасности значения измерения
    /// </summary>
    public virtual DbSet<Tailing.SafetyState> SafetyStates { get; set; }

    /// <summary>
    /// Перечень систем-источников
    /// </summary>
    public virtual DbSet<Tailing.SourceSystem> SourceSystems { get; set; }
}