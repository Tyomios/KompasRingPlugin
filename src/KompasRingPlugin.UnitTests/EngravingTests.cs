namespace KompasRingPlugin.UnitTests;

/// <summary>
/// Тесты гравировки кольца.
/// </summary>
[TestFixture]
public class EngravingTests
{
	/// <summary>
	/// Позитивный тест конструктора гравировки.
	/// </summary>
	[Test]
	public void ConstructorTest()
	{
		// Arrange
		var expectedText = String.Empty;
		var expectedTextSize = 4;
		var expectedHeight = 2;

		// Act
		var actual = new Engraving();

		// Assert
		Assert.AreEqual(expectedText, actual.Text);
		Assert.AreEqual(expectedTextSize, actual.TextSize);
		Assert.AreEqual(expectedHeight, actual.Height);
	}

    /// <summary>
	/// Позитивный тест геттера и сеттера текста гравировки.
	/// </summary>
	[Test]
	public void TextSetterTest()
	{
		// Arrange
		var expectedText = "Тестовые данные";
		var engraving = new Engraving();

		// Act
		engraving.Text = expectedText;
		var actual = engraving.Text;


		// Assert
		Assert.AreEqual(expectedText, actual);
	}

    /// <summary>
	/// Позитивный тест геттера и сеттера размера текста гравировки.
	/// </summary>
	[Test]
	public void TextSizeSetterTest()
	{
		// Arrange
		uint expected = 12;
		var engraving = new Engraving();

		// Act
		engraving.TextSize = expected;
		var actual = engraving.TextSize;


		// Assert
		Assert.AreEqual(expected, actual);
	}

	/// <summary>
	/// Позитивный тест геттера и сеттера глубины гравировки.
	/// </summary>
	[Test]
	public void HeightSetterTest()
	{
		// Arrange
		var expected = 100;
		var engraving = new Engraving();

		// Act
		engraving.Height = expected;
		var actual = engraving.Height;


		// Assert
		Assert.AreEqual(expected, actual);
	}
}
