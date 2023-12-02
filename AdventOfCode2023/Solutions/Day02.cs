using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions;

public static class Day02
{
    public static int Solve(string input, int redCubes, int greenCubes, int blueCubes)
    {
        var lines = input.Split(Environment.NewLine);

        var validGameIds = lines.Select(line => ParseGame(line, redCubes, greenCubes, blueCubes)).Where(id =>  id > -1).ToList();
        return validGameIds.Aggregate(0, (i1, i2) => i1 + i2);
    }

    private static int ParseGame(string gameLine, int redCubes, int greenCubes, int blueCubes)
    {
        var game = new Game(gameLine);
        return game.IsValid(blueCubes, greenCubes, redCubes) ? game.Id : -1;
    }
    
    
    private class Game
    {
        public Game(string gameLine)
        {
            Id = ParseId(gameLine);
            Blue = ParseCubes(gameLine, "blue");
            Green = ParseCubes(gameLine, "green");
            Red = ParseCubes(gameLine, "red");
        }

        public bool IsValid(int blueCubes, int greenCubes, int redCubes)
        {
            if (Blue.Any(count => blueCubes < count)) return false;
            if (Green.Any(count => greenCubes < count)) return false;
            if (Red.Any(count => redCubes < count)) return false;

            return true;
        }

        private List<int> ParseCubes(string gameLine, string colour)
        {
            var matches = Regex.Matches(gameLine, $"(\\d*) {colour}");
            var cubes = new List<int>();
            foreach (Match match in matches)
            {
                if (int.TryParse(match.Groups[1].Value, out var count))
                {
                    cubes.Add(count);
                }
                
            }
            return cubes;
        }

        private int ParseId(string gameLine)
        {
            var match = Regex.Match(gameLine, "^Game (\\d*):");
            if (match.Success)
            {
                if (int.TryParse(match.Groups[1].Value, out var id))
                {
                    return id;
                }
            }
            return -1;
        }

        public int Id { get; init; }
        public List<int> Red { get; init; }
        public List<int> Green { get; init; }
        public List<int> Blue { get; init; }
    }
}