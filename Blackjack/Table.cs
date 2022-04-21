﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Blackjack
{
    class Table
    {
        Dealer dealer;
        Player player;
        Deck deck;

        const int maxValue = 21;
        const int minBet = 5;
        const int maxBet = 500;

        public Table()
        {
            dealer = new Dealer();
            player = new Player();
            deck = new Deck(6, true);
        }

        public void PlayRound()
        {
            Console.Clear();
            player.bet = 0;
            TableBet(player);
            dealer.hand.Clear();
            player.ClearHands();

            dealer.AddCardFromDeck(deck, dealer.hand);
            dealer.AddCardFromDeck(deck, dealer.hand);
            player.AddCardFromDeck(deck, player.hand);
            player.AddCardFromDeck(deck, player.hand);
            Console.Clear();
            Console.SetCursorPosition(dealer.position[0], dealer.position[1] - 1);
            Console.WriteLine("Dealer's cards");
            Console.SetCursorPosition(dealer.position[0], dealer.position[1]);
            dealer.PrintFirstHand();
            Console.SetCursorPosition(player.position[0], player.position[1] - 1);
            Console.WriteLine("Player's cards");
            Console.SetCursorPosition(player.position[0], player.position[1]);
            player.PrintHand(player.hand);

            string moneyPrint = "Cash: " + player.money;
            Console.SetCursorPosition(Console.WindowWidth - moneyPrint.Length, 0);
            Console.Write(moneyPrint);

            if (dealer.HasBlackjack(dealer.hand))
            {
                Console.SetCursorPosition(dealer.position[0], dealer.position[1]);
                dealer.PrintHand(dealer.hand);
                if (player.HasBlackjack(player.hand))
                {

                    Console.WriteLine("Push");
                    player.money += player.bet;
                }
                else
                {
                    Console.WriteLine("Dealer wins!");
                }
            }
            else if (player.HasBlackjack(player.hand))
            {
                Console.SetCursorPosition(dealer.position[0], dealer.position[1]);
                dealer.PrintHand(dealer.hand);
                player.money += player.bet * 2.5F;
                Console.WriteLine("Player natural blackjack");
            }
            else
            {
                player.PlayerChoice(deck, player.hand, false, player.bet);


                while (dealer.CalculateHand(dealer.hand) < 17)
                {
                    dealer.Hit(deck, dealer.hand);
                }
                Console.SetCursorPosition(dealer.position[0], dealer.position[1]);
                dealer.PrintHand(dealer.hand);

                CheckResult(player.hand, player.bet);
                if (player.secondHand != null)
                {
                    CheckResult(player.secondHand, player.secondBet);
                }
            }
        }

        public void PlayTable()
        {
            bool runCheck = false;
            while (!runCheck)
            {
                Console.Clear();
                Console.WriteLine("Welcome to the Blackjack table\nPress any key to continue");
                Console.ReadKey(true);
                PlayRound();
                Console.WriteLine("Do you want to play another round? (Y/N)");
                bool continueCheck = false;
                while (!continueCheck)
                {
                    ConsoleKey ckey = Console.ReadKey(true).Key;
                    if (ckey == ConsoleKey.Y)
                    {
                        continueCheck = true;
                    }
                    else if (ckey == ConsoleKey.N)
                    {
                        runCheck = true;
                    }
                }
            }
        }

        void TableBet(Player player)
        {
            bool conversionCheck = false;
            if (player.money < minBet)
            {
                Console.WriteLine("Funds are too low for minimum bet.");
            }
            else
            {
                string moneyPrint = "Cash: " + player.money;
                Console.SetCursorPosition(Console.WindowWidth - moneyPrint.Length, 0);
                Console.Write(moneyPrint);
                string temp = string.Format("Insert your bets ({0} - {1})", minBet, maxBet);
                int midpos = (Console.WindowWidth / 2) - (temp.Length / 2);
                int toppos = (Console.WindowHeight / 3);
                Console.SetCursorPosition(midpos, toppos);
                Console.WriteLine(temp);
                Console.SetCursorPosition(0, Console.CursorTop += 1);
                Console.CursorVisible = true;
                while (!conversionCheck)
                {
                    Console.SetCursorPosition(midpos, toppos + 1);
                    string tempStr = Console.ReadLine().Trim();
                    if (float.TryParse(tempStr, out player.bet))
                    {

                        if (player.bet >= minBet && player.bet <= maxBet)
                        {
                            if (player.bet <= player.money && player.bet >= minBet)
                            {
                                conversionCheck = true;
                                player.money -= player.bet;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Number not in range");
                        }
                    }
                    else
                    {
                        Console.Write("Incorrect number      ");
                    }
                    Console.SetCursorPosition(0, toppos + 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
                Console.CursorVisible = false;
            }
        }

        public void CheckResult(List<Card> hand, float bet)
        {
            if (player.CalculateHand(hand) > 21)
            {
                Console.WriteLine("Bust");
            }
            else
            {
                if (dealer.CalculateHand(dealer.hand) > 21)
                {
                    Console.WriteLine("Player Wins!");
                    player.money += bet * 2;
                }
                else
                {
                    if (dealer.HasBlackjack(dealer.hand))
                    {
                        if (player.HasBlackjack(dealer.hand))
                        {
                            Console.WriteLine("Push");
                            player.money += bet;
                        }
                        else
                        {
                            Console.WriteLine("Dealer wins!");
                        }
                    }
                    else if (player.CalculateHand(hand) > dealer.CalculateHand(dealer.hand))
                    {
                        Console.WriteLine("Player wins!");
                        player.money += bet * 2;
                    }
                }
            }
        }
    }
}
