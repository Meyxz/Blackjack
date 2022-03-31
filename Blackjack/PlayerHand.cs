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
        int _splitValue;

        public int splitValue
        {
            get
            {
                HandValue(split);
                return splitValue;
            }
            private set
            {
                _splitValue = value;
            }
        }

        public PlayerHand()
        {
            split = new List<Card>();
            money = 500;
            bet = 0;
        }
    }
}
