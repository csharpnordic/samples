namespace BlogDownload;

static class Program
{
    /// <summary>
    /// Объект, осуществляющий протоколирование
    /// </summary>
    private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

    static void Main()
    {
        try
        {
            var db = new Database();
            log.Info("Соединение установлено");
            log.Info($"В базе данных {db.GetBlogCount()} публикаций");
            var list = db.GetBlogItems();
            log.Info($"К загрузке {list.Count} публикаций");
            foreach (var item in list)
            {
                item.Download();
                if (item.Save(db))
                {
                    log.Info($"{item} загружен и сохранён");
                }
                // Прерываемся если пользователь нажал кнопку
                if (Console.KeyAvailable) { break; }
                Thread.Sleep(10000);
            }
        }
        catch (Exception ex)
        {
            log.Error(ex);
        }
        finally
        {
            log.Info("Нажмите Enter для завершения");
            Console.ReadLine();
        }
    }
}
