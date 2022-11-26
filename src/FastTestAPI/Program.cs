using Model;

var ring = new Ring
{
    Height = 4,
    Width = 10,
    Radius = 10,
    RoundScale = 10
};

var ringBuilder = new RingBuilder();
ringBuilder.Build(ring);
Console.WriteLine("Кольцо построено.");