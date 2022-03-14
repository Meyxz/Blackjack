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

        public Table()
        {
            dealer = new Dealer(6);
            playerHand = new Hand();
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
    }
}
