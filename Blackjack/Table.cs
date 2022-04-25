using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;

namespace Blackjack
{

    class Table
    {
        Dealer dealer;
        List<Player> players;
        Deck deck;

        const int maxValue = 21;
        const int minBet = 5;
        const int maxBet = 500;

        public Table()
        {
            dealer = new Dealer();
            deck = new Deck(6, true);
        }

        public void PlayRound(int playercount)
        {
            Console.Clear();
            dealer.hand.Clear();

            dealer.Hit(deck, dealer.hand);
            dealer.Hit(deck, dealer.hand);

            foreach (Player player in players)
            {
                player.bet = 0;
                player.secondBet = 0;
                TableBet(player);
                player.ClearHands();

                player.Hit(deck, player.hand);
                player.Hit(deck, player.hand);
            }

            Console.Clear();
            Console.SetCursorPosition(dealer.position[0], dealer.position[1] - 1);
            Console.WriteLine("Dealer's cards");
            Console.SetCursorPosition(dealer.position[0], dealer.position[1]);
            dealer.PrintFirstHand();

            for (int i = 0; i < players.Count; i++)
            {
                players[i].PrintMoney(i);
            }

            foreach (Player player in players)
            {
                
                Console.SetCursorPosition(player.position[0], player.position[1] - 1);
                Console.WriteLine("Player's cards");
                Console.SetCursorPosition(player.position[0], player.position[1]);
                player.PrintHand(player.hand);
            }


            if (dealer.HasBlackjack(dealer.hand))
            {
                Console.SetCursorPosition(dealer.position[0], dealer.position[1]);
                dealer.PrintHand(dealer.hand);
                foreach (Player player in players)
                {
                    Console.SetCursorPosition(player.position[0], player.position[1] + player.hand.Count);
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
            }

            foreach (Player player in players.FindAll(x => x.HasBlackjack(x.hand) == true))
            {
                player.money += (float)Math.Round(player.bet * 2.5F, 1);
                Console.SetCursorPosition(player.position[0], player.position[1] + player.hand.Count + 1);
                Console.WriteLine("Player natural blackjack");
                player.naturalCheck = true;
            }

            foreach (Player player in players)
            {
                if (!player.naturalCheck)
                {
                    Console.SetCursorPosition(player.position[0], player.position[1]);
                    player.PlayerChoice(deck, player.hand, false, player.bet);
                    Console.SetCursorPosition(0, player.position[1] + player.hand.Count);
                    for (int i = 0; i < 4; i++)
                    {
                        Console.SetCursorPosition((player.position[0] + ("Total value: " + player.CalculateHand(player.hand)).Length + 2), player.position[1] + i);
                        Console.Write(new string(' ', 14));
                    }
                    if (player.secondHand != null)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            Console.SetCursorPosition((player.secondPos[0] + ("Total value: " + player.CalculateHand(player.secondHand)).Length + 2), player.secondPos[1]);
                            Console.WriteLine(new string(' ', 14));
                        }
                    }
                }
            }

            foreach (Player player in players)
            {
                if (!player.naturalCheck)
                {
                    while (dealer.CalculateHand(dealer.hand) < 17)
                    {
                        dealer.Hit(deck, dealer.hand);
                    }
                    Console.SetCursorPosition(dealer.position[0], dealer.position[1]);
                    dealer.PrintHand(dealer.hand);

                    player.money = CheckResult(player.hand, player.bet, player.money, player.position);
                    if (player.secondHand != null)
                    {
                        player.money = CheckResult(player.secondHand, player.secondBet, player.money, player.secondPos);
                    }
                }
            }

            for (int i = 0; i < players.Count; i++)
            {
                players[i].PrintMoney(i);
            }
        }

        public void PlayTable()
        {
            bool runCheck = false;

            bool continueCheck = false;
            int playerCount = 0;
            Console.Clear();
            Console.WriteLine("Welcome to the Blackjack table\nPress any key to continue");
            Console.ReadKey(true);
            playerCount = CheckPlayers(playerCount);

            players = new List<Player>(playerCount);

            for (int i = 0; i < playerCount; i++)
            {
                players.Add(new Player(i, 0.1F + (0.3F * i)));
            }

            while (!runCheck)
            {
                if (players.Count == 0)
                {
                    Console.WriteLine("No remaining players that can meet the minimum bet.");
                    Environment.Exit(0);
                }

                PlayRound(playerCount);

                Console.SetCursorPosition(0, (int)(Console.WindowHeight * 0.1F));
                foreach (Player player in players.ToList())
                {
                    if (player.money < minBet)
                    {
                        Console.WriteLine($"Player {player.playerID}'s funds are too low for minimum bet.\nThey have been removed from the game");
                        players.Remove(player);
                    }
                }
                players.TrimExcess();

                continueCheck = false;

                string temp = "Do you want to play another round? (Y/N)";
                Console.SetCursorPosition((Console.WindowWidth / 2) - (temp.Length / 2), 0);
                Console.WriteLine(temp);
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

        int CheckPlayers(int playerCount)
        {
            bool conversionCheck = false;
            Console.Clear();
            string temp = "How many players will be playing? (1-3)";
            int midpos = (Console.WindowWidth / 2) - (temp.Length / 2);
            int toppos = (Console.WindowHeight / 3);
            Console.SetCursorPosition(midpos, toppos);
            Console.WriteLine(temp);
            Console.CursorVisible = true;
            while (!conversionCheck)
            {
                Console.SetCursorPosition(midpos, toppos + 1);
                string tempStr = Console.ReadLine().Trim();
                if (int.TryParse(tempStr, out playerCount))
                {
                    if (playerCount <= 3 && playerCount >= 1)
                    {
                        conversionCheck = true;
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
            return playerCount;
        }

        void TableBet(Player player)
        {
            Console.Clear();
            bool conversionCheck = false;
                
            player.PrintMoney();
            string temp = string.Format($"Player {player.playerID + 1}: insert your bets ({minBet} - {maxBet})");
            int midpos = (Console.WindowWidth / 2) - (temp.Length / 2);
            int toppos = (Console.WindowHeight / 3);
            Console.SetCursorPosition(midpos, toppos);

            Console.WriteLine(temp);
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

        public float CheckResult(List<Card> hand, float bet, float money, int[] position)
        {
            int dealerValue = dealer.CalculateHand(dealer.hand);
            int playerValue = dealer.CalculateHand(hand);

            Console.SetCursorPosition(position[0], position[1] + hand.Count + 1);
            if (playerValue > 21)
            {
                Console.WriteLine("Player bust");
            }
            else
            {
                if (dealerValue > 21)
                {
                    Console.WriteLine("Player Wins!");
                    money += (float)Math.Round(bet * 2, 1);
                }
                else
                {
                    if (playerValue == dealerValue)
                    {
                        Console.WriteLine("Push");
                        money += bet;
                    }
                    else if (playerValue > dealerValue)
                    {
                        Console.WriteLine("Player wins!");
                        money += (float)Math.Round(bet * 2, 1);
                    }
                    else
                    {
                        Console.WriteLine("Dealer wins!");
                    }
                }
            }
            return money;
        }
    }
}
