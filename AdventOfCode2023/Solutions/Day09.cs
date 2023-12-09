using System.Diagnostics;
using System.Runtime;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions;

public static class Day09
{
    public static long Solve(string input, bool last)
    {
        var lines = input.Split(Environment.NewLine).ToList();

        var nextValues = lines.Select(x => ParseLine(x, last)).ToList();
        
        return nextValues.Aggregate(0, (i, j) => i + j);
    }

    private static int ParseLine(string line, bool last)
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

        if (last)
        {
            steps.Last().Add(0);
            for (var i = steps.Count - 2; i >= 0; i--)
            {
                var step = steps[i];
                step.Add(step.Last() + steps[i + 1].Last());
            }
            return steps.First().Last();
        }

        else
        {
            steps.Last().Insert(0, 0);
            for (var i = steps.Count - 2; i >= 0; i--)
            {
                var step = steps[i];
                step.Insert(0, step.First() - steps[i + 1].First());
            }
            return steps.First().First();
        }

        
    }
}