using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions;

public static class Day06
{
    public static long Solve(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var times = ParseLine(lines.Single(l => l.StartsWith("Time:", StringComparison.InvariantCultureIgnoreCase)));
        var distances = ParseLine(lines.Single(l => l.StartsWith("Distance:", StringComparison.InvariantCultureIgnoreCase)));

        var availableOptions = new List<int>();
        for (var i = 0; i < times.Count; i++)
        {
            var time = times.ElementAt(i);
            var distance = distances.ElementAt(i);

            availableOptions.Add(CalculateOptions(time, distance));
        }
        return availableOptions.Aggregate(1, (current, options) => current * options);
    }

    private static int CalculateOptions(int time, int distance)
    {
        var availableOptions = 0;
        for (var t = time; t > 0; t--)
        {
            var availableTime = time - t;
            if (availableTime * t > distance) availableOptions++;
        }
        return availableOptions;
    }

    private static List<int> ParseLine(string line)
    {
        return line.Split(" ").Where(x => int.TryParse(x, out var i)).Select(int.Parse).ToList();
    }
}