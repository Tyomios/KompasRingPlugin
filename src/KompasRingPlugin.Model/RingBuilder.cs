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
            var biggerCircleSketchDefinition = buildService.CreateSketch();
            CreateCircleSketch(biggerCircleSketchDefinition, 15);

            var smallerCircleSketchDefinition = buildService.CreateSketch();
            CreateCircleSketch(smallerCircleSketchDefinition, 12);

            buildService.SqueezeOut(biggerCircleSketchDefinition, 10);
            buildService.SqueezeOut(smallerCircleSketchDefinition, 10, true);


        }));
        
    }

    /// <summary>
    /// Создает эскиз кольца.
    /// </summary>
    /// <param name="radius"></param>
    /// <param name="sketchDefinition"></param>
    private void CreateCircleSketch(ksSketchDefinition sketchDefinition, double radius)
    {
        ksDocument2D flatDocument = (ksDocument2D)sketchDefinition.BeginEdit();
        flatDocument.ksCircle(0, 0, radius, 1);
        sketchDefinition.EndEdit();
    }
}