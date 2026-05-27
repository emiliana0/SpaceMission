namespace SPACE.Utilities;

public interface IMapGenerator
{
    string[,] Generate(
        int rows,
        int cols,
        int asteroidCount);
}