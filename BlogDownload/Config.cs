using Microsoft.Extensions.Configuration;

namespace BlogDownload;

/// <summary>
/// Работа с конфигурацией приложения
/// </summary>
public static class Config
{
    /// <summary>
    /// Объект, осуществляющий протоколирование
    /// </summary>
    private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Корень конфигурации
    /// </summary>
    private static readonly IConfigurationRoot config;

    /// <summary>
    /// Загрузка файла конфигурации
    /// </summary>
    static Config()
    {
        string name = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!System.IO.File.Exists(name))
        {
            log.Fatal($"Файл конфигурации не найден: {name}");
            Environment.Exit(1);
        }
        config = new ConfigurationBuilder().AddJsonFile(name, false, true).Build();
    }

    /// <summary>
    /// Значение параметра конфигурации с заданным именем
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static string? GetParameter(string name)
    {
        return config.GetSection(name).Value;
    }
}
