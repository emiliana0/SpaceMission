using SPACE.Models;

namespace SPACE.Core;

public class BFSPathfinder : IPathfinder
{
    private readonly int[] dRow = { -1, 1, 0, 0 };
    private readonly int[] dCol = { 0, 0, -1, 1 };

    public PathResult FindShortestPath(
        SpaceMap map,
        Astronaut astronaut)
    {
        bool[,] visited = new bool[map.Rows, map.Cols];

        Position[,] previous = new Position[map.Rows, map.Cols];

        int[,] distance = new int[map.Rows, map.Cols];

        Queue<Position> queue = new();

        Position start = astronaut.StartPosition;

        queue.Enqueue(start);

        visited[start.Row, start.Col] = true;

        while (queue.Count > 0)
        {
            Position current = queue.Dequeue();

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

                if (visited[newRow, newCol])
                    continue;

                string cell = map.Grid[newRow, newCol];

                if (cell == "X")
                    continue;

                visited[newRow, newCol] = true;

                distance[newRow, newCol] = distance[current.Row, current.Col] + 1;

                Position next = new Position(newRow, newCol);

                previous[newRow, newCol] = current;

                queue.Enqueue(next);
            }
        }

        if (!visited[map.FinishPosition.Row, map.FinishPosition.Col])
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

            node = previous[node.Row, node.Col];
        }

        path.Reverse();

        return new PathResult
        {
            AstronautName = astronaut.Name,
            Success = true,
            Cost = distance[map.FinishPosition.Row, map.FinishPosition.Col],
            Path = path
        };
    }
}