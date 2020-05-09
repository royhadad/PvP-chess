using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChessGame
{
    class King:Piece
    {
        public bool IsAbleToShortCastle;
        public bool IsAbleToLongCastle;
        public King()
        {
            IsAbleToShortCastle = true;
            IsAbleToLongCastle = true;
        }

        public override List<Move> GetThreateningMoves(Board board)
        {
            List<Move> moves = new List<Move>(8);
            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn + 1, this.Row + 1, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn + 1, this.Row + 1));

            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn + 1, this.Row + 0, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn + 1, this.Row + 0));

            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn + 1, this.Row - 1, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn + 1, this.Row - 1));

            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn - 1, this.Row + 1, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn - 1, this.Row + 1));

            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn - 1, this.Row + 0, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn - 1, this.Row + 0));

            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn - 1, this.Row - 1, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn - 1, this.Row - 1));

            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn + 0, this.Row + 1, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn + 0, this.Row + 1));

            if (this.isSquareInBoundsAndNotSameColorPieceOnIt(this.Collumn + 0, this.Row - 1, board))
                moves.Add(new Move(this.Collumn, this.Row, this.Collumn + 0, this.Row - 1));

            return moves;
        }
        public override Piece GetCopyPiece()
        {
            return new King() { IsAbleToShortCastle = this.IsAbleToShortCastle, IsAbleToLongCastle = this.IsAbleToLongCastle, IsWhite = this.IsWhite
            ,
                Row = this.Row,
                Collumn = this.Collumn
            };
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            King other = obj as King;
            if (other == null)
                return false;
            return this.IsWhite == other.IsWhite && this.IsAbleToShortCastle == other.IsAbleToShortCastle && this.IsAbleToLongCastle == other.IsAbleToLongCastle;
        }
        public override int GetHashCode()
        {
            return (typeof(King)).GetHashCode() * this.IsWhite.GetHashCode() *(this.IsAbleToShortCastle.GetHashCode()*3)*(this.IsAbleToLongCastle.GetHashCode() * 2);
        }
        public override Bitmap GetImage()
        {
            if (this.IsWhite)
                return Properties.Resources.WhiteKing;
            else
                return Properties.Resources.BlackKing;
        }
    }
}
