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
        public float money;
        public float bet;
        public float secondBet;
        private bool secondBusted;
        

        public Player()
        {
            money = 500f;
            bet = 0f;
            position[0] = (Console.WindowWidth / 4);
            position[1] = (int)(Console.WindowHeight * 0.40);
        }

        public void PlayerChoice(Deck deck, List<Card> hand, bool hasSplitted, float bet)
        {
            bool isPlayerDone = false;
            bool isFirstAction = true;

            int[] playerPos = new int[2];

            while (!isPlayerDone && CalculateHand(hand) < 22)
            {
                if (isFirstAction && money >= bet)
                {   
                    if (hand[0].cardValue.Equals(hand[1].cardValue) && !hasSplitted){
                        Console.CursorLeft = position[0];
                        Console.WriteLine("Space: Hit");
                        Console.CursorLeft = position[0];
                        Console.WriteLine("S: Stand");
                        Console.CursorLeft = position[0];
                        Console.WriteLine("D: Double-Down");
                        Console.CursorLeft = position[0];
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
                    } else
                    {
                        Console.CursorLeft = position[0];
                        Console.WriteLine("Space: Hit\nS: Stand\nD: Double-Down");
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
                    Console.SetCursorPosition(position[0], position[1]);
                    PrintHand(hand);
                    Console.CursorLeft = position[0];
                    Console.WriteLine("Space: Hit");
                    Console.CursorLeft = position[0];
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
                busted = true;
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
            secondBusted = false;
            // dela upp i två händer.
            secondHand.Add(hand[0]);
            hand.RemoveAt(0);

            // ge vardera hand 1 till kort.
            Hit(deck, hand);
            Hit(deck, secondHand);

            // Skapar ett andra bet
            money -= bet;
            secondBet += bet;

            // låt spelaren göra val för varje hand.
            int cursorPosition = Console.CursorTop;
            Console.WriteLine("First hand");
            PrintHand(hand);
            PlayerChoice(deck, hand, true, bet);
            Console.CursorTop = cursorPosition;
            Console.CursorLeft = 0;
            Console.WriteLine("Second hand");
            PrintHand(secondHand);
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
    }
}
