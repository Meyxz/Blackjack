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
        public readonly Value cardValue;
        public readonly Suit cardSuit;
        public int intValue;

        public Card(Value cardValue, Suit suit, int intValue)
        {
            this.cardValue = cardValue;
            this.cardSuit = suit;
            this.intValue = intValue;
        }

        public virtual string CardPrint(string output)
        {
            output = this.cardValue.ToString() + "ERROR";
            return output;
        }
    }
}
