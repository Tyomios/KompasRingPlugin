using System.Windows;

namespace KompasRingPlugin.UnitTests;


[TestFixture]
public class BuildServiceTests
{
    [Test]
    public void ConstructorTest_ExpectedBehavior()
    {
        // Arrange
        var doc = KompasConnector.Instance.GetDocument().Result;

        // Act
        var service = new BuildService(doc);

        // Assert
        Assert.IsNotNull(service);
    }

    [Test]
	public void CreateSketchOnBasePlane_ExpectedBehavior()
	{
        // Arrange
        var doc = KompasConnector.Instance.GetDocument().Result;

        var service = new BuildService(doc);
        BasePlane plane = BasePlane.XOY;
        var expectedPlaneId = 1;

        // Act
        var result = service.CreateSketchOnBasePlane(plane);
        var actualPlane = (ksEntity)result.GetPlane();
        var actualPlaneId = actualPlane.type;

        // Assert
		Assert.AreEqual(expectedPlaneId, actualPlaneId);
	}

	[Test]
	public void SqueezeOut_ExpectedBehavior()
	{
        // Arrange
        var doc = KompasConnector.Instance.GetDocument().Result;
        var service = new BuildService(doc);
        var sketch = service.CreateSketchOnBasePlane();
        ksDocument2D flatDocument = (ksDocument2D)sketch.BeginEdit();
        flatDocument.ksCircle(0, 0, 30, 1);
        sketch.EndEdit();
        var height = 10;

        // Act
		var result = service.SqueezeOut(sketch, height);
        var extrusionDefinition = (ksBaseExtrusionDefinition)result.GetDefinition();
        var actualParams = (ksExtrusionParam)extrusionDefinition.ExtrusionParam();
        
        // Assert
        Assert.AreEqual(height, actualParams.depthNormal);
	}

	[Test]
	public void RoundCorners_ExpectedBehavior()
	{
        // Arrange
        var doc = KompasConnector.Instance.GetDocument().Result;
        var service = new BuildService(doc);
        var sketch = service.CreateSketchOnBasePlane();
        ksDocument2D flatDocument = (ksDocument2D)sketch.BeginEdit();
        flatDocument.ksCircle(0, 0, 30, 1);
        sketch.EndEdit();
        var result = service.SqueezeOut(sketch, 20);
        var edges = service.GetCircleEdges();
        double radius = 9;

        // Act

        // Assert
        Assert.Throws<Exception>(()=>
        {
            service.RoundCorners(radius, edges);
        });
    }

	[Test]
	public void GetCircleEdges_CoinForm()
	{
        // Arrange
        var doc = KompasConnector.Instance.GetDocument().Result;
        var service = new BuildService(doc);
        var sketch = service.CreateSketchOnBasePlane();
        ksDocument2D flatDocument = (ksDocument2D)sketch.BeginEdit();
        flatDocument.ksCircle(0, 0, 30, 1);
        sketch.EndEdit();
        var detail = service.SqueezeOut(sketch, 20);
        var expected = 2;

        // Act
        var result = service.GetCircleEdges();
        var actual = result.Count;

		// Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void GetAllFaces_ExpectedBehavior()
	{
        // Arrange
        var doc = KompasConnector.Instance.GetDocument().Result;
        var service = new BuildService(doc);
        var sketch = service.CreateSketchOnBasePlane();
        ksDocument2D flatDocument = (ksDocument2D)sketch.BeginEdit();
        flatDocument.ksCircle(0, 0, 30, 1);
        sketch.EndEdit();
        var detail = service.SqueezeOut(sketch, 20);
        var expected = 3;

        // Act
        var result = service.GetAllFaces();
        var actual = result.GetCount();

		// Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void InjectText_StateUnderTest_ExpectedBehavior()
	{
        // Arrange
        var doc = KompasConnector.Instance.GetDocument().Result;
        var service = new BuildService(doc);
        var sketch = service.CreateSketchOnBasePlane();
        var engraving = new Engraving { Text = "Тест", TextSize = 4};

        // Act


        // Assert
        Assert.Throws<Exception>(() =>
        {
            service.InjectText(sketch, engraving, new Point(0, 0));
        });
    }
}
