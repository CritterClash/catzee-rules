using catzee_rules;

namespace yahtzee_rules.tests;

public static class Suit
{
    public static readonly string Hearts = "Heart";
    public static readonly string Spades = "Spade";
    public static readonly string Diamonds = "Diamond";
    public static readonly string Clovers = "Clover";
}

public class TestMain
{
    [Fact]
    public void StraightFlushTest()
    {
        {
            MyDeck deck = new MyDeck();

            deck.AddNumberRange(new Card[]
            {
                 new Card(10, Suit.Spades),
                new Card(11, Suit.Spades),
                new Card(12, Suit.Spades),
                new Card(13, Suit.Spades),
                new Card(14, Suit.Spades)
            });

            Assert.Equal(PokerHand.StraightFlush, deck.CheckPossibleHandRank());
        }
        
    }
    [Fact]
    public void TwoPairTest()
    {
        {
            MyDeck deck = new MyDeck();

            deck.AddNumberRange(new Card[]
            {
                new Card(2, Suit.Hearts),
                new Card(5, Suit.Diamonds),
                new Card(12, Suit.Spades),
                new Card(5, Suit.Clovers),
                new Card(2, Suit.Spades)
            });

            Assert.Equal(PokerHand.TwoPair, deck.CheckPossibleHandRank());
        }
    }
    [Fact]
    public void FullHouseTest()
    {
        {
            MyDeck deck = new MyDeck();

            deck.AddNumberRange(new Card[]
            {
                new Card(2, Suit.Hearts),
                new Card(2, Suit.Diamonds),
                new Card(5, Suit.Spades),
                new Card(5, Suit.Clovers),
                new Card(2, Suit.Spades)
            });

            Assert.Equal(PokerHand.FullHouse, deck.CheckPossibleHandRank());
        }
    }

    [Fact]
    public void FiveFlushTest()
    {
        {
            MyDeck deck = new MyDeck();

            deck.AddNumberRange(new Card[]
            {
                new Card(2, Suit.Hearts),
                new Card(2, Suit.Hearts),
                new Card(2, Suit.Hearts),
                new Card(2, Suit.Hearts),
                new Card(2, Suit.Hearts)
            });

            Assert.Equal(PokerHand.FiveFlush, deck.CheckPossibleHandRank());
        }
    }
}
