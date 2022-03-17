using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Dealer
    {
        public Deck deck;
        public Hand dealerHand;
        public bool revealDealer;

        public Dealer(int numOfDecks)
        {
            deck = new Deck(numOfDecks);
            deck.Shuffle();
            dealerHand = new Hand();
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
    }
}
