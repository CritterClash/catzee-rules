using System.ComponentModel;

namespace catzee_rules;

public record Card(int Number, string Suit);

public class MyDeck
{
    public List<Card> numbersInDeck = new();
    public void AddNumber(Card newOne)
    {
        numbersInDeck.Add(newOne);
    }
    public void AddNumberRange(Card[] numbers)
    {
        numbersInDeck.AddRange(numbers); 
    }

    private bool IsFlush(Card[] hand)
    {
        IDictionary<string, int> countOfEachSuit = new Dictionary<string, int>();
        countOfEachSuit = hand
            .GroupBy(x => x.Suit)
            .Select(s => new KeyValuePair<string, int>(s.Key, s.Count()))
            .ToDictionary();

        if (countOfEachSuit.Values.Any(s => s >= 5)) return true;
        else return false;
    }

    public PokerHand CheckPossibleHandRank()
    {
        bool isFlush = IsFlush(numbersInDeck.ToArray());
        bool isStraight = IsStraight(numbersInDeck.ToArray());

        if( isFlush && isStraight)
        {
            return PokerHand.StraightFlush;

        } 
        else if (isFlush && !isStraight)
        {
            return PokerHand.Flush;
        }
        else if (!isFlush && isStraight)
        {
            return PokerHand.Straight;
        }

        var counts = numbersInDeck
            .GroupBy(x => x.Number)
            .Select(g => g.Count())
            .OrderByDescending(x => x)
            .ToArray();

        // 2) 포카드
        if (counts[0] == 4) return PokerHand.FourOfAKind;

        // 3) 풀하우스
        if (counts[0] == 3 && counts[1] == 2) return PokerHand.FullHouse;
        

        // 6) 트리플
        if (counts[0] == 3) return PokerHand.ThreeOfAKind;

        // 7) 투페어
        if (counts[0] == 2 && counts[1] == 2) return PokerHand.TwoPair;

        // 8) 원페어
        if (counts[0] == 2) return PokerHand.OnePair;

        return PokerHand.HighCard;
    }
    
    private bool IsStraight(Card[] hand)
    {
        var sorted = hand
            .Select(s => s.Number)
            .Distinct()
            .OrderBy(x => x)
            .ToArray();

        // 중복 있으면 스트레이트 불가
        if (sorted.Length != 5)
            return false;

        // 일반 스트레이트
        for (int i = 1; i < sorted.Length; i++)
        {
            if (sorted[i] != sorted[i - 1] + 1)
                return false;
        }
        return true;
    }
}


public enum PokerHand
{
    HighCard,
    OnePair,
    TwoPair,
    ThreeOfAKind,
    Straight,
    Flush,
    FullHouse,
    FourOfAKind,
    StraightFlush,
    RoyalFlush
}