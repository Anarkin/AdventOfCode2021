using System.Collections.Generic;

namespace AdventOfCode2021;

public partial class Day5
{
    record Cell(int Count);

    public int Part1()
    {
        IEnumerable<(int x1, int y1, int x2, int y2)> GetLines()
        {
            for (var i = 0; i < this.data.GetLength(dimension: 0); i++)
            {
                yield return (
                    this.data[i, 0],
                    this.data[i, 1],
                    this.data[i, 2],
                    this.data[i, 3]
                );
            }
        }

        var map = GetLines()
            .Aggregate(
                seed: new Cell[1000, 1000],
                (worldMap, line) =>
                {
                    if (line.x1 == line.x2)
                    {
                        for (var y = line.y1; y < line.y2; y++)
                        {
                            var val = worldMap[line.x1, y];
                            if (val is null)
                            {
                                worldMap[line.x1, y] = new Cell(Count: 1);
                            }
                            else
                            {
                                val = val with { Count = val.Count + 1 };
                            }
                        }
                    }
                    else
                    {
                        for (var x = line.x1; x < line.x2; x++)
                        {
                            var val = worldMap[x, line.y1];
                            if (val is null)
                            {
                                worldMap[x, line.y1] = new Cell(Count: 1);
                            }
                            else
                            {
                                val = val with { Count = val.Count + 1 };
                            }
                        }
                    }
                    return worldMap;
                });

        var c = 0;
        for (var x = 0; x < 1000; x++)
        {
            for (var y = 0; y < 1000; y++)
            {
                if (map[x, y] is not null && map[x, y].Count > 1)
                {
                    c++;
                }
            }
        }


        return 0;
    }

    public int Part2()
    {
        return 0;
    }
}
