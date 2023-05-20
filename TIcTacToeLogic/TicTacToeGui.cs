using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Ex02.ConsoleUtils;
using Board = TicTacToeLogic.TicTacToeLogic;

namespace TicTacToeGui
{
    internal class TicTacToeGui
    {
        
        private const Boolean m_isValid = true;
        
        public static void Main(string[] args)
        {
            Board board = new Board(8);
            byte[,] board1 = board.Board;
            board1[1, 1] = 1;
            byte[] indices = new byte[2] {2,2};
            Console.WriteLine(createRandomMoveIndices(board)[1]);
            indices = GetPlayerMoveIndices(board);
            PrintBoard(board1);


        }

        internal static byte getBoardSizeFromUser(int i_MinAcceptedValue, int i_MaxAcceptedValue)
        {
            String openingMessage = String.Format(@"Hi, Welcome.
                Please enter a number in range {0} -{1}, as the board's size.", i_MinAcceptedValue, i_MaxAcceptedValue);

            Console.WriteLine(openingMessage);
            String userInput = Console.ReadLine();
            byte userInputAsByte;
            bool digitToByte = byte.TryParse(userInput, out userInputAsByte);
            bool isUserInputValid = !m_isValid;

            if (isUserInputValid == !m_isValid || digitToByte == !m_isValid)
            {
                String message = String.Format(@"Wrong input, please enter size board in range {0} - {1}.", i_MinAcceptedValue, i_MaxAcceptedValue);
                userInputAsByte = GetInputFromUserOnNotValidInput(message, i_MinAcceptedValue, i_MaxAcceptedValue);
            }

            return userInputAsByte;
        }

        internal static byte GetNumberOfPlayers(int i_minNumberOfPlayers, int i_MaxNumberOfPlayers)
        {
            String numOfPlayersOptionsMessage = String.Format(@"How many players?
it's a game for 2, to play against the computer press 1, else press 2.");
            Console.WriteLine(numOfPlayersOptionsMessage);

            String userInput = Console.ReadLine();
            byte numberOfPlayers;
            bool isInputValidNumber = byte.TryParse(userInput, out numberOfPlayers);

            if (isInputValidNumber == !m_isValid || numberOfPlayers > i_MaxNumberOfPlayers || numberOfPlayers < i_minNumberOfPlayers)
            {
                String wrongInputMessage = String.Format(@"It's a game for 2.
If you would like to play against the computer, press 1, else press 2");

                numberOfPlayers = GetInputFromUserOnNotValidInput(wrongInputMessage, i_minNumberOfPlayers, i_MaxNumberOfPlayers);
            }

            return numberOfPlayers;
        }

        internal static byte[] GetPlayerMoveIndices(Board i_GameBoard)
        {
            byte numOfRequiredNumbers = 2;
            byte[] indices = askPlayerForAMove();
            string possibleErrorType = "";
            bool isMoveApproved = !m_isValid;

            while(isMoveApproved != m_isValid)
            {
                if (numOfRequiredNumbers == indices.Length && i_GameBoard.NumberOfMovesRemains != 0)
                {
                    if (isIndicesAreWithinRangeOfBoardSIze(i_GameBoard, indices) == m_isValid)
                    {
                        if(isSlotEmpty(i_GameBoard, indices))
                        {
                            isMoveApproved = m_isValid;
                            break;
                        }
                        else
                        {
                            possibleErrorType = "Slot is occupied, choose another slot.";
                        }
                    }
                    else
                    {
                        possibleErrorType = "Indices are not in range of the board size.";
                    }

                }
                else
                {
                    possibleErrorType = (i_GameBoard.NumberOfMovesRemains != 0) ? "To many indices has inserted." : "Out Of Moves";
                }

                Console.WriteLine(possibleErrorType);
                indices = askPlayerForAMove();
            }
            
            return indices;
        }

        private static byte[] askPlayerForAMove()
        {
            string askForIndices = String.Format(@"Enter you're move with the format - Row Number: Column Number.");
            Console.WriteLine(askForIndices);
            char seperator = ',';
            string userInput = Console.ReadLine();
            byte[] indices = convertStringsToBytesArray(userInput,seperator);
            
            return indices;
        }

        private static bool isSlotEmpty(Board i_GameBoard, byte[] i_Indices)
        {
            int rowIndex = i_Indices[0];
            int columnIndex = i_Indices[1];
            bool isEmpty = i_GameBoard.CanIMakeAMove(rowIndex -1 , columnIndex -1);

            return isEmpty;
        }

        private static bool isIndicesAreWithinRangeOfBoardSIze(Board i_GameBoard, byte[] i_indices)
        {
            bool isIndicesWithinRange = m_isValid;
            int boardSize = i_GameBoard.Board.GetLength(0);

            foreach(byte index in i_indices)
            {
                if(index > boardSize || index <= 0)
                {
                    isIndicesWithinRange = !m_isValid;
                }
            }

            return isIndicesWithinRange;
        }

        private static byte[] convertStringsToBytesArray(string i_Input, char i_seperator)
        {
            string[] subInputs = i_Input.Split(i_seperator);
            List<byte> convertedBytes = new List<byte>();
            byte convertedByte = 0;
            bool digitToByte = m_isValid;

            foreach(string subInput in subInputs)
            {
                digitToByte = byte.TryParse((string)subInput, out convertedByte);

                if(digitToByte == m_isValid)
                {
                    convertedBytes.Add(convertedByte);
                }
            }
            
            return convertedBytes.ToArray();
        }
        
        internal static byte GetInputFromUserOnNotValidInput(String i_ErrorMessage, int i_MinAcceptedValue,  int i_MaxAcceptedValue)
        {
            Console.WriteLine(i_ErrorMessage);

            byte numberToReturn =0;
            String userInput = Console.ReadLine();
            bool digitToByte = byte.TryParse(userInput, out numberToReturn);
            bool isUserInputValid = !m_isValid;

            while(isUserInputValid == !m_isValid && digitToByte == !m_isValid)
            {
                Console.WriteLine(i_ErrorMessage);
                userInput = Console.ReadLine();
                digitToByte = byte.TryParse(userInput, out numberToReturn);
                isUserInputValid = isUserInputAValidNumber(numberToReturn, i_MinAcceptedValue, i_MaxAcceptedValue);
            }

            return numberToReturn;
        }

        internal static bool isUserInputAValidNumber(int i_UserInput, int i_MinBoardSize, int i_MaxBoardSize)
        {
            bool isInputValid = !m_isValid;

            if (i_UserInput >= i_MinBoardSize || i_MaxBoardSize <= 9) 
            {
                isInputValid = m_isValid;
            }

            return isInputValid;
        }

        internal static void PrintBoard(byte[,] i_Board)
        {
            Ex02.ConsoleUtils.Screen.Clear();
            int boardSize = i_Board.GetLength(0);
            String linesSeprator = lineSeprator(boardSize);
            StringBuilder boardPrint = new StringBuilder();
            boardPrint.AppendLine(columnsNumbers(boardSize));

            for ( int i = 1; i <= 2 *boardSize; i++)
            {
                if(i % 2 == 0)
                {
                    boardPrint.AppendLine(linesSeprator);
                }

                else
                {
                    boardPrint.AppendLine(getLineFromBoard(i/2, i_Board));
                }
            }

            Console.WriteLine(boardPrint.ToString());
        }

        private static String lineSeprator(int i_BoardSize)
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

        internal static byte[] createRandomMoveIndices(Board i_GameBoard)
        {
            Random rnd_generates = new Random();

            byte numOfRequiredNumbers = 2;
            int boadSize = i_GameBoard.Board.GetLength(0);
            byte[] indices = new byte[numOfRequiredNumbers];
            bool isRandomSlotAvailable = !m_isValid;
           
            while(isRandomSlotAvailable == !m_isValid)
            {
                for (int i = 0; i < numOfRequiredNumbers; i++)
                {
                    indices[i] = (byte)rnd_generates.Next(1, boadSize + 1);
                }

                if(isSlotEmpty(i_GameBoard, indices))
                {
                    isRandomSlotAvailable = m_isValid;
                    break;
                }
            }
            
            return indices;
        }
    }
}
