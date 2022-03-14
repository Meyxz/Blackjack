using System;
using System.Collections.Generic;
using System.Text;

namespace Blackjack
{
    public enum Value
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public enum Suit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }

    class Card
    {
        public bool isAvailable;
        public readonly Value cardValue;
        public readonly Suit cardSuit;

        public Card(Value cardValue, Suit suit)
        {
            this.cardValue = cardValue;
            this.cardSuit = suit;
            isAvailable = true;
        }

        public virtual string CardPrint(string output)
        {
            output = this.cardValue.ToString() + "ERROR";
            return output;
        }
    }
}
