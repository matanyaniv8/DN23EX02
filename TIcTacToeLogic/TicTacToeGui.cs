using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board = TIcTacToeLogic;

namespace TIcTacToeLogic
{
    internal class TicTacToeGui
    {
        
        private const Boolean m_isValid = true;
        private readonly byte r_MinBoardSize = 3;
        private readonly byte r_MaxBoardSize = 9;
        private byte m_BoardSize = 0;
        private byte m_NumOfPlayers =1;
        public static void Main(string[] args)
        {
            byte[,] board = new byte[3,3]{{ 1,2,1}, { 1,2,2}, { 1,1,2}};
            printBoard(board);

        }

        /*
        private byte getBoardSize()
        {
            String openingMessage = String.Format(@"Hi, Welcome.
                Please enter a number in range {0} -{1}, as the board's size.", r_MinBoardSize, r_MaxBoardSize);

            Console.WriteLine(openingMessage);
            String userInput = Console.ReadLine();
            int userInputAsInt;
            bool digitToInt = int.TryParse(userInput, out userInputAsInt);
            bool isUserInputValid = !m_isValid;

            while(isUserInputValid == !m_isValid && digitToInt==!m_isValid) 
            {
                String message = String.Format(@"Wrong input, please enter size board in range {0} - {1}.", r_MinBoardSize, r_MaxBoardSize);
                Console.WriteLine(message); 

                userInput = Console.ReadLine();

                digitToInt = int.TryParse(userInput,out userInputAsInt);
                isUserInputValid = isUserNumberValid(userInputAsInt, r_MinBoardSize, r_MaxBoardSize);
            }

            return (byte)userInputAsInt;
        }

        private static bool isUserNumberValid(int i_UserInput, int i_MinBoardSize, int i_MaxBoardSize)
        {
            bool isInputValid = !m_isValid;

            if (i_UserInput >= i_MinBoardSize || i_MaxBoardSize <= 9) 
            {
                isInputValid = m_isValid;
            }

            return isInputValid;
        }

        private byte getNumberOfPlayers()
        {
            Console.WriteLine("How many players? it's a game for 2.");
            String userInput = Console.ReadLine();
            byte numberOfPlayers;
            bool isInputValidNumber = byte.TryParse(userInput, out numberOfPlayers);
            
            while(isInputValidNumber == !m_isValid && m_NumOfPlayers > 2 && m_NumOfPlayers < 1) 
            { 
                Console.WriteLine("It's a game for 2, if you would like to play against the computer, press 1, else press 2");
                userInput = Console.ReadLine();
                isInputValidNumber = byte.TryParse(userInput, out numberOfPlayers);
            }

            return numberOfPlayers;
        }*/

        private static void printBoard(byte[,] i_Board)
        {
            int boardSize = i_Board.GetLength(0);
            String lineSeprator = LineSeprator(boardSize);
            StringBuilder boardPrint = new StringBuilder();

            for( int i = 0; i < 2 *boardSize; i++)
            {
                if(i % 2 == 0)
                {
                    if (i == 0)
                    {
                        boardPrint.AppendLine(ColumnsNumbers(boardSize));
                    }

                    else
                    {
                        boardPrint.AppendLine(lineSeprator);
                    }
                }

                else
                {
                    boardPrint.AppendLine(getLineFromBoard(i/2, i_Board));
                }
            }

            Console.WriteLine(boardPrint.ToString());
        }

        private static String LineSeprator(int i_BoardSize)
        {
            String slotSeperator = "====";
            StringBuilder lineSeperator = new StringBuilder();

            for( int i = 0; i < i_BoardSize; i++)
            {
                lineSeperator.Append(slotSeperator);
            }

            lineSeperator.Append("==");

            return lineSeperator.ToString();
        }

        private static String ColumnsNumbers(int i_BoardSize)
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
            
            for(int i=0; i < i_GameBoard.GetLength(0); i++)
            {
                int currentSlotValue = i_GameBoard[i_RowIndex, i];
                
                if(currentSlotValue == 2)
                {
                    lineFromBoard.Append("O  |");
                }

                else if(currentSlotValue == 1)
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
    }
}
