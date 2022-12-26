using System.Windows.Media.Media3D;

namespace KompasRingPlugin.UnitTests;

/// <summary>
/// Тесты валидатора параметров кольца.
/// </summary>
[TestFixture]
public class RingParamsValidatorTests
{
    /// <summary>
    /// Позитивный тест валидации кольца.
    /// </summary>
    [Test]
    public void CheckCorrectValuesTest_ExpectedBehavior()
    {
        // Arrange
        var ring = new Ring();
        ring.Width = 30;
        ring.Height = 30;
        ring.Radius = 40;
        ring.JewelryAngle = 45;
        ring.RoundScale = 5;
        Assert.DoesNotThrow(() => RingParamsValidator.CheckCorrectValues(ring));
    }

    /// <summary>
    /// Негативный тест валидации скругления граней кольца.
    /// </summary>
    [Test]
	public void CheckCorrectValuesTest_ValidateRoundCornersValue()
	{
		// Arrange
		var ring = new Ring();
		ring.Radius = 20;
		ring.Width = 10;
		ring.RoundScale = 11;

		var ex = Assert.Throws<Exception>(() => RingParamsValidator.CheckCorrectValues(ring));

		Assert.That(ex.Message, Is.EqualTo("1. Значение скругления превышает толщину кольца.\n"));
	}

    /// <summary>
    /// Негативный тест валидации скругления граней кольца c установленным углом выреза.
    /// </summary>
    [Test]
    public void CheckCorrectValuesTest_ValidateRoundCornersValue_JeverlyCutSet()
    {
        // Arrange
        var ring = new Ring();
        ring.Radius = 20;
        ring.Width = 10;
        ring.RoundScale = 5;
        ring.JewelryAngle = 45;

        var ex = Assert.Throws<Exception>(() => RingParamsValidator.CheckCorrectValues(ring));

        Assert.That(ex.Message, Is.EqualTo("1. Невозможно построить корректную деталь " +
                                           "при указанных значениях скругления и ювелирного выреза.\n"));
    }

    /// <summary>
    /// Позитивный тест валидации скругления граней кольца c установленным углом выреза.
    /// </summary>
    [Test]
    public void CheckCorrectValuesTest_ValidateRoundCornersValue_JeverlyCutSet_ExpectedBehavior()
    {
        // Arrange
        var ring = new Ring();
        ring.Radius = 20;
        ring.Width = 10;
        ring.RoundScale = 4;
        ring.JewelryAngle = 45;

        // Act
        Assert.DoesNotThrow(() => RingParamsValidator.CheckCorrectValues(ring));
    }

    /// <summary>
    /// Позитивный тест валидации скругления граней кольца.
    /// </summary>
    [Test]
    public void CheckCorrectValuesTest_ValidateRoundCornersValue_ExpectedBehavior()
    {
        // Arrange
        var ring = new Ring();
        ring.Radius = 20;
        ring.Width = 10;
        ring.RoundScale = 4;

        Assert.DoesNotThrow(() => RingParamsValidator.CheckCorrectValues(ring));
    }

    /// <summary>
    /// Негативный тест валидации размера текста гравировки.
    /// </summary>
    [Test]
	public void CheckCorrectValuesTest_ValidateTextSizeValue_TextSizeBiggerThanRingWidth()
	{
		// Arrange
		var ring = new Ring();
		ring.Radius = 70;
		ring.Engraving.Text = "Test";
		ring.Engraving.TextSize = 12;
		ring.Width = 10;

		var ex = Assert.Throws<Exception>(() => RingParamsValidator.CheckCorrectValues(ring));

		Assert.That(ex.Message, Is.EqualTo("1. Значение размера текста превышает толщину кольца.\n"));
	}

	/// <summary>
	/// Негативный тест валидации размера текста гравировки.
	/// </summary>
	[Test]
	public void CheckCorrectValuesTest_ValidateTextSizeValue_TextSizeBiggerThanHalfRingWidth()
	{
		// Arrange
		var ring = new Ring();
		ring.Engraving.Text = "Tt";
		ring.Engraving.TextSize = 7;
		ring.Radius = 10;
		ring.Width = 12;

		var ex = Assert.Throws<Exception>(() => RingParamsValidator.CheckCorrectValues(ring));

		Assert.That(ex.Message, Is.EqualTo("1. Значение размера текста превышает толщину кольца.\n"));
	}

	/// <summary>
	/// Позитивный тест валидации размера текста гравировки.
	/// </summary>
	[Test]
	public void CheckCorrectValuesTest_ValidateTextSizeValue_ExpectedBehavior()
	{
		// Arrange
		var ring = new Ring();
		ring.Radius = 30;
		ring.Engraving.Text = "Test";
		ring.Engraving.TextSize = 5;
		ring.Width = 12;

		Assert.DoesNotThrow(() => RingParamsValidator.CheckCorrectValues(ring));
	}

	/// <summary>
	/// Позитивный тест валидации размера текста гравировки при пустом содержании.
	/// </summary>
	[Test]
	public void CheckCorrectValuesTest_ValidateTextSizeValue_EmptyText()
	{
		// Arrange
		var ring = new Ring();
        ring.Radius = 30;
		ring.Engraving.TextSize = 8;
		ring.Width = 12;

        // Act
        Assert.DoesNotThrow(() => RingParamsValidator.CheckCorrectValues(ring));
    }
	

