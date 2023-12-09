using System.Diagnostics;
using System.Runtime;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions;

public static class Day09
{
    public static long Solve(string input)
    {
        var lines = input.Split(Environment.NewLine).ToList();

        var nextValues = lines.Select(ParseLine).ToList();
        
        return nextValues.Aggregate(0, (i, j) => i + j);
    }

    private static int ParseLine(string line)
    {
        var values = line.Split(" ").Select(int.Parse).ToList();
        var steps = new List<List<int>>() { values };
        while (values.Any(x => x != 0))
        {
            var currentStep = new List<int>();
            for (var i = 0; i < values.Count - 1; i++)
            {
                currentStep.Add(values.ElementAt(i+1) - values.ElementAt(i));
            }
            steps.Add(currentStep);
            values = currentStep;
        }

        steps.Last().Add(0);
        for (var i = steps.Count - 2; i >= 0; i--)
        {
            var step = steps[i];
            step.Add(step.Last() + steps[i+1].Last());
        }

        return steps.First().Last();
    }
}