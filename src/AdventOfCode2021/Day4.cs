using System.Collections.Generic;

namespace AdventOfCode2021;

public partial class Day4
{
    private record Location(int Board, int Column, int Row);

    const int boardSize = 5;

    private int BoardCount => this.data.GetLength(dimension: 0);

    private Dictionary<int, List<Location>> GetBoardValueLocationDict()
    {
        var d = new Dictionary<int, List<Location>>();

        for (var board = 0; board < this.BoardCount; board++)
        {
            for (var column = 0; column < boardSize; column++)
            {
                for (var row = 0; row < boardSize; row++)
                {
                    var value = Get(board, column, row);

                    if (!d.TryGetValue(value, out var list))
                    {
                        d.Add(value, new List<Location>() { new Location(board, column, row) });
                    }
                    else
                    {
                        list.Add(new Location(board, column, row));
                    }
                }
            }
        }

        return d;
    }

    public int Part1()
    {
        var boardValueLocationDict = GetBoardValueLocationDict();

        var solution = 0;

        foreach (var pickedNumber in pickedNumbers)
        {
            if (!boardValueLocationDict.TryGetValue(pickedNumber, out var locations))
            {
                throw new Exception("?^");
            }

            foreach (var location in locations)
            {
                Set(location.Board, location.Column, location.Row, -1);

                if (IsBoardWinner(location.Board))
                {
                    var sumOfRemainingNumbers = SumBoard(location.Board);
                    solution = sumOfRemainingNumbers * pickedNumber;
                    goto end;
                }
            }
        }
    end:

        return solution;
    }

    public int Part2()
    {
        var boardValueLocationDict = GetBoardValueLocationDict();

        var solution = 0;

        var boardsWon = new HashSet<int/*boardIndex*/>();

        foreach (var pickedNumber in pickedNumbers)
        {
            if (!boardValueLocationDict.TryGetValue(pickedNumber, out var locations))
            {
                throw new Exception("?^");
            }

            foreach (var location in locations)
            {
                Set(location.Board, location.Column, location.Row, -1);

                if (IsBoardWinner(location.Board))
                {
                    boardsWon.Add(location.Board);

                    if (boardsWon.Count == BoardCount)
                    {
                        var sumOfRemainingNumbers = SumBoard(location.Board);
                        solution = sumOfRemainingNumbers * pickedNumber;
                        goto end;
                    }
                }
            }
        }
    end:

        return solution;
    }



    private int Get(int board, int col, int row)
    {
        return this.data[board, row * boardSize + col];
    }

    private void Set(int board, int col, int row, int value)
    {
        this.data[board, row * boardSize + col] = value;
    }

    private bool IsBoardWinner(int board)
    {
        for (var column = 0; column < boardSize; column++)
        {
            var colSum = 0;

            for (var row = 0; row < boardSize; row++)
            {
                colSum += Get(board, row, column);
            }
            if (colSum == -5) return true;
        }

        for (var row = 0; row < boardSize; row++)
        {
            var rowSum = 0;

            for (var column = 0; column < boardSize; column++)
            {
                rowSum += Get(board, row, column);
            }
            if (rowSum == -5) return true;
        }

        return false;
    }

    private int SumBoard(int board)
    {
        var sum = 0;

        for (var column = 0; column < boardSize; column++)
        {
            for (var row = 0; row < boardSize; row++)
            {
                var x = Get(board, row, column);
                sum += x == -1 ? 0 : x;
            }
        }

        return sum;
    }
}
