using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions;

public static class Day08
{
    public static long Solve(string input)
    {
        var lines = input.Split(Environment.NewLine).ToList();
        var steps = lines.First();

        lines.Remove(steps);

        var places = lines.Select(ParsePlace).Where(x => x.HasValue).ToList();
        return GetStepCount(steps, places);
        return -1;
    }

    private static long GetStepCount(string steps, List<(string id, string left, string right)?> places)
    {
        var mapPlaces = new Dictionary<string, (string left, string right)>();
        foreach (var place in places.Where(x => x.HasValue))
        {
            var p = place ?? throw new Exception("Couldn't get place to map");
            mapPlaces[p.id] = (p.left, p.right);
        }

        var firstPlace = mapPlaces["AAA"];
        var currentPlace = (firstPlace.left, firstPlace.right);
        var currentStepCount = 0;
        var placeIdToGo = ""; 
        do
        {
            var currectDirPlace = currentStepCount % steps.Length;
            var dir = steps.ToCharArray()[currectDirPlace];
            if (dir == 'L')
            {
                placeIdToGo = currentPlace.left;
                currentPlace = mapPlaces[currentPlace.left];
            }
            if (dir == 'R')
            {
                placeIdToGo = currentPlace.right;
                currentPlace = mapPlaces[currentPlace.right];
            }
            currentStepCount++;
        } while (placeIdToGo != "ZZZ");

        return currentStepCount;
    }

    private static (string id, string left, string right)? ParsePlace(string line)
    {
        var match = Regex.Match(line, "(\\w+)\\s*=\\s*\\((\\w+),\\s*(\\w+)\\)");
        if (match.Success)
        {
            return new ValueTuple<string, string, string>(match.Groups[1].Value.Trim(), match.Groups[2].Value.Trim(),
                match.Groups[3].Value.Trim());
        }

        return null;
    }
}