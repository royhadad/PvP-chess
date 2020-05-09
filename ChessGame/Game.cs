using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChessGame
{
    class Game
    {
        public Board currentBoard;
        Queue<Board> last100Boards;
        int fiftyMoveRuleCounter;//counts half moves

        public Game()
        {
            this.currentBoard = new Board();
            this.currentBoard.IsWhiteTurn = true;
            this.last100Boards = new Queue<Board>(100);
            fiftyMoveRuleCounter = 0;
        }

        public TryMoveReturnObj TryMove(Move move, ChessForm form1)
        {
            //ChessForm.MessageShow(move.ToString());
            if(!IsLegalMove(move))
            {
                return new TryMoveReturnObj() { IsSuccesful = false };
            }

            TryMoveReturnObj output = new TryMoveReturnObj();
            output.IsSuccesful = true;
            this.Move(move);

            this.promoteIfNeeded();        

            if(currentBoard.isMate())
            {
                output.IsGameOver = true;
                output.IsMate = true;
                if (this.currentBoard.IsWhiteTurn)
                    output.IsMateForBlack = true;
                else
                    output.IsMateForWhite = true;
            }
            if(this.isDraw())
            {
                output.IsGameOver = true;
                output.IsDraw = true;
                output.IsStaleMate = this.currentBoard.IsStaleMate();
                output.Is3FoldRepetition = this.is3FoldRepetition();
                output.Is50MoveRule = this.is50MoveRule();
                output.IsInsufficientMaterialForWhite = this.currentBoard.isInsufficientMaterialForWhite();
                output.IsInsufficientMaterialForBlack = this.currentBoard.isInsufficientMaterialForBlack();
            }

            return output;
        }
        private bool is50MoveRule()
        {
            if (this.fiftyMoveRuleCounter >= 100)
                return true;
            else
                return false;
        }
        private bool is3FoldRepetition()
        {
            IEnumerator<Board> enumerator = this.last100Boards.GetEnumerator();
            Board current = this.currentBoard;
            int count = 0;
            while(enumerator.MoveNext())
            {
                if (enumerator.Current.Equals(current))
                    count++;
            }
            if (count >= 2)
                return true;
            else
                return false;
        }
        public bool isDraw()
        {
            if (this.currentBoard.IsStaleMate() || is50MoveRule() || is3FoldRepetition() || (this.currentBoard.isInsufficientMaterialForBlack() && this.currentBoard.isInsufficientMaterialForWhite()))
                return true;
            else
                return false;
        }
        private void Move(Move move)
        {
            set50MoveRuleCounterAndCleanQueue(move);
            this.last100Boards.Enqueue(this.currentBoard.GetCopyBoard());
            this.currentBoard.Move(move);
        }
        private void set50MoveRuleCounterAndCleanQueue(Move move)
        {
            this.fiftyMoveRuleCounter++;
            if (this.currentBoard[move.SourceRow, move.SourceCollumn] is Pawn)
                this.fiftyMoveRuleCounter = 0;
            if(!(this.currentBoard[move.TargetRow, move.TargetCollumn] is EmptyPiece))
                this.fiftyMoveRuleCounter = 0;
            if (fiftyMoveRuleCounter == 0)
                while (this.last100Boards.Count != 0)
                    this.last100Boards.Dequeue();
        }
        private void setPiecesProperties(Move move)
        {
            Piece movedPiece = this.currentBoard[move.TargetRow, move.TargetCollumn];

            this.currentBoard.ResetEnPassantPawns();
            if (movedPiece is Pawn)
            {
                ((Pawn)movedPiece).IsMoved = true;
                if (Math.Abs(move.SourceRow - move.TargetRow) == 2)
                {
                    if (move.TargetCollumn != 0)
                    {
                        if (this.currentBoard[move.TargetRow, move.TargetCollumn - 1] is Pawn && this.currentBoard[move.TargetRow, move.TargetCollumn - 1].IsWhite!=movedPiece.IsWhite)
                            ((Pawn)this.currentBoard[move.TargetRow, move.TargetCollumn - 1]).IsEnPassantRight = true;
                    }
                    if (move.TargetCollumn != 7)
                    {
                        if (this.currentBoard[move.TargetRow, move.TargetCollumn + 1] is Pawn && this.currentBoard[move.TargetRow, move.TargetCollumn + 1].IsWhite != movedPiece.IsWhite)
                            ((Pawn)this.currentBoard[move.TargetRow, move.TargetCollumn + 1]).IsEnPassantLeft = true;
                    }
                }
            }

            if (movedPiece is King)
            {
                ((King)movedPiece).IsAbleToShortCastle = false;
                ((King)movedPiece).IsAbleToLongCastle = false;
            }
            if (movedPiece is Rook)
            {
                if(movedPiece.IsWhite)
                {
                    if(move.SourceRow==0 && move.SourceCollumn==0)
                    {
                        this.setKingCastles(true, true);
                    }
                    if (move.SourceRow == 0 && move.SourceCollumn == 7)
                    {
                        this.setKingCastles(true, false);
                    }
                }
                if (!movedPiece.IsWhite)
                {
                    if (move.SourceRow == 7 && move.SourceCollumn == 0)
                    {
                        this.setKingCastles(false, true);
                    }
                    if (move.SourceRow == 7 && move.SourceCollumn == 7)
                    {
                        this.setKingCastles(false, false);
                    }
                }
            }
        }
        private void setKingCastles(bool isForWhite, bool isLong)
        {
            King king = this.currentBoard.GetKing(isForWhite);
            if (isLong)
                king.IsAbleToLongCastle = false;
            else
                king.IsAbleToShortCastle = false;
        }
        public bool IsLegalMove(Move move)
        {
            List<Move> legalMoves = this.currentBoard.GetLegalMoves();
            if (legalMoves.Contains(move))
                return true;
            else
                return false;
        }

        private void promoteIfNeeded()
        {
            Point promotionSquare = this.currentBoard.GetUnpromotedPawn();
            if (promotionSquare.Y != 1)//only if no pawn found to promote
            {
                Piece promotedPiece = null;

                string input = Microsoft.VisualBasic.Interaction.InputBox("insert desired promotion:\nqueen\nknight\nrook\nbishop\n" +
                    "clicking cancel or inserting an invalid string\nwill promote to a queen automaticly",
                       "Promotion",
                       "queen",
                       Screen.PrimaryScreen.Bounds.Width / 2-200,
                       Screen.PrimaryScreen.Bounds.Height / 2-100);
                switch (input)
                {
                    case "knight": promotedPiece = new Knight(); break;
                    case "rook": promotedPiece = new Rook(); break;
                    case "bishop": promotedPiece = new Bishop(); break;
                    default: promotedPiece = new Queen();break;
                }
                promotedPiece.Collumn = promotionSquare.X;
                promotedPiece.Row = promotionSquare.Y;
                promotedPiece.IsWhite = promotionSquare.Y == 7 ? true : false;
                this.currentBoard[promotionSquare.Y, promotionSquare.X] = promotedPiece;
            }
        }
}

    class TryMoveReturnObj
    {
        public bool IsSuccesful { get; set; }
        public bool IsGameOver { get; set; }
        public bool IsMate { get; set; }
        public bool IsDraw { get; set; }
        public bool IsMateForWhite { get; set; }
        public bool IsMateForBlack { get; set; }
        public bool IsStaleMate { get; set; }
        public bool Is50MoveRule { get; set; }
        public bool Is3FoldRepetition { get; set; }
        public bool IsInsufficientMaterialForWhite { get; set; }
        public bool IsInsufficientMaterialForBlack { get; set; }
    }
}
