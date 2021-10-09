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
        public PictureBox picCard1 { get; set; }
        public PictureBox picCard2 { get; set; }

        public Button btnRemovePlayer { get; set; }

        public Button btnRandomCards { get; set; }

        public Button btnAddPlayer { get; set; }

        public bool Active { get; set; }

        public Label lblProbability { get; set; }

        public Label lblHandName { get; set; }

        public PokerSimForm Form { get; set; }

        public Player(string player, int left, int top, PokerSimForm form)
        {
            Active = false;

            this.Form = form;
            this.Left = left;
            this.Top = top;
            this.Width = 190;
            this.Height = 122;

            picCard1 = new PictureBox();
            picCard2 = new PictureBox();
            picCard1.Image = PokerSim.Properties.Resources.red_back;
            picCard1.SizeMode = PictureBoxSizeMode.StretchImage;
            picCard1.Visible = false;
            picCard1.Height = 105;
            picCard1.Width = 69;
            picCard1.Top = 50;
            picCard1.Left = 6;
            //picCard1.Click += this.Form.pictureBox1_Click;

            picCard2.Image = PokerSim.Properties.Resources.red_back;
            picCard2.SizeMode = PictureBoxSizeMode.StretchImage;
            picCard2.Visible = false;
            picCard2.Height = 105;
            picCard2.Width = 69;
            picCard2.Top = 50;
            picCard2.Left = 81;
            //picCard2.Click += this.Form.pictureBox1_Click;

            lblHandName = new Label();
            lblHandName.Visible = false;
            lblHandName.Top = 25;
            lblHandName.Left = 6;
            lblHandName.Text = "Hand";

            lblProbability = new Label();
            lblProbability.Visible = false;
            lblProbability.Top = 17;
            lblProbability.Left = 85;
            lblProbability.Text = "100%";
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

            this.Controls.Add(picCard1);
            this.Controls.Add(picCard2);
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
            picCard1.Visible = false;
            picCard2.Visible = false;
            lblProbability.Visible = false;
            lblHandName.Visible = false;
            this.Active = false;
        }

        private void BtnAddPlayer_Click(object sender, EventArgs e)
        {
            btnAddPlayer.Visible = false;
            btnRandomCards.Visible = true;
            btnRemovePlayer.Visible = true;
            picCard1.Visible = true;
            picCard2.Visible = true;
            lblProbability.Visible = true;
            lblHandName.Visible = true;
            this.Active = true;
        }
    }
}
