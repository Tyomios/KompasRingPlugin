namespace KompasRingPlugin.UnitTests
{
    [TestFixture]
    public class KompasConnectorTests
    {
        [Test]
        public void Disconnect_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var kompasConnector = new KompasConnector();

            // Act
            kompasConnector.Disconnect();

            // Assert
            Assert.Fail();
        }

        [Test]
        public async Task GetDocument_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var kompasConnector = new KompasConnector();

            // Act
            var result = await kompasConnector.GetDocument();

            // Assert
            Assert.Fail();
        }
    }
}
