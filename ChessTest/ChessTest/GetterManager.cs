using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessSharp;
using ChessSharp.SquareData;
using ChessTest;
namespace ChessTest
{
    public class GetterManager
    {
        public static Piece GetPieceBeforeMove(Move move, GameBoard board)
        {
            Square square = new Square(move.Source.File, move.Source.Rank);
            Piece piece = board[square.File, square.Rank];
            return piece;
        }
        public static string CapturedPieceInMove(Move move, GameBoard board) 
        {
            Square square = new Square(move.Destination.File, move.Destination.Rank);
            Piece piece = board[square.File, square.Rank];
            if (piece == null) return "null";
            var pieceName = piece.GetType().Name;
            Console.WriteLine(pieceName);
            return pieceName;

        }
        public static string GetPieceNameAbbreviation(Piece piece)
        {
            var name = piece.GetType().Name;   
            Dictionary<string, string> chess_pieces = new Dictionary<string, string> { { "king", "K" }, { "queen", "Q" }, { "rook", "R" }, { "bishop", "B" }, { "knight", "N" }, { "pawn", "P" } };

            name = name.ToLower();
            if (chess_pieces.TryGetValue(name, out string? abbreviation)) 
            { 
                return abbreviation; 
            }
            else 
            {
                return "Abbreviation not found"; 
            }
            
        }
        public static bool GameFinished(GameBoard board)
        {
            if (board.GameState==GameState.Stalemate) return true;
            else if (board.GameState == GameState.WhiteWinner) return true;
            else if (board.GameState == GameState.BlackWinner) return true;
            else if (board.GameState == GameState.Draw) return true;
            else return false;
        }
        
    }
    
}
