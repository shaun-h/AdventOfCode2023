namespace AdventOfCode2023.Solutions;

public class Day01
{
    public static int Solve(string input)
    {
        var lines = input.Split(Environment.NewLine);

        var lineNumbers = lines.Select(ProcessLine);
        return lineNumbers.Aggregate(0, (i1, i2) => i1 + i2);
    }

    private static int ProcessLine(string line)
    {
        var numbersFound = new List<int>();
        foreach (var c in line)
        {
            if (int.TryParse(c.ToString(), out var i))
            {
                numbersFound.Add(i);
            }
        }

        return int.Parse($"{numbersFound.First()}{numbersFound.Last()}");
    }
}