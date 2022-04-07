using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class PlayerHand
    {
        public List<Card> cards;
        public List<Card> split;
        public float money;
        public float bet;

        public PlayerHand()
        {
            cards = new List<Card>();
            split = new List<Card>();
            money = 500;
            bet = 0;
        }
    }
}
