using SPACE.Models;

namespace SPACE.Utilities;

public class MapValidator : IMapValidator
{
    private readonly HashSet<string> allowed =
        new() { "0", "X", "F", "S1", "S2", "S3", "D" };

    public void Validate(string[,] grid)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        int finishCount = 0;
        HashSet<string> astronauts = new();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                string cell = grid[i, j];

                if (!allowed.Contains(cell))
                {
                    throw new Exception($"Invalid symbol: {cell}");
                }

                if (cell == "F")
                {
                    finishCount++;
                }

                if (cell.StartsWith("S"))
                {
                    astronauts.Add(cell);
                }
            }
        }

        if (finishCount != 1)
        {
            throw new Exception("Map must contain exactly one F.");
        }

        if (astronauts.Count < 1 || astronauts.Count > 3)
        {
            throw new Exception("Map must contain 1 to 3 astronauts.");
        }
    }
}