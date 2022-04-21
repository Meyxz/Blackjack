using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Hearts : Card
    {
        public Hearts(Value value, Suit suit, int intValue) : base(value, suit, intValue)
        {
        }

        public override string CardPrint(string output)
        {
            int index = Array.IndexOf(Enum.GetValues(cardValue.GetType()), cardValue);
            if (index < 10 && index > 0)
            {
                output = intValue + "♥";
                return output;
            }
            else
            {
                output = this.cardValue.ToString().Substring(0, 1) + "♥";
                return output;
            }
        }
    }
}
