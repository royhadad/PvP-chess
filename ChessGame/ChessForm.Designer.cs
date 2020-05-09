namespace ChessGame
{
    partial class ChessForm
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
            this.components = new System.ComponentModel.Container();
            this.BoardPanel = new System.Windows.Forms.Panel();
            this.textBoxIsBlackTurn = new System.Windows.Forms.Label();
            this.textBoxIsWhiteTurn = new System.Windows.Forms.Label();
            this.buttonRestartGame = new System.Windows.Forms.Button();
            this.buttonStartGame = new System.Windows.Forms.Button();
            this.buttonAgreeToDraw = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelWhiteTimer = new System.Windows.Forms.Label();
            this.labelBlackTimer = new System.Windows.Forms.Label();
            this.buttonResignWhite = new System.Windows.Forms.Button();
            this.buttonResignBlack = new System.Windows.Forms.Button();
            this.textBoxIncrement = new System.Windows.Forms.TextBox();
            this.textBoxInitialTime = new System.Windows.Forms.TextBox();
            this.labelTimeControlInitial = new System.Windows.Forms.Label();
            this.labelTimeControlIncrement = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // BoardPanel
            // 
            this.BoardPanel.Location = new System.Drawing.Point(203, 20);
            this.BoardPanel.Name = "BoardPanel";
            this.BoardPanel.Size = new System.Drawing.Size(512, 512);
            this.BoardPanel.TabIndex = 0;
            // 
            // textBoxIsBlackTurn
            // 
            this.textBoxIsBlackTurn.Location = new System.Drawing.Point(0, 0);
            this.textBoxIsBlackTurn.Name = "textBoxIsBlackTurn";
            this.textBoxIsBlackTurn.Size = new System.Drawing.Size(100, 20);
            this.textBoxIsBlackTurn.TabIndex = 0;
            // 
            // textBoxIsWhiteTurn
            // 
            this.textBoxIsWhiteTurn.Location = new System.Drawing.Point(0, 20);
            this.textBoxIsWhiteTurn.Name = "textBoxIsWhiteTurn";
            this.textBoxIsWhiteTurn.Size = new System.Drawing.Size(100, 20);
            this.textBoxIsWhiteTurn.TabIndex = 1;
            // 
            // buttonRestartGame
            // 
            this.buttonRestartGame.Enabled = false;
            this.buttonRestartGame.Location = new System.Drawing.Point(64, 155);
            this.buttonRestartGame.Name = "buttonRestartGame";
            this.buttonRestartGame.Size = new System.Drawing.Size(105, 23);
            this.buttonRestartGame.TabIndex = 2;
            this.buttonRestartGame.Text = "Restart Game";
            this.buttonRestartGame.UseVisualStyleBackColor = true;
            this.buttonRestartGame.Click += new System.EventHandler(this.ButtonRestartGame_Click);
            // 
            // buttonStartGame
            // 
            this.buttonStartGame.Location = new System.Drawing.Point(64, 184);
            this.buttonStartGame.Name = "buttonStartGame";
            this.buttonStartGame.Size = new System.Drawing.Size(105, 23);
            this.buttonStartGame.TabIndex = 3;
            this.buttonStartGame.Text = "Start";
            this.buttonStartGame.UseVisualStyleBackColor = true;
            this.buttonStartGame.Click += new System.EventHandler(this.ButtonStartGame_Click);
            // 
            // buttonAgreeToDraw
            // 
            this.buttonAgreeToDraw.Location = new System.Drawing.Point(64, 213);
            this.buttonAgreeToDraw.Name = "buttonAgreeToDraw";
            this.buttonAgreeToDraw.Size = new System.Drawing.Size(105, 23);
            this.buttonAgreeToDraw.TabIndex = 4;
            this.buttonAgreeToDraw.Text = "Agree To Draw";
            this.buttonAgreeToDraw.UseVisualStyleBackColor = true;
            this.buttonAgreeToDraw.Click += new System.EventHandler(this.ButtonAgreeToDraw_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // labelWhiteTimer
            // 
            this.labelWhiteTimer.AutoSize = true;
            this.labelWhiteTimer.Location = new System.Drawing.Point(64, 270);
            this.labelWhiteTimer.Name = "labelWhiteTimer";
            this.labelWhiteTimer.Size = new System.Drawing.Size(0, 13);
            this.labelWhiteTimer.TabIndex = 5;
            // 
            // labelBlackTimer
            // 
            this.labelBlackTimer.AutoSize = true;
            this.labelBlackTimer.Location = new System.Drawing.Point(64, 318);
            this.labelBlackTimer.Name = "labelBlackTimer";
            this.labelBlackTimer.Size = new System.Drawing.Size(0, 13);
            this.labelBlackTimer.TabIndex = 6;
            // 
            // buttonResignWhite
            // 
            this.buttonResignWhite.Location = new System.Drawing.Point(64, 387);
            this.buttonResignWhite.Name = "buttonResignWhite";
            this.buttonResignWhite.Size = new System.Drawing.Size(105, 23);
            this.buttonResignWhite.TabIndex = 7;
            this.buttonResignWhite.Text = "White Resign";
            this.buttonResignWhite.UseVisualStyleBackColor = true;
            this.buttonResignWhite.Click += new System.EventHandler(this.ButtonResignWhite_Click);
            // 
            // buttonResignBlack
            // 
            this.buttonResignBlack.Location = new System.Drawing.Point(64, 417);
            this.buttonResignBlack.Name = "buttonResignBlack";
            this.buttonResignBlack.Size = new System.Drawing.Size(105, 23);
            this.buttonResignBlack.TabIndex = 8;
            this.buttonResignBlack.Text = "Black Resign";
            this.buttonResignBlack.UseVisualStyleBackColor = true;
            this.buttonResignBlack.Click += new System.EventHandler(this.ButtonResignBlack_Click);
            // 
            // textBoxIncrement
            // 
            this.textBoxIncrement.Location = new System.Drawing.Point(64, 86);
            this.textBoxIncrement.Name = "textBoxIncrement";
            this.textBoxIncrement.Size = new System.Drawing.Size(100, 20);
            this.textBoxIncrement.TabIndex = 9;
            // 
            // textBoxInitialTime
            // 
            this.textBoxInitialTime.Location = new System.Drawing.Point(64, 43);
            this.textBoxInitialTime.Name = "textBoxInitialTime";
            this.textBoxInitialTime.Size = new System.Drawing.Size(100, 20);
            this.textBoxInitialTime.TabIndex = 10;
            // 
            // labelTimeControlInitial
            // 
            this.labelTimeControlInitial.AutoSize = true;
            this.labelTimeControlInitial.Location = new System.Drawing.Point(64, 26);
            this.labelTimeControlInitial.Name = "labelTimeControlInitial";
            this.labelTimeControlInitial.Size = new System.Drawing.Size(60, 13);
            this.labelTimeControlInitial.TabIndex = 11;
            this.labelTimeControlInitial.Text = "Initial Time:";
            // 
            // labelTimeControlIncrement
            // 
            this.labelTimeControlIncrement.AutoSize = true;
            this.labelTimeControlIncrement.Location = new System.Drawing.Point(64, 70);
            this.labelTimeControlIncrement.Name = "labelTimeControlIncrement";
            this.labelTimeControlIncrement.Size = new System.Drawing.Size(54, 13);
            this.labelTimeControlIncrement.TabIndex = 12;
            this.labelTimeControlIncrement.Text = "Increment";
            // 
            // ChessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 563);
            this.Controls.Add(this.labelTimeControlIncrement);
            this.Controls.Add(this.labelTimeControlInitial);
            this.Controls.Add(this.textBoxInitialTime);
            this.Controls.Add(this.textBoxIncrement);
            this.Controls.Add(this.buttonResignBlack);
            this.Controls.Add(this.buttonResignWhite);
            this.Controls.Add(this.labelBlackTimer);
            this.Controls.Add(this.labelWhiteTimer);
            this.Controls.Add(this.buttonAgreeToDraw);
            this.Controls.Add(this.buttonStartGame);
            this.Controls.Add(this.buttonRestartGame);
            this.Controls.Add(this.textBoxIsWhiteTurn);
            this.Controls.Add(this.textBoxIsBlackTurn);
            this.Controls.Add(this.BoardPanel);
            this.Name = "ChessForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel BoardPanel;
        private System.Windows.Forms.Label textBoxIsBlackTurn;
        private System.Windows.Forms.Label textBoxIsWhiteTurn;
        private System.Windows.Forms.Button buttonRestartGame;
        private System.Windows.Forms.Button buttonStartGame;
        private System.Windows.Forms.Button buttonAgreeToDraw;
        public System.Windows.Forms.PictureBox[] whitePromotionPictureBox;
        public System.Windows.Forms.PictureBox[] blackPromotionPictureBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelWhiteTimer;
        private System.Windows.Forms.Label labelBlackTimer;
        private System.Windows.Forms.Button buttonResignWhite;
        private System.Windows.Forms.Button buttonResignBlack;
        private System.Windows.Forms.TextBox textBoxIncrement;
        private System.Windows.Forms.TextBox textBoxInitialTime;
        private System.Windows.Forms.Label labelTimeControlInitial;
        private System.Windows.Forms.Label labelTimeControlIncrement;
    }
}

