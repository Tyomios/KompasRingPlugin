using System.Windows;

namespace Model;

/// <summary>
/// Занимается построением детали.
/// </summary>
public class RingBuilder
{
    public async void Build(Ring ring)
    {
        if (!Ring.IsReadyForBuild(ring))
        {
            throw new Exception("Жизненеобходимые параметры кольца не заполнены");
        }

        Document3D doc;
        await Task.Run((() =>
        {
            doc = KompasConnector.Instance.GetDocument().Result;
            var buildService = new BuildService(doc);

            var biggerCircleSketchDefinition = buildService.CreateSketchOnBasePlane();
            CreateCircleSketch(biggerCircleSketchDefinition, ring.Radius + ring.Height);

            var smallerCircleSketchDefinition = buildService.CreateSketchOnBasePlane();
            CreateCircleSketch(smallerCircleSketchDefinition, ring.Radius);

            buildService.SqueezeOut(biggerCircleSketchDefinition, ring.Width);
            buildService.CutSqueeze(smallerCircleSketchDefinition, ring.Width);

            var circleEdges = buildService.GetCircleEdges();
            if (circleEdges.Count < 2 && ring.RoundScale > 0)
            {
                MessageBox.Show("Деталь построена неверно. Недостаточно граней для скругления");
                return;
            }
            buildService.RoundCorners(ring.RoundScale, circleEdges);

            if (!ring.Engraving.Text.Equals(String.Empty))
            {
                var textSketch = buildService.CreateSketchOnBasePlane(BasePlane.XOZ);
                var fullEngravingHeight = ring.Engraving.Height + ring.Radius;

                var startPoint = GetEngravingStartPoint(ring);
                buildService.InjectText(textSketch, ring.Engraving, startPoint);
                var engraved = buildService.CutSqueeze(textSketch, fullEngravingHeight);
                buildService.ColoredPart(new System.Windows.Media.Color{A = 0, R = 0, G = 0, B = 1}, engraved);
            }

            buildService.ColoredDetail(ring.Color);
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

    private System.Windows.Point GetEngravingStartPoint(Ring ring)
    {
        var engraving = ring.Engraving;
        var startX = engraving.Text.Length * engraving.TextSize / 2;
        var startY = ring.Engraving.TextSize - ring.Width;

        return new System.Windows.Point(-startX, startY);
    }
}