using ChessSharp;
using ChessTest;
using ChessSharp.SquareData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTest
{
    public class RandomBot
    {
        private Random random= new Random();
        
        public Move GetMove(GameBoard board)
        {
            var legalMoves = ChessUtilities.GetValidMoves(board);
            var movementsChecked = 0;
            var movementsToCheck = legalMoves.Count;
            while (movementsChecked<movementsToCheck)
            {
                var randomIndex = random.Next(0, legalMoves.Count);
                var candidateMove = legalMoves[randomIndex];
                if (board.IsValidMove(candidateMove))
                {
                    return candidateMove;
                }
                else
                {
                    legalMoves.Remove(candidateMove);
                    movementsChecked++;
                }
            }
            var legalMoves2 = ChessUtilities.GetValidMoves(board);
            return legalMoves2[0];
            
        }
    }
}
