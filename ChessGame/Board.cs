using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChessGame
{
    class Board
    {
        private Piece[,] pieces;
        public bool IsWhiteTurn { get; set; }
        public Board()
        {
            pieces = new Piece[8, 8];
            for (int row = 0; row < this.pieces.GetLength(0); row++)
            {
                for (int collumn = 0; collumn < this.pieces.GetLength(1); collumn++)
                {
                    if (row == 1)
                        this.pieces[row, collumn] = new Pawn() { IsWhite = true };
                    if (row == 6)
                        this.pieces[row, collumn] = new Pawn() { IsWhite = false };
                    if (row == 0)
                    {
                        switch (collumn)
                        {
                            case 0: case 7: this.pieces[row, collumn] = new Rook() { IsWhite = true}; break;
                            case 1: case 6: this.pieces[row, collumn] = new Knight() { IsWhite = true }; break;
                            case 2: case 5: this.pieces[row, collumn] = new Bishop() { IsWhite = true }; break;
                            case 3: this.pieces[row, collumn] = new Queen() { IsWhite = true }; break;
                            case 4: this.pieces[row, collumn] = new King() { IsWhite = true }; break;
                        }
                    }
                    if (row == 7)
                    {
                        switch (collumn)
                        {
                            case 0: case 7: this.pieces[row, collumn] = new Rook() { IsWhite = false }; break;
                            case 1: case 6: this.pieces[row, collumn] = new Knight() { IsWhite = false }; break;
                            case 2: case 5: this.pieces[row, collumn] = new Bishop() { IsWhite = false }; break;
                            case 3: this.pieces[row, collumn] = new Queen() { IsWhite = false }; break;
                            case 4: this.pieces[row, collumn] = new King() { IsWhite = false }; break;
                        }
                    }
                    if(row>=2 && row<=5)
                    {
                        this.pieces[row, collumn] = new EmptyPiece();
                    }

                    this.pieces[row, collumn].Row = row;
                    this.pieces[row, collumn].Collumn = collumn;
                }
            }
        }

        public Board GetCopyBoard()
        {
            Board newBoard = new Board() { IsWhiteTurn = this.IsWhiteTurn };
            for (int row = 0; row < this.pieces.GetLength(0); row++)
            {
                for (int collumn = 0; collumn < this.pieces.GetLength(1); collumn++)
                {
                    newBoard.pieces[row, collumn] = this.pieces[row, collumn].GetCopyPiece();
                }
            }
            return newBoard;
        }
        
        public List<Move> GetLegalMoves()
        {
            return this.GetLegalMoves(this.IsWhiteTurn);
        }
        public bool IsInCheck()
        {
            return this.IsInCheck(this.IsWhiteTurn);
        }
        public bool IsInCheck(bool isForWhite)
        {
            King king = this.GetKing(isForWhite);
            int kingRow = king.Row;
            int kingCollumn = king.Collumn;
            return this.IsSquareChecked(isForWhite, kingRow, kingCollumn);
        }
        public bool IsSquareChecked(bool isForWhite, int squareRow, int squareCollumn)
        {
            List<Move> threatningMoves = this.GetThreatningMoves(!isForWhite);
            foreach(Move move in threatningMoves)
            {
                if(move.TargetRow == squareRow && move.TargetCollumn == squareCollumn)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isMate()
        {
            if (this.GetLegalMoves().Count == 0 && this.IsInCheck())
                return true;
            else
                return false;
        }
        public bool IsStaleMate()
        {
            if ((!this.IsInCheck()) && GetLegalMoves().Count == 0)
                return true;
            else
                return false;
        }
        
        public Point GetUnpromotedPawn()
        {
            for(int collumn=0;collumn<this.pieces.GetLength(1);collumn++)
            {
                if (this[7, collumn] is Pawn)
                    return new Point(collumn, 7);
            }
            for (int collumn = 0; collumn < this.pieces.GetLength(1); collumn++)
            {
                if (this[0, collumn] is Pawn)
                    return new Point(collumn, 0);
            }
            return new Point(1, 1);//if not found
        }

        public List<Move> GetLegalMoves(bool isForWhite)
        {
            List<Move> potentialMoves = this.GetThreatningMoves(isForWhite);
            List<Move> legalMoves = new List<Move>(potentialMoves.Count);
            Board boardCopy;

            foreach (Move move in potentialMoves)
            {
                boardCopy = this.GetCopyBoard();
                boardCopy.Move(move);
                if (!boardCopy.IsInCheck(isForWhite))
                    legalMoves.Add(move);
            }
            legalMoves.AddRange(GetCastlingMoves(isForWhite));
            
            //ChessForm.MessageShow("legal moves:\n"+Utilities.ListToString(legalMoves));
            
            return legalMoves;
        }

        //return the legal castling moves
        public List<Move> GetCastlingMoves(bool isForWhite)
        {
            List<Move> castlingMoves = new List<Move>(2);
            King king = this.GetKing(isForWhite);
            if (IsSquareChecked(isForWhite, king.Row, king.Collumn))
                return castlingMoves;
            if(king.IsAbleToLongCastle)
            {
                if((!IsSquareChecked(isForWhite, king.Row, king.Collumn-1)) && 
                    (!IsSquareChecked(isForWhite, king.Row, king.Collumn-2)))
                    castlingMoves.Add(new Move(king.Collumn, king.Row, king.Collumn-2, king.Row));
            }
            if (king.IsAbleToShortCastle)
            {
                if ((!IsSquareChecked(isForWhite, king.Row, king.Collumn + 1)) && (!IsSquareChecked(isForWhite, king.Row, king.Collumn + 2)))
                    castlingMoves.Add(new Move(king.Collumn, king.Row, king.Collumn + 2, king.Row));
            }
            return castlingMoves;
        }

        public void Move(Move move)
        {
            if (move.IsEnPassantMove(this))
            {
                this[move.SourceRow, move.TargetCollumn] = new EmptyPiece() { Row = move.SourceRow, Collumn = move.TargetCollumn };
            }
            else if(move.IsCastlingMove(this))
            {
                if(move.TargetCollumn == 6)
                {
                    this[move.SourceRow, move.SourceCollumn + 1] = this[move.SourceRow, 7];
                    this[move.SourceRow, move.SourceCollumn + 1].Collumn = move.SourceCollumn + 1;
                    this[move.SourceRow, 7]  = new EmptyPiece() { Row = move.SourceRow, Collumn = 7 };
                }
                if (move.TargetCollumn == 2)
                {
                    this[move.SourceRow, move.SourceCollumn - 1] = this[move.SourceRow, 0];
                    this[move.SourceRow, move.SourceCollumn - 1].Collumn = move.SourceCollumn - 1;
                    this[move.SourceRow, 0] = new EmptyPiece() { Row = move.SourceRow, Collumn = 0 };
                }
            }

            this[move.TargetRow, move.TargetCollumn] = this[move.SourceRow, move.SourceCollumn];
            this[move.TargetRow, move.TargetCollumn].Row = move.TargetRow;
            this[move.TargetRow, move.TargetCollumn].Collumn = move.TargetCollumn;
            this[move.SourceRow, move.SourceCollumn] = new EmptyPiece() { Row = move.SourceRow, Collumn = move.SourceCollumn };

            this.setPiecesProperties(move);

            this.IsWhiteTurn = !this.IsWhiteTurn;

        }
        public void setPiecesProperties(Move move)
        {
            Piece movedPiece = this[move.TargetRow, move.TargetCollumn];

            this.ResetEnPassantPawns();
            if (movedPiece is Pawn)
            {
                ((Pawn)movedPiece).IsMoved = true;
                if (Math.Abs(move.SourceRow - move.TargetRow) == 2)
                {
                    if (move.TargetCollumn != 0)
                    {
                        if (this[move.TargetRow, move.TargetCollumn - 1] is Pawn && this[move.TargetRow, move.TargetCollumn - 1].IsWhite != movedPiece.IsWhite)
                            ((Pawn)this[move.TargetRow, move.TargetCollumn - 1]).IsEnPassantRight = true;
                    }
                    if (move.TargetCollumn != 7)
                    {
                        if (this[move.TargetRow, move.TargetCollumn + 1] is Pawn && this[move.TargetRow, move.TargetCollumn + 1].IsWhite != movedPiece.IsWhite)
                            ((Pawn)this[move.TargetRow, move.TargetCollumn + 1]).IsEnPassantLeft = true;
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
                if (movedPiece.IsWhite)
                {
                    if (move.SourceRow == 0 && move.SourceCollumn == 0)
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
            King king = this.GetKing(isForWhite);
            if (isLong)
                king.IsAbleToLongCastle = false;
            else
                king.IsAbleToShortCastle = false;
        }

        //return a list of the moves threatend by white(true) or by black(false) on the current board
        public List<Move> GetThreatningMoves(bool isForWhite)
        {
            List<Move> threatningMoves = new List<Move>();
            for (int row = 0; row < pieces.GetLength(0); row++)
            {
                for (int collumn = 0; collumn < pieces.GetLength(1); collumn++)
                {
                   if(this.pieces[row, collumn].IsWhite==isForWhite)
                    {
                        threatningMoves.AddRange(this.pieces[row, collumn].GetThreateningMoves(this));
                    }
                }
            }
            return threatningMoves;
        }

        //returns the object of the king, white or black
        public King GetKing(bool isForWhite)
        {
            for (int row = 0; row < this.pieces.GetLength(0); row++)
            {
                for (int collumn = 0; collumn < this.pieces.GetLength(1); collumn++)
                {
                    if (this.pieces[row, collumn] is King && this.pieces[row, collumn].IsWhite == isForWhite)
                    {
                        return ((King)this.pieces[row, collumn]);
                    }
                }
            }
            throw new KingNotFoundException();
        }
        public Piece this[int row, int collumn]
        {
            get { return this.pieces[row, collumn]; }
            set { this.pieces[row, collumn] = value; }
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Board other = obj as Board;
            if (other == null)
                return false;
            for(int row=0;row<pieces.GetLength(0);row++)
            {
                for (int collumn = 0; collumn < pieces.GetLength(1); collumn++)
                {
                    if (!(this.pieces[row, collumn].Equals(other[row, collumn])))
                        return false;
                }
            }
            if (this.IsWhiteTurn != other.IsWhiteTurn)
                return false;

            return true;
        }
        public override int GetHashCode()
        {
            int code = 0;
            for (int row = 0; row < pieces.GetLength(0); row++)
            {
                for (int collumn = 0; collumn < pieces.GetLength(1); collumn++)
                {
                    code = code ^ this.pieces[row, collumn].GetHashCode();
                }
            }
            return code;
        }
        public void ResetEnPassantPawns()
        {
            foreach (Piece p in this.pieces)
            {
                if (p is Pawn)
                {
                    ((Pawn)p).IsEnPassantLeft = false;
                    ((Pawn)p).IsEnPassantRight = false;
                }
            }
        }

        public bool isInsufficientMaterialForBlack()
        {
            return isInsufficientMaterial(false);
        }
        public bool isInsufficientMaterialForWhite()
        {
            return isInsufficientMaterial(true);
        }
        public bool isInsufficientMaterial(bool isForWhite)
        {
            List<Piece> whitePieces = new List<Piece>();
            List<Piece> blackPieces = new List<Piece>();
            
            foreach(Piece piece in this.pieces)
            {
                if(!(piece is EmptyPiece))
                {
                    if (piece.IsWhite)
                        whitePieces.Add(piece);
                    else
                        blackPieces.Add(piece);
                }
            }

            //the function makes logical reading sense when isForWhite is true
            // when isForWhite is false we just flip the values between the two lists
            // this is because the check is totally semetrical
            if (!isForWhite)
            {
                List<Piece> temp = whitePieces;
                whitePieces = blackPieces;
                blackPieces = temp;
            }

            if (whitePieces.Count == 1)//lone white king case
                return true;
            else if(blackPieces.Count==1)//lone black king cases
            {
                //removing the white king
                for (int i=0;i<whitePieces.Count; i++)
                {
                    if (whitePieces[i] is King)
                        whitePieces.RemoveAt(i);
                }

                if (whitePieces.Count == 1 && whitePieces[0] is Knight)//king and knight case
                    return true;
                
                if(whitePieces[0] is Bishop)//king and bishops vs lone black king case
                {
                    bool squareColor = ((Bishop)whitePieces[0]).IsLightSquareBishop();
                    for(int i=1;i<whitePieces.Count;i++)
                    {
                        if (whitePieces[i] is Bishop && ((Bishop)whitePieces[i]).IsLightSquareBishop() == squareColor)
                            continue;
                        else
                            return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else//black king not lonely case
            {
                //removing the white king
                for (int i = 0; i < whitePieces.Count; i++)
                {
                    if (whitePieces[i] is King)
                        whitePieces.RemoveAt(i);
                }
                //removing the black king
                for (int i = 0; i < blackPieces.Count; i++)
                {
                    if (blackPieces[i] is King)
                        blackPieces.RemoveAt(i);
                }
                if (whitePieces[0] is Bishop)//checking if all white bishops are on the same colored square, if not, returns false
                {
                    bool squareColor = ((Bishop)whitePieces[0]).IsLightSquareBishop();
                    for (int i = 1; i < whitePieces.Count; i++)
                    {
                        if (whitePieces[i] is Bishop && ((Bishop)whitePieces[i]).IsLightSquareBishop() == squareColor)
                            continue;
                        else
                            return false;
                    }
                    for (int i = 1; i < blackPieces.Count; i++)
                    {
                        if (blackPieces[i] is Bishop && ((Bishop)blackPieces[i]).IsLightSquareBishop() == squareColor)
                            continue;
                        else
                            return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}