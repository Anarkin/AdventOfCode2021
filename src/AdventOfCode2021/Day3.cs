namespace AdventOfCode2021;

public partial class Day3
{
    public int Part1()
    {
        var dataColumnLength = this.data[0].Length;

        var bitArray = new BitArray(dataColumnLength);

        for (var column = 0; column < dataColumnLength; column++)
        {
            var (zeroes, ones) = (0, 0);

            foreach (string row in this.data)
            {
                if (row[column] == '0') zeroes++;
                if (row[column] == '1') ones++;
            }

            bitArray[bitArray.Length - column - 1] = zeroes > ones ? false : true;
        }

        // convert binary to decimal

        int[] array = new int[2];
        bitArray.CopyTo(array, 0); // gamma rate
        bitArray.Not();
        bitArray.CopyTo(array, 1); // epsilon rate

        return array[0] * array[1];
    }

    public long Part2()
    {
        var dataColumnLength = this.data[0].Length;

        long F(Func<string[], string[], string[]> f)
        {
            var data = this.data;

            for (var column = 0; column < dataColumnLength; column++)
            {
                if (data.Length == 1) break;

                var (zeroesList, onesList) = (new List<string>(), new List<string>());

                foreach (string row in data)
                {
                    if (row[column] == '0') zeroesList.Add(row);
                    if (row[column] == '1') onesList.Add(row);
                }
                data = f(zeroesList.ToArray(), onesList.ToArray());
            }

            return Convert.ToInt64(data[0], 2);
        }

        var oxygen = F((a, b) => a.Length <= b.Length
                                    ? b // most common is kept, or, if equals, 1
                                    : a);

        var co2 = F((a, b) => a.Length <= b.Length
                                    ? a // least common, or 0
                                    : b);

        return oxygen * co2;
    }
}
