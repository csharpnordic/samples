using CodeGeneration.Attributes;
using CodeGeneration.Storage.Administration;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using CodeGeneration.Interfaces;
using CodeGeneration.Storage;
using System.ComponentModel.DataAnnotations;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Виды наблюдения
/// </summary>
[DisplayName("{'ru':'Виды наблюдения','en':'Types of observation'}")]
[Comment("Виды наблюдения")]
[Table("MonitoringTypes", Schema = DB.SchemaName)]
[MasterData(SubSystem.GTS)]
public class MonitoringType : TreeEntity,
    ITreeNode
{
    /// <summary>
    /// Родительский элемент
    /// </summary>
    [Comment("Родительский элемент")]
    [ForeignKey(nameof(ParentID))]
    public virtual MonitoringType? Parent { get; set; }

    /// <summary>
    /// Родительский узел дерева
    /// </summary>
    public ITreeNode ParentNode => (Parent != null) ? (ITreeNode)Parent : null;

    // <summary>
    /// Узел дерева доступен для выбора
    /// </summary>
    [Display(AutoGenerateField = false)]
    public bool IsSelectable => true;

    /// <summary>
    /// Метод получения всех потомков
    /// </summary>
    /// <param name="db">Контекст БД</param>
    /// <param name="result">Список потомков</param>
    public void GetChildren(DB db, List<MonitoringType> result)
    {
        var directDescendants = db.MonitoringTypes
            .Where(t => t.ParentID == ID).ToList();

        result.AddRange(directDescendants);

        foreach (var child in directDescendants)
        {
            child.GetChildren(db, result);
        }
    }
}
