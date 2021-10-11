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

        public List<Card> deck = new List<Card>();

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
            deck = Card.CreateDeck();
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
            deck = deck.OrderBy(c => c.CardId).ToList();
            int currentSuit = deck[0].Suit;
            int leftSpacing = 0;

            foreach (Card c in deck)
            {
                c.Click -= MenuCard_Click;
                CardMenu.MenuCards.Add(c);
            }
            for (int i = 0; i < deck.Count; i++)
            {
                if (CardMenu.MenuCards[i].Suit != currentSuit)
                {
                    currentSuit++;
                    cardRow++;
                    leftSpacing = 0;
                }
                CardMenu.MenuCards[i].SizeMode = PictureBoxSizeMode.StretchImage;
                CardMenu.MenuCards[i].Height = CARD_HEIGHT;
                CardMenu.MenuCards[i].Width = CARD_WIDTH;

                CardMenu.MenuCards[i].Click += MenuCard_Click;

                CardMenu.MenuCards[i].Top = 10 + (108 * cardRow);
                CardMenu.MenuCards[i].Left = (70 * leftSpacing) + 10;
                CardMenu.Controls.Add(CardMenu.MenuCards[i]);
                leftSpacing++;
            }
        }

        /// <summary>
        /// Updates the properties of a card to match a desired card.
        /// </summary>
        /// <param name="newCard">The card being updated.</param>
        /// <param name="sourceCard">The card whose properties are being copied.</param>
        public void UpdateCard(ref Card newCard, Card sourceCard)
        {
            newCard.Image = sourceCard.Image;
            newCard.CardId = sourceCard.CardId;
            newCard.Suit = sourceCard.Suit;
            newCard.Strength = sourceCard.Strength;
            newCard.Type = sourceCard.Type;
            newCard.LongName = sourceCard.LongName;
            newCard.ShortName = sourceCard.ShortName;
            newCard.ImageName = sourceCard.ImageName;
        }

        private void MenuCard_Click(object sender, EventArgs e)
        {
            Card clickedCard = (Card)sender;
            UpdateCard(ref selectedCard, clickedCard);

            // remove the card from the deck
            deck.Remove(clickedCard);
            CardMenu.MenuCards.Clear();
            CardMenu.HideMenu();
            this.Controls.Remove(CardMenu);
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
                // if the card is not empty, add it back to the deck
                if (clickedCard.Image != cardBack)
                {
                    Card addBack = new Card();
                    UpdateCard(ref addBack, clickedCard);
                    deck.Add(addBack);
                }
                CreateCardMenu();
                CardMenu.ShowMenu();
            }
        }
    }
}
