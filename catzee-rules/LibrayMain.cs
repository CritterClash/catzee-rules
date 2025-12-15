using System.ComponentModel;

namespace catzee_rules;

public class LibrayMain
{

}

public class MyDeck
{
    public List<int> numbersInDeck = new List<int>();
    public void AddNumber(int number)
    {
        numbersInDeck.Add(number);
    }
    public void AddNumberRange(int[] numbers)
    {
        numbersInDeck.AddRange(numbers); 
    }

    public HandRanking CheckPossibleHandRank()
    {
        var groups = numbersInDeck
           .GroupBy(x => x)
           .Select(g => g.Count())
           .OrderByDescending(x => x)
           .ToArray();
        // 2. 스트레이트 체크
        bool isStraight = IsStraight(numbersInDeck.ToArray());

        // 3. 족보 판별
        //if (groups[0] == 4)
        //    return HandRanking.FourOfAKind;

        if (groups[0] == 3 && groups[1] == 2)
            return HandRanking.FullHouse;

        if (isStraight)
            return HandRanking.Straight;

        if (groups[0] == 3)
            return HandRanking.ThreeOfAKind;

        if (groups[0] == 2 && groups[1] == 2)
            return HandRanking.TwoPair;

        if (groups[0] == 2)
            return HandRanking.OnePair;

        return HandRanking.HighCard;


    }
    private static bool IsStraight(int[] hand)
    {
        var sorted = hand
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



public enum HandRanking
{
    HighCard,
    Straight,
    OnePair,
    TwoPair,
    Flush,
    FullHouse,
    StraightFlush,
    PairFlush
}