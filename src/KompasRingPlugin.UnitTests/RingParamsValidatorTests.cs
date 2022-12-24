namespace KompasRingPlugin.UnitTests;

/// <summary>
/// Тесты валидатора параметров кольца.
/// </summary>
[TestFixture]
public class RingParamsValidatorTests
{
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

		// Act
		try
		{
			RingParamsValidator.CheckCorrectValues(ring);
		}
		catch (Exception e)
		{
			if (e.Message.Equals("1. Значение скругления превышает толщину кольца.\n"))
			{
				Assert.Pass();
				return;
			}
		}

		// Assert
		Assert.Fail();
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

		// Act
		try
		{
			RingParamsValidator.CheckCorrectValues(ring);
		}
		catch (Exception e)
		{
			if (e.Message.Equals("1. Значение размера текста превышает толщину кольца.\n"))
			{
				Assert.Pass();
				return;
			}
		}

		// Assert
		Assert.Fail();
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

		// Act
		try
		{
			RingParamsValidator.CheckCorrectValues(ring);
		}
		catch (Exception e)
		{
			if (e.Message.Equals("1. Значение размера текста превышает толщину кольца.\n"))
			{
				Assert.Pass();
				return;
			}
		}

		// Assert
		Assert.Fail();
	}

    /// <summary>
    /// Позитивный тест валидации размера текста гравировки.
    /// </summary>
	[Test]
	public void CheckCorrectValuesTest_ValidateTextSizeValue_ExpectedBehavior()
	{
		// Arrange
		var ring = new Ring();
		ring.Engraving.Text = "Test";
		ring.Engraving.TextSize = 5;
		ring.Width = 12;

		// Act
		try
		{
			RingParamsValidator.CheckCorrectValues(ring);
		}
		catch (Exception e)
		{
			if (e.Message.Equals("Значение размера текста превышает толщину кольца"))
			{
				Assert.Fail();
				return;
			}
		}

		// Assert
		Assert.Pass();
	}

    /// <summary>
    /// Позитивный тест валидации размера текста гравировки при пустом содержании.
    /// </summary>
	[Test]
	public void CheckCorrectValuesTest_ValidateTextSizeValue_EmptyText()
	{
		// Arrange
		var ring = new Ring();
		ring.Engraving.TextSize = 8;
		ring.Width = 12;

		// Act
		try
		{
			RingParamsValidator.CheckCorrectValues(ring);
		}
		catch (Exception e)
		{
			if (e.Message.Equals("Значение размера текста превышает толщину кольца"))
			{
				Assert.Fail();
				return;
			}
		}

		// Assert
		Assert.Pass();
	}

    /// <summary>
    /// Позитивный тест валидации скругления граней кольца.
    /// </summary>
	[Test]
	public void CheckCorrectValuesTest_ValidateRoundScaleValue_ExpectedBehavior()
	{
		// Arrange
		var ring = new Ring();
		ring.Width = 12;
		ring.RoundScale = 5;

		// Act
		try
		{
			RingParamsValidator.CheckCorrectValues(ring);
		}
		catch (Exception e)
		{
			if (e.Message.Equals("Значение скругления превышает толщину кольца"))
			{
				Assert.Fail();
				return;
			}
		}

		// Assert
		Assert.Pass();
	}

    /// <summary>
    /// Негативный тест валидации угла выдавливания кольца.
    /// </summary>
    [Test]
    public void CheckCorrectValuesText_ValidateJewelryAngleValue_FullCircleValue()
    {
        // Arrange
        var ring = new Ring();
        ring.Engraving.Text = "Tt";
        ring.Engraving.TextSize = 3;
        ring.Radius = 10;
        ring.Width = 12;
        ring.JewelryAngle = 360;

        // Act
        try
        {
            RingParamsValidator.CheckCorrectValues(ring);
        }
        catch (Exception e)
        {
            if (e.Message.Equals("1. При выбранном угле выреза нарушена целостность кольца.\n"))
            {
                Assert.Pass();
                return;
            }
        }

        // Assert
        Assert.Fail();
    }

    /// <summary>
    /// Тест валидации угла выдавливания кольца при отсутствии гравировки.
    /// </summary>
    [Test]
    public void CheckCorrectValuesText_ValidateJewelryAngleValue_EngravingIsNull()
    {
        // Arrange
        var ring = new Ring();
        ring.Engraving.Text = String.Empty;
        ring.Engraving.TextSize = 3;
        ring.Radius = 10;
        ring.Width = 12;
        ring.JewelryAngle = 315;

        // Act
        try
        {
            RingParamsValidator.CheckCorrectValues(ring);
        }
        catch (Exception e)
        {
            
            Assert.Fail();
        }

        // Assert
        Assert.Pass();
    }

    /// <summary>
    /// Тест валидации угла выдавливания кольца при установленной гравировке.
    /// </summary>
    [Test]
    public void CheckCorrectValuesText_ValidateJewelryAngleValue_EngravingSet()
    {
        // Arrange
        var ring = new Ring();
        ring.Engraving.Text = "test";
        ring.Engraving.TextSize = 3;
        ring.Radius = 10;
        ring.Width = 12;
        ring.JewelryAngle = 315;

        // Act
        try
        {
            RingParamsValidator.CheckCorrectValues(ring);
        }
        catch (Exception e)
        {
            if (e.Message.Equals("1. При выбранном угле выреза нарушена целостность гравировки кольца.\n"))
            {
                Assert.Pass();
                return;
            }
        }

        // Assert
        Assert.Fail();
    }

    /// <summary>
    /// Позитивный тест валидации угла выдавливания 225 градусов при установленной гравировке.
    /// </summary>
    [Test]
    public void CheckCorrectValuesText_ValidateJewelryAngleValueOn225_ExpectedBehavior()
    {
        // Arrange
        var ring = new Ring();
        ring.Engraving.Text = "Tt3453";
        ring.Engraving.TextSize = 3;
        ring.Radius = 10;
        ring.Width = 12;
        ring.JewelryAngle = 225;

        // Act
        try
        {
            RingParamsValidator.CheckCorrectValues(ring);
        }
        catch (Exception e)
        {
            if (e.Message.Equals("1. При выбранном угле выреза нарушена целостность гравировки кольца.\n"))
            {
                Assert.Fail();
                return;
            }
        }

        // Assert
        Assert.Pass();
    }

    /// <summary>
    /// Позитивный тест валидации угла выдавливания 270 градусов при установленной гравировке.
    /// </summary>
    [Test]
    public void CheckCorrectValuesText_ValidateJewelryAngleValueOn270_ExpectedBehavior()
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
        try
        {
            RingParamsValidator.CheckCorrectValues(ring);
        }
        catch (Exception e)
        {
            Assert.Fail();
        }

        // Assert
        Assert.Pass();
    }

    /// <summary>
    /// Негативный тест валидации угла выдавливания 270 градусов при установленной гравировке.
    /// </summary>
    [Test]
    public void CheckCorrectValuesText_ValidateJewelryAngleValueOn270_UnexpectedBehavior()
    {
        // Arrange
        var ring = new Ring();
        ring.Engraving.Text = "T66553";
        ring.Engraving.TextSize = 14;
		ring.RoundScale = 5;
        ring.Radius = 40;
        ring.Width = 30;
        ring.JewelryAngle = 270;

        // Act
        try
        {
            RingParamsValidator.CheckCorrectValues(ring);
        }
        catch (Exception e)
        {
            if (e.Message.Contains("При выбранном угле выреза нарушена целостность гравировки кольца.\n"))
            {
                Assert.Pass();
                return;
            }
        }

        // Assert
        Assert.Fail();
    }

    /// <summary>
    /// Позитивный тест валидации угла выдавливания 180 градусов при установленной гравировке.
    /// </summary>
    [Test]
    public void CheckCorrectValuesText_ValidateJewelryAngleValue_180()
    {
        // Arrange
        var ring = new Ring();
        ring.Engraving.Text = "Tt";
        ring.Engraving.TextSize = 3;
        ring.Radius = 10;
        ring.Width = 12;
        ring.JewelryAngle = 180;

        // Act
        try
        {
            RingParamsValidator.CheckCorrectValues(ring);
        }
        catch (Exception e)
        {
            //if (e.Message.Equals("1. При выбранном угле выреза нарушена целостность кольца.\n"))
            //{
            //    Assert.Pass();
            //    return;
            //}
			Assert.Fail();
        }

        // Assert
        Assert.Pass();
    }

}
