namespace SPACE.Utilities;

public class RandomMapGenerator : IMapGenerator
{
    private Random random = new();

    public string[,] Generate(
        int rows,
        int cols,
        int asteroidCount)
    {
        string[,] grid = new string[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                grid[i, j] = "0";
            }
        }

        int astronautCount = random.Next(1, 4);

        for (int i = 1; i <= astronautCount; i++)
        {
            while (!PlaceRandom(grid, $"S{i}"))
            {
            }
        }

        while (!PlaceRandom(grid, "F"))
        {
        }

        int placedAsteroids = 0;

        while (placedAsteroids < asteroidCount)
        {
            int r = random.Next(rows);
            int c = random.Next(cols);

            if (PlaceRandom(grid, "X"))
            {
                placedAsteroids++;
            }
        }

        return grid;
    }

        private bool PlaceRandom(
        string[,] grid,
        string symbol)
    {
        int rows = grid.GetLength(0);
        int cols = grid.GetLength(1);

        int r = random.Next(rows);
        int c = random.Next(cols);

        if (grid[r, c] == "0")
        {
            grid[r, c] = symbol;
            return true;
        }

        return false;
    }
}