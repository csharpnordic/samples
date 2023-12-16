namespace CodeGeneration.Enums;

/// <summary>
/// Типы данных, которые поддерживаются в таблице Table 
/// </summary>
public enum ColumnBrowseDataType
{
    /// <summary>
    /// Тип не задан
    /// </summary>
    None,
    /// <summary>
    /// Ячейка заливается цветом
    /// </summary>
    Color,
    /// <summary>
    /// Гиперссылка на объект соответствующего класса
    /// </summary>
    Hyperlink,
    /// <summary>
    /// Значок состояния: (+), (v), (x), (*)
    /// </summary>
    Icon,
    /// <summary>
    /// 
    /// </summary>
    ITreeNode,
    /// <summary>
    /// Строка
    /// </summary>
    String,
    /// <summary>
    /// Состояние - цвет и наименование, см. <seealso cref="Interfaces.IState"/>
    /// </summary>
    State,
    /// <summary>
    /// Изображение в формате SVG
    /// </summary>
    Svg,
    /// <summary>
    /// Значок из общего набора
    /// </summary>
    SvgIcon
}
