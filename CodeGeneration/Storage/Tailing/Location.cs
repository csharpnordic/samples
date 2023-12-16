using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CodeGeneration.Attributes;
using CodeGeneration.Extensions;
using CodeGeneration.Interfaces;
using CodeGeneration.Storage;
using CodeGeneration.Storage.Administration;

namespace CodeGeneration.Storage.Tailing;

/// <summary>
/// Структура хвостохранилища - местоположение
/// </summary>
[DisplayName("{'ru':'Местоположение','en':'Location'}")]
[Comment("Местоположение")]
[Table("Locations", Schema = DB.SchemaName)]
[MasterData(SubSystem.GTS)]
[ParentEntity(typeof(Plant))]
public class Location : TreeEntity,
    IPlantEntity,
    ITreeNode
{
    /// <summary>
    /// Техническое поле для завода, относящегося к данному объекту
    /// </summary>
    private Plant plant = null;

    /// <summary>
    /// Идентификатор завода
    /// </summary>
    [Comment("{'ru':'Идентификатор завода','en':'Plant identifier'}")]
    [Column("Plant_ID")]
    public Guid PlantID { get; set; }

    /// <summary>
    /// Завод
    /// </summary>
    [NotMapped]
    [DisplayName("{'ru':'Завод','en':'Plant'}")]
    public Plant Plant
    {
        get
        {
            if (plant == null)
            {
                using var db = new CodeGeneration.Storage.DB();
                plant = db.Plants.Find(PlantID);
            }

            return plant;
        }
        set
        {
            PlantID = value.ID;
            plant = null;
        }
    }

    /// <summary>
    /// Родительский элемент
    /// </summary>
    [Comment("Родительский элемент")]
    [ForeignKey(nameof(ParentID))]
    public virtual Location? Parent { get; set; }

    /// <inheritdoc />
    public IEnumerable<Guid> Plants => new List<Guid> { PlantID };

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
    /// Возвращает дерево с дочерними объектами для заданного объекта
    /// </summary>
    /// <param name="db">Контекст базы данных</param>
    /// <param name="entityId">Идентификатор местоположения</param>
    /// <returns></returns>
    public static IEnumerable<Location> GetLocationsTree(DB db, Guid entityId)
    {
        var plantLocation = db.Locations.Where(x => x.PlantID == entityId).ToList();

        if (plantLocation.Count == 0)
        {
            plantLocation = db.Locations.Where(x => x.ID == entityId).ToList();
        }

        var res = new List<Location>();
        res.AddRange(plantLocation);

        foreach (Location? location in plantLocation)
        {
            AddChildLocations(db, res, location);
        }

        return res;
    }

    /// <summary>
    /// Метод получения прямых потомков месторасположения
    /// </summary>
    /// <param name="db">Контекст базы данных</param>
    /// <param name="entityId">Идентификатор местоположения</param>
    /// <returns></returns>
    public static IEnumerable<Location> GetDirectDescendant(DB db, Guid entityId)
    {
        var plantLocation = db.Locations.Where(x => x.PlantID == entityId && x.Parent == null);

        if (plantLocation.Any())
        {
            return plantLocation;
        }

        var location = db.Locations.FirstOrDefault(x => x.ID == entityId);

        var res = new List<Location>();

        if (location != null)
        {
            res.AddRange(db.Locations.Where(x => x.ParentID == location.ID));
        }

        return res;
    }

    /// <summary>
    /// Метод добавления дочерних месторасположений
    /// </summary>
    /// <param name="db">Контекст БД</param>
    /// <param name="res">Список месторасполодений</param>
    /// <param name="location">Родительский элемент</param>
    private static void AddChildLocations(DB db, List<Location> res, Location location)
    {
        var childLocations = db.Locations.Where(x => x.ParentID == location.ID).ToList();
        res.AddRange(childLocations);
        foreach (var childLocation in childLocations)
        {
            AddChildLocations(db, res, childLocation);
        }
    }

    /// <summary>
    /// Возвращает локацию или завод с данным идентификатором. Либо null, если ничего не найдено
    /// </summary>
    /// <param name="db">Контекст БД</param>
    /// <param name="admDB">Контекст административной БД</param>
    /// <param name="entityId">Идентификатор местоположения</param>
    /// <returns></returns>
    public static NamedEntity GetLocation(DB db, AdmDB admDB, Guid entityId)
    {
        var location = db.Locations.Find(entityId);
        if (location != null)
            return location;
        var plant = admDB.Plants.Find(entityId);
        return plant;
    }

    /// <summary>
    /// Метод получения полного имени объекта
    /// </summary>
    /// <param name="lang">Код языка</param>
    /// <returns></returns>
    public string GetFullName(string lang)
    {
        using var db = new CodeGeneration.Storage.DB();
        var name = FullName;
        var plant = db.Plants.FirstOrDefault(x => x.ID == PlantID);

        if (plant != null)
        {
            name = $"{plant.Name.Localize(lang)}{FullNameDelimiter}{name}";
        }

        return name;
    }
}
