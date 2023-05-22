using System;
using System.Text;
using Board = InverseTicTacToeLogic.InverseTicTacToeLogic;

namespace InverseTicTacToeUtils
{
    internal class InverseTicTacToeUtils
    {
        private static readonly bool r_isValid = true;
        private const byte r_FirstPlayerSign = 1;
        private const byte r_SecondPlayerSign = 2;

        internal static bool isUserInputAValidNumber(int i_UserInput, PointsForGame i_MinMaxValue)
        {
            bool isInputValid = !r_isValid;

            if (i_UserInput >= i_MinMaxValue.RowIndexOrMinValue && i_UserInput <= i_MinMaxValue.ColumnIndexOrMaxValue)
            {
                isInputValid = r_isValid;
            }

            return isInputValid;
        }

        internal static Board.BoardSigns MakeTheMove(Board i_GameBoard, PointsForGame i_UserIndices)
        {
            Board.BoardSigns playerMoveId = 0;

            if (isSlotExistAndEmpty(i_GameBoard, i_UserIndices))
            {
                playerMoveId = i_GameBoard.PlayMove(i_UserIndices.RowIndexOrMinValue - 1, i_UserIndices.ColumnIndexOrMaxValue - 1);
            }
            PrintBoard(i_GameBoard.Board);

            return playerMoveId;
        }

        internal static PointsForGame createRandomMoveIndices(Board i_GameBoard)
        {
            Random rnd_generates = new Random();
            PointsForGame indices = new PointsForGame(0, 0);
            bool isRandomSlotAvailable = !r_isValid;

            while (isRandomSlotAvailable == !r_isValid)
            {
                indices.RowIndexOrMinValue = (byte)rnd_generates.Next(1, i_GameBoard.BoardSize + 1);
                indices.ColumnIndexOrMaxValue = (byte)rnd_generates.Next(1, i_GameBoard.BoardSize + 1);

                if (isSlotExistAndEmpty(i_GameBoard, indices))
                {
                    isRandomSlotAvailable = r_isValid;
                }
            }

            return indices;
        }

        internal static bool isSlotExistAndEmpty(Board i_GameBoard, PointsForGame i_Indices)
        {
            bool isEmpty = i_GameBoard.isSlotAvailable(i_Indices.RowIndexOrMinValue - 1, i_Indices.ColumnIndexOrMaxValue - 1);

            return isEmpty;
        }

        internal static bool isIndicesAreWithinRangeOfBoardSIze(Board i_GameBoard, PointsForGame i_Indices)
        {
            bool isIndicesWithinRange = i_GameBoard.isIndicesAreWithinRangeOfBoardSIze(i_Indices.RowIndexOrMinValue - 1, i_Indices.ColumnIndexOrMaxValue - 1);

            return isIndicesWithinRange;
        }

        internal static void PrintBoard(Board.BoardSigns[,] i_Board)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            int boardSize = i_Board.GetLength(0);
            String linesSeprator = lineSeprator(boardSize);
            StringBuilder boardPrint = new StringBuilder();
            boardPrint.AppendLine(columnsNumbers(boardSize));

            for (int i = 1; i <= 2 * boardSize; i++)
            {
                if (i % 2 == 0)
                {
                    boardPrint.AppendLine(linesSeprator);
                }

                else
                {
                    boardPrint.AppendLine(getLineFromBoard(i / 2, i_Board));
                }
            }

            Console.WriteLine(boardPrint.ToString());
        }

        private static String lineSeprator(int i_BoardSize)
        {
            String slotSeperator = "====";
            StringBuilder lineSeperator = new StringBuilder();

            for (int i = 0; i < i_BoardSize; i++)
            {
                lineSeperator.Append(slotSeperator);
            }

            lineSeperator.Append("==");

            return lineSeperator.ToString();
        }

        private static String columnsNumbers(int i_BoardSize)
        {
            StringBuilder columnsNumbers = new StringBuilder();

            for (int i = 1; i <= i_BoardSize; i++)
            {
                if (i == 1)
                {
                    columnsNumbers.Append("  " + i.ToString() + "   ");
                }
                else
                {
                    columnsNumbers.Append(i.ToString() + "   ");
                }

            }

            return columnsNumbers.ToString();
        }

        private static String getLineFromBoard(int i_RowIndex, Board.BoardSigns[,] i_GameBoard)
        {
            StringBuilder lineFromBoard = new StringBuilder();
            lineFromBoard.Append((i_RowIndex + 1).ToString() + "|");

            for (int i = 0; i < i_GameBoard.GetLength(0); i++)
            {
                Board.BoardSigns currentSlotValue = i_GameBoard[i_RowIndex, i];

                if (currentSlotValue == Board.BoardSigns.LastPlayerSign)
                {
                    lineFromBoard.Append("O  |");
                }

                else if (currentSlotValue == Board.BoardSigns.FirstPlayerSign)
                {
                    lineFromBoard.Append("X  |");
                }

                else
                {
                    lineFromBoard.Append("   |");
                }
            }

            return lineFromBoard.ToString();
        }

        internal static void UpdatesResultArray(int[] i_GameResults, Board.BoardSigns i_LastRoundResult)
        {
            if (i_LastRoundResult != 0)
            {
                i_GameResults[(int)i_LastRoundResult - 1]++;
            }
        }

        internal static void PrintLastMoveIndices(PointsForGame i_Indices, Board.BoardSigns i_LastPlayerId)
        {
            char lastXorCircleMove = (i_LastPlayerId == Board.BoardSigns.FirstPlayerSign) ? 'X' : 'O';
            string lastMoveLocation = String.Format(@"{0}'s Choice is {1}:{2}", lastXorCircleMove.ToString(),i_Indices.RowIndexOrMinValue, i_Indices.ColumnIndexOrMaxValue);
            Console.WriteLine(lastMoveLocation);
        }

        internal static void PrintErrorMessageInColor(string i_ErrorMessage) 
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(i_ErrorMessage);
            Console.ResetColor();
        }
    }

    public struct PointsForGame
    {
        private bool m_IsUserWantsToQuit;
        private byte m_RowIndexOrMinValue;
        private byte m_ColumnIndexOrMaxValue;
        internal PointsForGame(byte i_RowIndexOrMinValueToSave, byte i_ColumnIndexOrMaxValueToSave)
        {
            m_RowIndexOrMinValue = i_RowIndexOrMinValueToSave;
            m_ColumnIndexOrMaxValue = i_ColumnIndexOrMaxValueToSave;
            m_IsUserWantsToQuit = false;
        }

        internal bool IsUserWantsToQuit
        {
            get
            {
                return m_IsUserWantsToQuit;
            }
            set
            {
                m_IsUserWantsToQuit = value;
            }
        }
        internal byte RowIndexOrMinValue
        {
            get
            {
                return m_RowIndexOrMinValue;
            }

            set
            {
                m_RowIndexOrMinValue = value;
            }
        }
        internal byte ColumnIndexOrMaxValue
        {
            get
            {
                return m_ColumnIndexOrMaxValue;
            }

            set
            {
                m_ColumnIndexOrMaxValue = value;
            }
        }
    }
}
