using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions;

public static class Day07
{
    public static long Solve(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var hands = lines.Select(ParseLine)
            .OrderByDescending(x => x.HandRank())
            .ThenByDescending(x => x.CardRank(1))
            .ThenByDescending(x => x.CardRank(2))
            .ThenByDescending(x => x.CardRank(3))
            .ThenByDescending(x => x.CardRank(4))
            .ThenByDescending(x => x.CardRank(5))
            .ToList();

        var total = 0;
        var multiplier = hands.Count();
        foreach (var hand in hands)
        {
            total += hand.Score(multiplier);
            multiplier--;
        }
        return total;
    }

    private static CamelCardHand ParseLine(string line)
    {
        var lineComponents = line.Split(" ");
        var cards = lineComponents.First().ToArray().ToList();
        var bid = int.Parse(lineComponents.Last());
        return new CamelCardHand(cards, bid);
    }
}

public class CamelCardHand(List<char> Cards, int Bid)
{
    private readonly Dictionary<char, int> _cardRanks = new()
    {
        ['A'] = 13,
        ['K'] = 12,
        ['Q'] = 11,
        ['J'] = 10,
        ['T'] = 9,
        ['9'] = 8,
        ['8'] = 7,
        ['7'] = 6,
        ['6'] = 5,
        ['5'] = 4,
        ['4'] = 3,
        ['3'] = 2,
        ['2'] = 1,
    };
    
    
    public int HandRank()
    {
        if (IsFiveOfAKind()) return 7;
        if (IsFourOfAKind()) return 6;
        if (IsAFullHouse()) return 5;
        if (IsThreeOfAKind()) return 4;
        if (IsTwoPair()) return 3;
        if (IsPair()) return 2;
        return 1;
    }

    public int Score(int rank = 1)
    {
        return Bid * rank;
    }

    public int CardRank(int cardNumber)
    {
        var c = Cards.ElementAt(cardNumber - 1);
        return _cardRanks[c];
    }
    private bool IsFiveOfAKind()
    {
        var card = Cards.First();
        return Cards.All(c => c == card);
    }
    
    private bool IsFourOfAKind()
    {
        var cardTypes = new Dictionary<char, int>();
        foreach (var card in Cards.Where(card => !cardTypes.TryAdd(card, 1)))
        {
            cardTypes[card]++;
        }

        return cardTypes.Values.Any(v => v == 4);
    }
    
    private bool IsAFullHouse()
    {
        var cardTypes = new Dictionary<char, int>();
        foreach (var card in Cards.Where(card => !cardTypes.TryAdd(card, 1)))
        {
            cardTypes[card]++;
        }

        if (cardTypes.Count != 2) return false;

        return cardTypes.Values.All(c => c is 2 or 3);
    }
    
    private bool IsThreeOfAKind()
    {
        var cardTypes = new Dictionary<char, int>();
        foreach (var card in Cards.Where(card => !cardTypes.TryAdd(card, 1)))
        {
            cardTypes[card]++;
        }

        return cardTypes.Values.Any(v => v == 3) && cardTypes.Values.All(v => v != 2);
    }
    
    private bool IsTwoPair()
    {
        var cardTypes = new Dictionary<char, int>();
        foreach (var card in Cards.Where(card => !cardTypes.TryAdd(card, 1)))
        {
            cardTypes[card]++;
        }

        return cardTypes.Values.Count(v => v == 2) == 2;
    }
    
    private bool IsPair()
    {
        var cardTypes = new Dictionary<char, int>();
        foreach (var card in Cards.Where(card => !cardTypes.TryAdd(card, 1)))
        {
            cardTypes[card]++;
        }

        return cardTypes.Values.Count(v => v == 2) == 1 && cardTypes.Values.All(v => v < 3);
    }
    
    private bool IsHighCard()
    {
        var cardTypes = new Dictionary<char, int>();
        foreach (var card in Cards.Where(card => !cardTypes.TryAdd(card, 1)))
        {
            cardTypes[card]++;
        }

        return cardTypes.Values.All(v => v == 1);
    }
};