using SPACE.Core;
using SPACE.Models;
using SPACE.Services;
using SPACE.Utilities;

try
{
    // ================================
    // OPTIONAL FEATURE: RANDOM MAP GENERATION
    // ================================
    // Uncomment to test automatic map generation.

    // IMapGenerator generator = new RandomMapGenerator();
    // string[,] generatedMap = generator.Generate(5, 7, 8);
    // PrintGrid(generatedMap);


    int rows = ReadInt("Map rows: ");

    int cols = ReadInt("Map columns: ");

    ValidateSize(rows, cols);

    SpaceMap map = new(rows, cols);

    map.ReadMap();

    IMapValidator mapValidator = new MapValidator();

    mapValidator.Validate(map.Grid);

    IPathfinder pathfinder = new AStarPathfinder();

    List<PathResult> results = new();

    foreach (Astronaut astronaut in map.Astronauts)
    {
        results.Add(pathfinder.FindShortestPath(map, astronaut));
    }

    List<PathResult> failed = results
        .Where(r => !r.Success)
        .ToList();

    foreach (PathResult fail in failed)
    {
        Console.WriteLine(
            $"Mission failed — Astronaut {fail.AstronautName} lost in space!");
    }

    List<PathResult> successful = results
        .Where(r => r.Success)
        .OrderBy(r => r.Cost)
        .ToList();

    foreach (PathResult result in successful)
    {
        Console.WriteLine();

        Console.WriteLine(
            $"Astronaut {result.AstronautName} - Shortest path: {result.Cost} steps");

        PrintMap(map, result.Path);
    }

    // ================================
    // OPTIONAL FEATURE: EMAIL REPORTS
    // ================================
    // Uncomment to test email reports.

    // EmailService emailService = new SmtpEmailService();
    // MissionReportBuilder reportBuilder = new();
    // Console.Write("Sender email: ");
    // string senderEmail = Console.ReadLine();
    // Console.Write("App password: ");
    // string appPassword = Console.ReadLine();
    // Console.Write("Receiver email: ");
    // string receiverEmail = Console.ReadLine();
    
    // foreach (PathResult result in results)
    // {
    //     string report = reportBuilder.Build(result);

    //     emailService.SendEmail(
    //         senderEmail,
    //         appPassword,
    //         receiverEmail,
    //         $"Mission Report - {result.AstronautName}",
    //         report);
    // }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}

static void PrintMap(
    SpaceMap map,
    List<Position> path)
{
    string[,] copy =
        (string[,])map.Grid.Clone();

    foreach (Position p in path)
    {
        if (copy[p.Row, p.Col] == "0" ||
            copy[p.Row, p.Col] == "D")
        {
            copy[p.Row, p.Col] = "*";
        }
    }

    for (int i = 0; i < map.Rows; i++)
    {
        for (int j = 0; j < map.Cols; j++)
        {
            Console.Write(copy[i, j] + " ");
        }

        Console.WriteLine();
    }
}

static int ReadInt(string message)
{
    Console.Write(message);

    if (!int.TryParse(Console.ReadLine(), out int value))
    {
        throw new Exception("Invalid number input.");
    }

    return value;
}

static void ValidateSize(int rows, int cols)
{
     if (rows < 2 || rows > 100)
        throw new Exception("Rows must be between 2 and 100.");

    if (cols < 2 || cols > 100)
        throw new Exception("Cols must be between 2 and 100.");
}

static void PrintGrid(string[,] grid)
{
    int rows = grid.GetLength(0);
    int cols = grid.GetLength(1);

    for (int i = 0; i < rows; i++)
    {
        for (int j = 0; j < cols; j++)
        {
            Console.Write(grid[i, j] + " ");
        }

        Console.WriteLine();
    }
}