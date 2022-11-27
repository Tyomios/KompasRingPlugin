using Model;

var ring = new Ring
{
    Height = 5,
    Width = 10,
    Radius = 5,
    RoundScale = 5
};

var ringBuilder = new RingBuilder();
await Task.Run(() =>
{
    ringBuilder.Build(ring);
    Console.WriteLine("Кольцо построено.");
});

