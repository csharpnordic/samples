using NLog;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CodeGeneration.Serialization;

/// <summary>
/// Особая сериализация типа System.Enum
/// </summary>
public class EnumConverter<T> : JsonConverter<T> where T : Enum
{
    /// <summary>
    /// Протоколирование
    /// </summary>
    private static readonly Logger log = LogManager.GetCurrentClassLogger();

    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        switch (reader.TokenType)
        {
            case JsonTokenType.Number:
                int num = reader.GetInt32();
                return (T)Enum.ToObject(typeof(T), num);

            case JsonTokenType.String:
                string s = reader.GetString();
                if (!int.TryParse(s, out int n))
                {
                    log.Warn($"EnumConverter<{typeof(T).FullName}>: Некорректное значение '{s}'");
                    return (T)Enum.ToObject(typeof(T), 0);
                }
                return (T)Enum.ToObject(typeof(T), n);

            case JsonTokenType.True:
            case JsonTokenType.False:
                return (T)Enum.ToObject(typeof(T), reader.GetBoolean());

            default:
                reader.Skip();
                log.Error($"EnumConverter<{typeof(T).FullName}>: Формат {reader.TokenType} не поддерживается");
                return (T)Enum.ToObject(typeof(T), 0);
        }
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(Convert.ToInt32(value).ToString());
    }
}
