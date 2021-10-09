using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerSim
{
    public partial class PokerSimForm : Form
    {
        private const int PLAYER_TOP = 12;
        private const int PLAYER_LEFT = 12;
        private const int MAX_PLAYERS = 10;
        public const int CARD_HEIGHT = 106;
        public const int CARD_WIDTH = 69;

        private CardMenu CardMenu = new CardMenu();
        private Card selectedCard = new Card();

        public Image cardBack = PokerSim.Properties.Resources.gray_back;

        public PokerSimForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Create the 10 Players.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PokerSimForm_Load(object sender, EventArgs e)
        {
            int playerLeft = PLAYER_LEFT;
            int playerTop = PLAYER_LEFT;
            int numPlayers = 0;
            for (int i = 0; i < MAX_PLAYERS; i++)
            {
                Player newPlayer = new Player((i + 1).ToString(), playerLeft, playerTop, this);
                pnlPlayers.Controls.Add(newPlayer);
                numPlayers++;
                if (numPlayers == MAX_PLAYERS / 2)
                {
                    playerTop = PLAYER_TOP + 140;
                    playerLeft = PLAYER_LEFT;
                }
                else
                {
                    playerLeft += 200;
                }
            }
            CreateCardMenu();
        }

        /// <summary>
        /// Creates a CardMenu object.
        /// </summary>
        private void CreateCardMenu()
        {
            // generate card menu using deck
            CardMenu = new CardMenu();
            this.Controls.Add(CardMenu);
            CardMenu.BringToFront();
            CardMenu.Visible = false;
            int cardRow = 0;

            for (int i = 0; i < 13; i++)
            {
                // don't do this here--create the deck, then use the deck to make the menu
                Card card = new Card();
                //card.Image = PokerSim.Properties.Resources.AC;
                card.Image = (Image)Properties.Resources.ResourceManager.GetObject("_10D");

                //card.Image = PokerSim.Properties.Resources._10D;
                card.SizeMode = PictureBoxSizeMode.StretchImage;
                card.Height = CARD_HEIGHT;
                card.Width = CARD_WIDTH;

                //card.Click += Card_Click;
                card.Click += MenuCard_Click;

                CardMenu.MenuCards.Add(card);
                card.Top = 10 + (108 * cardRow);
                card.Left = (70 * i) + 10;
                CardMenu.Controls.Add(card);
                if (i == 12 && cardRow < 3)
                {
                    cardRow++;
                    i = -1;
                }
            }
        }

        private void MenuCard_Click(object sender, EventArgs e)
        {
            Card clickedCard = (Card)sender;
            selectedCard.Image = clickedCard.Image;
            //selectedCard.Suit = clickedCard.Suit;
            // remove the card from the deck
            CardMenu.HideMenu();
        }

        /// <summary>
        /// When any card is clicked on, show the card menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PlayerCard1_Click(object sender, EventArgs e)
        {
            if (!CardMenu.ActivityState)
            {
                Card clickedCard = (Card)sender;
                selectedCard = clickedCard;
                CardMenu.ShowMenu();
            }
        }
    }
}
