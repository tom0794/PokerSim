
namespace PokerSim
{
    partial class PokerSimForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlPlayers = new System.Windows.Forms.Panel();
            this.pnlCommunity = new System.Windows.Forms.Panel();
            this.btnClearCommCards = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pnlPlayers
            // 
            this.pnlPlayers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlPlayers.Location = new System.Drawing.Point(8, 8);
            this.pnlPlayers.Name = "pnlPlayers";
            this.pnlPlayers.Size = new System.Drawing.Size(1016, 285);
            this.pnlPlayers.TabIndex = 0;
            // 
            // pnlCommunity
            // 
            this.pnlCommunity.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCommunity.Location = new System.Drawing.Point(8, 299);
            this.pnlCommunity.Name = "pnlCommunity";
            this.pnlCommunity.Size = new System.Drawing.Size(786, 245);
            this.pnlCommunity.TabIndex = 1;
            // 
            // btnClearCommCards
            // 
            this.btnClearCommCards.Location = new System.Drawing.Point(800, 521);
            this.btnClearCommCards.Name = "btnClearCommCards";
            this.btnClearCommCards.Size = new System.Drawing.Size(137, 23);
            this.btnClearCommCards.TabIndex = 2;
            this.btnClearCommCards.Text = "Clear Community Cards";
            this.btnClearCommCards.UseVisualStyleBackColor = true;
            this.btnClearCommCards.Click += new System.EventHandler(this.btnClearCommCards_Click);
            // 
            // PokerSimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1035, 556);
            this.Controls.Add(this.btnClearCommCards);
            this.Controls.Add(this.pnlCommunity);
            this.Controls.Add(this.pnlPlayers);
            this.Name = "PokerSimForm";
            this.Text = "Poker Sim";
            this.Load += new System.EventHandler(this.PokerSimForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPlayers;
        private System.Windows.Forms.Panel pnlCommunity;
        private System.Windows.Forms.Button btnClearCommCards;
    }
}

