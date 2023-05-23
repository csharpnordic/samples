using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleSolver.Extenders
{
    /// <summary>
    /// Класс расширения
    /// </summary>
    public static class PropertyInfoExtender
    {
        /// <summary>
        /// Порядок сортировки полей класса
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public static int Order(this PropertyInfo property)
        {
            var attr = property.GetCustomAttribute<DisplayAttribute>();

            return (attr != null && attr.Order > 0) ? attr.Order : int.MaxValue;
        }

        /// <summary>
        /// Наименование элемента перечислимого типа
        /// </summary>
        /// <param name="enumerate">Перечислимый тип</param>
        /// <returns></returns>
        public static string Description(this Enum enumerate)
        {
            if (enumerate == null) return null; // Проверка, если значение не задано
            var type = enumerate.GetType();
            var fieldInfo = type.GetField(enumerate.ToString());
            DescriptionAttribute attr = null;
            if (fieldInfo != null)
            {
                attr = fieldInfo.GetCustomAttribute<DescriptionAttribute>();
            }
            return attr != null ? attr.Description : enumerate.ToString();
        }
    }
}
