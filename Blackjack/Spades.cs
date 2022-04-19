using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Spades : Card
    {
        public Spades(Value value, Suit suit, int intValue) : base(value, suit, intValue)
        {
        }

        public override string CardPrint(string output)
        {
            output = this.cardValue.ToString() + "♠ " + intValue;
            return output;
        }
    }
}
