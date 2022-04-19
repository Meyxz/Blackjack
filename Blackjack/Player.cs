using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Blackjack
{
    class Player : Person
    {
        private List<Card> secondHand;
        public float money;
        public float bet;
        public float secondBet;

        public Player()
        {
            money = 500;
            bet = 0;
        }

        public void PlayerChoice(Deck deck, List<Card> hand, bool hasSplitted, float bet)
        {
            // Double down 
            // Split 
            // Stand
            // Hit
            bool isPlayerDone = false;
            bool isFirstAction = true;

            while (!isPlayerDone)
            {
                if (isFirstAction && money >= bet)
                {   
                    if (hand[0].cardValue.Equals(hand[1].cardValue) && !hasSplitted){
                        Console.WriteLine("Space: Hit S: Stand D: Double-Down Q: Split");
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
                        Console.WriteLine("Space: Hit S: Stand D: Double-Down");
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
                } else
                {
                    // After hit
                    Console.WriteLine("Your cards:");
                    PrintHand(hand);
                    Console.WriteLine("Space: Hit\nS: Stand");
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
            // dela upp i två händer.
            secondHand.Add(hand[0]);
            hand.RemoveAt(0);

            // ge vardera hand 1 till kort.
            Hit(deck, hand);
            Hit(deck, secondHand);

            // printa hands
            PrintHand(hand);
            PrintHand(secondHand);

            // Skapar ett andra bet
            money -= bet;
            secondBet += bet;

            // låt spelaren göra val för varje hand.
            PlayerChoice(deck, hand, true, bet);
            PlayerChoice(deck, hand, true, secondBet);

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
