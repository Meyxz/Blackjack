using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Deck
    {
        public List<Card> cards;

        public Deck(int numOfDecks)
        {
            cards = new List<Card>(GetSortedCards(numOfDecks));
        }

        IEnumerable<Card> GetSortedCards(int numOfdecks)
        {
            for (int i = 0; i < numOfdecks; i++)
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (Value cardValue in Enum.GetValues(typeof(Value)))
                    {
                        switch ((int)suit)
                        {
                            case (int)Blackjack.Suit.Clubs:
                                yield return new Clubs(cardValue, suit);
                                break;
                            case (int)Blackjack.Suit.Diamonds:
                                yield return new Diamonds(cardValue, suit);
                                break;
                            case (int)Blackjack.Suit.Hearts:
                                yield return new Hearts(cardValue, suit);
                                break;
                            case (int)Blackjack.Suit.Spades:
                                yield return new Spades(cardValue, suit);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        public void Shuffle()
        {
            Random rng = new Random();

            int remainingCards = cards.Count;
            while (remainingCards > 1)
            {
                remainingCards--;
                int rCard = rng.Next(remainingCards + 1);
                Card card = cards[rCard];
                cards[rCard] = cards[remainingCards];
                cards[remainingCards] = card;
            }
        }
    }
}
