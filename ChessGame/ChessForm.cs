using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class ChessForm : Form
    {
        //default time control
        int timeControlInitial = 300;//in seconds
        int timeControlIncrement = 3;//in seconds
        private Game game;
        PictureBox[,] PictureSquares = new PictureBox[8, 8];
        private int whiteTime;
        private int blackTime;
        public ChessForm()
        {
            InitializeComponent();
            this.Location = new Point(0, 500);
            this.Size = new Size(1200, 800);
            this.initialBoardPanelSet();
            this.initialClockPanelSet();
            whiteTime = timeControlInitial;
            blackTime = timeControlInitial;
    }


        private void initialClockPanelSet()
        {
            this.textBoxIsBlackTurn.Size = new System.Drawing.Size(100, 20);
            this.textBoxIsBlackTurn.Location = new System.Drawing.Point(this.BoardPanel.Location.X + this.BoardPanel.Width + 10,
                                        this.BoardPanel.Location.Y + (this.BoardPanel.Height / 2) - this.textBoxIsBlackTurn.Height);
            this.textBoxIsBlackTurn.Name = "black turn text box";
            this.textBoxIsBlackTurn.Text = "Black's turn";
            this.textBoxIsBlackTurn.Visible = false;
            this.textBoxIsBlackTurn.BackColor = Color.AntiqueWhite;


            this.textBoxIsWhiteTurn.Size = new System.Drawing.Size(100, 20);
            this.textBoxIsWhiteTurn.Location = new System.Drawing.Point(this.BoardPanel.Location.X + this.BoardPanel.Width + 10,
                                        this.BoardPanel.Location.Y + (this.BoardPanel.Height / 2));
            this.textBoxIsWhiteTurn.Name = "black turn text box";
            this.textBoxIsWhiteTurn.Text = "White's turn";
            this.textBoxIsWhiteTurn.Visible = false;
            this.textBoxIsWhiteTurn.BackColor = Color.AntiqueWhite;

            //restart button
            this.buttonRestartGame.Location = new System.Drawing.Point(this.BoardPanel.Location.X,
                                        this.BoardPanel.Location.Y - this.buttonRestartGame.Height - 10);
            this.buttonRestartGame.Enabled = false;
            
            //start clock button
            this.buttonStartGame.Location = new System.Drawing.Point(this.buttonRestartGame.Location.X+this.buttonRestartGame.Width+10,
                                        this.BoardPanel.Location.Y - this.buttonStartGame.Height - 10);
            this.buttonStartGame.Enabled = true;
            
            //draw agreement button
            this.buttonAgreeToDraw.Location = new System.Drawing.Point(this.buttonStartGame.Location.X + this.buttonStartGame.Width + 10,
                                        this.BoardPanel.Location.Y - this.buttonAgreeToDraw.Height - 10);
            this.buttonAgreeToDraw.Enabled = false;

            this.labelWhiteTimer.Location = new Point(this.textBoxIsWhiteTurn.Location.X, this.textBoxIsWhiteTurn.Location.Y + 50);
            this.labelBlackTimer.Location = new Point(this.textBoxIsBlackTurn.Location.X, this.textBoxIsBlackTurn.Location.Y - 50);
            this.buttonResignWhite.Location = new Point(this.labelWhiteTimer.Location.X, this.labelWhiteTimer.Location.Y + 50);
            this.buttonResignBlack.Location = new Point(this.labelBlackTimer.Location.X, this.labelBlackTimer.Location.Y - 50);
            this.buttonResignBlack.Enabled = false;
            this.buttonResignWhite.Enabled = false;
            this.labelWhiteTimer.Text = TimeSpan.FromSeconds(this.whiteTime).ToString();
            this.labelBlackTimer.Text = TimeSpan.FromSeconds(this.blackTime).ToString();

            this.labelTimeControlInitial.Location = new Point(this.BoardPanel.Location.X-120, this.BoardPanel.Location.Y);
            this.textBoxInitialTime.Location = new Point(this.BoardPanel.Location.X - 120, this.BoardPanel.Location.Y+30);
            this.labelTimeControlIncrement.Location = new Point(this.BoardPanel.Location.X - 120, this.textBoxInitialTime.Location.Y+50);
            this.textBoxIncrement.Location = new Point(this.BoardPanel.Location.X - 120, this.labelTimeControlIncrement.Location.Y+30);
        }

        private void initialBoardPanelSet()
        {
            this.game = new Game();
            this.BoardPanel.AllowDrop = true;
            this.BoardPanel.Size = new Size(512, 512);
            this.BoardPanel.Location = new Point(((this.Size.Width / 2) - (this.BoardPanel.Size.Width / 2)),
                                                ((this.Size.Height / 2) - (this.BoardPanel.Size.Height / 2)));
            int squareLength = this.BoardPanel.Height / 8;

            //bottom left == 0,0
            for (int row = 0; row < this.PictureSquares.GetLength(0); row++)
            {
                for (int collumn = 0; collumn < this.PictureSquares.GetLength(1); collumn++)
                {
                    this.PictureSquares[row, collumn] = new System.Windows.Forms.PictureBox();
                    this.PictureSquares[row, collumn].Size = new System.Drawing.Size(squareLength, squareLength);
                    this.PictureSquares[row, collumn].Location = new System.Drawing.Point(squareLength * collumn, this.BoardPanel.Height - squareLength * (row + 1));
                    this.PictureSquares[row, collumn].Name = "PictureSquare" + row + "," + collumn;
                    this.PictureSquares[row, collumn].SizeMode = PictureBoxSizeMode.StretchImage;

                    this.PictureSquares[row, collumn].AllowDrop = false;
                    this.PictureSquares[row, collumn].MouseDown += Square_MouseMove;
                    this.PictureSquares[row, collumn].DragEnter += Square_DragEnter;
                    this.PictureSquares[row, collumn].DragDrop += Square_DragDrop;

                    this.BoardPanel.Controls.Add(this.PictureSquares[row, collumn]);
                }
            }
            this.UpdateScreenBoard();
        }

        public void UpdateScreenBoard()
        {
            for (int row = 0; row < this.PictureSquares.GetLength(0); row++)
            {
                for (int collumn = 0; collumn < this.PictureSquares.GetLength(1); collumn++)
                {
                    this.PictureSquares[row, collumn].BackgroundImage = this.game.currentBoard[row, collumn].GetBackgroundImage();
                    this.PictureSquares[row, collumn].Image = this.game.currentBoard[row, collumn].GetImage();
                }
            }
        }
        public void EndTheGame()
        {
            for (int row = 0; row < this.PictureSquares.GetLength(0); row++)
            {
                for (int collumn = 0; collumn < this.PictureSquares.GetLength(1); collumn++)
                {
                    this.PictureSquares[row, collumn].AllowDrop = false;
                }
            }
            this.buttonRestartGame.Enabled = true;
            this.buttonAgreeToDraw.Enabled = false;
            this.buttonResignBlack.Enabled = false;
            this.buttonResignWhite.Enabled = false;
            this.timer1.Stop();
        }
        public void RestartTheGame()
        {
            this.game = new Game();
            this.UpdateScreenBoard();
            this.initialClockPanelSet();
            this.whiteTime = timeControlInitial;
            this.blackTime = timeControlInitial;
            this.labelWhiteTimer.Text = TimeSpan.FromSeconds(this.whiteTime).ToString();
            this.labelBlackTimer.Text = TimeSpan.FromSeconds(this.blackTime).ToString();
            this.textBoxIsWhiteTurn.Visible = false;
            this.textBoxIsBlackTurn.Visible = false;
            this.updateTimeControl();
        }
        public void StartTheGame()
        {
            this.updateTimeControl();
            for (int row = 0; row < this.PictureSquares.GetLength(0); row++)
            {
                for (int collumn = 0; collumn < this.PictureSquares.GetLength(1); collumn++)
                {
                    this.PictureSquares[row, collumn].AllowDrop = true;
                }
            }
            this.buttonStartGame.Enabled = false;
            this.textBoxIsWhiteTurn.Visible = true;

            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            this.timer1.Start();
        }
        private void Square_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                PictureBox pb = (PictureBox)sender;
                if (pb.Image != null)
                {
                    pb.DoDragDrop(pb, DragDropEffects.Move);
                }
            }
        }
        private void Square_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        //once a move is tried:
        private void Square_DragDrop(object sender, DragEventArgs e)
        {
            PictureBox target = (PictureBox)sender;
            if (e.Data.GetDataPresent(typeof(PictureBox)))
            {
                PictureBox source = (PictureBox)e.Data.GetData(typeof(PictureBox));
                if (source != target)
                {
                    try
                    {
                        TryMoveReturnObj output = this.game.TryMove(new Move()
                        {
                            SourceCollumn = this.getSquarePoint(source).X,
                            SourceRow = this.getSquarePoint(source).Y,
                            TargetCollumn = this.getSquarePoint(target).X,
                            TargetRow = this.getSquarePoint(target).Y
                        }
                        , this
                        );
                        if (output.IsSuccesful)
                        {
                            //if succesful
                            this.UpdateScreenBoard();
                            if (this.game.currentBoard.IsWhiteTurn)
                                this.blackTime += timeControlIncrement;
                            else
                                this.whiteTime += timeControlIncrement;
                            this.textBoxIsWhiteTurn.Visible = this.game.currentBoard.IsWhiteTurn;
                            this.textBoxIsBlackTurn.Visible = !this.game.currentBoard.IsWhiteTurn;
                            this.buttonAgreeToDraw.Enabled = true;
                            this.buttonResignBlack.Enabled = true;
                            this.buttonResignWhite.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("not succesful");
                        }

                        if(output.IsGameOver)
                        {
                            this.EndTheGame();
                            if(output.IsDraw)
                            {
                                if(output.IsStaleMate)
                                {
                                    MessageBox.Show("draw by stalemate!");
                                }
                                if (output.Is3FoldRepetition)
                                {
                                    MessageBox.Show("draw 3 fold repetition!");
                                }
                                if(output.Is50MoveRule)
                                {
                                    MessageBox.Show("draw by 50 move rule!");
                                }
                                if(output.IsInsufficientMaterialForWhite && output.IsInsufficientMaterialForBlack)
                                {
                                    MessageBox.Show("draw by insufficient material!");
                                }
                            }
                            else if(output.IsMate)
                            {
                                if(output.IsMateForWhite)
                                {
                                    MessageBox.Show("white won by check mate!");
                                }
                                else
                                {
                                    MessageBox.Show("black won by check mate!");
                                }
                            }
                        }                   
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }  
                }
            }
        }

        public static void MessageShow(string str)
        {
            MessageBox.Show(str);
        }
        private Point getSquarePoint(PictureBox pictureSquare)
        {
            string str = pictureSquare.Name;
            int collumn = int.Parse(str[str.Length - 1].ToString());
            int row = int.Parse(str[str.Length - 3].ToString());
            return new Point(collumn, row);
        }

        private void ButtonRestartGame_Click(object sender, EventArgs e)
        {
            this.RestartTheGame();
        }

        private void ButtonStartGame_Click(object sender, EventArgs e)
        {
            this.StartTheGame();
        }

        private void ButtonAgreeToDraw_Click(object sender, EventArgs e)
        {
            this.EndTheGame();
            MessageBox.Show("draw by agreement!");
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (game.currentBoard.IsWhiteTurn)
                this.whiteTime--;
            else
                this.blackTime--;
            this.labelWhiteTimer.Text = TimeSpan.FromSeconds(this.whiteTime).ToString();
            this.labelBlackTimer.Text = TimeSpan.FromSeconds(this.blackTime).ToString();

            if (this.whiteTime <= 0 || this.blackTime <= 0)
                this.EndTheGame();

            if (this.whiteTime <= 0)
            {
                if(!game.currentBoard.isInsufficientMaterialForBlack())
                    MessageBox.Show("black won on time!");
                else
                    MessageBox.Show("draw! black has insufficient material and white has run out of time");
            }
            if (this.blackTime <= 0)
            {
                if (!game.currentBoard.isInsufficientMaterialForWhite())
                    MessageBox.Show("white won on time!");
                else
                    MessageBox.Show("draw! white has insufficient material and black has run out of time");
            }
        }

        private void ButtonResignWhite_Click(object sender, EventArgs e)
        {
            this.EndTheGame();
            MessageBox.Show("black has won by resignation");
        }

        private void ButtonResignBlack_Click(object sender, EventArgs e)
        {
            this.EndTheGame();
            MessageBox.Show("white has won by resignation");
        }
        private void updateTimeControl()
        {
            try
            {
                this.timeControlInitial = int.Parse(textBoxInitialTime.Text) * 60;
                this.timeControlIncrement = int.Parse(textBoxIncrement.Text);
            }
            catch (FormatException)
            {
                MessageBox.Show("invalid time control, set to "+ ((double)this.timeControlInitial / 60).ToString()+"+"+this.timeControlIncrement+" instead");
                this.textBoxInitialTime.Text = ((double)this.timeControlInitial / 60).ToString();
                this.textBoxIncrement.Text = this.timeControlIncrement.ToString();
            }
            this.whiteTime = this.timeControlInitial;
            this.blackTime = this.timeControlInitial;
            this.labelWhiteTimer.Text = TimeSpan.FromSeconds(this.whiteTime).ToString();
            this.labelBlackTimer.Text = TimeSpan.FromSeconds(this.blackTime).ToString();
        }
        /**
        public void Promote(Point point)
        {
            if(point.Y==7)
            {
                foreach(PictureBox p in whitePromotionPictureBox)
                {
                    p.Visible = true;
                    p.Enabled = true;
                }
            }
            else if(point.Y==0)
            {
                foreach (PictureBox p in blackPromotionPictureBox)
                {
                    p.Visible = true;
                    p.Enabled = true;
                }
            }
            
        }
        private void promote_Click(object sender, EventArgs e)
        {
            MessageBox.Show("aba");
            Thread thread = new Thread(promote_ClickThreadFunc);
            thread.Start(sender);
        }
        private void promote_ClickThreadFunc(object sender)
        {
            for (int i = 0; i < 4; i++)
            {
                this.whitePromotionPictureBox[i].Visible = false;
                this.whitePromotionPictureBox[i].Enabled = false;

                this.blackPromotionPictureBox[i].Visible = false;
                this.blackPromotionPictureBox[i].Enabled = false;
            }
            Piece piece;
            switch (((PictureBox)sender).Name)
            {
                case "WQ": piece = new Queen() { IsWhite = true }; break;
                case "WR": piece = new Rook() { IsWhite = true }; break;
                case "WK": piece = new Knight() { IsWhite = true }; break;
                case "WB": piece = new Bishop() { IsWhite = true }; break;
                case "BQ": piece = new Queen() { IsWhite = false }; break;
                case "BR": piece = new Rook() { IsWhite = false }; break;
                case "BK": piece = new Knight() { IsWhite = false }; break;
                case "BB": piece = new Bishop() { IsWhite = false }; break;
                default: throw new Exception("no piece found to promote");
            }
            piece.Collumn = promotionPoint.X;
            piece.Row = promotionPoint.Y;
            this.game.currentBoard[this.promotionPoint.Y, this.promotionPoint.X] = piece;
            this.waitForPromotionPick.Set();
        }
                private void initialPromotionRectangleSet()
        {
            int squareLength = this.BoardPanel.Height / 8;
            this.whitePromotionPictureBox = new PictureBox[4];
            this.blackPromotionPictureBox = new PictureBox[4];
            for (int i = 0; i < 4; i++)
            {
                this.whitePromotionPictureBox[i] = new System.Windows.Forms.PictureBox();
                this.whitePromotionPictureBox[i].Size = new System.Drawing.Size(squareLength, squareLength);
                this.whitePromotionPictureBox[i].Location = new System.Drawing.Point(this.BoardPanel.Location.X + this.BoardPanel.Width + (squareLength * i + squareLength/2), this.BoardPanel.Location.Y + this.BoardPanel.Height-squareLength);
                this.whitePromotionPictureBox[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.whitePromotionPictureBox[i].BackgroundImage = this.game.currentBoard[0, i].GetBackgroundImage();
                this.whitePromotionPictureBox[i].Image = this.game.currentBoard[0, i].GetImage();
                this.whitePromotionPictureBox[i].AllowDrop = false;
                this.whitePromotionPictureBox[i].Visible = false;
                this.whitePromotionPictureBox[i].Enabled = false;
                this.whitePromotionPictureBox[i].Click += new System.EventHandler(this.promote_Click);

                this.blackPromotionPictureBox[i] = new System.Windows.Forms.PictureBox();
                this.blackPromotionPictureBox[i].Size = new System.Drawing.Size(squareLength, squareLength);
                this.blackPromotionPictureBox[i].Location = new System.Drawing.Point(this.BoardPanel.Location.X + this.BoardPanel.Width + (squareLength * i + squareLength / 2), this.BoardPanel.Location.Y);
                this.blackPromotionPictureBox[i].SizeMode = PictureBoxSizeMode.StretchImage;
                this.blackPromotionPictureBox[i].BackgroundImage = this.game.currentBoard[7, i].GetBackgroundImage();
                this.blackPromotionPictureBox[i].Image = this.game.currentBoard[7, i].GetImage();
                this.blackPromotionPictureBox[i].AllowDrop = false;
                this.blackPromotionPictureBox[i].Visible = false;
                this.blackPromotionPictureBox[i].Enabled = false;
                this.blackPromotionPictureBox[i].Click += new System.EventHandler(this.promote_Click);

                this.Controls.Add(this.whitePromotionPictureBox[i]);
                this.Controls.Add(this.blackPromotionPictureBox[i]);
            }
            this.whitePromotionPictureBox[0].Name = "WR";
            this.whitePromotionPictureBox[0].Name = "WK";
            this.whitePromotionPictureBox[0].Name = "WB";
            this.whitePromotionPictureBox[0].Name = "WQ";
            this.blackPromotionPictureBox[0].Name = "BR";
            this.blackPromotionPictureBox[0].Name = "BK";
            this.blackPromotionPictureBox[0].Name = "BB";
            this.blackPromotionPictureBox[0].Name = "BQ";
        }
    **/
    }
}
