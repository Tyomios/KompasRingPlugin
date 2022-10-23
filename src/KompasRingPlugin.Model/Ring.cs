using System.Runtime.CompilerServices;

namespace KompasRingPlugin.Model;

/// <summary>
/// Кольцо.
/// </summary>
public class Ring
{
    private float _width;

    public float Width
    {
        get => _width;
        set
        {
            _width = value;
        }
    }

    
    private float _height;

    public float Height
    {
        get => _height;
        set
        {
            _height = value;
        }
    }
    

    private float _radius;

    public float Radius
    {
        get => _radius;
        set
        {
            _radius = value;
        }
    }

    private uint _roundScale;

    public uint RoundScale
    {
        get => _roundScale;
        set
        {
            _roundScale = value;
        }
    }

    
    private Engraving _engraving;

    public Engraving Engraving
    {
        get => _engraving;
        set
        {
            _engraving = value;
        }
    }

    public static bool IsReadyForBuild(Ring ring)
    {
        if (ring.Width.Equals(0) || ring.Height.Equals(0) || ring.Radius.Equals(0)|| ring.RoundScale.Equals(0))
        {
            return false;
        }

        return true;
    }
}