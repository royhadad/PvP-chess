using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChessGame
{
    class Knight : Piece
    {
        public override Piece GetCopyPiece()
        {
            return new Knight() { IsWhite = this.IsWhite, Row = this.Row, Collumn = this.Collumn };
        }
        public override List<Move> GetThreateningMoves(Board board)
        {
            List<Move> moves = new List<Move>(8);
            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn + 2, this.Row + 1, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn + 2, this.Row + 1));

            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn + 2, this.Row - 1, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn + 2, this.Row - 1));

            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn - 2, this.Row + 1, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn - 2, this.Row + 1));

            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn - 2, this.Row - 1, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn - 2, this.Row - 1));

            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn + 1, this.Row + 2, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn + 1, this.Row + 2));

            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn - 1, this.Row + 2, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn - 1, this.Row + 2));

            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn + 1, this.Row - 2, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn + 1, this.Row - 2));

            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn - 1, this.Row - 2, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn - 1, this.Row - 2));

            return moves;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Knight other = obj as Knight;
            if (other == null)
                return false;
            return this.IsWhite == other.IsWhite;
        }
        public override int GetHashCode()
        {
            return (typeof(Knight)).GetHashCode() * this.IsWhite.GetHashCode();
        }
        public override Bitmap GetImage()
        {
            if (this.IsWhite)
                return Properties.Resources.WhiteKnight;
            else
                return Properties.Resources.BlackKnight;
        }
    }
}
