using System.Drawing;
using System.Windows.Controls;
using Kompas6API5;
using KompasAPI7;

namespace Model;

/// <summary>
/// Сервис содержащий методы для построения детали.
/// </summary>
public class BuildService
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
    public void CreateSketch()
    {
        ksPart topPart = (ksPart)_document.GetPart(_topPartType);

        ksEntity _drawEntity = (ksEntity)topPart.NewEntity(5);

        ksSketchDefinition _sketchDefinition = (ksSketchDefinition)_drawEntity.GetDefinition();
        
        ksEntity entityPlane = (ksEntity)topPart.GetDefaultEntity(1);

        _sketchDefinition.SetPlane(entityPlane);
        _drawEntity.Create();
    }

    public void CreateCircleSketch(IPlane3D plane, ISketch sketch)
    {

    }

    public void CreateAuxSurface(Point point, IPlane3DTangentToFace parentPlane)
    {

    }

    public void SqueezeOut(ISketch sketch)
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