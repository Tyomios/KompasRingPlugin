﻿using Kompas6API5;
using KompasAPI7;
using System.Collections.Generic;
using System.Windows.Media.Media3D;
using ColorHelper;
using Color = System.Windows.Media.Color;
using ColorConverter = ColorHelper.ColorConverter;


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

    public void ColoredPart(Color color)
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

    public ksEntity CutSqueeze(ksSketchDefinition sketch, double height, short blindType = 0)
    {
        // Вырезать выдавливанием.
        const int o3d_CutExtrusion = 26;

        // Тип объекта DrawMode. Устанавливает полутоновое изображение модели
        const int vm_Shaded = 3;

        var extrusionEntity = (ksEntity)_topPart.NewEntity(o3d_CutExtrusion);
        var extrusionDefinition = (ksCutExtrusionDefinition)extrusionEntity.GetDefinition();
        extrusionDefinition.SetSketch(sketch);
        extrusionDefinition.cut = true;
        extrusionDefinition.SetSideParam(false, 0, height);

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

    public void InjectText(ksSketchDefinition sketch, Engraving engraving, System.Windows.Point startLocation)
    {
        var charSize = engraving.TextSize != 0 ? engraving.TextSize : 0;
        ksDocument2D flatDocument = (ksDocument2D)sketch.BeginEdit();
        flatDocument.ksText(startLocation.X,startLocation.Y,0,charSize,0, 0, engraving.Text);
        sketch.EndEdit();
    }
}