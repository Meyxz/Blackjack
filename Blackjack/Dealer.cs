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
            position[0] = (Console.WindowWidth / 4);
            position[1] = (int)(Console.WindowHeight * 0.15);
        }

        public void PrintFirstHand()
        {
            Console.WriteLine(hand[0].CardPrint(""));
            Console.CursorLeft = position[0];
            Console.WriteLine("??");
        }
    }

    
}
