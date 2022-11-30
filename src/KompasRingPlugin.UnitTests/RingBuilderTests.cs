namespace KompasRingPlugin.UnitTests
{
    [TestFixture]
    public class RingBuilderTests
    {
        [Test]
        public async Task Build_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var ringBuilder = new RingBuilder();
            Ring ring = null;

            // Act
            await ringBuilder.Build(
                ring);

            // Assert
            Assert.Fail();
        }
    }
}
