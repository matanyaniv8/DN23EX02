using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex02.ConsoleUtils;
using Board = TicTacToeLogic.TicTacToeLogic;

namespace TIcTacToeUtils
{
    internal class TicTacToeUtils
    {
        private static readonly bool r_isValid = true;

        internal static bool isUserInputAValidNumber(int i_UserInput, Coords i_MinMaxValue)
        {
            bool isInputValid = !r_isValid;

            if (i_UserInput >= i_MinMaxValue.RowIndex && i_UserInput <= i_MinMaxValue.ColumnIndex)
            {
                isInputValid = r_isValid;
            }

            return isInputValid;
        }

        internal static byte MakeTheMove(Board i_GameBoard, Coords i_UserIndices)
        {
            byte playerMoveId = 0;

            if (isSlotExistAndEmpty(i_GameBoard, i_UserIndices))
            {
                playerMoveId = i_GameBoard.PlayMove(i_UserIndices.RowIndex - 1, i_UserIndices.ColumnIndex - 1);
            }
            PrintBoard(i_GameBoard.Board);
     
            return playerMoveId;
        }

        internal static Coords createRandomMoveIndices(Board i_GameBoard)
        {
            Random rnd_generates = new Random();
            Coords indices = new Coords(0,0);
            bool isRandomSlotAvailable = !r_isValid;

            while (isRandomSlotAvailable == !r_isValid)
            {
                indices.RowIndex = (byte)rnd_generates.Next(1, i_GameBoard.BoardSize + 1);
                indices.ColumnIndex = (byte)rnd_generates.Next(1, i_GameBoard.BoardSize + 1);

                if (isSlotExistAndEmpty(i_GameBoard, indices))
                {
                    isRandomSlotAvailable = r_isValid;
                }
            }

            return indices;
        }

        internal static bool isSlotExistAndEmpty(Board i_GameBoard, Coords i_Indices)
        {
            bool isEmpty = i_GameBoard.isSlotAvailable(i_Indices.RowIndex - 1, i_Indices.ColumnIndex - 1);
         
            return isEmpty;
        }

        internal static bool isIndicesAreWithinRangeOfBoardSIze(Board i_GameBoard, Coords i_Indices)
        {
            bool isIndicesWithinRange = i_GameBoard.isIndicesAreWithinRangeOfBoardSIze(i_Indices.RowIndex -1, i_Indices.ColumnIndex -1);

            return isIndicesWithinRange;
        }

        internal static void PrintBoard(byte[,] i_Board)
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

        private static String getLineFromBoard(int i_RowIndex, byte[,] i_GameBoard)
        {
            StringBuilder lineFromBoard = new StringBuilder();
            lineFromBoard.Append((i_RowIndex + 1).ToString() + "|");

            for (int i = 0; i < i_GameBoard.GetLength(0); i++)
            {
                int currentSlotValue = i_GameBoard[i_RowIndex, i];

                if (currentSlotValue == 2)
                {
                    lineFromBoard.Append("O  |");
                }

                else if (currentSlotValue == 1)
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

        internal static void UpdatesResultArray(int [] i_GameResults, int i_LastRoundResult)
        {
            if (i_LastRoundResult != 0)
            {
                i_GameResults[i_LastRoundResult - 1]++;
            }
        }
    }

    public struct Coords
    {
        private bool m_IsUserWantsToQuit;
        private byte m_RowIndex;
        private byte m_ColumnIndex;
        internal Coords(byte i_RowIndex, byte i_ColumnIndxex)
        {
            m_RowIndex = i_RowIndex;
            m_ColumnIndex = i_ColumnIndxex;
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
        internal byte RowIndex
        {
            get 
            { 
                return m_RowIndex; 
            }

            set 
            {
                m_RowIndex = value; 
            }
        }
        internal byte ColumnIndex
        {
            get 
            { 
                return m_ColumnIndex; 
            }

            set 
            { 
                m_ColumnIndex = value;
            }
        }
    }
}
