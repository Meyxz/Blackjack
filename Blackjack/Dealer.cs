using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    class Dealer : Person
    {
        public int[] position = new int[2];

        public Dealer()
        {
            position[0] = (int)(Console.WindowWidth * 0.4F);
            position[1] = (int)(Console.WindowHeight * 0.1F);
        }

        public void PrintFirstHand()
        {
            Console.WriteLine(hand[0].CardPrint(""));
            Console.CursorLeft = position[0];
            Console.WriteLine("??");
        }
    }

    
}
