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
        public string HandName { get; set; }

        private const int STRAIGHT_FLUSH = 8;
        private const int QUADS = 7;
        private const int FULL_HOUSE = 6;
        private const int FLUSH = 5;
        private const int STRAIGHT = 4;
        private const int TRIPS = 3;
        private const int TWO_PAIR = 2;
        private const int ONE_PAIR = 1;
        private const int HIGH_CARD = 0;

        public static HandStrength GetHandStrengthSeven(List<Card> inCards)
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
                for (int k = 0; k < inCards.Count - 4; k++)
                {
                    if (inCards[k].Strength == inCards[k + 1].Strength - 1 &&
                        inCards[k].Strength == inCards[k + 2].Strength - 2 &&
                        inCards[k].Strength == inCards[k + 3].Strength - 3 &&
                        inCards[k].Strength == inCards[k + 4].Strength - 4)
                    {
                        handStrength.HandValue[0] = strength;
                        handStrength.HandValue[1] = inCards[k + 4].Strength;
                        for (int j = 2; j < 6; j++)
                        {
                            handStrength.HandValue[j] = 0;
                        }
                        return true;
                    }
                }

                if (inCards.Count(c => c.Strength == 2) >= 1 &&
                    inCards.Count(c => c.Strength == 3) >= 1 &&
                    inCards.Count(c => c.Strength == 4) >= 1 &&
                    inCards.Count(c => c.Strength == 5) >= 1 &&
                    inCards.Count(c => c.Strength == 14) >= 1)
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
                handStrength.HandValue[2] = inCards[2].Strength;
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
                handStrength.HandValue[2] = inCards[3].Strength;
                handStrength.HandValue[3] = inCards[2].Strength;
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
        public static HandStrength GetHandStrengthFive(List<Card> inCards)
        {
            HandStrength handStrength = new HandStrength();
            handStrength.HandValue = new int[6];

            bool isFlush = IsFlush(inCards);
            bool isStraight = IsStraight(inCards);

            // Check for straight flush
            if (isFlush && isStraight)
            {
                handStrength.HandValue[0] = STRAIGHT_FLUSH;
                // if it's a five high straight flush the five is 4th and ace is 5th
                if (inCards[3].Strength == 5 && inCards[4].Strength == 14)
                {
                    handStrength.HandValue[1] = inCards[3].Strength;
                }
                else
                {
                    handStrength.HandValue[1] = inCards[4].Strength;
                }
                for (int i = 2; i < 6; i++)
                {
                    handStrength.HandValue[i] = 0;
                }
                return handStrength;
            }

            // Set value if flush
            if (isFlush)
            {
                handStrength.HandValue[0] = FLUSH;
                for (int i = 0; i < inCards.Count; i++)
                {
                    handStrength.HandValue[inCards.Count - i] = inCards[i].Strength;
                }
                return handStrength;
            }

            // Set value if straight
            if (isStraight)
            {
                handStrength.HandValue[0] = STRAIGHT;
                if (inCards[4].Strength == 14 && inCards[3].Strength == 5)
                {
                    handStrength.HandValue[1] = 5;
                }
                else
                {
                    handStrength.HandValue[1] = inCards[4].Strength;
                }
                for (int i = 2; i < 6; i++)
                {
                    handStrength.HandValue[i] = 0;
                }
                return handStrength;
            }

            // Check for quads
            foreach (Card card in inCards)
            {
                if (inCards.Count(c => c.Strength == card.Strength) == 4)
                {
                    handStrength.HandValue[0] = QUADS;
                    handStrength.HandValue[1] = card.Strength;
                    if (inCards[0].Strength == inCards[1].Strength)
                    {
                        handStrength.HandValue[2] = inCards[4].Strength;
                    }
                    else
                    {
                        handStrength.HandValue[2] = inCards[0].Strength;
                    }
                    for (int i = 3; i < 6; i++)
                    {
                        handStrength.HandValue[i] = 0;
                    }
                }
            }

            // Check for Full House and Trips
            foreach (Card card in inCards)
            {
                if (inCards.Count(c => c.Strength == card.Strength) == 3)
                {
                    if (inCards[0].Strength == inCards[1].Strength && inCards[0].Strength != card.Strength ||
                        inCards[3].Strength == inCards[4].Strength && inCards[3].Strength != card.Strength)
                    {
                        handStrength.HandValue[0] = FULL_HOUSE;
                        handStrength.HandValue[1] = card.Strength;
                        if (inCards[1].Strength < inCards[2].Strength)
                        {
                            handStrength.HandValue[2] = inCards[1].Strength;
                        }
                        else
                        {
                            handStrength.HandValue[2] = inCards[3].Strength;
                        }
                        for (int i = 3; i < 6; i++)
                        {
                            handStrength.HandValue[i] = 0;
                        }
                    }
                    else
                    {
                        handStrength.HandValue[0] = TRIPS;
                        handStrength.HandValue[1] = card.Strength;
                        inCards.RemoveAll(c => c.Strength == card.Strength);
                        handStrength.HandValue[2] = inCards[1].Strength;
                        handStrength.HandValue[3] = inCards[0].Strength;
                        for (int i = 4; i < 6; i++)
                        {
                            handStrength.HandValue[i] = 0;
                        }
                    }
                    return handStrength;
                }
            }

            // Check for two pair
            if (inCards[0].Strength == inCards[1].Strength)
            {
                if (inCards[2].Strength == inCards[3].Strength)
                {
                    handStrength.HandValue[0] = TWO_PAIR;
                    handStrength.HandValue[1] = inCards[2].Strength;
                    handStrength.HandValue[2] = inCards[0].Strength;
                    handStrength.HandValue[3] = inCards[4].Strength;
                }
                else if (inCards[3].Strength == inCards[4].Strength)
                {
                    handStrength.HandValue[0] = TWO_PAIR;
                    handStrength.HandValue[1] = inCards[3].Strength;
                    handStrength.HandValue[2] = inCards[0].Strength;
                    handStrength.HandValue[3] = inCards[2].Strength;
                }
                for (int i = 4; i < 6; i++)
                {
                    handStrength.HandValue[i] = 0;
                }
            }
            if (inCards[1].Strength == inCards[2].Strength &&
                inCards[3].Strength == inCards[4].Strength)
            {
                handStrength.HandValue[0] = TWO_PAIR;
                handStrength.HandValue[1] = inCards[3].Strength;
                handStrength.HandValue[2] = inCards[1].Strength;
                handStrength.HandValue[3] = inCards[0].Strength;
                for (int i = 4; i < 6; i++)
                {
                    handStrength.HandValue[i] = 0;
                }
            }

            // Check for one pair
            foreach (Card card in inCards)
            {
                if (inCards.Count(c => c.Strength == card.Strength) == 2)
                {
                    handStrength.HandValue[0] = ONE_PAIR;
                    handStrength.HandValue[1] = card.Strength;
                    inCards.RemoveAll(c => c.Strength == card.Strength);
                    handStrength.HandValue[2] = inCards[2].Strength;
                    handStrength.HandValue[3] = inCards[1].Strength;
                    handStrength.HandValue[4] = inCards[0].Strength;
                    handStrength.HandValue[5] = 0;
                    return handStrength;
                }
            }

            // if no other hands were found, set the high card values
            handStrength.HandValue[0] = HIGH_CARD;
            for (int i = 0; i < inCards.Count; i++)
            {
                handStrength.HandValue[inCards.Count - i] = inCards[i].Strength;
            }

            return handStrength;
        }

        /// <summary>
        /// Consumes a list of five cards and returns true if they are all the same suit (flush).
        /// </summary>
        /// <param name="inCards">List of five cards</param>
        /// <returns>True if all cards are the same suit, otherwise false.</returns>
        private static bool IsFlush(List<Card> inCards)
        {
            // Check for flush
            int numOfSuit = 0;
            foreach (Card c in inCards)
            {
                if (c.Suit == inCards[0].Suit)
                {
                    numOfSuit++;
                }
            }
            if (numOfSuit >= 5)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Consumes a list of five cards (sorted weakest to strongest) and returns true if there is a straight.
        /// </summary>
        /// <param name="inCards"></param>
        /// <returns></returns>
        private static bool IsStraight(List<Card> inCards)
        { 

            if (inCards.Count == 5)
            {
                int firstCard = inCards[0].Strength;

                if (inCards[1].Strength == firstCard + 1 &&
                    inCards[2].Strength == firstCard + 2 &&
                    inCards[3].Strength == firstCard + 3)
                {
                    if (inCards[4].Strength == firstCard + 4 ||
                        inCards[4].Strength == firstCard + 12)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
