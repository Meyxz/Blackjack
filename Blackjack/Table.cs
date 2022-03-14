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
        const int minBet = 5;
        const int maxBet = 250;

        public Table()
        {
            dealer = new Dealer(6);
            playerHand = new Hand();
        }

        public void PlayTable()
        {
            playerHand.bet = 0;
            TableBet();

            dealer.Deal(playerHand, 2);
            dealer.Deal(dealer.dealerHand, 2);
            if (dealer.dealerHand.cards.Any(card => card.cardValue == Value.Ace))
            {
                dealer.dealerHand.HandValue();
                if (dealer.dealerHand.handValue == maxValue)
                {
                    dealer.revealDealer = true;
                    EndCheck();
                }
            }
            PrintCards();
            PlayerChoice();
            EndCheck();            
        }

        void TableBet()
        {
            bool conversionCheck = false;
            Console.WriteLine("Insert your bets ({0} - {1})", minBet, maxBet);
            Console.CursorVisible = true;
            while (!conversionCheck)
            {
                string tempStr = Console.ReadLine().Trim();
                if (float.TryParse(tempStr, out playerHand.bet))
                {

                    if (playerHand.bet >= minBet && playerHand.bet <= maxBet)
                    {
                        if (playerHand.bet < playerHand.money && playerHand.bet >= minBet)
                        {
                            conversionCheck = true;
                            playerHand.money -= playerHand.bet;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Number not in range");
                    }
                }
                else
                {
                    Console.WriteLine("Incorrect number");
                }
            }
            Console.CursorVisible = false;
        }

        void EndCheck()
        {
            // Blackjack
            if (playerHand.handValue == maxValue)
            {
                // Push or Blackjack
                if (playerHand.handValue == dealer.dealerHand.handValue)
                {
                    Console.WriteLine("Push");
                    playerHand.money += playerHand.bet;
                }
                else
                {
                    playerHand.money += (playerHand.bet * 2.5F);
                }
            }
        }

        void PrintCards()
        {
            Console.Clear();
            string tempPrint = string.Empty;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Dealer's Cards");
            foreach (Card card in dealer.dealerHand.cards)
            {
                Console.WriteLine(card.CardPrint(tempPrint));
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Your Cards");
            foreach (Card card in playerHand.cards)
            {
                Console.WriteLine(card.CardPrint(tempPrint));
            }
        }

        void PlayerChoice()
        {
            bool isPlayerDone = false;
            Console.WriteLine("Space: Hit\nS: Stand\nD: Double-Down\nQ: Split");

            while (!isPlayerDone && !playerHand.isBusted)
	        {
                switch (Console.ReadKey(true).Key)
                {
                    // Hit
                    case ConsoleKey.Spacebar:
                        Hit();
                        break;
                    // Stand
                    case ConsoleKey.S:
                        isPlayerDone = true;
                        break;
                    // Double-down
                    case ConsoleKey.D:
                        DoubleDown();
                        isPlayerDone = true;
                        break;
                    // Split
                    case ConsoleKey.Q:
                        break;
                    default:
                        break;
                }
	        }
        }

        void Hit()
        {
            dealer.Deal(playerHand, 1);
            playerHand.HandValue();
            if (playerHand.handValue > maxValue)
            {
                playerHand.isBusted = true;
            }
        }

        void DoubleDown()
        {
            if (playerHand.money < playerHand.bet)
            {
                Console.WriteLine("Not enough money");
                return;
            }
            else
            {
                playerHand.money -= playerHand.bet;
                playerHand.bet += playerHand.bet;
            }
            
        }
    }
}
