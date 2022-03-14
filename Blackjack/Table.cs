using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Blackjack
{
    class Table
    {
        Dealer dealer;
        Hand playerHand;
        const int maxValue = 21;
        bool isBusted;

        public Table()
        {
            dealer = new Dealer(6);
            playerHand = new Hand();
            isBusted = false;
        }

        public void PlayTable()
        {
            dealer.Deal(playerHand, 2);
            dealer.Deal(dealer.dealerHand, 2);
            if (dealer.dealerHand.cards.Any(card => card.cardValue == Value.Ace))
            {
                dealer.dealerHand.HandValue();
                if (dealer.dealerHand.handValue == 21)
                {
                    dealer.revealDealer = true;
                }
            }
            PrintCards();
            PlayerChoice();
        }

        void PrintCards()
        {
            string tempPrint = string.Empty;
            Console.WriteLine("Dealer's Cards");
            foreach (Card card in dealer.dealerHand.cards)
            {
                Console.WriteLine(card.CardPrint(tempPrint));
            }
            Console.WriteLine("Your Cards");
            foreach (Card card in playerHand.cards)
            {
                Console.WriteLine(card.CardPrint(tempPrint));
            }
        }

        void PlayerChoice()
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Spacebar:
                    Hit();
                    break;
                case ConsoleKey.S:
                    Stand();
                    break;
                case ConsoleKey.D:
                    break;
                case ConsoleKey.Q:
                    break;
                default:
                    break;
            }
        }

        void Hit()
        {
            dealer.Deal(playerHand, 1);
            playerHand.HandValue();
            if (playerHand.handValue > maxValue)
            {
                isBusted = true;
            }
        }

        void Stand()
        {

        }


    }
}
