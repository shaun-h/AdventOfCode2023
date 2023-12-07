using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Solutions;

public static class Day07
{
    public static long Solve(string input, bool withWild)
    {
        var lines = input.Split(Environment.NewLine);
        var hands = lines.Select(ParseLine)
            .OrderByDescending(x => x.HandRank(withWild))
            .ThenByDescending(x => x.CardRank(1, withWild))
            .ThenByDescending(x => x.CardRank(2, withWild))
            .ThenByDescending(x => x.CardRank(3, withWild))
            .ThenByDescending(x => x.CardRank(4, withWild))
            .ThenByDescending(x => x.CardRank(5, withWild))
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

public class CamelCardHand(List<char> cards, int bid)
{
    public List<char> Cards => cards;
    
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
    
    private readonly Dictionary<char, int> _wildCardRanks = new()
    {
        ['A'] = 13,
        ['K'] = 12,
        ['Q'] = 11,
        ['T'] = 10,
        ['9'] = 9,
        ['8'] = 8,
        ['7'] = 7,
        ['6'] = 6,
        ['5'] = 5,
        ['4'] = 4,
        ['3'] = 3,
        ['2'] = 2,
        ['J'] = 1,
    };
    
    
    public int HandRank(bool withWild)
    {
        if (IsFiveOfAKind(withWild)) return 7;
        if (IsFourOfAKind(withWild)) return 6;
        if (IsAFullHouse(withWild)) return 5;
        if (IsThreeOfAKind(withWild)) return 4;
        if (IsTwoPair(withWild)) return 3;
        if (IsPair(withWild)) return 2;
        return 1;
    }

    public int Score(int rank = 1)
    {
        return bid * rank;
    }

    public int CardRank(int cardNumber, bool withWild)
    {
        var c = cards.ElementAt(cardNumber - 1);
        return withWild ? _wildCardRanks[c] : _cardRanks[c];
    }
    private bool IsFiveOfAKind(bool withWild)
    {
        var cardTypes = new Dictionary<char, int>();
        foreach (var card in cards.Where(card => !cardTypes.TryAdd(card, 1)))
        {
            cardTypes[card]++;
        }

        var wilds = 0;
        if (withWild)
        {
            if (cardTypes.ContainsKey('J'))
            {
                wilds = cardTypes['J'];
                cardTypes.Remove('J');
            }
        }
        
        return cardTypes.Values.OrderDescending().FirstOrDefault() + wilds == 5;
    }
    
    private bool IsFourOfAKind(bool withWild)
    {
        var cardTypes = new Dictionary<char, int>();
        foreach (var card in cards.Where(card => !cardTypes.TryAdd(card, 1)))
        {
            cardTypes[card]++;
        }

        var wilds = 0;
        if (withWild)
        {
            if (cardTypes.ContainsKey('J'))
            {
                wilds = cardTypes['J'];
                cardTypes.Remove('J');
            }
        }
        
        return cardTypes.Values.OrderDescending().FirstOrDefault() + wilds == 4;
    }
    
    private bool IsAFullHouse(bool withWild)
    {
        var cardTypes = new Dictionary<char, int>();
        foreach (var card in cards.Where(card => !cardTypes.TryAdd(card, 1)))
        {
            cardTypes[card]++;
        }

        var ct = cardTypes.OrderByDescending(kv => kv.Value).ToList();
        if (ct.Count() == 2 && ct.First().Value == 3 && ct.Last().Value == 2) return true;

        var wilds = 0;
        if (withWild)
        {
            if (cardTypes.ContainsKey('J'))
            {
                wilds = cardTypes['J'];
                cardTypes.Remove('J');
            }
        }
        
        if (cardTypes.Count != 2) return false;

        foreach (var key in cardTypes.OrderByDescending(kv => kv.Value))
        {
            while (wilds > 0 && cardTypes[key.Key] < 3)
            {
                cardTypes[key.Key]++;
                wilds--;
            }
        }
        
        if (wilds > 0)
        {
            cardTypes['J'] = wilds;
        }
        
        return cardTypes.Values.All(c => c is 2 or 3);
    }
    
    private bool IsThreeOfAKind(bool withWild)
    {
        var cardTypes = new Dictionary<char, int>();
        foreach (var card in cards.Where(card => !cardTypes.TryAdd(card, 1)))
        {
            cardTypes[card]++;
        }

        var wilds = 0;
        if (withWild)
        {
            if (cardTypes.ContainsKey('J'))
            {
                wilds = cardTypes['J'];
                cardTypes.Remove('J');
            }
        }
        
        return cardTypes.Values.OrderDescending().FirstOrDefault() + wilds == 3;
    }
    
    private bool IsTwoPair(bool withWild)
    {
        var cardTypes = new Dictionary<char, int>();
        foreach (var card in cards.Where(card => !cardTypes.TryAdd(card, 1)))
        {
            cardTypes[card]++;
        }

        var wilds = 0;
        if (withWild)
        {
            if (cardTypes.ContainsKey('J'))
            {
                wilds = cardTypes['J'];
                cardTypes.Remove('J');
            }
        }
        
        foreach (var key in cardTypes.OrderByDescending(kv => kv.Value))
        {
            while (wilds > 0 && cardTypes[key.Key] < 2)
            {
                cardTypes[key.Key]++;
                wilds--;
            }
        }
        
        if (wilds > 0)
        {
            cardTypes['J'] = wilds;
        }
        
        return cardTypes.Values.Count(v => v == 2) == 2;
    }
    
    private bool IsPair(bool withWild)
    {
        var cardTypes = new Dictionary<char, int>();
        foreach (var card in cards.Where(card => !cardTypes.TryAdd(card, 1)))
        {
            cardTypes[card]++;
        }

        var wilds = 0;
        if (withWild)
        {
            if (cardTypes.ContainsKey('J'))
            {
                wilds = cardTypes['J'];
                cardTypes.Remove('J');
            }
        }
        
        foreach (var key in cardTypes.OrderByDescending(kv => kv.Value))
        {
            while (wilds > 0 && cardTypes[key.Key] < 2)
            {
                cardTypes[key.Key]++;
                wilds--;
            }
        }

        if (wilds > 0)
        {
            cardTypes['J'] = wilds;
        }
        
        return cardTypes.Values.Count(v => v == 2) == 1;
    }
    
    // private bool IsHighCard()
    // {
    //     var cardTypes = new Dictionary<char, int>();
    //     foreach (var card in cards.Where(card => !cardTypes.TryAdd(card, 1)))
    //     {
    //         cardTypes[card]++;
    //     }
    //
    //     
    //     return cardTypes.Values.All(v => v == 1);
    // }
};