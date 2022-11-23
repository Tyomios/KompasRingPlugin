using System.Collections.ObjectModel;
using System.Drawing;
using Kompas6API5;
using KompasAPI7;

namespace Model;

/// <summary>
/// Сервис содержащий методы для построения детали.
/// </summary>
public class BuildService //todo ReadOnlyDictionary для констант. Подумать над ключами.
{
    private ksDocument3D _document;

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
    }

    /// <summary>
    /// Создает эскиз.
    /// </summary>
    /// <param name="part"></param>
    public ksSketchDefinition CreateSketch()
    {
        ksPart topPart = (ksPart)_document.GetPart(_topPartType);

        ksEntity drawEntity = (ksEntity)topPart.NewEntity(5);

        ksSketchDefinition sketchDefinition = (ksSketchDefinition)drawEntity.GetDefinition();
        
        ksEntity entityPlane = (ksEntity)topPart.GetDefaultEntity(1);

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

        var topPart = (ksPart)_document.GetPart(_topPartType);
        ksEntity extrusionEntity = null;
        dynamic extrusionDefinition = null;
        bool draftOutward = true;

        if (cutMode)
        {
            extrusionEntity = (ksEntity)topPart.NewEntity(o3d_CutExtrusion);

            extrusionDefinition = (ksCutExtrusionDefinition)extrusionEntity.GetDefinition();
            extrusionDefinition.cut = true;
            extrusionDefinition.directionType = dtReverse;
            draftOutward = false;
        }
        else if (!cutMode)
        {
            extrusionEntity = (ksEntity)topPart.NewEntity(o3d_baseExtrusion);
            extrusionDefinition = (ksBaseExtrusionDefinition)extrusionEntity.GetDefinition();
        }
        extrusionDefinition.SetSideParam(true, blindType, height, 0, draftOutward);
        extrusionDefinition.SetSketch(sketch);
        extrusionEntity.Create();

        _document.drawMode = vm_Shaded;
        _document.shadedWireframe = true;
    }

    /// <summary>
    /// Выполнение операции обечайка.
    /// </summary>
    /// <param name="sketch"> Используемый эскиз. </param>
    public void SheetMetalRuledShell(ksEntity sketch)
    {
        ksPart topPart = (ksPart)_document.GetPart(_topPartType);

        ksEntity shellEntity = (ksEntity)topPart.NewEntity(606);

        ksBaseExtrusionDefinition baseDefinition = (ksBaseExtrusionDefinition)shellEntity.GetDefinition();

        baseDefinition.SetSideParam(true);
        baseDefinition.SetSketch(sketch);

        shellEntity.Create();

    }

    public void CreateAuxSurface(Point point, IPlane3DTangentToFace parentPlane)
    {

    }

    public void Bend(IPart7 part, double bendValue)
    {

    }

    public void OpenRing(IPart7 part, IPlane3DTangentToFace parentPlane)
    {

    }

    public void InjectText(IText text, IPart7 part, ISketch sketch)
    {

    }
}