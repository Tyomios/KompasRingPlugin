namespace KompasRingPlugin.UnitTests;

/// <summary>
/// Тесты кольца.
/// </summary>
[TestFixture]
public class RingTests
{
    /// <summary>
	/// Негативный тест проверки корректности значений параметров кольца.
	/// </summary>
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

    /// <summary>
    /// Негативный тест проверки корректности значений параметров кольца.
    /// </summary>
    [Test]
    public void IsReadyForBuild_NegativeTest_NullValue()
    {
        // Arrange
        
        var expected = false;

        // Act
        var actual = Ring.IsReadyForBuild(null);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    /// <summary>
    /// Позитивный тест проверки корректности значений параметров кольца.
    /// </summary>
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

	/// <summary>
	/// Тест конструктора класса <see cref="Ring"/>.
	/// </summary>
	[Test]
	public void ConstructorTest()
	{
		// Act
		var ring = new Ring();

		// Assert
		Assert.IsNotNull(ring);
		Assert.IsNotNull(ring.Engraving);
	}

	/// <summary>
	/// Тест геттера и сеттера ширины кольца.
	/// </summary>
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

    /// <summary>
	/// Тест геттера и сеттера размера кольца.
	/// </summary>
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

    /// <summary>
    /// Тест геттера и сеттера толщины кольца.
    /// </summary>
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

    /// <summary>
    /// Тест геттера и сеттера цвета кольца.
    /// </summary>
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

    /// <summary>
    /// Тест геттера и сеттера гравировки кольца.
    /// </summary>
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

    /// <summary>
    /// Тест геттера и сеттера скругления граней кольца.
    /// </summary>
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