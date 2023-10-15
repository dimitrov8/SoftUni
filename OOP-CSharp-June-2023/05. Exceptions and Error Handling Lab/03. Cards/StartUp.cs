namespace Cards
{
    using System;
    using System.Collections.Generic;

    public class StartUp
    {
        static void Main(string[] args)
        {
            List<Card> validCards = new List<Card>();
            string[] inputCards = Console.ReadLine().Split(", ");

            foreach (var card in inputCards)
            {
                string[] currentCard = card.Split();
                try
                {
                    Card newCard = new Card(currentCard);
                    validCards.Add(newCard);
                }
                catch (ArgumentException argex)
                {
                    Console.WriteLine(argex.Message);
                }
            }

            Console.WriteLine(string.Join(' ', validCards));
        }

        class Card
        {
            private string face;
            private string suit;

            private readonly HashSet<string> faces = new HashSet<string>
            {
                "2", "3", "4", "5", "6", "7", "8", "9", "10",
                "J", "Q", "K", "A"
            };

            public Card(string[] args)
            {
                this.Face = args[0];
                this.Suit = args[1];
            }

            public string Face
            {
                get => this.face;
                private set
                {
                    if (!this.faces.Contains(value))
                        throw new ArgumentException("Invalid card!");

                    this.face = value;
                }
            }

            public string Suit
            {
                get => this.suit;
                private set
                {
                    this.suit = value switch
                    {
                        "S" => value = "\u2660",
                        "H" => value = "\u2665",
                        "D" => value = "\u2666",
                        "C" => value = "\u2663",
                        _ => throw new ArgumentException("Invalid card!")
                    };
                }
            }

            public override string ToString() => $"[{this.Face}{this.Suit}]";
        }
    }
}