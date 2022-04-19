using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Deck
    {
        private List<Card> cards;

        public Deck(int numOfDecks, bool shuffleDecks)
        {
            cards = new List<Card>(GetSortedCards(numOfDecks));
            if (shuffleDecks)
            {
                Shuffle();
            }
                
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
                            case (int)Suit.Clubs:
                                yield return new Clubs(cardValue, suit, GetValueFromRank(cardValue));
                                break;
                            case (int)Suit.Diamonds:
                                yield return new Diamonds(cardValue, suit, GetValueFromRank(cardValue));
                                break;
                            case (int)Suit.Hearts:
                                yield return new Hearts(cardValue, suit, GetValueFromRank(cardValue));
                                break;
                            case (int)Suit.Spades:
                                yield return new Spades(cardValue, suit, GetValueFromRank(cardValue));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        private int GetValueFromRank(Value rank )
        {
            int value = 0;
            switch (rank)
            {
                case Value.Ace:
                    value = 11;
                    break;
                case Value.Two:
                    value = 2;
                    break;
                case Value.Three:
                    value = 3;
                    break;
                case Value.Four:
                    value = 4; 
                    break;
                case Value.Five:
                    value = 5;
                    break;
                case Value.Six:
                    value = 6;
                    break;
                case Value.Seven:
                    value = 7;
                    break;
                case Value.Eight:
                    value = 8;
                    break;
                case Value.Nine:
                    value = 9;
                    break;
                case Value.Ten:
                case Value.Jack:    
                case Value.Queen: 
                case Value.King:
                    value = 10;
                    break;
                default:
                    break;
            }
            return value;
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

        public Card TakeCard()
        {
            Card card = cards[0];
            cards.Remove(card);
            return card;
        }
    }
}
