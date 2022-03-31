using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Hand
    {
        public List<Card> cards;
        private const int maxValue = 21;
        private bool isAceHard;
        private bool aceCheck;
        public bool isBusted;
        int _handValue;

        public int handValue
        {
            get
            {
                HandValue(cards);
                return handValue;
            }
            private set
            {
                _handValue = value;
            }
        }

        public Hand()
        {
            cards = new List<Card>();
            isAceHard = false;
            aceCheck = false;
            isBusted = false;
        }

        public void HandValue(List<Card> deck)
        {
            foreach (Card card in deck)
            {
                switch (card.cardValue)
                {
                    case Value.Ace:
                        handValue += isAceHard ? 1 : 11;
                        aceCheck = true;
                        break;
                    case Value.Two:
                        handValue += 2;
                        break;
                    case Value.Three:
                        handValue += 3;
                        break;
                    case Value.Four:
                        handValue += 4;
                        break;
                    case Value.Five:
                        handValue += 5;
                        break;
                    case Value.Six:
                        handValue += 6;
                        break;
                    case Value.Seven:
                        handValue += 7;
                        break;
                    case Value.Eight:
                        handValue += 8;
                        break;
                    case Value.Nine:
                        handValue += 9;
                        break;
                    case Value.Ten:
                        handValue += 10;
                        break;
                    case Value.Jack:
                        handValue += 10;
                        break;
                    case Value.Queen:
                        handValue += 10;
                        break;
                    case Value.King:
                        handValue += 10;
                        break;
                    default:
                        break;
                }
            }

            if (aceCheck)
            {
                if (handValue > maxValue && !isAceHard)
                {
                    isAceHard = true;
                    handValue -= 10;
                }
            }
        }
    }
}
