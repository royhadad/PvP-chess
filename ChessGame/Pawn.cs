using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChessGame
{
    class Pawn : Piece
    {
        public bool IsMoved{get;set;}
        public bool IsEnPassantLeft { get; set; }
        public bool IsEnPassantRight { get; set; }
        public override Piece GetCopyPiece()
        {
            return new Pawn() { IsWhite = this.IsWhite, IsMoved = this.IsMoved, IsEnPassantLeft = this.IsEnPassantLeft, IsEnPassantRight = this.IsEnPassantRight
            ,
                Row = this.Row,
                Collumn = this.Collumn
            };
        }
        public override List<Move> GetThreateningMoves(Board board)
        {
            List<Move> moves = new List<Move>(4);
            if(this.IsWhite)
            {

                if (board[this.Row + 1, this.Collumn] is EmptyPiece)
                {
                    moves.Add(new Move(this.Collumn, this.Row, this.Collumn, this.Row + 1));

                    if (this.Row == 1 && board[this.Row + 2, this.Collumn] is EmptyPiece)
                        moves.Add(new Move(this.Collumn, this.Row, this.Collumn, this.Row + 2));
                }

                if (this.Collumn < 7)
                {
                    if (this.IsEnPassantRight || ((!(board[this.Row + 1, this.Collumn + 1] is EmptyPiece)) && board[this.Row + 1, this.Collumn + 1].IsWhite == false))
                        moves.Add(new Move(this.Collumn, this.Row, this.Collumn + 1, this.Row + 1));
                }

                if (this.Collumn>0)
                {
                    if (this.IsEnPassantLeft || ((!(board[this.Row + 1, this.Collumn - 1] is EmptyPiece)) && board[this.Row + 1, this.Collumn - 1].IsWhite == false))
                        moves.Add(new Move(this.Collumn, this.Row, this.Collumn - 1, this.Row + 1));
                }
            }
            else
            {
                if (board[this.Row - 1, this.Collumn] is EmptyPiece)
                {
                    moves.Add(new Move(this.Collumn, this.Row, this.Collumn, this.Row - 1));

                    if (this.Row == 6 && board[this.Row - 2, this.Collumn] is EmptyPiece)
                        moves.Add(new Move(this.Collumn, this.Row, this.Collumn, this.Row - 2));
                }

                if (this.Collumn < 7)
                {
                    if (this.IsEnPassantRight || ((!(board[this.Row - 1, this.Collumn + 1] is EmptyPiece)) && board[this.Row - 1, this.Collumn + 1].IsWhite == true))
                        moves.Add(new Move(this.Collumn, this.Row, this.Collumn + 1, this.Row - 1));
                }
                if (this.Collumn > 0)
                {
                    if (this.IsEnPassantLeft || ((!(board[this.Row - 1, this.Collumn - 1] is EmptyPiece)) && board[this.Row - 1, this.Collumn - 1].IsWhite == true))
                        moves.Add(new Move(this.Collumn, this.Row, this.Collumn - 1, this.Row - 1));
                }
            }

            return moves;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Pawn other = obj as Pawn;
            if (other == null)
                return false;
            return this.IsWhite == other.IsWhite && this.IsEnPassantLeft == other.IsEnPassantLeft && this.IsEnPassantRight == other.IsEnPassantRight;
        }
        public override int GetHashCode()
        {
            return (typeof(Rook)).GetHashCode() * this.IsWhite.GetHashCode() * (this.IsEnPassantLeft.GetHashCode() * 3) * (this.IsEnPassantRight.GetHashCode() * 7);
        }
        public override Bitmap GetImage()
        {
            if (this.IsWhite)
                return Properties.Resources.WhitePawn;
            else
                return Properties.Resources.BlackPawn;
        }
    }
}
