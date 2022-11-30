namespace KompasRingPlugin.UnitTests
{
    [TestFixture]
    public class RingParamsValidatorTests
    {
        [Test]
        public void CheckCorrectValues_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var ringParamsValidator = new RingParamsValidator();
            Ring ring = null;

            // Act
            ringParamsValidator.CheckCorrectValues(
                ring);

            // Assert
            Assert.Fail();
        }
    }
}
