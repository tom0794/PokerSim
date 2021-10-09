using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerSim
{
    class CardMenu : Panel
    {
        public bool ActivityState { get; set; }

        public List<Card> MenuCards { get; set; }


        public CardMenu()
        {
            this.Top = 50;
            this.Left = 50;
            this.Width = 930;
            this.Height = 450;
            this.BackColor = System.Drawing.Color.LawnGreen;
            this.MenuCards = new List<Card>();
            ActivityState = false;
        }

        public void ShowMenu()
        {
            this.Visible = true;
            this.ActivityState = true;
        }

        public void HideMenu()
        {
            this.ActivityState = false;
            this.Visible = false;
        }
    }
}
