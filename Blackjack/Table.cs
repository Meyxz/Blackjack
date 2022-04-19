using System;
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
        const int maxBet = 250;


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
            Console.WriteLine("Dealer's hand");
            dealer.PrintFirstHand();
            Console.WriteLine("Player's hand");
            player.PrintHand(player.hand);

            if (dealer.HasBlackjack())
            {
                dealer.PrintHand(dealer.hand);
                if (player.HasBlackjack())
                {
                    Console.WriteLine("Push");
                    player.money += player.bet;
                }
                else
                {
                    Console.WriteLine("Dealer wins!");
                }
            }
            else if (player.HasBlackjack())
            {

                player.money += player.bet * 2.5F;
            }

            

            player.PlayerChoice(deck, player.hand, false, player.bet);


            while (dealer.CalculateHand() < 17)
            {
                dealer.Hit(deck, dealer.hand);
            }
            Console.Clear();
            Console.WriteLine("Dealer's hand");
            dealer.PrintHand(dealer.hand);
            Console.WriteLine("Player's hand");
            player.PrintHand(player.hand);

            // Kolla resultat

            if (player.CalculateHand() > 21)
            {
                Console.WriteLine("Bust");
            }
            else
            {
                if (dealer.CalculateHand() > 21)
                {
                    Console.WriteLine("Player Wins!");
                    player.money += player.bet * 2;
                }
                else
                {
                    if (dealer.HasBlackjack())
                    {
                        if (player.HasBlackjack())
                        {
                            Console.WriteLine("Push");
                            player.money += player.bet;
                        }
                        else
                        {
                            Console.WriteLine("Dealer wins!");
                        }
                    }
                    else if (player.CalculateHand() > dealer.CalculateHand())
                    {
                        Console.WriteLine("Player wins!");
                        player.money += player.bet * 2;
                    }
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
                Console.WriteLine("Too low funds to play.");
            }
            else
            {
                Console.WriteLine("Insert your bets ({0} - {1})", minBet, maxBet);
                Console.CursorVisible = true;
                while (!conversionCheck)
                {
                    string tempStr = Console.ReadLine().Trim();
                    if (float.TryParse(tempStr, out player.bet))
                    {

                        if (player.bet >= minBet && player.bet <= maxBet)
                        {
                            if (player.bet < player.money && player.bet >= minBet)
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
                }
                Console.CursorVisible = false;
            }
        }
    }
}
