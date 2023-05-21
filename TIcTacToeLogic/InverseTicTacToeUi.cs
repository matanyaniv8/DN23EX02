﻿using System;
using GameUtils = InverseTicTacToeUtils.InverseTicTacToeUtils;
using PointsForGame = InverseTicTacToeUtils.PointsForGame;
using Board = InverseTicTacToeLogic.InverseTicTacToeLogic;

namespace InverseTicTacToeUi
{
    internal class InverseTicTacToeUi
    {
        private const String k_PlayerWantsToQuitSign1 = "Q";
        private const String k_PlayerWantsToQuitSign2 = "q";
        private const string k_PlayerWantsAnotherRoundSign1 = "R";
        private const string k_PlayerWantsAnotherRoundSign2 = "r";
        private const int k_PlayerWantsToQuitIntSign = -1;

        private static readonly bool r_isValid = true;

        internal static int getBoardSizeFromUser(PointsForGame i_MinANdMaxAcceptedValue)
        {
            String openingMessage = String.Format(@"Hi, Welcome to TicTacToe game.
Please enter a number in range {0} - {1}, as the board's size.", i_MinANdMaxAcceptedValue.RowIndexOrMinValue, i_MinANdMaxAcceptedValue.ColumnIndexOrMaxValue);

            Console.WriteLine(openingMessage);
            String userInput = Console.ReadLine();
            int userInputAsByte = k_PlayerWantsToQuitIntSign;

            if (!isUserWantsToQuit(userInput, ref i_MinANdMaxAcceptedValue))
            {
                bool digitToByte = int.TryParse(userInput, out userInputAsByte);
                bool isUserInputValid = GameUtils.isUserInputAValidNumber(userInputAsByte, i_MinANdMaxAcceptedValue);

                if (isUserInputValid == !r_isValid)
                {
                    String message = String.Format(@"Wrong input, please enter size board in range {0} - {1}.", i_MinANdMaxAcceptedValue.RowIndexOrMinValue, i_MinANdMaxAcceptedValue.ColumnIndexOrMaxValue);
                    userInputAsByte = getInputFromUserOnNotValidInput(message, i_MinANdMaxAcceptedValue);
                }
            }

            return userInputAsByte;
        }

        internal static int GetNumberOfPlayers(PointsForGame i_MinAndMaxNumOfPlayers)
        {
            String numOfPlayersOptionsMessage = String.Format(@"How many players?
it's a game for 2, to play against the computer press 1, else press 2.");
            Console.WriteLine(numOfPlayersOptionsMessage);

            String userInput = Console.ReadLine();
            int numberOfPlayers = k_PlayerWantsToQuitIntSign;

            if (!isUserWantsToQuit(userInput, ref i_MinAndMaxNumOfPlayers))
            {
                bool isInputValidNumber = int.TryParse(userInput, out numberOfPlayers);

                if (isInputValidNumber == !r_isValid || numberOfPlayers > i_MinAndMaxNumOfPlayers.ColumnIndexOrMaxValue || numberOfPlayers < i_MinAndMaxNumOfPlayers.RowIndexOrMinValue)
                {
                    String wrongInputMessage = String.Format(@"It's a game for 2.
    If you would like to play against the computer, press 1, else press 2");

                    numberOfPlayers = getInputFromUserOnNotValidInput(wrongInputMessage, i_MinAndMaxNumOfPlayers);
                }
            }

            return numberOfPlayers;
        }

        internal static PointsForGame GetPlayerMoveIndices(Board i_GameBoard)
        {
            PointsForGame indices = askPlayerForAMove();
            string possibleErrorType = "";
            bool isMoveApproved = !r_isValid;


            while (isMoveApproved != r_isValid && indices.IsUserWantsToQuit == !r_isValid)
            {
                if (i_GameBoard.NumberOfMovesRemains > 0)
                {
                    if (GameUtils.isIndicesAreWithinRangeOfBoardSIze(i_GameBoard, indices) == r_isValid)
                    {
                        if (GameUtils.isSlotExistAndEmpty(i_GameBoard, indices) == r_isValid)
                        {
                            isMoveApproved = r_isValid;
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
                    possibleErrorType = "Out Of Moves";
                }

                Console.WriteLine(possibleErrorType);
                indices = askPlayerForAMove();
            }

            return indices;
        }

        private static PointsForGame askPlayerForAMove()
        {
            string askForIndices = String.Format(@"Enter you're move with the format - Row Number:Column Number.");
            Console.WriteLine(askForIndices);

            char seperator = ':';
            string userInput = Console.ReadLine();
            PointsForGame indices = new PointsForGame(0, 0);

            if (!isUserWantsToQuit(userInput, ref indices))
            {
                indices = convertStringsToCoordsIndices(userInput, seperator);
            }

            return indices;
        }

        private static PointsForGame convertStringsToCoordsIndices(string i_Input, char i_seperator)
        {
            string[] subInputs = i_Input.Split(i_seperator);
            PointsForGame indicesToReturn = new PointsForGame(0, 0);
            byte convertedByte = 0;
            bool rowValueHasInserted = !r_isValid;

            foreach (string subInput in subInputs)
            {
                bool digitToByte = byte.TryParse(subInput, out convertedByte);

                if (digitToByte == r_isValid)
                {
                    if (!rowValueHasInserted)
                    {
                        indicesToReturn.RowIndexOrMinValue = convertedByte;
                        rowValueHasInserted = r_isValid;
                    }
                    else
                    {
                        indicesToReturn.ColumnIndexOrMaxValue = convertedByte;
                        break;
                    }
                }
            }

            return indicesToReturn;
        }

        private static int getInputFromUserOnNotValidInput(String i_ErrorMessage, PointsForGame i_MinAndMaxValues)
        {
            Console.WriteLine(i_ErrorMessage);
            String userInput = Console.ReadLine();
            int numberToReturn = 0;
            bool digitToByte = int.TryParse(userInput, out numberToReturn);
            bool isUserInputValid = GameUtils.isUserInputAValidNumber(numberToReturn, i_MinAndMaxValues);
            bool isUserWantsQuit = isUserWantsToQuit(userInput, ref i_MinAndMaxValues);

            while (isUserInputValid == !r_isValid && !isUserWantsQuit)
            {
                Console.WriteLine(i_ErrorMessage);
                userInput = Console.ReadLine();
                isUserWantsQuit = isUserWantsToQuit(userInput, ref i_MinAndMaxValues);

                if (isUserWantsQuit)
                {
                    numberToReturn = k_PlayerWantsToQuitIntSign;
                    break;
                }

                digitToByte = int.TryParse(userInput, out numberToReturn);
                isUserInputValid = GameUtils.isUserInputAValidNumber(numberToReturn, i_MinAndMaxValues);
            }

            return numberToReturn;
        }

        private static bool isUserWantsToQuit(string i_userInput, ref PointsForGame i_Indices)
        {
            bool isUserWantsToQuitFromGame = i_userInput.Equals(k_PlayerWantsToQuitSign1) || i_userInput.Equals(k_PlayerWantsToQuitSign2);

            if (isUserWantsToQuitFromGame)
            {
                i_Indices.IsUserWantsToQuit = r_isValid;
            }

            return isUserWantsToQuitFromGame;
        }

        internal static bool IsPlayerWantsAnotherRound(int[] i_Results)
        {
            bool isThereAntoherRound = !r_isValid;
            string scoresDetails = String.Format(@"Final Results are: 
X has won {0} times.
O has Won {1} times.
If you want another round, please press {2} or {3}", i_Results[0].ToString(), i_Results[1].ToString(), k_PlayerWantsAnotherRoundSign1, k_PlayerWantsAnotherRoundSign2);

            Console.WriteLine(scoresDetails);
            string userResponse = Console.ReadLine();

            if (userResponse.Equals(k_PlayerWantsAnotherRoundSign1) || userResponse.Equals(k_PlayerWantsAnotherRoundSign2))
            {
                isThereAntoherRound = r_isValid;
            }

            return isThereAntoherRound;
        }
    }
}