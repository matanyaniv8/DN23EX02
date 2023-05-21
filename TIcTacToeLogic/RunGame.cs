using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameUi = TicTacToeUi.TicTacToeUi;
using GameUtils = TIcTacToeUtils.TicTacToeUtils;
using Points = TIcTacToeUtils.Coords;
using GameLogic = TicTacToeLogic.TicTacToeLogic;

namespace RunGame
{
    internal class RunGame
    {
        private static readonly bool r_isValid = true;
        private static readonly int r_MinBoardSize = 3;
        private static readonly int r_MaxBoardSize = 9;
        private static readonly int r_MinNumOfPlayers = 1;
        private static readonly int r_MaxNumOfPlayers = 2;
        private const int k_XHasWon = 1;
        private const int k_CircleHasWon = 2;
        private const int k_PlayerWantsToQuitIntSign = -1;
        public static void Main()
        {
            RunProgram();
        }

        private static void RunProgram()
        {
            Points minAndMaxNumberOfPlayers = new Points(1, 2);
            Points minMaxSizeOfBoard = new Points(3, 9);
            Points playerMoveIndices = new Points(0, 0);
            int[] results = new int[r_MaxNumOfPlayers];
            byte boardSize = 0;
            byte numOfPlayers = 0;
            int roundResult = 0;

            while(!minMaxSizeOfBoard.IsUserWantsToQuit && !minAndMaxNumberOfPlayers.IsUserWantsToQuit && !playerMoveIndices.IsUserWantsToQuit)
            {
                if (boardSize == 0 && numOfPlayers ==0)
                {
                    boardSize = (byte)GameUi.getBoardSizeFromUser(minMaxSizeOfBoard);
                    numOfPlayers = (byte)GameUi.GetNumberOfPlayers(minAndMaxNumberOfPlayers);
                }

                roundResult = singleRun(boardSize, numOfPlayers, ref playerMoveIndices);
                GameUtils.UpdatesResultArray(results, roundResult);

                if (!GameUi.IsPlayerWantsAnotherRound(results))
                {
                    break;
                }
            }

            return;
        }

        private static int singleRun(byte i_BoardSize, byte i_NumOfPlayers, ref Points i_PlayerMovesIndices)
        {
            GameLogic round = new GameLogic(i_BoardSize);
            GameUtils.PrintBoard(round.Board);
            int lastPlayerPlayed = 0;

            while (!i_PlayerMovesIndices.IsUserWantsToQuit && !round.isThereAWin())
            {

                if(!round.FirstPlayerTurn && i_NumOfPlayers == 1)
                {
                    i_PlayerMovesIndices = GameUtils.createRandomMoveIndices(round);
                }
                else
                {
                    i_PlayerMovesIndices = GameUi.GetPlayerMoveIndices(round);
                }

                lastPlayerPlayed = GameUtils.MakeTheMove(round, i_PlayerMovesIndices);

                if (round.IsThereTie())
                {
                    lastPlayerPlayed = 0;
                    break;
                }
            }

            return lastPlayerPlayed;
        }
    }
}
