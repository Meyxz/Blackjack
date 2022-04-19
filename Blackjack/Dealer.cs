using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Dealer : Person
    {
        public Dealer()
        {

        }

        public void PrintFirstHand()
        {
            Console.WriteLine(hand[0].CardPrint(""));
            Console.WriteLine("?");
        }
    }

    
}
