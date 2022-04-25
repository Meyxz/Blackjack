using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Blackjack
{
    class Player : Person
    {
        public List<Card> secondHand;
        public int[] position = new int[2];
        public int[] secondPos;
        public float money;
        public float bet;
        public float secondBet;
        public int playerID;
        public bool naturalCheck;

        public Player(int id, float width)
        {
            money = 500F;
            bet = 0F;
            playerID = id;
            position[0] = (int)(Console.WindowWidth * width);
            position[1] = (int)(Console.WindowHeight * 0.4F);
            naturalCheck = false;
        }

        public void PlayerChoice(Deck deck, List<Card> hand, bool hasSplitted, float bet)
        {
            bool isPlayerDone = false;
            bool isFirstAction = true;

            int tempPos = Console.CursorTop;
            int printPos = Console.CursorLeft + ("Total value: " + CalculateHand(hand)).Length + 2;

            while (!isPlayerDone && CalculateHand(hand) < 22)
            {
                if (isFirstAction && money >= bet)
                {
                    if (hand[0].cardValue.Equals(hand[1].cardValue) && !hasSplitted)
                    {
                        Console.CursorTop = tempPos;
                        Console.CursorLeft = printPos;
                        Console.WriteLine("Space: Hit");
                        Console.CursorLeft = printPos;
                        Console.WriteLine("S: Stand");
                        Console.CursorLeft = printPos;
                        Console.WriteLine("D: Double-Down");
                        Console.CursorLeft = printPos;
                        Console.WriteLine("Q: Split");
                        switch (Console.ReadKey(true).Key)
                        {
                            // Hit
                            case ConsoleKey.Spacebar:
                                Hit(deck, hand);
                                break;
                            // Stand
                            case ConsoleKey.S:
                                isPlayerDone = true;
                                break;
                            // Double-down
                            case ConsoleKey.D:
                                DoubleDown(deck);
                                Console.SetCursorPosition(position[0], tempPos);
                                PrintHand(hand);
                                isPlayerDone = true;
                                break;
                            // Split
                            case ConsoleKey.Q:
                                Split(deck);
                                isPlayerDone = true;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        Console.CursorTop = tempPos;
                        Console.CursorLeft = printPos;
                        Console.WriteLine("Space: Hit");
                        Console.CursorLeft = printPos;
                        Console.WriteLine("S: Stand");
                        Console.CursorLeft = printPos;
                        Console.WriteLine("D: Double-Down");
                        switch (Console.ReadKey(true).Key)
                        {
                            // Hit
                            case ConsoleKey.Spacebar:
                                Hit(deck, hand);
                                break;
                            // Stand
                            case ConsoleKey.S:
                                isPlayerDone = true;
                                break;
                            // Double-down
                            case ConsoleKey.D:
                                DoubleDown(deck);
                                Console.SetCursorPosition(position[0], tempPos);
                                PrintHand(hand);
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
                    Console.SetCursorPosition(position[0], tempPos);
                    PrintHand(hand);
                    Console.CursorTop = tempPos;
                    Console.CursorLeft = printPos;
                    Console.WriteLine("Space: Hit");
                    Console.CursorLeft = printPos;
                    Console.WriteLine("S: Stand");
                    switch (Console.ReadKey(true).Key)
                    {
                        // Hit
                        case ConsoleKey.Spacebar:
                            Hit(deck, hand);
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

            if (CalculateHand(hand) > 21)
            {
                Console.SetCursorPosition(position[0], tempPos);
                PrintHand(hand);
            }
        }


        private void DoubleDown(Deck deck)
        {

            money -= bet;
            bet += bet;

            Hit(deck, this.hand);
        }

        private void Split(Deck deck)
        {
            secondHand = new List<Card>();
            secondPos = new int[2];
            secondPos[0] = position[0];
            secondPos[1] = (int)(Console.WindowHeight * 0.7F);
            // dela upp i två händer.
            secondHand.Add(hand[1]);
            hand.RemoveAt(1);

            // ge vardera hand 1 till kort.
            Hit(deck, hand);
            Hit(deck, secondHand);

            // Skapar ett andra bet
            money -= bet;
            secondBet += bet;
            PrintMoney(playerID);

            // Print hands
            Console.SetCursorPosition(position[0], position[1]);
            PrintHand(hand);
            Console.SetCursorPosition(secondPos[0], (secondPos[1] - 1));
            Console.WriteLine("Split Cards");
            Console.CursorLeft = secondPos[0];
            PrintHand(secondHand);

            // låt spelaren göra val för varje hand.
            Console.SetCursorPosition(position[0], position[1]);
            PlayerChoice(deck, hand, true, bet);
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(position[0] + ("Total value: " + CalculateHand(hand)).Length + 2, position[1] + i);
                Console.Write(new string(' ', 14));
            }

            Console.SetCursorPosition(secondPos[0], secondPos[1]);
            PlayerChoice(deck, secondHand, true, secondBet);
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(secondPos[0] + ("Total value: " + CalculateHand(secondHand)).Length + 2, secondPos[1] + i);
                Console.Write(new string(' ', 14));
            }
        }

        public void ClearHands()
        {
            hand.Clear();
            if (secondHand != null)
            {
                secondHand.Clear();
            }
        }

        public void PrintMoney()
        {
            string moneyPrint = "Cash: " + money;

            Console.SetCursorPosition(Console.WindowWidth - (moneyPrint.Length + 7), 0);
            Console.Write(new string(' ', moneyPrint.Length + 7));
            Console.SetCursorPosition(Console.WindowWidth - moneyPrint.Length, 0);
            Console.Write(moneyPrint);
        }

        public void PrintMoney(int player)
        {
            string moneyPrint = "Cash: " + money;

            Console.SetCursorPosition(Console.WindowWidth - (moneyPrint.Length + 7), player);
            Console.Write(new string(' ', moneyPrint.Length + 7));
            Console.SetCursorPosition(Console.WindowWidth - moneyPrint.Length, player);
            Console.Write(moneyPrint);
        }
    }
}
