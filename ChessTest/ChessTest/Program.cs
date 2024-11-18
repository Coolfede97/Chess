using ChessSharp;
using ChessTest;
using ChessSharp.Pieces;
using System;
using ChessSharp.SquareData;

public class Program
{
    public static void Main(string[] args)
    {
        var board = new GameBoard();
        var bot1 = new RandomBot();
        var bot2 = new RandomBot();

        var pgnMovements = new List<string>();
        var movementCount = 1;

        while (board.GameState==GameState.NotCompleted)
        {
            var move1 = bot1.GetMove(board);

            Piece piece1 = GetterManager.GetPieceBeforeMove(move1,board);
            string piece1Name = GetterManager.GetPieceNameAbbreviation(piece1);
            if (piece1Name == "P") piece1Name = "";
            var capturedPieceName = GetterManager.CapturedPieceInMove(move1, board); 
            if (capturedPieceName!="null") Console.WriteLine(capturedPieceName);
            board.MakeMove(move1, true);

            pgnMovements.Add($"{movementCount}. {piece1Name}{move1.Destination.ToString().ToLower()}");

            if (GetterManager.GameFinished(board)) break;

            var move2 = bot2.GetMove(board);

            Piece piece2 = GetterManager.GetPieceBeforeMove(move2 ,board);
            string piece2Name = GetterManager.GetPieceNameAbbreviation(piece2);
            if (piece2Name == "P") piece2Name = "";

            board.MakeMove(move2, true);
            pgnMovements.Add($" {piece2Name}{move2.Destination.ToString().ToLower()}");

            if (GetterManager.GameFinished(board)) break;

            movementCount++;
        }
        var pgn = $"[Event \"Partida de prueba\"]\n[Site \"Chess.com\"]\n[Date \"{DateTime.Now:yyyy.MM.dd}\"]\n" +
                  "[Round \"1\"]\n[White \"RandomBot1\"]\n[Black \"RandomBot2\"]\n[Result \"*\"]\n\n" +
                  string.Join(" ", pgnMovements);
        Console.WriteLine(pgn);

    }
}