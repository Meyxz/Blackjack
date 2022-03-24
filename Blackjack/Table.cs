using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Blackjack
{
    class Table
    {
        Dealer dealer;
        PlayerHand playerHand;
        const int maxValue = 21;
        const int minBet = 5;
        const int maxBet = 250;

        public Table()
        {
            dealer = new Dealer(6);
            playerHand = new PlayerHand();
        }

        public void PlayTable()
        {
            playerHand.bet = 0;
            TableBet();

            

            dealer.Deal(playerHand.cards, 2);
            dealer.Deal(dealer.dealerHand.cards, 2);
            if (dealer.dealerHand.cards.Any(card => card.cardValue == Value.Ace))
            {
                dealer.dealerHand.HandValue(dealer.dealerHand.cards);
                if (dealer.dealerHand.handValue == maxValue)
                {
                    dealer.revealDealer = true;
                    EndCheck();
                }
            }
            PlayerChoice();
            PrintCards();
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

        public void Reset()
        {
            playerHand.split.Clear();
            playerHand.cards.Clear();
            dealer.dealerHand.cards.Clear();
            if (dealer.deck.cards.Count >= 200)
            {
                dealer = new Dealer(6);
            }
        }

        void EndCheck() //todo
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
                    Console.WriteLine("Blackjack");
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
            Console.ResetColor();
            foreach (Card card in dealer.dealerHand.cards)
            {
                Console.WriteLine(card.CardPrint(tempPrint));
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Your Cards");
            Console.ResetColor();
            foreach (Card card in playerHand.cards)
            {
                Console.WriteLine(card.CardPrint(tempPrint));
            }
            if (playerHand.split.Any())
            {
                Console.WriteLine();
                foreach (Card card in playerHand.split)
                {
                    Console.WriteLine(card.CardPrint(tempPrint));
                }
            }
        }

        void PlayerChoice()
        {
            bool isPlayerDone = false;
            bool isSplit = false;
            bool isFirstAction = false;

            while (!isPlayerDone && !playerHand.isBusted)
	        {
                PrintCards();
                if (!isSplit)
                {
                    if (!isFirstAction)
                    {
                        if (playerHand.cards[0].Equals(playerHand.cards[1]))
                        {
                            Console.WriteLine("Space: Hit S: Stand D: Double-Down Q: Split");
                            switch (Console.ReadKey(true).Key)
                            {
                                // Hit
                                case ConsoleKey.Spacebar:
                                    Hit(playerHand);
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
                                    Split();
                                    isSplit = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Space: Hit S: Stand D: Double-Down");
                            switch (Console.ReadKey(true).Key)
                            {
                                // Hit
                                case ConsoleKey.Spacebar:
                                    Hit(playerHand);
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
                                default:
                                    break;
                            }
                        }
                        isFirstAction = false;
                    }
                    else
                    {
                        // After hit
                        PrintCards();
                        Console.WriteLine("Space: Hit\nS: Stand");
                        switch (Console.ReadKey(true).Key)
                        {
                            // Hit
                            case ConsoleKey.Spacebar:
                                Hit(playerHand);
                                break;
                            // Stand
                            case ConsoleKey.S:
                                isPlayerDone = true;
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    // Split
                    bool firstSplit = true;
                    bool firstSplitCall = true;
                    if (firstSplit)
                    {
                        if (firstSplitCall) //First action on first deck
                        {
                            PrintCards();
                            Console.WriteLine("First Deck\nSpace: Hit\nS: Stand\nD: Double-Down");
                            switch (Console.ReadKey(true).Key)
                            {
                                // Hit
                                case ConsoleKey.Spacebar:
                                    Hit(playerHand);
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
                                default:
                                    break;
                            }
                            firstSplitCall = false;
                        }
                        else 
                        {
                            Console.WriteLine("Space: Hit S: Stand D: Double-Down");
                            switch (Console.ReadKey(true).Key)
                            {
                                // Hit
                                case ConsoleKey.Spacebar:
                                    Hit(playerHand, isSplit);
                                    break;
                                // Stand
                                case ConsoleKey.S:
                                    isPlayerDone = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else // Second deck
                    {
                        firstSplitCall = true;
                        if (firstSplitCall) //First action on first deck
                        {
                            PrintCards();
                            Console.WriteLine("First Deck\nSpace: Hit S: Stand D: Double-Down");
                            switch (Console.ReadKey(true).Key)
                            {
                                // Hit
                                case ConsoleKey.Spacebar:
                                    Hit(playerHand, isSplit);
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
                                default:
                                    break;
                            }
                            firstSplitCall = false;
                        }
                        else
                        {
                            Console.WriteLine("Space: Hit S: Stand D: Double-Down");
                            switch (Console.ReadKey(true).Key)
                            {
                                // Hit
                                case ConsoleKey.Spacebar:
                                    Hit(playerHand, isSplit);
                                    break;
                                // Stand
                                case ConsoleKey.S:
                                    isPlayerDone = true;
                                    break;
                                default:
                                    break;
                            }
                        }

                    }
                }
	        }
        }

        Hand Hit(PlayerHand player)
        {
            dealer.Deal(player.cards, 1);
            player.HandValue(player.cards);
            if (player.handValue > maxValue)
            {
                Console.WriteLine("\nBust");
                player.isBusted = true;
            }

            return player;
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

        void Split()
        {
            playerHand.split.Add(playerHand.cards[1]);
            playerHand.cards.Remove(playerHand.cards[1]);
        }
    }
}
