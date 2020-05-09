using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChessGame
{
    abstract class Piece
    {
        public bool IsWhite { get; set; }
        public int Row { get; set; }
        public int Collumn { get; set; }
        public abstract List<Move> GetThreateningMoves(Board board);
        public abstract Piece GetCopyPiece();
        public abstract Bitmap GetImage();
        public Bitmap GetBackgroundImage()
        {
            if ((this.Row + this.Collumn) % 2 == 0)
                return Properties.Resources.DarkSquare;
            else
                return Properties.Resources.LightSquare;

        }
        protected bool isSquareInBoundsAndNotSameColorPieceOnIt(int collumn, int row, Board board)
        {
            if (row < 0 || row > 7 || collumn < 0 || collumn > 7)
                return false;
            if (board[row, collumn] is EmptyPiece)
                return true;
            if (board[row, collumn].IsWhite != this.IsWhite)
                return true;
            return false;
        }
    }
}
