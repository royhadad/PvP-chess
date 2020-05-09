using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChessGame
{
    class Queen : Piece
    {
        public override Piece GetCopyPiece()
        {
            return new Queen() { IsWhite = this.IsWhite, Row = this.Row, Collumn = this.Collumn };
        }
        public override List<Move> GetThreateningMoves(Board board)
        {
            List<Move> moves = new List<Move>(28);
            bool isStopped;
            int count;

            //UP
            isStopped = false;
            count = 1;
            while (!isStopped)
            {
                if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn, this.Row + count, board))
                {
                    moves.Add(new Move(this.Collumn, this.Row, this.Collumn, this.Row + count));
                    if (!(board[this.Row + count, this.Collumn] is EmptyPiece))
                        isStopped = true;
                }
                else
                    isStopped = true;
                count++;
            }

            //DOWN
            isStopped = false;
            count = 1;
            while (!isStopped)
            {
                if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn, this.Row - count, board))
                {
                    moves.Add(new Move(this.Collumn, this.Row, this.Collumn, this.Row - count));
                    if (!(board[this.Row - count, this.Collumn] is EmptyPiece))
                        isStopped = true;
                }
                else
                    isStopped = true;
                count++;
            }

            //RIGHT
            isStopped = false;
            count = 1;
            while (!isStopped)
            {
                if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn + count, this.Row, board))
                {
                    moves.Add(new Move(this.Collumn, this.Row, this.Collumn + count, this.Row));
                    if (!(board[this.Row, this.Collumn + count] is EmptyPiece))
                        isStopped = true;
                }
                else
                    isStopped = true;
                count++;
            }

            //LEFT
            isStopped = false;
            count = 1;
            while (!isStopped)
            {
                if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn - count, this.Row, board))
                {
                    moves.Add(new Move(this.Collumn, this.Row, this.Collumn - count, this.Row));
                    if (!(board[this.Row, this.Collumn - count] is EmptyPiece))
                        isStopped = true;
                }
                else
                    isStopped = true;
                count++;
            }

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
            Queen other = obj as Queen;
            if (other == null)
                return false;
            return this.IsWhite == other.IsWhite;
        }
        public override int GetHashCode()
        {
            return (typeof(Queen)).GetHashCode() * this.IsWhite.GetHashCode();
        }

        public override Bitmap GetImage()
        {
            if(this.IsWhite)
                return Properties.Resources.WhiteQueen;
            else
                return Properties.Resources.BlackQueen;
        }
    }
}
