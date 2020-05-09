using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChessGame
{
    class EmptyPiece : Piece
    {
        public override Piece GetCopyPiece()
        {
            return new EmptyPiece() { Row = this.Row, Collumn = this.Collumn };
        }
        public override List<Move> GetThreateningMoves(Board board)
        {
            return new List<Move>(0);
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            EmptyPiece other = obj as EmptyPiece;
            if (other == null)
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            return (typeof(EmptyPiece)).GetHashCode();
        }
        public override Bitmap GetImage()
        {
            return null;
        }
    }
}
