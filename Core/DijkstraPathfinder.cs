using SPACE.Models;

namespace SPACE.Core;

public class DijkstraPathfinder : IPathfinder
{
    private readonly int[] dRow = { -1, 1, 0, 0 };
    private readonly int[] dCol = { 0, 0, -1, 1 };

    public PathResult FindShortestPath(
        SpaceMap map,
        Astronaut astronaut)
    {
        int[,] dist = new int[map.Rows, map.Cols];

        for (int i = 0; i < map.Rows; i++)
        {
            for (int j = 0; j < map.Cols; j++)
            {
                dist[i, j] = int.MaxValue;
            }
        }

        PriorityQueue<Position, int> pq = new();

        Dictionary<(int, int), Position> previous = new();

        dist[
            astronaut.StartPosition.Row,
            astronaut.StartPosition.Col] = 0;

        pq.Enqueue(astronaut.StartPosition, 0);

        while (pq.Count > 0)
        {
            Position current = pq.Dequeue();

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

                int newDistance = dist[current.Row, current.Col] + moveCost;

                if (newDistance < dist[newRow, newCol])
                {
                    dist[newRow, newCol] = newDistance;

                    Position next = new Position(newRow, newCol);

                    previous[(newRow, newCol)] = current;

                    pq.Enqueue(next, newDistance);
                }
            }
        }

        if (dist[
            map.FinishPosition.Row,
            map.FinishPosition.Col] == int.MaxValue)
        {
            return new PathResult
            {
                AstronautName = astronaut.Name,
                Success = false
            };
        }

        List<Position> path = new();

        Position node = map.FinishPosition;

        while (!(node.Row == astronaut.StartPosition.Row &&
                 node.Col == astronaut.StartPosition.Col))
        {
            path.Add(node);

            node = previous[(node.Row, node.Col)];
        }

        path.Reverse();

        return new PathResult
        {
            AstronautName = astronaut.Name,
            Success = true,
            Cost = dist[map.FinishPosition.Row, map.FinishPosition.Col],
            Path = path
        };
    }
}