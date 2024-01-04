// ******************************************************************//
// Файл строится автоматически на основе перечислимых типов проекта  //
// ******************************************************************//

using CodeGeneration.Enums;

namespace CodeGeneration.Serialization
{
    /// <summary>
    /// Особая сериализация типа <seealso cref="AccessLevel"/>
    /// </summary>
    public class AccessLevelConverter : EnumConverter<AccessLevel> { }
    /// <summary>
    /// Особая сериализация типа <seealso cref="AuthenticationMethod"/>
    /// </summary>
    public class AuthenticationMethodConverter : EnumConverter<AuthenticationMethod> { }
    /// <summary>
    /// Особая сериализация типа <seealso cref="ColumnBrowseDataType"/>
    /// </summary>
    public class ColumnBrowseDataTypeConverter : EnumConverter<ColumnBrowseDataType> { }
    /// <summary>
    /// Особая сериализация типа <seealso cref="DateType"/>
    /// </summary>
    public class DateTypeConverter : EnumConverter<DateType> { }
    /// <summary>
    /// Особая сериализация типа <seealso cref="FeatureType"/>
    /// </summary>
    public class FeatureTypeConverter : EnumConverter<FeatureType> { }
    /// <summary>
    /// Особая сериализация типа <seealso cref="IconType"/>
    /// </summary>
    public class IconTypeConverter : EnumConverter<IconType> { }
    /// <summary>
    /// Особая сериализация типа <seealso cref="NotificationsStatus"/>
    /// </summary>
    public class NotificationsStatusConverter : EnumConverter<NotificationsStatus> { }
    /// <summary>
    /// Особая сериализация типа <seealso cref="NotificationType"/>
    /// </summary>
    public class NotificationTypeConverter : EnumConverter<NotificationType> { }
    /// <summary>
    /// Особая сериализация типа <seealso cref="StaffType"/>
    /// </summary>
    public class StaffTypeConverter : EnumConverter<StaffType> { }
}