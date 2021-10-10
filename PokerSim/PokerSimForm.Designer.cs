
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
            this.SuspendLayout();
            // 
            // pnlPlayers
            // 
            this.pnlPlayers.Location = new System.Drawing.Point(8, 8);
            this.pnlPlayers.Name = "pnlPlayers";
            this.pnlPlayers.Size = new System.Drawing.Size(1016, 321);
            this.pnlPlayers.TabIndex = 0;
            // 
            // PokerSimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 544);
            this.Controls.Add(this.pnlPlayers);
            this.Name = "PokerSimForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.PokerSimForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlPlayers;
    }
}

