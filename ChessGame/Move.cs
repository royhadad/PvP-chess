using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChessGame
{
    class Move
    {
        public int SourceCollumn { get; set; }
        public int SourceRow { get; set; }
        public int TargetCollumn { get; set; }
        public int TargetRow { get; set; }
        public Move() { }
        public Move(int sourceCollumn, int sourceRow, int targetCollumn, int targetRow)
        {
            this.SourceCollumn = sourceCollumn;
            this.SourceRow = sourceRow;
            this.TargetCollumn = targetCollumn;
            this.TargetRow = targetRow;
        }
        public override string ToString()
        {
            return this.SourceCollumn + "," + this.SourceRow + " TO " + this.TargetCollumn + "," + this.TargetRow;
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Move other = obj as Move;
            if (other == null)
                return false;
            return this.SourceCollumn == other.SourceCollumn &&
                this.SourceRow == other.SourceRow &&
                this.TargetCollumn == other.TargetCollumn &&
                this.TargetRow == other.TargetRow;
        }
        public override int GetHashCode()
        {
            return this.SourceCollumn ^ this.SourceRow ^ this.TargetCollumn ^ this.TargetRow;
        }
        public bool IsCastlingMove(Board board)
        {
            if ((board[SourceRow, SourceCollumn] is King) && (Math.Abs(TargetCollumn - SourceCollumn) > 1))
                return true;
            else
                return false;
        }
        public bool IsEnPassantMove(Board board)
        {
            Pawn movedPiece;
            if ((board[SourceRow, SourceCollumn] is Pawn) && (Math.Abs(TargetCollumn - SourceCollumn) != 0))
            {
                movedPiece = ((Pawn)board[SourceRow, SourceCollumn]);
                if (movedPiece.IsWhite)
                {
                    if (board[this.TargetRow - 1, this.TargetCollumn] is Pawn && board[this.TargetRow, this.TargetCollumn] is EmptyPiece)
                        return true;
                }
                if (!movedPiece.IsWhite)
                {
                    if (board[this.TargetRow + 1, this.TargetCollumn] is Pawn && board[this.TargetRow, this.TargetCollumn] is EmptyPiece)
                        return true;
                }
            }
            return false;
        }
        public bool IsPromotionMove(Board board)
        {
            if ((board[SourceRow, SourceCollumn] is Pawn))
            {
                if (this.TargetRow == 0 || this.TargetRow == 7)
                    return true;
            }
            return false;
        }
    }
}
