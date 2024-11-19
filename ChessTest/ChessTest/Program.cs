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

        while (board.GameState==GameState.NotCompleted)
        {
            var move1 = bot1.GetMove(board);

            NotationFromMove(move1,board,pgnMovements,movementCount);
            board.MakeMove(move1, true);

            if (GetterManager.GameFinished(board)) break;

            movementCount++;

            var move2 = bot2.GetMove(board);

            NotationFromMove(move2,board,pgnMovements,movementCount);
            board.MakeMove(move2, true);

            if (GetterManager.GameFinished(board)) break;

            movementCount++;

        }
        var pgn = $"[Event \"Partida de prueba\"]\n[Site \"Chess.com\"]\n[Date \"{DateTime.Now:yyyy.MM.dd}\"]\n" +
                  "[Round \"1\"]\n[White \"RandomBot1\"]\n[Black \"RandomBot2\"]\n[Result \"*\"]\n\n" +
                  string.Join(" ", pgnMovements);
        Console.WriteLine(pgn);

    }
    public static void NotationFromMove(Move move, GameBoard board, List<string> pgnMovements, int movementCount)
    {
        Piece piece = GetterManager.GetPieceBeforeMove(move,board);
        string pieceName = GetterManager.GetPieceNameAbbreviation(piece);
        if (pieceName == "P") pieceName = "";
        var capturedPieceName = GetterManager.CapturedPieceInMove(move, board);
        var capturedPiece = capturedPieceName == "null" ? "": "x";
        pgnMovements.Add($"{movementCount}. {pieceName}{capturedPiece}{move.Destination.ToString().ToLower()}");
    }
}