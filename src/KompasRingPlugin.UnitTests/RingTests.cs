namespace KompasRingPlugin.UnitTests;


[TestFixture]
public class RingTests
{
	[Test]
	public void IsReadyForBuild_NegativeTest()
	{
		// Arrange
		var ring = new Ring();
		var expected = false;

		// Act
		var actual = Ring.IsReadyForBuild(ring);

		// Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void IsReadyForBuild_PositiveTest()
	{
		// Arrange
		var ring = new Ring{ Width = 12, Radius = 15, Height = 40};
		var expected = true;

		// Act
		var actual = Ring.IsReadyForBuild(ring);

		// Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void ConstructorTest()
	{
		// Act
		var ring = new Ring();

		// Assert
		Assert.IsNotNull(ring);
		Assert.IsNotNull(ring.Engraving);
	}

	[Test]
	public void WidthSetterTest()
	{
		// Arrange
		var expectedWidth = 24;
		var ring = new Ring();

		// Act
		ring.Width = expectedWidth;
		var actual = ring.Width;


		// Assert
		Assert.AreEqual(expectedWidth, actual);
	}

	[Test]
	public void RadiusSetterTest()
	{
		// Arrange
		var expected = 12;
		var ring = new Ring();

		// Act
		ring.Radius = expected;
		var actual = ring.Radius;


		// Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void HeightSetterTest()
	{
		// Arrange
		var expected = 100;
		var ring = new Ring();

		// Act
		ring.Height = expected;
		var actual = ring.Height;


		// Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void ColorSetterTest()
	{
		// Arrange
		var expectedColor = new System.Windows.Media.Color();
		var ring = new Ring();

		// Act
		ring.Color = expectedColor;
		var actual = ring.Color;


		// Assert
		Assert.AreEqual(expectedColor, actual);
	}

	[Test]
	public void EngravingSetterTest()
	{
		// Arrange
		var expected = new Engraving();
		var ring = new Ring();

		// Act
		ring.Engraving = expected;
		var actual = ring.Engraving;


		// Assert
		Assert.AreEqual(expected, actual);
	}

	[Test]
	public void RoundScaleSetterTest()
	{
		// Arrange
		uint expected = 10;
		var ring = new Ring();

		// Act
		ring.RoundScale = expected;
		var actual = ring.RoundScale;


		// Assert
		Assert.AreEqual(expected, actual);
	}
}