using System.Text;
using SPACE.Models;

namespace SPACE.Services;

public class MissionReportBuilder
{
    public string Build(PathResult result)
    {
        StringBuilder sb = new();

        sb.AppendLine($"Astronaut: {result.AstronautName}");
        sb.AppendLine($"Success: {result.Success}");

        if (!result.Success)
        {
            sb.AppendLine("Path: NOT FOUND");
            return sb.ToString();
        }

        sb.AppendLine($"Cost: {result.Cost}");
        sb.AppendLine("Path:");

        foreach (var pos in result.Path)
        {
            sb.Append($"({pos.Row},{pos.Col}) -> ");
        }

        sb.AppendLine("END");

        return sb.ToString();
    }
}