
using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Person
    {

        public List<Card> hand { get; protected set; }
        public bool busted;

        public Person()
        {
            hand = new List<Card>();
            busted = false;
        }

        public void AddCardFromDeck(Deck deck, List<Card> hand)
        {
            hand.Add(deck.TakeCard());
        }
        public int CalculateHand(List<Card> hand)
        {
            int handValue = 0;
            int numOfAces = 0;
            hand.ForEach((card) => { 
                handValue += card.intValue; 
                if (card.cardValue == Value.Ace)
                {
                    numOfAces += 1;
                }
            });
           
            if (handValue > 21 && numOfAces > 0)
            {
                for (;numOfAces > 0 && handValue > 21; numOfAces--)
                {
                    handValue -= 10;
                }
            }

            return handValue;

        }
        public void Hit(Deck deck, List<Card> hand)
        {
            AddCardFromDeck(deck, hand);
        }

        public bool HasBlackjack(List<Card> hand)
        {
            if (CalculateHand(hand) == 21)
            {
                return true;
            }
            return false;
        }
        protected bool IsBusted(List<Card> hand)
        {
            if (CalculateHand(hand) > 21)
            {
                return true;
            }
            return false;
        }

        public void PrintHand(List<Card> hand)
        {
            int xPos = Console.CursorLeft;
            foreach (Card card in hand)
            {
                Console.CursorLeft = xPos;
                Console.WriteLine(card.CardPrint("") + "      ");
            }

            
        }

    }
}
