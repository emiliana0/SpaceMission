namespace SPACE.Models;
public class Astronaut
{
    public string Name { get; set; }

    public Position StartPosition { get; set; }

    public Astronaut(string name, Position startPosition)
    {
        Name = name;
        StartPosition = startPosition;
    }
}