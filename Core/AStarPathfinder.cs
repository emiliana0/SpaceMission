using SPACE.Models;

namespace SPACE.Core;

public class AStarPathfinder : IPathfinder
{
    private readonly int[] dRow = { -1, 1, 0, 0 };
    private readonly int[] dCol = { 0, 0, -1, 1 };

    public PathResult FindShortestPath(
        SpaceMap map,
        Astronaut astronaut)
    {
        int[,] gScore = new int[map.Rows, map.Cols];

        for (int i = 0; i < map.Rows; i++)
        {
            for (int j = 0; j < map.Cols; j++)
            {
                gScore[i, j] = int.MaxValue;
            }
        }

        PriorityQueue<Position, int> openSet = new();

        Dictionary<(int, int), Position> previous = new();

        Position start = astronaut.StartPosition;

        gScore[start.Row, start.Col] = 0;

        int startHeuristic = Heuristic(start, map.FinishPosition);

        openSet.Enqueue(start, startHeuristic);

        while (openSet.Count > 0)
        {
            Position current = openSet.Dequeue();

            if (current.Row == map.FinishPosition.Row &&
                current.Col == map.FinishPosition.Col)
            {
                break;
            }

            for (int d = 0; d < 4; d++)
            {
                int newRow = current.Row + dRow[d];
                int newCol = current.Col + dCol[d];

                if (!map.IsInside(newRow, newCol))
                    continue;

                string cell = map.Grid[newRow, newCol];

                if (cell == "X")
                    continue;

                int moveCost = cell == "D" ? 2 : 1;

                int tentativeGScore = gScore[current.Row, current.Col] + moveCost;

                if (tentativeGScore < gScore[newRow, newCol])
                {
                    gScore[newRow, newCol] = tentativeGScore;

                    Position next = new Position(newRow, newCol);

                    previous[(newRow, newCol)] = current;

                    int heuristic = Heuristic(next, map.FinishPosition);

                    int fScore = tentativeGScore + heuristic;

                    openSet.Enqueue(next, fScore);
                }
            }
        }

        if (gScore[map.FinishPosition.Row, map.FinishPosition.Col] == int.MaxValue)
        {
            return new PathResult
            {
                AstronautName = astronaut.Name,
                Success = false
            };
        }

        List<Position> path = new();

        Position node = map.FinishPosition;

        while (!(node.Row == start.Row && node.Col == start.Col))
        {
            path.Add(node);

            node = previous[(node.Row, node.Col)];
        }

        path.Reverse();

        return new PathResult
        {
            AstronautName = astronaut.Name,
            Success = true,
            Cost = gScore[map.FinishPosition.Row, map.FinishPosition.Col],
            Path = path
        };
    }

    private int Heuristic(Position current, Position finish) 
    {
        return Math.Abs(current.Row - finish.Row) + Math.Abs(current.Col - finish.Col);
    }
}