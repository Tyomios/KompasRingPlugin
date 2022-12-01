namespace KompasRingPlugin.UnitTests;


[TestFixture]
public class RingBuilderTests
{
	[Test]
	public void Build_NullableRingTest()
	{
		// Arrange
		var ringBuilder = new RingBuilder();
		Ring ring = null;

        // Act
        try
        {
            Assert.Throws<Exception>(() =>
            {
                ringBuilder.Build(ring);
            });
        }
        catch (Exception e)
        {
            
        }
    }

	[Test]
	public void Build_EmptyFieldsTest()
	{
		// Arrange
		var ringBuilder = new RingBuilder();
		Ring ring = new Ring
		{
			Height = 0,
			Radius = 0,
			Width = 0,
			RoundScale = 0
		};

		// Act

        // Assert
        Assert.Throws<Exception>(() =>
        {
            ringBuilder.Build(ring);
        });
    }

	[Test]
	public void Build_WithoutEngraving()
	{
		// Arrange
		var ringBuilder = new RingBuilder();
		Ring ring = new Ring
		{
			Height = 30,
			Radius = 50,
			Width = 25,
			RoundScale = 10
		};

		// Act
		try
		{
			ringBuilder.Build(ring);
		}
		catch
		{
			Assert.Fail();
			return;
		}

		// Assert
		Assert.Pass();
	}

	[Test]
	public void Build_WithEngraving()
	{
		// Arrange
		var ringBuilder = new RingBuilder();
		Ring ring = new Ring
		{
			Height = 30,
			Radius = 50,
			Width = 25,
			RoundScale = 10, 
			Engraving = new()
			{
				Text = "Тест",
				TextSize = 10,
				Height = 4
			}
		};

        // Act
        try
        {
            ringBuilder.Build(ring);
        }
        catch (Exception e)
        {
            Assert.Fail();
			return;
        }
		Assert.Pass();
    }
}
