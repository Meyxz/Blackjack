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


        public Player()
        {
            money = 500F;
            bet = 0F;
            position[0] = (Console.WindowWidth / 4);
            position[1] = (int)(Console.WindowHeight * 0.4F);
        }

        public void PlayerChoice(Deck deck, List<Card> hand, bool hasSplitted, float bet)
        {
            bool isPlayerDone = false;
            bool isFirstAction = true;

            int tempPos = Console.CursorLeft;

            while (!isPlayerDone && CalculateHand(hand) < 22)
            {
                if (isFirstAction && money >= bet)
                {
                    if (hand[0].cardValue.Equals(hand[1].cardValue) && !hasSplitted)
                    {

                        Console.CursorLeft = tempPos;
                        Console.WriteLine("Space: Hit");
                        Console.CursorLeft = tempPos; ;
                        Console.WriteLine("S: Stand");
                        Console.CursorLeft = tempPos;
                        Console.WriteLine("D: Double-Down");
                        Console.CursorLeft = tempPos;
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
                        Console.CursorLeft = tempPos;
                        Console.WriteLine("Space: Hit");
                        Console.CursorLeft = tempPos;
                        Console.WriteLine("S: Stand");
                        Console.CursorLeft = tempPos;
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
                    Console.SetCursorPosition(tempPos, position[1]);
                    PrintHand(hand);
                    Console.CursorLeft = tempPos;
                    Console.WriteLine("Space: Hit");
                    Console.CursorLeft = tempPos;
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
                Console.SetCursorPosition(tempPos, position[1]);
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
            secondPos[0] = (int)(Console.WindowWidth * 0.6F);
            secondPos[1] = position[1];
            // dela upp i två händer.
            secondHand.Add(hand[0]);
            hand.RemoveAt(0);

            // ge vardera hand 1 till kort.
            Hit(deck, hand);
            Hit(deck, secondHand);

            // Skapar ett andra bet
            money -= bet;
            secondBet += bet;
            PrintMoney();

            // Print hands
            Console.SetCursorPosition(position[0], position[1]);
            PrintHand(hand);
            Console.SetCursorPosition(secondPos[0], (secondPos[1] - 1));
            Console.WriteLine("Split Cards");
            Console.CursorLeft = secondPos[0];
            PrintHand(secondHand);

            // låt spelaren göra val för varje hand.
            Console.SetCursorPosition(position[0], position[1] + hand.Count);
            PlayerChoice(deck, hand, true, bet);
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(0, position[1] + hand.Count + i);
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(secondPos[0], secondPos[1] + secondHand.Count);
            PlayerChoice(deck, secondHand, true, secondBet);

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
            Console.SetCursorPosition(Console.WindowWidth - moneyPrint.Length, 0);
            Console.Write(moneyPrint);
        }
    }
}
