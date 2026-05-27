namespace SPACE.Models;

public class PathResult
{
    public string AstronautName { get; set; }

    public bool Success { get; set; }

    public int Cost { get; set; }

    public List<Position> Path { get; set; } = new();
}