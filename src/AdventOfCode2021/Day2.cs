using System.Text.RegularExpressions;

namespace AdventOfCode2021;

public partial class Day2
{
    private readonly Regex regex = new(@"([a-z]+)(\s)(\d)", RegexOptions.Compiled);

    private record Position(int Horizontal, int Depth);

    public int Part1()
    {
        var position = this.data.Aggregate(
            new Position(0, 0),
            (root, command) =>
            {
                var commandParts = this.regex.Match(command);
                var direction = commandParts.Groups[1].Value;
                var parameter = int.Parse(commandParts.Groups[3].Value);

                return direction switch
                {
                    "forward" => root with { Horizontal = root.Horizontal + parameter },
                    "down" => root with { Depth = root.Depth + parameter },
                    "up" => root with { Depth = root.Depth - parameter },
                    _ => throw new NotImplementedException(),
                };
            }
        );

        var solution = position.Horizontal * position.Depth;
        return solution;
    }

    private record Position2(int Horizontal, int Depth, int Aim);

    public int Part2()
    {
        var position = this.data.Aggregate(
            new Position2(0, 0, 0),
            (root, command) =>
            {
                var commandParts = this.regex.Match(command);
                var direction = commandParts.Groups[1].Value;
                var parameter = int.Parse(commandParts.Groups[3].Value);

                return direction switch
                {
                    "forward" => root with { Horizontal = root.Horizontal + parameter, Depth = root.Depth + (root.Aim * parameter) },
                    "down" => root with { Aim = root.Aim + parameter },
                    "up" => root with { Aim = root.Aim - parameter },
                    _ => throw new NotImplementedException(),
                };
            }
        );

        var solution = position.Horizontal * position.Depth;
        return solution;
    }
}
