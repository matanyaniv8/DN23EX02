namespace ReverseTicTacToeLogic
{
    public class ReverseTicTacToeLogic
    {
        public enum BoardSigns
        {
            EmptySlot = 0,
            FirstPlayerSign =1,
            LastPlayerSign =2
        }

        private BoardSigns[,] m_Board;
        private static readonly int r_MinimalBoardSize = 3;
        private static readonly int r_MaximalBoardSize = 9;
        private readonly bool r_isValid = true;
        private int m_NumberOfMovesRemains = 0;
        private bool m_FirstPlayerMove = true;

        public ReverseTicTacToeLogic(int i_BoardSize)
        {
            if(i_BoardSize >= r_MinimalBoardSize && i_BoardSize <= r_MaximalBoardSize)
            {
                m_NumberOfMovesRemains = i_BoardSize * i_BoardSize;
                m_Board = new BoardSigns[i_BoardSize, i_BoardSize];
                m_FirstPlayerMove = true;
            }   
        }
        
        public static int MinimalBoardSize
        {
            get
            {
                return r_MinimalBoardSize;
            }
        }

        public static int MaximalBoardSize
        {
            get 
            { 
                return r_MaximalBoardSize; 
            }
        }

        public int BoardSize
        {
            get 
            { 
                return m_Board.GetLength(0);
            }
        }

        public bool FirstPlayerTurn
        {
            get 
            { 
                return m_FirstPlayerMove; 
            }
        }

        public BoardSigns[,] Board
        {
            get
            {
                return m_Board;
            }
        }

        public int NumberOfMovesRemains
        {
            get
            {
                return (byte)m_NumberOfMovesRemains;
            }
        }

        public bool CanIMakeAMove(int i_Row, int i_Column)
        {
            bool response = r_isValid;

            if (isSlotAvailable(i_Row, i_Column) != r_isValid)
            {
                response = !r_isValid;
            }

            return response;
        }

        public BoardSigns PlayMove(int i_Row, int i_Column)
        {
            BoardSigns playerSign = GetPlayerSign();

            if (CanIMakeAMove(i_Row, i_Column) == r_isValid)
            {
                m_Board[i_Row, i_Column] = playerSign;
                m_FirstPlayerMove = (playerSign == BoardSigns.FirstPlayerSign) ? !r_isValid : r_isValid;
                m_NumberOfMovesRemains--;
            }

            return playerSign;
        }

        public bool isSlotAvailable(int i_Row, int i_Column)
        {
            bool isSlotAvailable = !r_isValid;

            if (IsIndicesAreWithinRangeOfBoardSIze(i_Row, i_Column) == r_isValid)
            {
                isSlotAvailable = (m_Board[i_Row, i_Column] == BoardSigns.EmptySlot) ? r_isValid : !r_isValid;
            }

            return isSlotAvailable;
        }

        public bool IsIndicesAreWithinRangeOfBoardSIze(int i_Row, int i_Column)
        {
            bool isColumnWithInRange = i_Column >= 0 && i_Column < BoardSize;
            bool isRowWithInRange = i_Row >= 0 && i_Row < BoardSize;
            bool isIndicesAreWithinRange = isColumnWithInRange && isRowWithInRange;

            return isIndicesAreWithinRange;
        }

        public BoardSigns GetPlayerSign()
        {
            BoardSigns sign = m_FirstPlayerMove ? BoardSigns.FirstPlayerSign : BoardSigns.LastPlayerSign;
            return sign;
        }

        public bool IsThereTie()
        {
            bool isATie = false;

            if (m_NumberOfMovesRemains == 0)
            {
                isATie = r_isValid;
            }

            return isATie;
        }

        public bool IsThereAWin()
        {
            bool isWinning = !r_isValid;
            bool horizontalWinning = isHorizontalWinning();
            bool verticalWinning = isVerticalWinning();
            bool diagonalWinning = isDiagonlWinning();
            bool seconderyDiagonalWinning = isSeconderyDiagonalWinning();

            if ((horizontalWinning || verticalWinning || diagonalWinning || seconderyDiagonalWinning) == true)
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
    }
}
