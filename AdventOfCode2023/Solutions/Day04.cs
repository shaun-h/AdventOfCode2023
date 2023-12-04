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
    
    public static int SolveTotal(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var games = lines.Select(ParseGame);

        var winning = new Dictionary<int, int>();
        foreach (var game in games)
        {
            if (!winning.TryAdd(game.Id, 1))
            {
                winning[game.Id]++;
            }

            var repeat = winning[game.Id];
            for (var i = 0; i < repeat; i++)
            {
                foreach (var winningCard in game.WinningCards.Where(winningCard => !winning.TryAdd(winningCard, 1)))
                {
                    winning[winningCard]++;
                }
            }
            
        }

        return winning.Values.Aggregate(0, (score, w) => score+w);

    }

    private static Game ParseGame(string line)
    {
        var numbers = line.Split(":");
        var id = int.Parse(numbers[0].Replace("Card ", ""));
        var parts = numbers[1].Split("|");
        var winning = parts[0].Split(" ").Where(p => p != "").Select(int.Parse).ToList();
        var gameNumbers = parts[1].Split(" ").Where(p => p != "").Select(int.Parse).ToList();
        return new Game(id, winning, gameNumbers);
    }
}

record Game(int Id, List<int> WinningNumbers, List<int> GameNumbers)
{
    public List<int> MatchingNumbers => GameNumbers.Where(i => WinningNumbers.Contains(i)).ToList();

    public int Score => MatchingNumbers.Aggregate(0, (score, _) =>
    {
        if (score == 0) return 1;
        return score * 2;
    });
    
    public List<int> WinningCards => Enumerable.Range(Id+1, MatchingNumbers.Count).ToList();
        
}