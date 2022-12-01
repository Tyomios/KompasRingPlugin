namespace KompasRingPlugin.UnitTests
{
    [TestFixture]
    public class KompasConnectorTests
    {
        [Test]
        public void GetDocument_ExpectedBehavior()
        {
            // Act
            var result = KompasConnector.Instance.GetDocument().Result;
            
            // Assert
            Assert.IsNotNull(result);
        }
    }
}
