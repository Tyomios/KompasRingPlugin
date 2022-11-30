namespace KompasRingPlugin.UnitTests
{
    [TestFixture]
    public class RingTests
    {
        [Test]
        public void IsReadyForBuild_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var ring = new Ring();
            Ring ring = null;

            // Act
            var result = ring.IsReadyForBuild(
                ring);

            // Assert
            Assert.Fail();
        }
    }
}
