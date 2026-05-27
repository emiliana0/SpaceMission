using SPACE.Models;

namespace SPACE.Core;

public interface IPathfinder
{
    PathResult FindShortestPath(
        SpaceMap map,
        Astronaut astronaut);
}