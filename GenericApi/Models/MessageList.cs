using System.Collections.Generic;
using System.Linq;

namespace GenericApi.Models
{
    /// <summary>
    /// Список сообщений
    /// </summary>
    public class MessageList
    {
        /// <summary>
        /// Разделитель сообщений
        /// </summary>
        private const string Delimiter = "; ";

        /// <summary>
        /// Список сообщений
        /// </summary>
        private readonly List<string> list = new List<string>();

        /// <summary>
        /// Список сообщений через разделитель или пустая строка, если сообщений нет
        /// </summary>    
        public override string ToString() => list.Any() ? string.Join(Delimiter, list) : string.Empty;

        /// <summary>
        /// Добавление одного сообщения в список с контролем на дубликаты
        /// </summary>
        /// <param name="message">Сообщение</param>
        public void Add(string message)
        {
            if (!list.Contains(message))
            {
                list.Add(message);
            }
        }

        /// <summary>
        /// Добавление списка сообщений с контролем на дубликаты
        /// </summary>
        /// <param name="messages">Список сообщений</param>
        public void AddRange(IEnumerable<string> messages)
        {
            foreach (var message in messages)
            {
                Add(message);
            }
        }
    }
}
