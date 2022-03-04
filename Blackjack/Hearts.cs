using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Hearts : Card
    {
        public Hearts(Value value, Suit suit) : base(value, suit)
        {
        }

        public override string CardPrint(string output)
        {
            output = this.cardValue.ToString() + "♥";
            return output;
        }
    }
}
