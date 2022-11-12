using Kompas6API5;
using System.Threading.Tasks;

namespace Model;

/// <summary>
/// Занимается построением детали.
/// </summary>
public class RingBuilder
{
    public async void Build(Ring ring)
    {
        //if(!Ring.IsReadyForBuild(ring)) return;

        Document3D doc;
        await Task.Run((() =>
        {
            doc = KompasConnector.Instance.GetDocument().Result;
            var buildService = new BuildService(doc);
            buildService.CreateSketch();
        }));
        
    }
}