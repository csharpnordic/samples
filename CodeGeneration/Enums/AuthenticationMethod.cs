namespace CodeGeneration.Enums;

/// <summary>
/// Варианты аутенфикации пользователя
/// </summary>
public enum AuthenticationMethod
{
    /// <summary>
    /// Аутенфикация по имени пользователя и паролю
    /// </summary>
    BasicAuthentication,
    /// <summary>
    /// Аутенфикация с помощью Windows
    /// </summary>
    WindowsAuthentication
}
