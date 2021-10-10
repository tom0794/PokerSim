using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerSim
{
    public class Card : PictureBox
    {
        public int CardId { get; set; }
        public int Suit { get; set; }
        public int Strength { get; set; }
        public string Type { get; set; }
        public string ImageName { get; set; }
        public string LongName { get; set; }
        public string ShortName { get; set; }

        private const int DECK_SIZE = 52;

        public Card()
        {

        }

        public enum Suits
        {
            Diamonds = 0,
            Hearts = 1,
            Spades = 2,
            Clubs = 3
        }

        public enum Strengths
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 11,
            Queen = 12,
            King = 13,
            Ace = 14
        }

        public static List<Card> CreateDeck()
        {
            List<Card> deck = new List<Card>();
            string[] cardNames = { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace" };
            string[] cardShort = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
            string[] suits = { "Diamonds", "Hearts", "Spades", "Clubs" };
            int suitNumber = 0;
            int numAddedOfCurrentSuit = 0;

            for (int i = 0; i < DECK_SIZE; i++)
            {
                Card newCard = new Card();
                newCard.CardId = i;
                newCard.Suit = suitNumber;
                newCard.Strength = numAddedOfCurrentSuit + 2;
                newCard.Type = cardNames[numAddedOfCurrentSuit];
                newCard.LongName = newCard.Type + " of " + suits[newCard.Suit];
                newCard.ShortName = cardShort[numAddedOfCurrentSuit] + suits[newCard.Suit][0];
                if (numAddedOfCurrentSuit <= 8)
                {
                    newCard.ImageName = "_" + newCard.ShortName;
                }
                else
                {
                    newCard.ImageName = newCard.ShortName;
                }
                newCard.Image = (Image)Properties.Resources.ResourceManager.GetObject(newCard.ImageName);
                deck.Add(newCard);

                numAddedOfCurrentSuit++;
                if (numAddedOfCurrentSuit == 13)
                {
                    numAddedOfCurrentSuit = 0;
                    suitNumber++;
                    if (suitNumber == 4)
                    {
                        break;
                    }
                }
            }


            return deck;
        }
    }
}
