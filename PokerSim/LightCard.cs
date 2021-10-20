using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSim
{
    public class LightCard
    {
        public int Suit { get; set; }
        public int Strength { get; set; }

        public LightCard(int suit, int strength)
        {
            Suit = suit;
            Strength = strength;
        }

        public LightCard()
        {

        }
    }
}
