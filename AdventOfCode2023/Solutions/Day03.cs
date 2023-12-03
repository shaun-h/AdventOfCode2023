using System.Diagnostics;

namespace AdventOfCode2023.Solutions;

public static class Day03
{
    public static int Solve(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var numbers = GetSchemaNumbers(lines);
        var partNumbers = new List<ScehmaNumber>();
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                if (char.IsDigit(line[x]) || line[x] == '.') continue;
                var adNums = AjacentTo(x, y, numbers);
                foreach (var num in adNums.Where(num => !partNumbers.Contains(num)))
                {
                    partNumbers.Add(num);
                }
            }
        }

        return partNumbers.Aggregate(0, (i, sn) => i + sn.Number);
    }
    
    public static int SolveGear(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var number = 0;
        var numbers = GetSchemaNumbers(lines);

        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (var x = 0; x < line.Length; x++)
            {
                if (line[x] == '*')
                {
                    var adNums = AjacentTo(x, y, numbers);
                    if (adNums.Count == 2)
                    {
                        var gearRatio = adNums.Aggregate(1, (i1, g) => i1 * g.Number);
                        number += gearRatio;
                    }
                }
            }
        }
        return number;
    }

    private static List<ScehmaNumber> GetSchemaNumbers(string[] lines)
    {
        var numbers = new List<ScehmaNumber>();
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            var currentNumber = "";
            for (var x = 0; x < line.Length; x++)
            {
                if (char.IsDigit(line[x]))
                {
                    currentNumber += line[x];
                }
                else if(currentNumber != "")
                {
                    numbers.Add(new ScehmaNumber(int.Parse(currentNumber), x - currentNumber.Length, x - 1, y));
                    currentNumber = "";
                }
            }

            if (currentNumber != "")
            {
                numbers.Add(new ScehmaNumber(int.Parse(currentNumber), line.Length - 1 - currentNumber.Length, line.Length - 1, y));
            }
        }

        return numbers;
    }

    private static List<ScehmaNumber> AjacentTo(int x, int y, List<ScehmaNumber> numbers)
    {
        var adNum = new List<ScehmaNumber>();

        foreach (var number in numbers)   
        {
            if (number.y + 1 == y || number.y - 1 == y || number.y == y)
            {
                if (number.x1 - 1 == x || number.x1 == x || number.x1 + 1 == x )
                {
                    adNum.Add(number);
                }
                else if (number.x2 - 1 == x || number.x2 == x || number.x2 + 1 == x )
                {
                    adNum.Add(number);
                }
            }
        }
        
        return adNum;
    }
}

public record ScehmaNumber(int Number, int x1, int x2, int y);