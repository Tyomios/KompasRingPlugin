namespace Model;

/// <summary>
/// Занимается построением детали.
/// </summary>
public class RingBuilder
{
    public void Build(Ring ring)
    {
        //if(!Ring.IsReadyForBuild(ring)) return;
        
        var buildService = new BuildService(KompasConnector.Instance.GetDocument().Result);
    }
}