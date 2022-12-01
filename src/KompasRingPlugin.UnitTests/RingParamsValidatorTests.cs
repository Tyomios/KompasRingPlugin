namespace KompasRingPlugin.UnitTests
{
    [TestFixture]
    public class RingParamsValidatorTests
    {
        [Test]
        public void CheckCorrectValuesTest_ValidateRoundCornersValue()
        {
            // Arrange
            var ring = new Ring();
            ring.Width = 10;
            ring.RoundScale = 11;

            // Act
            try
            {
                RingParamsValidator.CheckCorrectValues(ring);
            }
            catch (Exception e)
            {
                if (e.Message.Equals("Значение скругления превышает толщину кольца"))
                {
                    Assert.Pass();
                    return;
                }
            }

            // Assert
            Assert.Fail();
        }

        [Test]
        public void CheckCorrectValuesTest_ValidateTextSizeValue_TextSizeBiggerThanRingWidth()
        {
            // Arrange
            var ring = new Ring();
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
                if (e.Message.Equals("Значение размера текста превышает толщину кольца"))
                {
                    Assert.Pass();
                    return;
                }
            }

            // Assert
            Assert.Fail();
        }

        [Test]
        public void CheckCorrectValuesTest_ValidateTextSizeValue_TextSizeBiggerThanHalfRingWidth()
        {
            // Arrange
            var ring = new Ring();
            ring.Engraving.Text = "Test";
            ring.Engraving.TextSize = 7;
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
                    Assert.Pass();
                    return;
                }
            }

            // Assert
            Assert.Fail();
        }

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
    }
}
