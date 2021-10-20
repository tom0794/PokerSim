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
        private const int NUM_DEAD_CARDS = 10;

        private CardMenu cardMenu = new CardMenu();
        private Card selectedCard = new Card();

        public List<Card> deck = new List<Card>();
        public List<Player> activePlayers = new List<Player>();
        public List<Card> communityCards = new List<Card>();

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
            CreateCommunityCards();
            CreateDeadCards();
        }

        /// <summary>
        /// Creates the five community cards
        /// </summary>
        private void CreateCommunityCards()
        {
            int cardTop = 15;
            int cardLeft = 15;
            int cardSpacing = 15;
            string commCardName = "communityCard";
            for (int i = 0; i < 5; i++)
            {
                Card newCommCard = new Card();
                newCommCard.Image = cardBack;
                newCommCard.SizeMode = PictureBoxSizeMode.StretchImage;
                newCommCard.Width = CARD_WIDTH * 2;
                newCommCard.Height = CARD_HEIGHT * 2;
                newCommCard.Top = cardTop;
                newCommCard.Left = cardLeft + (i * cardSpacing);
                newCommCard.Name = commCardName + (i + 1).ToString();
                newCommCard.Click += PlayerCard1_Click;
                pnlCommunity.Controls.Add(newCommCard);
                cardLeft += newCommCard.Width;
            }
        }

        /// <summary>
        /// Clears the community cards, returning any set cards to the deck
        /// </summary>
        private void ClearCommunityCards()
        {
            foreach (Control c in pnlCommunity.Controls)
            {
                if (c is Card)
                {
                    Card existing = (Card)c;
                    if (existing.Image != cardBack)
                    {
                        Card addBack = new Card();
                        UpdateCard(ref addBack, (Card)c);
                        deck.Add(addBack);
                    }
                }
            }
            pnlCommunity.Controls.Clear();
            communityCards.Clear();
            CreateCommunityCards();
        }

        private void CreateDeadCards()
        {
            int cardTop = 15;
            int cardLeft = 10;
            int cardSpacing = 5;
            string deadCardName = "deadCard";
            for (int i = 0; i < NUM_DEAD_CARDS; i++)
            {
                Card newDeadCard = new Card();
                newDeadCard.Image = cardBack;
                newDeadCard.SizeMode = PictureBoxSizeMode.StretchImage;
                newDeadCard.Width = CARD_WIDTH;
                newDeadCard.Height = CARD_HEIGHT;
                newDeadCard.Top = cardTop;
                newDeadCard.Left = cardLeft + (i * cardSpacing);
                newDeadCard.Name = deadCardName + (i + 1).ToString();
                newDeadCard.Click += PlayerCard1_Click;
                grpDeadCards.Controls.Add(newDeadCard);
                cardLeft += newDeadCard.Width;
            }
        }

        private void ClearDeadCards()
        {
            foreach (Control c in grpDeadCards.Controls)
            {
                if (c is Card)
                {
                    Card existing = (Card)c;
                    if (existing.Image != cardBack)
                    {
                        Card addBack = new Card();
                        UpdateCard(ref addBack, (Card)c);
                        deck.Add(addBack);
                    }
                }
            }
            grpDeadCards.Controls.Clear();
            CreateDeadCards();
        }

        /// <summary>
        /// Creates a CardMenu object.
        /// </summary>
        private void CreateCardMenu()
        {
            // generate card menu using deck
            cardMenu = new CardMenu();
            this.Controls.Add(cardMenu);
            cardMenu.BringToFront();
            cardMenu.Visible = false;
            int cardRow = 0;
            deck = deck.OrderBy(c => c.CardId).ToList();
            int currentSuit = deck[0].Suit;
            int leftSpacing = 0;

            foreach (Card c in deck)
            {
                c.Click -= MenuCard_Click;
                cardMenu.MenuCards.Add(c);
            }
            for (int i = 0; i < deck.Count; i++)
            {
                if (cardMenu.MenuCards[i].Suit != currentSuit)
                {
                    currentSuit++;
                    cardRow++;
                    leftSpacing = 0;
                }
                cardMenu.MenuCards[i].SizeMode = PictureBoxSizeMode.StretchImage;
                cardMenu.MenuCards[i].Height = CARD_HEIGHT;
                cardMenu.MenuCards[i].Width = CARD_WIDTH;

                cardMenu.MenuCards[i].Click += MenuCard_Click;

                cardMenu.MenuCards[i].Top = 10 + (108 * cardRow);
                cardMenu.MenuCards[i].Left = (70 * leftSpacing) + 10;
                cardMenu.Controls.Add(cardMenu.MenuCards[i]);
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
            newCard.LightVersion = new LightCard();
            newCard.LightVersion.Suit = sourceCard.Suit;
            newCard.LightVersion.Strength = sourceCard.Strength;
            newCard.Type = sourceCard.Type;
            newCard.LongName = sourceCard.LongName;
            newCard.ShortName = sourceCard.ShortName;
            newCard.ImageName = sourceCard.ImageName;
        }

        private void SetHandNames()
        {
            communityCards.Clear();
            foreach (Control ctr in pnlCommunity.Controls)
            {
                if (ctr is Card)
                {
                    Card commCard = (Card)ctr;
                    if (commCard.Image != cardBack)
                    {
                        communityCards.Add(commCard);
                    }
                }
            }
            List<LightCard> lightCommCards = new List<LightCard>(5);
            foreach (Card c in communityCards)
            {
                lightCommCards.Add(c.LightVersion);
            }
            foreach (Player p in activePlayers)
            {
                List<LightCard> playerCards = new List<LightCard>();
                playerCards.Add(p.PlayerCard1.LightVersion);
                playerCards.Add(p.PlayerCard2.LightVersion);
                foreach (LightCard lc in lightCommCards)
                {
                    playerCards.Add(lc);
                }
                if (playerCards.Count >= 5)
                {
                    HandStrength handStrength = HandStrength.GetHandStrength(playerCards.OrderBy(c => c.Strength).ToList());
                    p.lblHandName.Text = HandStrength.HandName(handStrength.HandValue);
                }
            }
        }

        private bool IncrementIndices(ref int[] indices, int listSize)
        {
            // return false when either an invalid sequence is given or when it can no longer be incremented
            if (indices[0] == listSize - indices.Length)
            {
                return false;
            }
            else
            {
                int finalPosition = listSize - 1;
                for (int i = indices.Length - 1; i >= 0; i--)
                {
                    if (indices[i] < finalPosition)
                    {
                        indices[i]++;
                        // reset everything after and reset final position
                        if (i != indices.Length - 1)
                        {
                            for (int j = i + 1; j < indices.Length; j++)
                            {
                                indices[j] = indices[j - 1] + 1;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        finalPosition--;
                    }
                }
                return false;
            }
        }

        #region Card Click Events

        private void MenuCard_Click(object sender, EventArgs e)
        {
            Card clickedCard = (Card)sender;
            UpdateCard(ref selectedCard, clickedCard);

            // remove the card from the deck
            deck.Remove(clickedCard);
            cardMenu.MenuCards.Clear();
            cardMenu.HideMenu();
            this.Controls.Remove(cardMenu);
        }

        /// <summary>
        /// When any card is clicked on, show the card menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PlayerCard1_Click(object sender, EventArgs e)
        {
            if (!cardMenu.ActivityState)
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
                cardMenu.ShowMenu();
            }
        }

        #endregion

        #region Buttons

        private void btnClearCommCards_Click(object sender, EventArgs e)
        {
            ClearCommunityCards();
        }

        private void btnClearDead_Click(object sender, EventArgs e)
        {
            ClearDeadCards();
        }

        private void btnTestStrength_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            communityCards.Clear();
            foreach (Control ctr in pnlCommunity.Controls)
            {
                if (ctr is Card)
                {
                    Card commCard = (Card)ctr;
                    if (commCard.Image != cardBack)
                    {
                        communityCards.Add(commCard);
                    }
                }
            }
            if (communityCards.Count >= 3 && activePlayers.Count >= 1)
            {
                communityCards.Add(activePlayers[0].PlayerCard1);
                communityCards.Add(activePlayers[0].PlayerCard2);
                List<LightCard> lightCards = new List<LightCard>();
                foreach (Card c in communityCards)
                {
                    lightCards.Add(new LightCard(c.Suit, c.Strength));
                }
                HandStrength handStrength = HandStrength.GetHandStrength(lightCards.OrderBy(c => c.Strength).ToList());
                foreach (int i in handStrength.HandValue)
                {
                    txtOutput.Text += i + "\n";
                }
                activePlayers[0].lblHandName.Text = HandStrength.HandName(handStrength.HandValue);
            }
        }

        private void btnDealRandom_Click(object sender, EventArgs e)
        {
            if (btnDealRandom.Text.Contains("Flop"))
            {
                Random r = new Random();
                for (int i = 0; i < 3; i++)
                {
                    int index = r.Next(0, deck.Count - 1);

                    if (pnlCommunity.Controls[i] is Card)
                    {
                        Card commCard = (Card)pnlCommunity.Controls[i];
                        if (commCard.Image == cardBack)
                        {
                            commCard.Image = deck[index].Image;
                            commCard.CardId = deck[index].CardId;
                            commCard.Suit = deck[index].Suit;
                            commCard.Strength = deck[index].Strength;
                            commCard.LightVersion = new LightCard();
                            commCard.LightVersion.Suit = deck[index].Suit;
                            commCard.LightVersion.Strength = deck[index].Strength;
                            commCard.Type = deck[index].Type;
                            commCard.LongName = deck[index].LongName;
                            commCard.ShortName = deck[index].ShortName;
                            commCard.ImageName = deck[index].ImageName;
                            communityCards.Add(commCard);
                            deck.Remove(deck[index]);
                        }
                    }
                }
                btnDealRandom.Text = "Deal Random Turn";
                return;
            }

            if (btnDealRandom.Text.Contains("Turn"))
            {
                Random r = new Random();
                int index = r.Next(0, deck.Count - 1);

                if (pnlCommunity.Controls[3] is Card)
                {
                    Card commCard = (Card)pnlCommunity.Controls[3];
                    if (commCard.Image == cardBack)
                    {
                        commCard.Image = deck[index].Image;
                        commCard.CardId = deck[index].CardId;
                        commCard.Suit = deck[index].Suit;
                        commCard.Strength = deck[index].Strength;
                        commCard.LightVersion = new LightCard();
                        commCard.LightVersion.Suit = deck[index].Suit;
                        commCard.LightVersion.Strength = deck[index].Strength;
                        commCard.Type = deck[index].Type;
                        commCard.LongName = deck[index].LongName;
                        commCard.ShortName = deck[index].ShortName;
                        commCard.ImageName = deck[index].ImageName;
                        communityCards.Add(commCard);
                        deck.Remove(deck[index]);
                    }
                }
                btnDealRandom.Text = "Deal Random River";
                return;
            }

            if (btnDealRandom.Text.Contains("River"))
            {
                Random r = new Random();
                int index = r.Next(0, deck.Count - 1);

                if (pnlCommunity.Controls[4] is Card)
                {
                    Card commCard = (Card)pnlCommunity.Controls[4];
                    if (commCard.Image == cardBack)
                    {
                        commCard.Image = deck[index].Image;
                        commCard.CardId = deck[index].CardId;
                        commCard.Suit = deck[index].Suit;
                        commCard.Strength = deck[index].Strength;
                        commCard.LightVersion = new LightCard();
                        commCard.LightVersion.Suit = deck[index].Suit;
                        commCard.LightVersion.Strength = deck[index].Strength;
                        commCard.Type = deck[index].Type;
                        commCard.LongName = deck[index].LongName;
                        commCard.ShortName = deck[index].ShortName;
                        commCard.ImageName = deck[index].ImageName;
                        communityCards.Add(commCard);
                        deck.Remove(deck[index]);
                    }
                }
                btnDealRandom.Text = "Deal Random Flop";
            }
        }

        #endregion

        private void btnGetOdds_Click(object sender, EventArgs e)
        {
            communityCards.Clear();
            foreach (Control ctr in pnlCommunity.Controls)
            {
                if (ctr is Card)
                {
                    Card commCard = (Card)ctr;
                    if (commCard.Image != cardBack)
                    {
                        communityCards.Add(commCard);
                    }
                }
            }
            List<LightCard> lightCommCards = new List<LightCard>(5);
            foreach (Card c in communityCards)
            {
                lightCommCards.Add(c.LightVersion);
            }
            if (lightCommCards.Count == 5)
            {
                SetHandNames();
                return;
            }

            List<LightCard> lightDeck = new List<LightCard>(48);
            foreach (Card c in deck)
            {
                lightDeck.Add(c.LightVersion);
            }

            int[] deckIndices = new int[5 - communityCards.Count()];
            for (int i = 0; i < deckIndices.Length; i++)
            {
                deckIndices[i] = i;
            }

            // set up player win counts
            int[] playerWins = new int[activePlayers.Count];
            //for (int i = 0; i < playerWins.Length; i++)
            //{
            //    playerWins[i] = 0;
            //}
            int totalRunouts = 0;

            //List<LightCard> playerHand = new List<LightCard>();
            //List<LightCard> currentHand = new List<LightCard>();

            do
            {
                int winningPlayerIndex = -1;
                int[] strongestHand = new int[6];
                strongestHand[0] = -1;
                for (int i = 0; i < activePlayers.Count; i++)
                {
                    List<LightCard> currentHand = new List<LightCard>(7);
                    //currentHand.Clear();
                    currentHand.Add(activePlayers[i].PlayerCard1.LightVersion);
                    currentHand.Add(activePlayers[i].PlayerCard2.LightVersion);
                    foreach (LightCard lc in lightCommCards)
                    {
                        currentHand.Add(lc);
                    }
                    for (int j = 0; j < deckIndices.Length; j++)
                    {
                        currentHand.Add(lightDeck[deckIndices[j]]);
                    }
                    int[] currentHandStrength = HandStrength.GetHandStrength(currentHand.OrderBy(c => c.Strength).ToList()).HandValue;
                    if (HandStrength.CompareHands(currentHandStrength, strongestHand) == 1 || strongestHand[0] == -1)
                    {
                        for (int k = 0; k < strongestHand.Length; k++)
                        {
                            int temp = currentHandStrength[k];
                            strongestHand[k] = temp;
                            //strongestHand[k] = currentHandStrength[k];
                        }
                        winningPlayerIndex = i;
                    }
                    else if (HandStrength.CompareHands(currentHandStrength, strongestHand) == 0)
                    {
                        winningPlayerIndex = -1;
                    }

                }
                if (winningPlayerIndex != -1)
                {
                    playerWins[winningPlayerIndex]++;
                }
                totalRunouts++;
            } while (IncrementIndices(ref deckIndices, deck.Count) && totalRunouts < 100000);
            txtOutput.Text = $"Total runouts: {totalRunouts} \n 1: {playerWins[0]} 2: {playerWins[1]}";
            for (int i = 0; i < activePlayers.Count; i++)
            {
                activePlayers[i].lblProbability.Text = ((double)playerWins[i] / totalRunouts).ToString("p");
            }
            SetHandNames();
        }

        private void btnTestCompare_Click(object sender, EventArgs e)
        {
            int[] hand1 = { 5, 5, 0, 0, 0, 0 };
            int[] hand2 = { 4, 6, 0, 0, 0, 0 };

            txtOutput.Text = HandStrength.CompareHands(hand1, hand2).ToString();

        }
    }
}
