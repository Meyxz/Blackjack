using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class PlayerHand : Hand
    {
        public List<Card> split;
        public float money;
        public float bet;

        public PlayerHand()
        {
            money = 500;
            bet = 0;
        }
    }
}
