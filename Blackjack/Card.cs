using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public enum Values
    {
        Ace = 0,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    class Card
    {
        private bool isActive;

        public Card()
        {
            isActive = true;
        }
    }
}
