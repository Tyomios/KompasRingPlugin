using System.Drawing;
using KompasAPI7;

namespace Model;

/// <summary>
/// Сервис содержащий методы для построения детали.
/// </summary>
public class BuildService
{
    private IKompasDocument3D _document;

    /// <summary>
    /// создает экземпляр класса <see cref="BuildService"/>.
    /// </summary>
    /// <param name="document">
    /// Используемый для построения документ.
    /// </param>
    public BuildService(IKompasDocument3D document)
    {
        _document = document;
    }

    public void CreateSketch(IPart7 part, ISketch sketch)
    {
        
    }

    public void CreateSketch(IPlane3D plane, ISketch sketch)
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