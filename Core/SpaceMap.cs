using SPACE.Models;

namespace SPACE.Core;

public class SpaceMap
{
    public string[,] Grid { get; set; }

    public int Rows { get; }

    public int Cols { get; }

    public List<Astronaut> Astronauts { get; } = new();

    public Position FinishPosition { get; private set; }

    public SpaceMap(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;

        Grid = new string[rows, cols];
    }

    public void ReadMap()
    {
        Console.WriteLine("Cosmic map:");

        for (int i = 0; i < Rows; i++)
        {
            string[] input = Console.ReadLine().Split();

            for (int j = 0; j < Cols; j++)
            {
                Grid[i, j] = input[j];

                if (input[j].StartsWith("S"))
                {
                    Astronauts.Add(
                        new Astronaut(
                            input[j],
                            new Position(i, j)));
                }

                if (input[j] == "F")
                {
                    FinishPosition = new Position(i, j);
                }
            }
        }
    }

    public bool IsInside(int row, int col)
    {
        return row >= 0 &&
               row < Rows &&
               col >= 0 &&
               col < Cols;
    }
}