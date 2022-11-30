namespace KompasRingPlugin.UnitTests;


[TestFixture]
public class BuildServiceTests
{
	[Test]
	public void CreateSketchOnBasePlane_StateUnderTest_ExpectedBehavior()
	{
		// Arrange
		var service = new BuildService(TODO);
		BasePlane plane = default(global::Model.BasePlane);

		// Act
		var result = service.CreateSketchOnBasePlane(
			plane);

		// Assert
		Assert.Fail();
	}

	[Test]
	public void SqueezeOut_StateUnderTest_ExpectedBehavior()
	{
		// Arrange
		var service = new BuildService(TODO);
		ksSketchDefinition sketch = null;
		double height = 0;
		bool cutMode = false;
		short blindType = 0;

		// Act
		var result = service.SqueezeOut(
			sketch,
			height,
			cutMode,
			blindType);

		// Assert
		Assert.Fail();
	}

	[Test]
	public void RoundCorners_StateUnderTest_ExpectedBehavior()
	{
		// Arrange
		var service = new BuildService(TODO);
		double radius = 0;
		List roundedEdges = null;

		// Act
		service.RoundCorners(
			radius,
			roundedEdges);

		// Assert
		Assert.Fail();
	}

	[Test]
	public void GetCircleEdges_StateUnderTest_ExpectedBehavior()
	{
		// Arrange
		var service = new BuildService(TODO);

		// Act
		var result = service.GetCircleEdges();

		// Assert
		Assert.Fail();
	}

	[Test]
	public void GetAllFaces_StateUnderTest_ExpectedBehavior()
	{
		// Arrange
		var service = new BuildService(TODO);

		// Act
		var result = service.GetAllFaces();

		// Assert
		Assert.Fail();
	}

	[Test]
	public void InjectText_StateUnderTest_ExpectedBehavior()
	{
		// Arrange
		var service = new BuildService(TODO);
		ksSketchDefinition sketch = null;
		string text = null;

		// Act
		service.InjectText(
			sketch,
			text);

		// Assert
		Assert.Fail();
	}
}
