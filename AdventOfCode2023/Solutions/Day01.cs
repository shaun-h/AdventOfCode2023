namespace AdventOfCode2023.Solutions;

public class Day01
{
    public static int Solve(string input, bool parseWords)
    {
        var lines = input.Split(Environment.NewLine);

        var lineNumbers = lines.Select(line => ProcessLine(line, parseWords)).Where(number => number > -1);
        return lineNumbers.Aggregate(0, (i1, i2) => i1 + i2);
    }

    private static int ProcessLine(string line, bool parseWords)
    {
        var words = new List<string> { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        var numbersFound = new List<int>();
        for(var i = 0; i < line.Length; i++)
        {
            var c = line[i];
            
            if (int.TryParse(c.ToString(), out var num))
            {
                numbersFound.Add(num);
            }
            else if (parseWords)
            {
                for (var j = 1; j <= words.Count; j++)
                {
                    var word = words[j-1];
                    if(line.Length < i + word.Length) continue;
                    if (line.Substring(i, word.Length) == word)
                    {
                        numbersFound.Add(j);
                    }
                }
            }
        }

        if (!numbersFound.Any())
        {
            return -1;
        }

        return int.Parse($"{numbersFound.First()}{numbersFound.Last()}");
    }
}