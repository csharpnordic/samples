using Microsoft.Data.SqlClient;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace BlogDownload;

/// <summary>
/// Запись в блоге
/// </summary>
public class BlogItem
{
    /// <summary>
    /// Объект, осуществляющий протоколирование
    /// </summary>
    private static readonly NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();

    /// <summary>
    /// Уникальный идентификатор записи
    /// </summary>
    public Guid ID;
    /// <summary>
    /// Дата публикации
    /// </summary>
    public DateTime Date;
    /// <summary>
    /// Тема публикации из реестра Excel
    /// </summary>
    public string Topic;
    /// <summary>
    /// Тема публикации с сайта
    /// </summary>
    public string Title;
    /// <summary>
    /// Описание публикации с сайта
    /// </summary>
    public string Description;
    /// <summary>
    /// Суффикс URL
    /// </summary>
    public string URL;
    /// <summary>
    /// Признак публикации в Instagram 
    /// </summary>
    public bool Instagram;
    /// <summary>
    /// Ссылка на источник изображения в Интернет
    /// </summary>
    public string Image;
    /// <summary>
    /// Текст сообщения
    /// </summary>
    public string Body;

    /// <summary>
    /// Имя файла для сохранения загруженного текста и картинок
    /// </summary>
    private string Name => $"{Date:yyyy-MM-dd} {URL}";

    /// <summary>
    /// Конструктор из базы данных
    /// Порядок столбцов: [ID], [Date], [Topic], [URL], [Instagram], [Image]
    /// </summary>
    /// <param name="reader"></param>
    public BlogItem(SqlDataReader reader)
    {
        ID = reader.GetGuid(0);
        Date = reader.GetDateTime(1);
        Topic = reader.GetString(2);
        URL = reader.GetString(3);
        string i = reader.GetString(4);
        Instagram = (i.ToLower() == "да");
        // Проверка на NULL
        if (!reader.IsDBNull(5))
        {
            Image = reader.GetString(5);
        }
    }

    /// <summary>
    /// Предупреждение о перезаписи файла
    /// </summary>
    /// <param name="name">Имя локального файла</param>
    private static string CheckFile(string name)
    {
        // Определение имени каталога
        string dir = System.IO.Path.GetDirectoryName(name);
        // Создание каталогов при необходимости
        System.IO.Directory.CreateDirectory(dir);
        // Проверка на существование файла
        if (System.IO.File.Exists(name))
        {
            log.Warn($"Файл {name} уже существует и будет перезаписан");
        }
        return name;
    }

    /// <summary>
    /// Очистка строки от спецсимволов (например, &quot;)
    /// </summary>
    /// <param name="s">Строка</param>
    /// <returns></returns>
    private static string Clean(string s)
    {
        return s.Replace("&quot;", @"""");
    }

    /// <summary>
    /// Загрузить статью из интернета
    /// </summary>
    public void Download()
    {
        // Корень для сохранения файлов - распределяем по годам
        string root = Path.Combine(Config.GetParameter<string>("FileRoot"), Date.Year.ToString());

        Dictionary<string,string> headers = Config.GetParameter<Dictionary<string, string>>("Headers");
        // Загрузка веб-страницы в виде массива байт
        using WebClient client = new();
        string url = Config.GetParameter<string>("WebRoot") + URL;
        foreach (var header in headers)
        {
            client.Headers.Add(header.Key, header.Value);
        }
        byte[] data = client.DownloadData(url);
        // Перекодировка в UTF-8
        string s = Encoding.UTF8.GetString(data);

        // Сохранение загруженной веб-страницы в файл
        string fileName = Path.Combine(root, "txt", $"{Name}.html");
        string imageName = Path.Combine(root, "img", $"{Name}.png");
        System.IO.File.WriteAllText(CheckFile(fileName), s);

        // Поиск заголовока регулярным выражением
        var title = Regex.Match(s, @"<meta property=""og:title"" content=""([^""]+)""/>");
        if (title.Success)
        {
            Title = title.Groups[1].Value;
        }
        else
        {
            log.Warn("Заголовок не найден");
        }

        // Поиск описания регулярным выражением
        var descr = Regex.Match(s, @"<meta property=""og:description"" content=""([^""]+)""/>");
        if (descr.Success)
        {
            Description = descr.Groups[1].Value;
            // Замена HTML-представления на нормальную кавычку
            Description = Clean(Description);
        }
        else
        {
            log.Warn("Описание не найдено");
        }

        // Поиск картинки регулярным выражением
        var picture = Regex.Match(s, @"<meta property=""og:image"" content=""([^""]+)""/>");
        if (picture.Success)
        {
            // Адрес картинки на сайте Wix
            string urli = picture.Groups[1].Value;
            // Загрузка картинки в файл
            client.DownloadFile(urli, CheckFile(imageName));
        }
        else
        {
            log.Warn("Изображение не найдено");
        }

        // Поиск текста сообщения регулярным выражением
        var message = Regex.Match(s, @"<article class=""blog-post-page-font"">(.+?)</article>", RegexOptions.Singleline);
        if (message.Success)
        {
            // Строка с тегами
            string m = message.Groups[1].Value;
            // Очистка от стилевых таблиц
            m = Regex.Replace(m, @"<style type=""text/css"">(.+?)</style>", string.Empty, RegexOptions.Singleline);
            // Очистка от тегов и спецсимволов
            Body = Clean(Regex.Replace(m, "<.*?>", string.Empty));           
        }
        else
        {
            log.Warn("Текст сообщения не найден");
        }
    }

    /// <summary>
    /// Сохранение текста сообщения в БД
    /// </summary>
    /// <param name="db"></param>
    public bool Save(Database db)
    {
        if (string.IsNullOrEmpty(Body))
        {
            log.Warn("Тело сообщения не найдено");
            return false;
        }
        // Создание параметрического запроса
        using SqlCommand cmd = db.GetCommand();
        cmd.CommandText = "UPDATE Blog SET Body=@body, Title=@title, Description=@descr WHERE ID = @id";
        cmd.Parameters.AddWithValue("id", ID);
        cmd.Parameters.AddWithValue("body", Body);
        cmd.Parameters.AddWithValue("descr", Description);
        cmd.Parameters.AddWithValue("title", Title);

        // Обновление записи в БД
        int result = cmd.ExecuteNonQuery();
        // Несколько избыточный контроль корректности выполнения запроса
        return result == 1;
    }

    /// <summary>
    /// Строковое представление объекта
    /// </summary>
    /// <returns></returns>
    public override string ToString() => URL;
}
