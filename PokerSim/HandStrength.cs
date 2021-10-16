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

        public static HandStrength GetHandStrength(List<Card> inCards)
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
            if (numOfSuit == 5)
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
            return false;
        }

    }
}
