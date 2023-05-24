using GameUi = InverseTicTacToeUi.InverseTicTacToeUi;
using GameUtils = InverseTicTacToeUtils.InverseTicTacToeUtils;
using Points = InverseTicTacToeUtils.PointsForGame;
using GameLogic = ReverseTicTacToeLogic.ReverseTicTacToeLogic;

namespace RunGame
{
    internal class RunGame
    {
        private const int k_MinBoardSize = 3;
        private const int k_MaxBoardSize = 9;
        private const int k_MiNumOfPlayers = 1;
        private const int k_MaxNumOfPlayers = 2;

        public static void Main()
        {
            RunProgram();
        }

        private static void RunProgram()
        {
            Points minAndMaxNumberOfPlayers = new Points(k_MiNumOfPlayers, k_MaxNumOfPlayers);
            Points minMaxSizeOfBoard = new Points(k_MinBoardSize, k_MaxBoardSize);
            Points playerMoveIndices = new Points(0, 0);
            int[] results = new int[k_MaxNumOfPlayers];
            int boardSize = 0;
            int numOfPlayers = 0;
            GameLogic.BoardSigns roundResult = 0;

            while (playerMoveIndices.IsUserWantsToQuit == false)
            {
                if (boardSize == 0 && numOfPlayers == 0)
                {
                    boardSize = GameUi.getBoardSizeFromUser();

                    if(boardSize == GameUi.k_PlayerWantsToQuitIntSign)
                    {
                        break;
                    }
                    numOfPlayers = GameUi.GetNumberOfPlayers(minAndMaxNumberOfPlayers);

                    if(numOfPlayers == GameUi.k_PlayerWantsToQuitIntSign)
                    {
                        break;
                    }

                }

                roundResult = singleRun(boardSize, numOfPlayers, ref playerMoveIndices);
                GameUtils.UpdatesResultArray(results, roundResult);

                if(playerMoveIndices.IsUserWantsToQuit)
                {
                    break;
                }

                if (!GameUi.IsPlayerWantsAnotherRound(results))
                {
                    break;
                }
            }

            return;
        }

        private static GameLogic.BoardSigns singleRun(int i_BoardSize, int i_NumOfPlayers, ref Points i_PlayerMovesIndices)
        {
            GameLogic round = new GameLogic(i_BoardSize);
            GameUtils.PrintBoard(round);
            GameLogic.BoardSigns lastPlayerPlayed = 0;

            while (!i_PlayerMovesIndices.IsUserWantsToQuit && !round.IsThereAWin())
            {
                if (!round.FirstPlayerTurn && i_NumOfPlayers == 1)
                {
                    i_PlayerMovesIndices = GameUtils.createRandomMoveIndices(round);
                }
                else
                {
                    i_PlayerMovesIndices = GameUi.GetPlayerMoveIndices(round);
                }

                lastPlayerPlayed = GameUtils.MakeTheMove(round, i_PlayerMovesIndices);
                GameUtils.PrintLastMoveIndices(i_PlayerMovesIndices, lastPlayerPlayed);

                if (round.IsThereAWin()){
                    lastPlayerPlayed = round.GetPlayerSign();
                    break;
                }

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
