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

            dealer.Hit(deck, dealer.hand);
            dealer.Hit(deck, dealer.hand);

            player.Hit(deck, player.hand);
            player.Hit(deck, player.hand);

            Console.Clear();
            Console.SetCursorPosition(dealer.position[0], dealer.position[1] - 1);
            Console.WriteLine("Dealer's cards");
            Console.SetCursorPosition(dealer.position[0], dealer.position[1]);
            dealer.PrintFirstHand();
            Console.SetCursorPosition(player.position[0], player.position[1] - 1);
            Console.WriteLine("Player's cards");
            Console.SetCursorPosition(player.position[0], player.position[1]);
            player.PrintHand(player.hand);

            player.PrintMoney();

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
                    Console.WriteLine("Dealer blackjack!");
                }
            }
            else if (player.HasBlackjack(player.hand))
            {
                Console.SetCursorPosition(dealer.position[0], dealer.position[1]);
                dealer.PrintHand(dealer.hand);
                player.money += player.bet * 2.5F;
                Console.SetCursorPosition(player.position[0], player.position[1] + player.hand.Count);
                Console.WriteLine("Player natural blackjack");
            }
            else
            {
                Console.SetCursorPosition(player.position[0], player.position[1] + player.hand.Count);
                player.PlayerChoice(deck, player.hand, false, player.bet);
                Console.SetCursorPosition(0, player.position[1] + player.hand.Count);
                for (int i = 0; i < 4; i++)
                {
                    Console.SetCursorPosition(player.position[0], player.position[1] + player.hand.Count + i);
                    Console.WriteLine(new string(' ', 14));
                }
                if (player.secondHand != null)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        Console.SetCursorPosition(player.secondPos[0], player.secondPos[1] + player.secondHand.Count + i);
                        Console.WriteLine(new string(' ', 14));
                    }
                }

                while (dealer.CalculateHand(dealer.hand) < 17)
                {
                    dealer.Hit(deck, dealer.hand);
                }
                Console.SetCursorPosition(dealer.position[0], dealer.position[1]);
                dealer.PrintHand(dealer.hand);

                CheckResult(player.hand, player.bet, player.position);
                if (player.secondHand != null)
                {
                    CheckResult(player.secondHand, player.secondBet, player.secondPos);
                }

                player.PrintMoney();
            }
        }

        public void PlayTable()
        {
            bool runCheck = false;
            Console.Clear();
            Console.WriteLine("Welcome to the Blackjack table\nPress any key to continue");
            Console.ReadKey(true);
            while (!runCheck)
            {
                PlayRound();
                string temp = "Do you want to play another round? (Y/N)";
                Console.SetCursorPosition((Console.WindowWidth / 2) - (temp.Length / 2), (int)(Console.WindowHeight * 0.70));
                Console.WriteLine(temp);

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
                        Environment.Exit(0);
                    }
                }
            }
        }

        void TableBet(Player player)
        {
            Console.Clear();
            bool conversionCheck = false;
            if (player.money < minBet)
            {
                Console.WriteLine("Funds are too low for minimum bet.\nReturn when your funds are higher.");
                Console.ReadKey(true);
                Environment.Exit(0);
            }
            else
            {
                player.PrintMoney();
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
                        Console.WriteLine("Incorrect number");
                    }
                    Console.SetCursorPosition(0, toppos + 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
                Console.CursorVisible = false;
            }
        }

        public void CheckResult(List<Card> hand, float bet, int[] position)
        {
            int dealerValue = dealer.CalculateHand(dealer.hand);
            int playerValue = player.CalculateHand(hand);

            Console.SetCursorPosition(position[0], position[1] + hand.Count);
            if (playerValue > 21)
            {
                Console.WriteLine("Player bust");
            }
            else
            {
                if (dealerValue > 21)
                {
                    Console.WriteLine("Player Wins!");
                    player.money += (bet * 2);
                }
                else
                {
                    if (playerValue == dealerValue)
                    {
                        Console.WriteLine("Push");
                        player.money += player.bet;
                    }
                    else if (playerValue > dealerValue)
                    {
                        Console.WriteLine("Player wins!");
                        player.money += (bet * 2);
                    }
                    else
                    {
                        Console.WriteLine("Dealer wins!");
                    }
                }
            }
        }
    }
}
