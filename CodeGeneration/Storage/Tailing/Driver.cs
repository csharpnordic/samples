using CodeGeneration.Attributes;
using CodeGeneration.Storage;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Драйверы
/// </summary>
[DisplayName("{'ru':'Драйверы','en':'Drivers'}")]
[Comment("Драйверы")]
[Table("Drivers", Schema = DB.SchemaName)]
[MasterData(CodeGeneration.Storage.Administration.SubSystem.GTS)]
public class Driver : NamedEntity
{
    /// <summary>
    /// Адрес драйвера 
    /// </summary>       
    [DisplayName("{'ru':'Адрес драйвера','en':'Url'}")]
    [Comment("Адрес драйвера")]
    [MaxLength(1000)]
    public string? Url { get; set; }
}
