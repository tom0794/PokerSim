using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSim
{
    class HandStrength
    {
        /* HandValue: int[6]
         *  0 - Hand strength (0 for high card, 1 for pair, 2 for two pair, etc.)
         *  1-5 - Tiebreakers. Hands with fewer than 5 tiebreakers will use 0 as a placeholder.
         */
        public int[] HandValue { get; set; }

        private const int STRAIGHT_FLUSH = 8;
        private const int QUADS = 7;
        private const int FULL_HOUSE = 6;
        private const int FLUSH = 5;
        private const int STRAIGHT = 4;
        private const int TRIPS = 3;
        private const int TWO_PAIR = 2;
        private const int ONE_PAIR = 1;
        private const int HIGH_CARD = 0;

        public static HandStrength GetHandStrength(List<Card> inCards)
        {
            HandStrength handStrength = new HandStrength();
            handStrength.HandValue = new int[6];

            // Straight Flush and Flush Check
            for (int i = 0; i < 3; i++)
            {
                if (inCards.Count(c => c.Suit == inCards[i].Suit) >= 5)
                {
                    inCards.RemoveAll(c => c.Suit != inCards[i].Suit);

                    if (IsStraight(STRAIGHT_FLUSH))
                    {
                        return handStrength;
                    }

                    handStrength.HandValue[0] = FLUSH;
                    for (int j = 1; j < 6; j++)
                    {
                        handStrength.HandValue[j] = inCards[inCards.Count - j].Strength;
                    }
                    return handStrength;
                }
            }

            // Check for straight
            if (IsStraight(STRAIGHT))
            {
                return handStrength;
            }
            // If a straight is found, sets the handStrength hand values and returns true
            bool IsStraight(int strength)
            {
                List<Card> noDupes = new List<Card>();
                foreach (Card card in inCards)
                {
                    if (noDupes.Count(c => c.Strength == card.Strength) == 0)
                    {
                        noDupes.Add(card);
                    }
                }
                for (int k = noDupes.Count - 5; k >= 0; k--)
                {
                    if (noDupes[k].Strength == noDupes[k + 1].Strength - 1 &&
                        noDupes[k].Strength == noDupes[k + 2].Strength - 2 &&
                        noDupes[k].Strength == noDupes[k + 3].Strength - 3 &&
                        noDupes[k].Strength == noDupes[k + 4].Strength - 4)
                    {
                        handStrength.HandValue[0] = strength;
                        handStrength.HandValue[1] = noDupes[k + 4].Strength;
                        for (int j = 2; j < 6; j++)
                        {
                            handStrength.HandValue[j] = 0;
                        }
                        return true;
                    }
                }

                if (noDupes.Count(c => c.Strength == 2) >= 1 &&
                    noDupes.Count(c => c.Strength == 3) >= 1 &&
                    noDupes.Count(c => c.Strength == 4) >= 1 &&
                    noDupes.Count(c => c.Strength == 5) >= 1 &&
                    noDupes.Count(c => c.Strength == 14) >= 1)
                {
                    handStrength.HandValue[0] = strength;
                    handStrength.HandValue[1] = 5;
                    for (int j = 2; j < 6; j++)
                    {
                        handStrength.HandValue[j] = 0;
                    }
                    return true;
                }
                return false;
            }

            // Check for and store quads, trips and pairs
            Card quadCard = new Card();
            List<Card> tripsCards = new List<Card>();
            List<Card> pairCards = new List<Card>();

            foreach (Card card in inCards)
            {
                if (inCards.Count(c => c.Strength == card.Strength) == 4)
                {
                    quadCard.Strength = card.Strength;
                }
                else if (inCards.Count(c => c.Strength == card.Strength) == 3)
                {
                    Card tripCard = new Card();
                    tripCard.Strength = card.Strength;
                    if (tripsCards.Count == 1 && tripsCards[0].Strength != tripCard.Strength || tripsCards.Count == 0)
                    {
                        tripsCards.Add(tripCard);
                    }
                }
                else if (inCards.Count(c => c.Strength == card.Strength) == 2)
                {
                    Card pairCard = new Card();
                    pairCard.Strength = card.Strength;
                    if (pairCards.Count == 1 && pairCards[0].Strength != pairCard.Strength ||
                        pairCards.Count == 2 && pairCards[0].Strength != pairCard.Strength && pairCards[1].Strength != pairCard.Strength ||
                        pairCards.Count == 0)
                    {
                        pairCards.Add(pairCard);
                    }
                }
            }

            // Check for quads
            if (quadCard.Strength != 0)
            {
                inCards.RemoveAll(c => c.Strength == quadCard.Strength);
                handStrength.HandValue[0] = QUADS;
                handStrength.HandValue[1] = quadCard.Strength;
                handStrength.HandValue[2] = inCards[inCards.Count - 1].Strength;
                return handStrength;
            }

            // Check for full house
            if (tripsCards.Count == 1 && pairCards.Count >= 1 || tripsCards.Count == 2)
            {
                handStrength.HandValue[0] = FULL_HOUSE;
                handStrength.HandValue[1] = tripsCards[tripsCards.Count - 1].Strength;
                if (pairCards.Count >= 1)
                {
                    handStrength.HandValue[2] = pairCards[pairCards.Count - 1].Strength;
                }
                else
                {
                    handStrength.HandValue[2] = tripsCards[tripsCards.Count - 2].Strength;
                }
                return handStrength;
            }

            // Check for trips
            if (tripsCards.Count == 1)
            {
                handStrength.HandValue[0] = TRIPS;
                inCards.RemoveAll(c => c.Strength == tripsCards[0].Strength);
                handStrength.HandValue[1] = tripsCards[0].Strength;
                handStrength.HandValue[2] = inCards[inCards.Count - 1].Strength;
                handStrength.HandValue[3] = inCards[inCards.Count - 2].Strength;
                return handStrength;
            }

            // check for two pair
            if (pairCards.Count >= 2)
            {
                handStrength.HandValue[0] = TWO_PAIR;
                handStrength.HandValue[1] = pairCards[pairCards.Count - 1].Strength;
                handStrength.HandValue[2] = pairCards[pairCards.Count - 2].Strength;
                inCards.RemoveAll(c => c.Strength == pairCards[pairCards.Count - 1].Strength || c.Strength == pairCards[pairCards.Count - 2].Strength);
                handStrength.HandValue[3] = inCards[2].Strength;
                return handStrength;
            }

            // check for one pair
            if (pairCards.Count == 1)
            {
                handStrength.HandValue[0] = ONE_PAIR;
                handStrength.HandValue[1] = pairCards[0].Strength;
                inCards.RemoveAll(c => c.Strength == pairCards[0].Strength);
                for (int j = 2; j < 5; j++)
                {
                    handStrength.HandValue[j] = inCards[inCards.Count - j + 1].Strength;
                }
                return handStrength;
            }

            // set high card
            handStrength.HandValue[0] = HIGH_CARD;
            for (int i = 1; i < 6; i++)
            {
                handStrength.HandValue[i] = inCards[inCards.Count - i].Strength;
            }

            return handStrength;
        }
        
        public static string HandName(int[] handValues)
        {
            string handName = "";
            string highCard = CardName(handValues[1]);

            switch (handValues[0])
            {
                case 0:
                    handName = $"{highCard} high";
                    break;
                case 1:
                    if (handValues[1] != 6)
                    {
                        handName = $"Pair of{highCard}s";
                    }
                    else
                    {
                        handName = $"Pair of {highCard}es";
                    }
                    break;
                case 2:
                    handName = $"Two Pair\n({highCard} / {CardName(handValues[2])})";
                    break;
                case 3:
                    if (handValues[1] != 6)
                    {
                        handName = $"Three of a Kind\n({highCard}s)";
                    }
                    else
                    {
                        handName = $"Three of a Kind\n({highCard}es)";
                    }
                    break;
                case 4:
                    handName = $"Straight ({highCard} high)";
                    break;
                case 5:
                    handName = $"Flush ({highCard} high)";
                    break;
                case 6:
                    handName = $"Full House\n({highCard} full of {CardName(handValues[2])})";
                    break;
                case 7:
                    handName = $"Four of a Kind\n({highCard})";
                    break;
                case 8:
                    if (highCard == "Ace")
                    {
                        handName = "Royal Flush";
                    }
                    else
                    {
                        handName = $"Straigh Flush\n({highCard} high)";
                    }
                    break;
                default:
                    break;
            }

            return handName;
        }

        private static string CardName(int strength)
        {
            string cardName = "";
            switch (strength)
            {
                case 2:
                    cardName = "Two";
                    break;
                case 3:
                    cardName = "Three";
                    break;
                case 4:
                    cardName = "Four";
                    break;
                case 5:
                    cardName = "Five";
                    break;
                case 6:
                    cardName = "Six";
                    break;
                case 7:
                    cardName = "Seven";
                    break;
                case 8:
                    cardName = "Eight";
                    break;
                case 9:
                    cardName = "Nine";
                    break;
                case 10:
                    cardName = "Ten";
                    break;
                case 11:
                    cardName = "Jack";
                    break;
                case 12:
                    cardName = "Queen";
                    break;
                case 13:
                    cardName = "King";
                    break;
                case 14:
                    cardName = "Ace";
                    break;
                default:
                    break;
            }
            return cardName;
        }

    }
}
