using ChessSharp;
using ChessTest;
using ChessSharp.Pieces;
using System;
using ChessSharp.SquareData;

public class Program
{
    public static void Main(string[] args)
    {
        var pgnMovements = new List<string>();
        var board = new GameBoard();
        var bot1 = new RandomBot();
        var bot2 = new RandomBot();

        var movementCount = 1;

        while (!GetterManager.GameFinished(board))
        {
            var move1 = bot1.GetMove(board);

            var notation1 = NotationFromMove(move1,board);
            board.MakeMove(move1, true);

            if (GetterManager.GameFinished(board)) break;

            var move2 = bot2.GetMove(board);

            var notation2 = NotationFromMove(move2,board);
            board.MakeMove(move2, true);

            pgnMovements.Add($"{movementCount}. {notation1} {notation2} ");

            if (GetterManager.GameFinished(board)) break;

            movementCount++;

        }
        var pgn = string.Join(" ", pgnMovements);
        Console.WriteLine(pgn);

    }
    public static string NotationFromMove(Move move, GameBoard board)
    {
        var capturedPieceName = GetterManager.CapturedPieceInMove(move, board);
        var capturedPiece = capturedPieceName == "null" ? "": "x";
        Piece piece = GetterManager.GetPieceBeforeMove(move,board);
        string pieceName = GetterManager.GetPieceNameAbbreviation(piece);
        if (pieceName == "P") pieceName = "";

        if (capturedPiece == "x" && pieceName == "") pieceName = move.Source.File.ToString().ToLower(); 
        return $"{pieceName}{capturedPiece}{move.Destination.ToString().ToLower()}";
    }
}