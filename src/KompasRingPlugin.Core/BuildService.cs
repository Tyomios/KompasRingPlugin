using System.Collections.Generic;
using Model;
using Color = System.Windows.Media.Color;


namespace Core;

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
    /// Перекрашивает компонент в указанный цвет.
    /// </summary>
    /// <param name="color">Цвет покраски</param>
    /// <param name="part">Деталь</param>
    public void ColoredPart(Color color, ksEntity part)
    {
        var commonColor = 0.50;
        var diffusion = 0.60;
        var mirroring = 0.80;
        var bright = 0.80;
        var rays = 0.50;

        int hex = (255 << color.A) | (color.B << 16) | (color.G << 8) | (color.R << 0);
        part.SetAdvancedColor(hex, 
            commonColor, diffusion, mirroring, bright, 1, rays);
        part.Update();
    }

    /// <summary>
    /// Перекрашивает деталь в указанный цвет.
    /// </summary>
    /// <param name="color">Цвет покраски</param>
    public void ColoredDetail(Color color)
    {
        var commonColor = 0.50;
        var diffusion = 0.60;
        var mirroring = 0.80;
        var bright = 0.80;
        var rays = 0.50;

        int hex = (255 << color.A) | (color.B << 16) | (color.G << 8) | (color.R << 0);
        _topPart.SetAdvancedColor(hex,
            commonColor, diffusion, mirroring, bright, 1, rays);
        _topPart.Update();
    }

    /// <summary>
    /// Создает эскиз на одной из базовых плоскостей.
    /// </summary>
    /// <param name="plane"> Базовая плоскость на которой строится эскиз.</param>
    /// <returns>
    /// Пустой эскиз.
    /// </returns>
    public ksSketchDefinition CreateSketch(BasePlane plane = BasePlane.XOY)
    {
        var drawEntity = (ksEntity)_topPart.NewEntity(5);
        var sketchDefinition = (ksSketchDefinition)drawEntity.GetDefinition();
        var entityPlane = (ksEntity)_topPart.GetDefaultEntity((short)plane);

        sketchDefinition.SetPlane(entityPlane);
        drawEntity.Create();

        return sketchDefinition;
    }

    /// <summary>
    /// Создает эскиз на смещенной плоскости.
    /// </summary>
    /// <param name="offsetPlane"> Смещенная плоскость на которой строится эскиз.</param>
    /// <returns>
    /// Пустой эскиз.
    /// </returns>
    public ksSketchDefinition CreateSketch(ksPlaneOffsetDefinition offsetPlane)
    {
        var drawEntity = (ksEntity)_topPart.NewEntity(5);
        var sketchDefinition = (ksSketchDefinition)drawEntity.GetDefinition();

        sketchDefinition.SetPlane(offsetPlane);
        drawEntity.Create();

        return sketchDefinition;
    }

    /// <summary>
    /// Создает смещенную плоскость относительно одной из базовой.
    /// </summary>
    /// <param name="plane"> Базовая плоскость. </param>
    /// <param name="offset"> Смещение. </param>
    /// <returns> Определение смещенной плоскости. </returns>
    public ksPlaneOffsetDefinition CreateAdditionPlane(BasePlane plane, double offset)
    {
        var additionPlaneEntity = (ksEntity)_topPart.NewEntity(14);
        var entityPlane = (ksEntity)_topPart.GetDefaultEntity((short)plane);

        var planeOffsetDefinition = (ksPlaneOffsetDefinition)additionPlaneEntity.GetDefinition();
        planeOffsetDefinition.SetPlane(entityPlane);
        planeOffsetDefinition.offset = offset;

        additionPlaneEntity.Create();
        return planeOffsetDefinition;
    }

    /// <summary>
    /// Выполняет операцию выдавливания по эскизу на заданное расстояние.
    /// </summary>
    /// <param name="sketch"> Эскиз для выдавливания. </param>
    /// <param name="height"> Расстояние выдавливания. </param>
    /// <param name="blindType"> Тип выдавливания (по умолчанию задан на «Строго в глубину»). </param>
    public ksEntity SqueezeOut(ksSketchDefinition sketch, double height, short blindType = 0)
    {
        // Указывает на создание операции выдавливания.
        const int o3d_baseExtrusion = 24;

        // Тип объекта DrawMode. Устанавливает полутоновое изображение модели
        const int vm_Shaded = 3;

        var extrusionEntity = (ksEntity)_topPart.NewEntity(o3d_baseExtrusion);
        var extrusionDefinition = (ksBaseExtrusionDefinition)extrusionEntity.GetDefinition();
        extrusionDefinition.SetSideParam(true, blindType, height, 0, true);
        extrusionDefinition.SetSketch(sketch);
        extrusionEntity.Create();

        _document.drawMode = vm_Shaded;
        _document.shadedWireframe = true;
        
        return extrusionEntity;
    }

    /// <summary>
    /// Выполняет операцию вырезания выдавливанием по переданному эскизу.
    /// </summary>
    /// <param name="sketch"> Эскиз для вырезания выдавливанием. </param>
    /// <param name="height"> Толщина расстояние выдавливания. </param>
    /// <param name="blindType"> тип выдавливания по расстоянию. </param>
    /// <returns> Объект операции вырезания выдавливанием.  </returns>
    public ksEntity CutSqueeze(ksSketchDefinition sketch, double height)
    {
        // Вырезать выдавливанием.
        const int o3d_CutExtrusion = 26;

        // Тип объекта DrawMode. Устанавливает полутоновое изображение модели
        const int vm_Shaded = 3;

        var extrusionEntity = (ksEntity)_topPart.NewEntity(o3d_CutExtrusion);
        var extrusionDefinition = (ksCutExtrusionDefinition)extrusionEntity.GetDefinition();
        extrusionDefinition.SetSketch(sketch);
        extrusionDefinition.cut = true;
        extrusionDefinition.SetSideParam(false, 0, height); //todo не ставить true - ставит расстояние 10.

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
    /// Возвращает список наибольших ребер плоских поверхностей.
    /// </summary>
    /// <returns>  </returns>
    public List<ksEdgeDefinition> GetCircleEdges()
    {
        var cylinderFaces = GetCylinderFaces();
        ksFaceDefinition biggerFace = null;
        if (cylinderFaces.Count.Equals(0))
        {
            return new List<ksEdgeDefinition>();
        }
        if (cylinderFaces.Count.Equals(1))
        {
            biggerFace = cylinderFaces[0];
        }
        if (cylinderFaces.Count.Equals(2))
        {
            biggerFace = cylinderFaces[0].GetArea(0x1) > cylinderFaces[1].GetArea(0x1)
                ? cylinderFaces[0] : cylinderFaces[1];
        }
        

        var edges = new List<ksEdgeDefinition>();
        var j = 0;
        var currentEdgeCollection = (ksEdgeCollection)biggerFace.EdgeCollection();

        while (currentEdgeCollection.Next() is not null)
        {
            var edge = (ksEdgeDefinition)currentEdgeCollection.GetByIndex(j);
            edges.Add(edge);

            ++j;
        }
        return edges;
    }

    /// <summary>
    /// Возвращает все цилиндрические грани детали.
    /// </summary>
    /// <returns> Список цилиндрических граней. </returns>
    public List<ksFaceDefinition> GetCylinderFaces()
    {
        var faces = GetAllFaces();
        var facesCount = faces.GetCount();
        if (facesCount == 0)
        {
            return new List<ksFaceDefinition>();
        }

        var cylinderFaces = new List<ksFaceDefinition>();
        var i = 0;
        while (faces.Next() is not null)
        {
            var currentFace = (ksFaceDefinition)faces.GetByIndex(i);
            if (currentFace.IsCylinder())
            {
                cylinderFaces.Add(currentFace);
            }

            ++i;
        }

        return cylinderFaces;
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

    /// <summary>
    /// Наносит текст на эскиз.
    /// </summary>
    /// <param name="sketch"> Используемый эскиз </param>
    /// <param name="engraving"> Гравировка </param>
    /// <param name="startLocation"> Расположение текста </param>
    public void InjectText(ksSketchDefinition sketch, Engraving engraving, System.Windows.Point startLocation)
    {
        var charSize = engraving.TextSize != 0 ? engraving.TextSize : 0;
        ksDocument2D flatDocument = (ksDocument2D)sketch.BeginEdit();
        flatDocument.ksText(startLocation.X / 2, startLocation.Y,0,charSize,0, 0, engraving.Text);
        sketch.EndEdit();
    }
}