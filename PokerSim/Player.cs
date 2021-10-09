using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerSim
{
    class Player : GroupBox
    {
        public Card PlayerCard1 { get; set; }
        public Card PlayerCard2 { get; set; }

        public Button btnRemovePlayer { get; set; }

        public Button btnRandomCards { get; set; }

        public Button btnAddPlayer { get; set; }

        public bool Active { get; set; }

        public Label lblProbability { get; set; }

        public Label lblHandName { get; set; }

        public PokerSimForm Form { get; set; }

        /// <summary>
        /// Create all Player object controls.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="form"></param>
        public Player(string player, int left, int top, PokerSimForm form)
        {
            Active = false;

            this.Form = form;
            this.Left = left;
            this.Top = top;
            this.Width = 190;
            this.Height = 122;

            PlayerCard1 = new Card();
            PlayerCard2 = new Card();
            PlayerCard1.Image = this.Form.cardBack;
            PlayerCard1.SizeMode = PictureBoxSizeMode.StretchImage;
            PlayerCard1.Visible = false;
            PlayerCard1.Height = 106;
            PlayerCard1.Width = 69;
            PlayerCard1.Top = 50;
            PlayerCard1.Left = 6;
            PlayerCard1.Click += this.Form.PlayerCard1_Click;

            PlayerCard2.Image = this.Form.cardBack;
            PlayerCard2.SizeMode = PictureBoxSizeMode.StretchImage;
            PlayerCard2.Visible = false;
            PlayerCard2.Height = 106;
            PlayerCard2.Width = 69;
            PlayerCard2.Top = 50;
            PlayerCard2.Left = 81;
            PlayerCard2.Click += this.Form.PlayerCard1_Click;

            lblHandName = new Label();
            lblHandName.Visible = false;
            lblHandName.Top = 25;
            lblHandName.Left = 6;
            lblHandName.Text = "";

            lblProbability = new Label();
            lblProbability.Visible = false;
            lblProbability.Top = 17;
            lblProbability.Left = 85;
            lblProbability.Text = "";
            lblProbability.Font = new Font("Microsoft Sans Serif", 14);
            lblProbability.TextAlign = ContentAlignment.MiddleRight;

            btnAddPlayer = new Button();
            btnAddPlayer.Top = 30;
            btnAddPlayer.Left = 30;
            btnAddPlayer.Width = 100;
            btnAddPlayer.Height = 50;
            btnAddPlayer.Tag = player;
            btnAddPlayer.Text = "Add Player " + player;

            btnRemovePlayer = new Button();
            btnRemovePlayer.Left = 156;
            btnRemovePlayer.Top = 50;
            btnRemovePlayer.Height = 25;
            btnRemovePlayer.Width = 25;
            btnRemovePlayer.Text = "X";
            btnRemovePlayer.Visible = false;

            btnRemovePlayer.Click += BtnRemovePlayer_Click;

            btnRandomCards = new Button();
            btnRandomCards.Height = 25;
            btnRandomCards.Width = 25;
            btnRandomCards.Text = "?";
            btnRandomCards.Left = 156;
            btnRandomCards.Top = 91;
            btnRandomCards.Visible = false;

            btnAddPlayer.Click += BtnAddPlayer_Click;

            this.Controls.Add(PlayerCard1);
            this.Controls.Add(PlayerCard2);
            this.Controls.Add(btnAddPlayer);
            this.Controls.Add(btnRandomCards);
            this.Controls.Add(btnRemovePlayer);
            this.Controls.Add(lblHandName);
            this.Controls.Add(lblProbability);
            this.Text = "Player " + player;
        }

        private void BtnRemovePlayer_Click(object sender, EventArgs e)
        {
            btnAddPlayer.Visible = true;
            btnRandomCards.Visible = false;
            btnRemovePlayer.Visible = false;
            lblProbability.Visible = false;
            lblHandName.Visible = false;
            this.Active = false;

            // Remove and recreate cards to reset them
            this.Controls.Remove(PlayerCard1);
            this.Controls.Remove(PlayerCard2);

            PlayerCard1 = new Card();
            PlayerCard2 = new Card();
            PlayerCard1.Image = this.Form.cardBack;
            PlayerCard1.SizeMode = PictureBoxSizeMode.StretchImage;
            PlayerCard1.Visible = false;
            PlayerCard1.Height = 106;
            PlayerCard1.Width = 69;
            PlayerCard1.Top = 50;
            PlayerCard1.Left = 6;
            PlayerCard1.Click += this.Form.PlayerCard1_Click;

            PlayerCard2.Image = this.Form.cardBack;
            PlayerCard2.SizeMode = PictureBoxSizeMode.StretchImage;
            PlayerCard2.Visible = false;
            PlayerCard2.Height = 106;
            PlayerCard2.Width = 69;
            PlayerCard2.Top = 50;
            PlayerCard2.Left = 81;
            PlayerCard2.Click += this.Form.PlayerCard1_Click;
            this.Controls.Add(PlayerCard1);
            this.Controls.Add(PlayerCard2);
        }

        private void BtnAddPlayer_Click(object sender, EventArgs e)
        {
            btnAddPlayer.Visible = false;
            btnRandomCards.Visible = true;
            btnRemovePlayer.Visible = true;
            PlayerCard1.Visible = true;
            PlayerCard2.Visible = true;
            lblProbability.Visible = true;
            lblHandName.Visible = true;
            this.Active = true;
        }
    }
}
