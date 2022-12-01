using Kompas6API5;

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
        var actualParams = (ksExtrusionParam)result.GetDefinition();

        // Assert
		Assert.AreEqual(height, actualParams.depthNormal);
	}

	[Test]
	public void RoundCorners_StateUnderTest_ExpectedBehavior()
	{
        // Arrange
        var doc = KompasConnector.Instance.GetDocument().Result;
        var service = new BuildService(doc);
		double radius = 0;

        // Act
		//service.RoundCorners(radius, roundedEdges);

		// Assert
		Assert.Fail();
	}

	[Test]
	public void GetCircleEdges_StateUnderTest_ExpectedBehavior()
	{
        // Arrange
        var doc = KompasConnector.Instance.GetDocument().Result;
        var service = new BuildService(doc);

		// Act
		var result = service.GetCircleEdges();

		// Assert
		Assert.Fail();
	}

	[Test]
	public void GetAllFaces_StateUnderTest_ExpectedBehavior()
	{
        // Arrange
        var doc = KompasConnector.Instance.GetDocument().Result;
        var service = new BuildService(doc);

		// Act
		var result = service.GetAllFaces();

		// Assert
		Assert.Fail();
	}

	[Test]
	public void InjectText_StateUnderTest_ExpectedBehavior()
	{
        // Arrange
        var doc = KompasConnector.Instance.GetDocument().Result;
        var service = new BuildService(doc);
		ksSketchDefinition sketch = null;
		string text = null;

		// Act
		//service.InjectText(sketch, text);

		// Assert
		Assert.Fail();
	}
}
