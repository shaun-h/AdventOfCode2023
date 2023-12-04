using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions;

public static class Day04
{
    public static int Solve(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var games = lines.Select(ParseGame);
        return games.Aggregate(0, (i, game) => i + game.Score);
    }

    private static Game? ParseGame(string line)
    {
        var numbers = line.Split(":");
        var parts = numbers[1].Split("|");
        var winning = parts[0].Split(" ").Where(p => p != "").Select(int.Parse).ToList();
        var gameNumbers = parts[1].Split(" ").Where(p => p != "").Select(int.Parse).ToList();
        return new Game(winning, gameNumbers);
    }
}

record Game(List<int> WinningNumbers, List<int> GameNumbers)
{
    public List<int> MatchingNumbers => GameNumbers.Where(i => WinningNumbers.Contains(i)).ToList();

    public int Score => MatchingNumbers.Aggregate(0, (score, _) =>
    {
        if (score == 0) return 1;
        return score * 2;
    });
        
}