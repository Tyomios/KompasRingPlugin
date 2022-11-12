using Kompas6API5;
using System.Threading.Tasks;

namespace Model;

/// <summary>
/// Занимается построением детали.
/// </summary>
public class RingBuilder
{
    public async void Build(Ring ring)
    {
        //if(!Ring.IsReadyForBuild(ring)) return;

        Document3D doc;
        await Task.Run((() =>
        {
            doc = KompasConnector.Instance.GetDocument().Result;
            var buildService = new BuildService(doc);
            var sketchDefinition = buildService.CreateSketch();
            CreateCircleSketch(sketchDefinition, 15);
        }));
        
    }

    /// <summary>
    /// Создает эскиз кольца.
    /// </summary>
    /// <param name="emptySketch"></param>
    /// <param name="radius"></param>
    /// <param name="sketchDefinition"></param>
    private void CreateCircleSketch(ksSketchDefinition sketchDefinition, double radius)
    {
        ksDocument2D flatDocument = (ksDocument2D)sketchDefinition.BeginEdit();
        flatDocument.ksCircle(0, 0, radius, 1);
        sketchDefinition.EndEdit();
    }
}