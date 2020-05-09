using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChessGame
{
    class Bishop : Piece
    {
        public override Piece GetCopyPiece()
        {
            return new Bishop() { IsWhite = this.IsWhite, Row=this.Row, Collumn = this.Collumn };
        }
        public override List<Move> GetThreateningMoves(Board board)
        {
            List<Move> moves = new List<Move>(14);
            bool isStopped;
            int count;

            //UP & RIGHT
            isStopped = false;
            count = 1;
            while (!isStopped)
            {
                if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn + count, this.Row + count, board))
                {
                    moves.Add(new Move(this.Collumn, this.Row, this.Collumn + count, this.Row + count));
                    if (!(board[this.Row + count, this.Collumn + count] is EmptyPiece))
                        isStopped = true;
                }
                else
                    isStopped = true;
                count++;
            }

            //UP & LEFT
            isStopped = false;
            count = 1;
            while (!isStopped)
            {
                if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn - count, this.Row + count, board))
                {
                    moves.Add(new Move(this.Collumn, this.Row, this.Collumn - count, this.Row + count));
                    if (!(board[this.Row + count, this.Collumn - count] is EmptyPiece))
                        isStopped = true;
                }
                else
                    isStopped = true;
                count++;
            }

            //DOWN & RIGHT
            isStopped = false;
            count = 1;
            while (!isStopped)
            {
                if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn + count, this.Row - count, board))
                {
                    moves.Add(new Move(this.Collumn, this.Row, this.Collumn + count, this.Row - count));
                    if (!(board[this.Row - count, this.Collumn + count] is EmptyPiece))
                        isStopped = true;
                }
                else
                    isStopped = true;
                count++;
            }

            //DOWN & LEFT
            isStopped = false;
            count = 1;
            while (!isStopped)
            {
                if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn - count, this.Row - count, board))
                {
                    moves.Add(new Move(this.Collumn, this.Row, this.Collumn - count, this.Row - count));
                    if (!(board[this.Row - count, this.Collumn - count] is EmptyPiece))
                        isStopped = true;
                }
                else
                    isStopped = true;
                count++;
            }

            return moves;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Bishop other = obj as Bishop;
            if (other == null)
                return false;
            return this.IsWhite == other.IsWhite;
        }
        public override int GetHashCode()
        {
            return (typeof(Bishop)).GetHashCode() * this.IsWhite.GetHashCode();
        }
        public override Bitmap GetImage()
        {
            if (this.IsWhite)
                return Properties.Resources.WhiteBishop;
            else
                return Properties.Resources.BlackBishop;
        }
        public bool IsLightSquareBishop()
        {
            if ((this.Row + this.Collumn) % 2 == 0)
                return false;
            else
                return true;
        }
    }
}
