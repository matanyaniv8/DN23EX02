namespace InverseTicTacToeLogic
{

    internal class InverseTicTacToeLogic
    {
        internal enum BoardSigns
        {
            EmptySlot = 0,
            FirstPlayerSign =1,
            LastPlayerSign =2
        }


        private BoardSigns[,] m_Board;
        private readonly bool r_isValid = true;
        private int m_NumberOfMovesRemains = 0;
        private bool m_FirstPlayerMove = true;

        public InverseTicTacToeLogic(int i_BoardSize)
        {
            m_NumberOfMovesRemains = i_BoardSize * i_BoardSize;
            m_Board = new BoardSigns[i_BoardSize, i_BoardSize];
            m_FirstPlayerMove = true;
        }
        
        internal int BoardSize
        {
            get 
            { 
                return m_Board.GetLength(0);
            }
        }

        internal bool FirstPlayerTurn
        {
            get 
            { 
                return m_FirstPlayerMove; 
            }
        }

        internal BoardSigns[,] Board
        {
            get
            {
                return m_Board;
            }
        }

        internal int NumberOfMovesRemains
        {
            get
            {
                return (byte)m_NumberOfMovesRemains;
            }
        }

        internal bool CanIMakeAMove(int i_Row, int i_Column)
        {
            bool response = r_isValid;

            if (!isSlotAvailable(i_Row, i_Column))
            {
                response = !r_isValid;
            }

            return response;
        }

        public BoardSigns PlayMove(int i_Row, int i_Column)
        {
            BoardSigns playerSign = GetPlayerSign();

            if (CanIMakeAMove(i_Row, i_Column))
            {
                m_Board[i_Row, i_Column] = playerSign;
                m_FirstPlayerMove = (playerSign == BoardSigns.FirstPlayerSign) ? !r_isValid : r_isValid;
                m_NumberOfMovesRemains--;
            }

            return playerSign;
        }

        internal bool isSlotAvailable(int i_Row, int i_Column)
        {
            bool isSlotAvailable = !r_isValid;

            if (isIndicesAreWithinRangeOfBoardSIze(i_Row, i_Column))
            {
                isSlotAvailable = (m_Board[i_Row, i_Column] == BoardSigns.EmptySlot) ? r_isValid : !r_isValid;
            }

            return isSlotAvailable;
        }

        internal bool isIndicesAreWithinRangeOfBoardSIze(int i_Row, int i_Column)
        {
            bool isColumnWithInRange = i_Column >= 0 && i_Column < BoardSize;
            bool isRowWithInRange = i_Row >= 0 && i_Row < BoardSize;
            bool isIndicesAreWithinRange = isColumnWithInRange && isRowWithInRange;

            return isIndicesAreWithinRange;
        }

        internal bool IsThereTie()
        {
            bool isATie = false;

            if (m_NumberOfMovesRemains == 0)
            {
                isATie = r_isValid;
            }

            return isATie;
        }

        internal bool IsThereAWin()
        {
            bool isWinning = !r_isValid;
            bool horizontalWinning = isHorizontalWinning();
            bool verticalWinning = isVerticalWinning();
            bool diagonalWinning = isDiagonlWinning();
            bool seconderyDiagonalWinning = isSeconderyDiagonalWinning();

            if (horizontalWinning || verticalWinning || diagonalWinning || seconderyDiagonalWinning)
            {
                isWinning = r_isValid;
            }

            return isWinning;
        }

        private bool isHorizontalWinning()
        {
            bool isThereAnIdenticalElementsRow = !r_isValid;

            for (int row = 0; row < BoardSize; row++)
            {
                isThereAnIdenticalElementsRow = rowHorizontallWinning(row);

                if (isThereAnIdenticalElementsRow)
                {
                    break;
                }
            }

            return isThereAnIdenticalElementsRow;
        }

        private bool rowHorizontallWinning(int i_RowIndex)
        {
            bool isElementsSeenSoFarAreTheSame = r_isValid;
            BoardSigns firstResult = m_Board[i_RowIndex, 0];

            for (int col = 0; col < BoardSize; col++)
            {
                BoardSigns horizontalElement = m_Board[i_RowIndex, col];

                if (firstResult != horizontalElement || firstResult == BoardSigns.EmptySlot)
                {
                    isElementsSeenSoFarAreTheSame = !r_isValid;
                    break;
                }
            }

            return isElementsSeenSoFarAreTheSame;
        }

        private bool isVerticalWinning()
        {
            bool isThereAnIdenticalElementsColumn = !r_isValid;

            for (int col = 0; col < BoardSize; col++)
            {
                isThereAnIdenticalElementsColumn = colVerticalWinning(col);

                if (isThereAnIdenticalElementsColumn)
                {
                    break;
                }
            }

            return isThereAnIdenticalElementsColumn;
        }

        private bool colVerticalWinning(int i_ColIndex)
        {
            bool isElementsSeenSoFarAreTheSame = r_isValid;
            BoardSigns firstElement = m_Board[0, i_ColIndex];

            for (int row = 1; row < BoardSize; row++)
            {
                BoardSigns verticalElement = m_Board[row, i_ColIndex];

                if (firstElement != verticalElement || firstElement == BoardSigns.EmptySlot)
                {
                    isElementsSeenSoFarAreTheSame = !r_isValid;
                    break;
                }
            }

            return isElementsSeenSoFarAreTheSame;
        }

        private bool isDiagonlWinning()
        {
            bool isElementsSeenSoFarAreTheSame = r_isValid;
            BoardSigns firstElement = m_Board[0, 0];

            for (int i = 1; i < BoardSize; i++)
            {
                BoardSigns diagonalElement = m_Board[i, i];

                if (firstElement != diagonalElement || firstElement == BoardSigns.EmptySlot)
                {
                    isElementsSeenSoFarAreTheSame = !r_isValid;
                    break;
                }
            }

            return isElementsSeenSoFarAreTheSame;
        }

        private bool isSeconderyDiagonalWinning()
        {
            bool isElementsSeenSoFarAreTheSame = r_isValid;
            BoardSigns firstElement = m_Board[0, BoardSize - 1];

            for (int i = 0; i < BoardSize; i++)
            {
                BoardSigns seconderyDiagonalElement = m_Board[i, BoardSize - i - 1];

                if (seconderyDiagonalElement != firstElement || firstElement == BoardSigns.EmptySlot)
                {
                    isElementsSeenSoFarAreTheSame = !r_isValid;
                    break;
                }
            }

            return isElementsSeenSoFarAreTheSame;
        }

        internal BoardSigns GetPlayerSign()
        {
            BoardSigns sign = m_FirstPlayerMove ? BoardSigns.FirstPlayerSign : BoardSigns.LastPlayerSign;
            return sign;
        }
    }
}
