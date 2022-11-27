using Kompas6API5;
using System.Collections.Generic;


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

    /// <summary>
    /// создает экземпляр класса <see cref="BuildService"/>.
    /// </summary>
    /// <param name="document">
    /// Используемый для построения документ.
    /// </param>
    public BuildService(ksDocument3D document)
    {
        _document = document;
        var topPartType = -1;
        _topPart = (ksPart)_document.GetPart(topPartType);
    }

    /// <summary>
    /// Создает эскиз на одной из базовых плоскостей.
    /// </summary>
    /// <param name="plane"> Базовая плоскость на которой строится эскиз.</param>
    /// <returns>
    /// Пустой эскиз.
    /// </returns>
    public ksSketchDefinition CreateSketchOnBasePlane(BasePlane plane = BasePlane.XOY)
    {
        var drawEntity = (ksEntity)_topPart.NewEntity(5);
        var sketchDefinition = (ksSketchDefinition)drawEntity.GetDefinition();
        var entityPlane = (ksEntity)_topPart.GetDefaultEntity((short)plane);

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
    public ksEntity SqueezeOut(ksSketchDefinition sketch, double height, bool cutMode = false, short blindType = 0)
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
        
        return extrusionEntity;
    }

    /// <summary>
    /// Проводит операцию скругления на ребрах.
    /// </summary>
    /// <param name="radius"> Угол скругления. </param>
    /// <param name="roundedEdges"> Ребра скругления. </param>
    public void RoundCorners(double radius, List<ksEdgeDefinition> roundedEdges)
    {
        if (roundedEdges.Count.Equals(0))
        {
            throw new Exception("Переданный список ребер пуст.");
        }

        short o3d_fillet = 34;

        var filletEntity = (ksEntity)_topPart.NewEntity(o3d_fillet);
        ksFilletDefinition filletDefinition = (ksFilletDefinition)filletEntity.GetDefinition();
        ksEntityCollection items = (ksEntityCollection)filletDefinition.array();

        filletDefinition.radius = radius;
        roundedEdges.ForEach(edge => items.Add(edge));
        filletEntity.Create();
    }

    /// <summary>
    /// Возвращает список ребер плоских поверхностей.
    /// </summary>
    /// <returns>  </returns>
    public List<ksEdgeDefinition> GetCircleEdges()
    {
        var faces = GetAllFaces();
        var facesCount = faces.GetCount();
        if (facesCount == 0)
        {
            return new List<ksEdgeDefinition>();
        }

        var planeFaces = new List<ksFaceDefinition>();
        var i = 0;
        while (faces.Next() is not null)
        {
            var currentFace = (ksFaceDefinition)faces.GetByIndex(i);
            if (currentFace.IsPlanar())
            {
                planeFaces.Add(currentFace);
            }

            ++i;
        }

        if (planeFaces.Count > 0)
        {
            var edges = new List<ksEdgeDefinition>();
            foreach (var face in planeFaces)
            {
                var j = 0;
                var currentEdgeCollection = (ksEdgeCollection)face.EdgeCollection();
                while (currentEdgeCollection.Next() is not null)
                {
                    var edge = (ksEdgeDefinition)currentEdgeCollection.GetByIndex(j);
                    edges.Add(edge);

                    ++j;
                }
            }
            return edges;

        }
        var items = new List<ksEdgeDefinition>();
        return items;
    }

    /// <summary>
    /// Получает все поверхности твердотельного объекта документа.
    /// </summary>
    /// <returns></returns>
    public ksFaceCollection GetAllFaces()
    {
        var body = (ksBody)_topPart.GetMainBody();
        var faces = (ksFaceCollection)body.FaceCollection();

        return faces;
    }

    public void InjectText(ksSketchDefinition sketch, string text)
    {
        ksDocument2D flatDocument = (ksDocument2D)sketch.BeginEdit();
        flatDocument.ksText(0,0,0,4,0, 0, text);
        sketch.EndEdit();
    }
}