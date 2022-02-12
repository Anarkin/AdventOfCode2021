namespace AdventOfCode2021;

public partial class Day1
{
    public int Part1()
    {
        return this.data
            .Chunk2(size: 2, step: 1)
            .Count(pair => pair[0] < pair[1]);
    }

    public int Part2()
    {
        return this.data
            .Chunk2(size: 3, step: 1)
            .Select(triplet => triplet.Sum())
            .Chunk2(size: 2, step: 1)
            .Count(pair => pair[0] < pair[1]);
    }
}
