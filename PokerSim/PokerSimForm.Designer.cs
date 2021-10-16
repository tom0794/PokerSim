
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
            this.grpDeadCards = new System.Windows.Forms.GroupBox();
            this.btnClearDead = new System.Windows.Forms.Button();
            this.btnTestStrength = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
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
            // grpDeadCards
            // 
            this.grpDeadCards.Location = new System.Drawing.Point(8, 551);
            this.grpDeadCards.Name = "grpDeadCards";
            this.grpDeadCards.Size = new System.Drawing.Size(785, 68);
            this.grpDeadCards.TabIndex = 3;
            this.grpDeadCards.TabStop = false;
            this.grpDeadCards.Text = "Dead Cards";
            // 
            // btnClearDead
            // 
            this.btnClearDead.Location = new System.Drawing.Point(801, 598);
            this.btnClearDead.Name = "btnClearDead";
            this.btnClearDead.Size = new System.Drawing.Size(136, 21);
            this.btnClearDead.TabIndex = 4;
            this.btnClearDead.Text = "Clear Dead Cards";
            this.btnClearDead.UseVisualStyleBackColor = true;
            this.btnClearDead.Click += new System.EventHandler(this.btnClearDead_Click);
            // 
            // btnTestStrength
            // 
            this.btnTestStrength.Location = new System.Drawing.Point(801, 299);
            this.btnTestStrength.Name = "btnTestStrength";
            this.btnTestStrength.Size = new System.Drawing.Size(96, 39);
            this.btnTestStrength.TabIndex = 5;
            this.btnTestStrength.Text = "test str";
            this.btnTestStrength.UseVisualStyleBackColor = true;
            this.btnTestStrength.Click += new System.EventHandler(this.btnTestStrength_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(809, 369);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(174, 85);
            this.txtOutput.TabIndex = 6;
            this.txtOutput.Text = "";
            // 
            // PokerSimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1035, 625);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnTestStrength);
            this.Controls.Add(this.btnClearDead);
            this.Controls.Add(this.grpDeadCards);
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
        private System.Windows.Forms.GroupBox grpDeadCards;
        private System.Windows.Forms.Button btnClearDead;
        private System.Windows.Forms.Button btnTestStrength;
        private System.Windows.Forms.RichTextBox txtOutput;
    }
}

