using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Dealer
    {
        public Deck deck;
        public bool revealDealer;
        public List<Card> dealerCards;
        private const int maxValue = 21;

        public Dealer(int numOfDecks)
        {
            deck = new Deck(numOfDecks);
            deck.Shuffle();
            dealerCards = new List<Card>();
        }

        public List<Card> Deal(List<Card> cards, int numOfCards)
        {
            Card card;
            for (int i = 0; i < numOfCards; i++)
            {
                card = deck.cards.Find(x => x.isAvailable);
                cards.Add(card);
                card.isAvailable = false;
            }

            return cards;
        }

        public void HandValue(List<Card> deck)
        {
            bool isAceHard = false;
            bool aceCheck = false;
            foreach (Card card in deck)
            {
                switch (card.cardValue)
                {
                    case Value.Ace:
                        card.intValue = isAceHard ? 1 : 11;
                        aceCheck = true;
                        break;
                    case Value.Two:
                        card.intValue = 2;
                        break;
                    case Value.Three:
                        card.intValue = 3;
                        break;
                    case Value.Four:
                        card.intValue = 4;
                        break;
                    case Value.Five:
                        card.intValue = 5;
                        break;
                    case Value.Six:
                        card.intValue = 6;
                        break;
                    case Value.Seven:
                        card.intValue = 7;
                        break;
                    case Value.Eight:
                        card.intValue = 8;
                        break;
                    case Value.Nine:
                        card.intValue = 9;
                        break;
                    case Value.Ten:
                        card.intValue = 10;
                        break;
                    case Value.Jack:
                        card.intValue = 10;
                        break;
                    case Value.Queen:
                        card.intValue = 10;
                        break;
                    case Value.King:
                        card.intValue = 10;
                        break;
                    default:
                        break;
                }
            }

            if (aceCheck)
            {
                int handValue = 0;
                foreach (Card card in deck)
                {
                    handValue += card.intValue;
                }

                if (handValue > maxValue && !isAceHard)
                {
                    isAceHard = true;
                    Card card = deck.Find(x => x.cardValue == Value.Ace);
                    card.intValue = 1;
                }
            }
        }
}