	/// <summary>
	/// Негативный тест валидации угла выдавливания кольца.
	/// </summary>
	[Test]
	public void CheckCorrectValuesTest_ValidateJewelryAngleValue_FullCircleValue()
	{
		// Arrange
		var ring = new Ring();
		ring.Engraving.Text = "Tt";
		ring.Engraving.TextSize = 3;
		ring.Radius = 10;
		ring.Width = 12;
		ring.JewelryAngle = 360;

        var ex = Assert.Throws<Exception>(() => RingParamsValidator.CheckCorrectValues(ring));

        Assert.That(ex.Message, Is.EqualTo("1. При выбранном угле выреза нарушена целостность кольца.\n"));
    }

	/// <summary>
	/// Тест валидации угла выдавливания кольца при отсутствии гравировки.
	/// </summary>
	[Test]
	public void CheckCorrectValuesTest_ValidateJewelryAngleValue_EngravingIsNull()
	{
		// Arrange
		var ring = new Ring();
		ring.Engraving.Text = String.Empty;
		ring.Engraving.TextSize = 3;
		ring.Radius = 10;
		ring.Width = 12;
		ring.JewelryAngle = 315;

        // Act
        Assert.DoesNotThrow(() => RingParamsValidator.CheckCorrectValues(ring));
    }

	/// <summary>
	/// Тест валидации угла выдавливания кольца при установленной гравировке.
	/// </summary>
	[Test]
	public void CheckCorrectValuesTest_ValidateJewelryAngleValue_EngravingSet()
	{
		// Arrange
		var ring = new Ring();
		ring.Engraving.Text = "test";
		ring.Engraving.TextSize = 3;
		ring.Radius = 10;
		ring.Width = 12;
		ring.JewelryAngle = 315;

        var ex = Assert.Throws<Exception>(() => RingParamsValidator.CheckCorrectValues(ring));

        Assert.That(ex.Message, Is.EqualTo("1. При выбранном угле выреза нарушена целостность гравировки кольца.\n"));
    }

	/// <summary>
	/// Позитивный тест валидации угла выдавливания 225 градусов при установленной гравировке.
	/// </summary>
	[Test]
	public void CheckCorrectValuesTest_ValidateJewelryAngleValueOn225_ExpectedBehavior()
	{
		// Arrange
		var ring = new Ring();
		ring.Engraving.Text = "Tt3453";
		ring.Engraving.TextSize = 3;
		ring.Radius = 10;
		ring.Width = 12;
		ring.JewelryAngle = 225;

        // Act
        Assert.DoesNotThrow(() => RingParamsValidator.CheckCorrectValues(ring));
    }

	/// <summary>
	/// Позитивный тест валидации угла выдавливания 270 градусов при установленной гравировке.
	/// </summary>
	[Test]
	public void CheckCorrectValuesTest_ValidateJewelryAngleValueOn270_ExpectedBehavior()
	{
		// Arrange
		var ring = new Ring();
		ring.Engraving.Text = "T663";
		ring.Engraving.TextSize = 14;
		ring.RoundScale = 5;
		ring.Radius = 40;
		ring.Width = 30;
		ring.JewelryAngle = 270;


        // Act
        Assert.DoesNotThrow(() => RingParamsValidator.CheckCorrectValues(ring));
    }

	/// <summary>
	/// Негативный тест валидации угла выдавливания 270 градусов при установленной гравировке.
	/// </summary>
	[Test]
	public void CheckCorrectValuesTest_ValidateJewelryAngleValueOn270_UnexpectedBehavior()
	{
		// Arrange
		var ring = new Ring();
		ring.Engraving.Text = "T66553";
		ring.Engraving.TextSize = 14;
		ring.RoundScale = 5;
		ring.Radius = 40;
		ring.Width = 30;
		ring.JewelryAngle = 270;


        Assert.Throws<Exception>(() => RingParamsValidator.CheckCorrectValues(ring));
    }

	/// <summary>
	/// Позитивный тест валидации угла выдавливания 180 градусов при установленной гравировке.
	/// </summary>
	[Test]
	public void CheckCorrectValuesTest_ValidateJewelryAngleValue_180()
	{
		// Arrange
		var ring = new Ring();
		ring.Engraving.Text = "Tt";
		ring.Engraving.TextSize = 3;
		ring.Radius = 10;
		ring.Width = 12;
		ring.JewelryAngle = 180;

        // Act
        Assert.DoesNotThrow(() => RingParamsValidator.CheckCorrectValues(ring));
    }

    /// <summary>
    /// Позитивный тест валидации угла выдавливания меньше чем 180 градусов при установленной гравировке.
    /// </summary>
    [Test]
    public void CheckCorrectValuesTest_ValidateJewelryAngleValue_LessThan180()
    {
        // Arrange
        var ring = new Ring();
        ring.Engraving.Text = "Tt";
        ring.Engraving.TextSize = 3;
        ring.Radius = 10;
        ring.Width = 12;
        ring.JewelryAngle = 90;

        // Act
        Assert.DoesNotThrow(() => RingParamsValidator.CheckCorrectValues(ring));
    }

    /// <summary>
    /// Позитивный тест валидации угла выдавливания меньше чем 180 градусов при неустановленной гравировке.
    /// </summary>
    [Test]
    public void CheckCorrectValuesTest_ValidateJewelryAngleValue_LessThan180_EngravingEmpty()
    {
        // Arrange
        var ring = new Ring();
        ring.Engraving.Text = String.Empty;
        ring.Engraving.TextSize = 3;
        ring.Radius = 10;
        ring.Width = 12;
        ring.JewelryAngle = 90;

        // Act
        Assert.DoesNotThrow(() => RingParamsValidator.CheckCorrectValues(ring));
    }
}
