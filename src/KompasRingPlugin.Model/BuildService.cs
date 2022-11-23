using System.Collections.Generic;
using System.Windows.Documents;

namespace Model;

/// <summary>
/// Сервис содержащий методы для построения детали.
/// </summary>
public class BuildService //todo ReadOnlyDictionary для констант. Подумать над ключами.
{
    /// <summary>
    /// Документ для построения детали.
    /// </summary>
    private ksDocument3D _document;

    /// <summary>
    /// Первый элемент в дереве элементов документа.
    /// </summary>
    private ksPart _topPart;

    private const int _topPartType = -1;

    /// <summary>
    /// создает экземпляр класса <see cref="BuildService"/>.
    /// </summary>
    /// <param name="document">
    /// Используемый для построения документ.
    /// </param>
    public BuildService(ksDocument3D document)
    {
        _document = document;
        _topPart = (ksPart)_document.GetPart(_topPartType);
    }

    /// <summary>
    /// Создает эскиз.
    /// </summary>
    /// <param name="part"></param>
    public ksSketchDefinition CreateSketch()
    {
        var drawEntity = (ksEntity)_topPart.NewEntity(5);
        var sketchDefinition = (ksSketchDefinition)drawEntity.GetDefinition();
        var entityPlane = (ksEntity)_topPart.GetDefaultEntity(1);

        sketchDefinition.SetPlane(entityPlane);
        drawEntity.Create();

        return sketchDefinition;
    }

    /// <summary>
    /// Выполняет операцию выдавливания по эскизу на заданное расстояние.
    /// </summary>
    /// <param name="sketch"> Эскиз для выдавливания. </param>
    /// <param name="height"> Расстояние выдавливания. </param>
    /// <param name="blindType"> Тип выдавливания (по умолчанию задан на «Строго в глубину»). </param>
    public void SqueezeOut(ksSketchDefinition sketch, double height, bool cutMode = false, short blindType = 0)
    {
        // Указывает на создание операции выдавливания.
        const int o3d_baseExtrusion = 24;

        // Вырезать выдавливанием.
        const int o3d_CutExtrusion = 26;

        // Тип объекта DrawMode. Устанавливает полутоновое изображение модели
        const int vm_Shaded = 3;

        //Тип направления вырезания. Обратное направление.
        const int dtReverse = 1;

        ksEntity extrusionEntity = null;
        dynamic extrusionDefinition = null;
        bool draftOutward = true;

        if (cutMode)
        {
            extrusionEntity = (ksEntity)_topPart.NewEntity(o3d_CutExtrusion);

            extrusionDefinition = (ksCutExtrusionDefinition)extrusionEntity.GetDefinition();
            extrusionDefinition.cut = true;
            extrusionDefinition.directionType = dtReverse;
            draftOutward = false;
        }
        else if (!cutMode)
        {
            extrusionEntity = (ksEntity)_topPart.NewEntity(o3d_baseExtrusion);
            extrusionDefinition = (ksBaseExtrusionDefinition)extrusionEntity.GetDefinition();
        }
        extrusionDefinition.SetSideParam(true, blindType, height, 0, draftOutward);
        extrusionDefinition.SetSketch(sketch);
        extrusionEntity.Create();

        _document.drawMode = vm_Shaded;
        _document.shadedWireframe = true;
    }

    /// <summary>
    /// Проводит операцию скругления на ребрах.
    /// </summary>
    /// <param name="radius"> Угол скругления. </param>
    /// <param name="roundedEdges"> Ребра скругления. </param>
    public void RoundCorners(double radius, List<ksEdgeDefinition> roundedEdges)
    {
        short o3d_fillet = 34;

        var filletEntity = (ksEntity)_topPart.NewEntity(o3d_fillet);
        var filletDefinition = (ksFilletDefinition)filletEntity.GetDefinition();
        filletDefinition.radius = radius;

        var items = (ksEntityCollection)filletDefinition.array();
        roundedEdges.ForEach(edge => items.Add(edge));

        filletEntity.Create();
    }

    public List<ksEdgeDefinition> GetCircleEdges(ksEntity part)
    {
        var edge = (ksEdgeDefinition)part.GetDefinition();
        var edges = (ksEdgeCollection)edge.EdgeCollection(true);

        var items = new List<ksEdgeDefinition>();
        for (int i = 0; i < edges.GetCount(); ++i)
        {
            var currentEdge = (ksEdgeDefinition)edges.GetByIndex(i);
            if (currentEdge.IsCircle())
            {
                items.Add(currentEdge);
            }
        }

        return items;
    }

    public void InjectText(IText text, IPart7 part, ISketch sketch)
    {

    }
}