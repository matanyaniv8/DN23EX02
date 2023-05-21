using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Ex02.ConsoleUtils;



namespace TicTacToeLogic
{
    
    internal class TicTacToeLogic
    {
        private byte[,] m_Board;
        private const byte k_AvailableSlotSign = 0;
        private readonly bool r_isValid = true;
        private int m_NumberOfMovesRemains = 0;
        private bool m_FirstPlayerMove = true;
        private readonly byte r_FirstPlayerSign = 1;
        private readonly byte r_SecondPlayerSign = 2;

        public TicTacToeLogic(int i_BoardSize)
        {
            m_NumberOfMovesRemains = i_BoardSize * i_BoardSize;
            m_Board = new byte[i_BoardSize, i_BoardSize];
            m_FirstPlayerMove = true;
        }

        internal int BoardSize
        {
            get { return m_Board.GetLength(0); }
        }

        internal bool FirstPlayerTurn
        {
            get { return m_FirstPlayerMove; }
        }

        internal byte[,] Board
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

        public byte PlayMove(int i_Row, int i_Column)
        {
            byte playerSign = getPlayerSign();

            if (CanIMakeAMove(i_Row, i_Column))
            {
                m_Board[i_Row, i_Column] = playerSign;
                m_FirstPlayerMove = (playerSign == r_FirstPlayerSign) ? !r_isValid : r_isValid;
                m_NumberOfMovesRemains--;
            }

            return playerSign;
        }

        internal bool isSlotAvailable(int i_Row, int i_Column) 
        {
            bool isSlotAvailable = !r_isValid;

            if(isIndicesAreWithinRangeOfBoardSIze(i_Row, i_Column))
            {
                isSlotAvailable = (m_Board[i_Row, i_Column] == k_AvailableSlotSign) ? r_isValid: !r_isValid;
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
            
            if(m_NumberOfMovesRemains == 0)
            {
                isATie = r_isValid;
            }

            return isATie;
        }

        internal bool isThereAWin() 
        {
            bool isWinning = !r_isValid;
            bool horizontalWinning = isHorizontalWinning();
            bool verticalWinning = isVerticalWinning();
            bool diagonalWinning = isDiagonlWinning();
            bool seconderyDiagonalWinning = isSeconderyDiagonalWinning();   

            if(horizontalWinning || verticalWinning || diagonalWinning || seconderyDiagonalWinning)
            {
                isWinning = r_isValid;
            }

            return isWinning;
        }

        private bool isHorizontalWinning()
        {
            bool isThereAnIdenticalRow = !r_isValid;

            for( int row = 0; row < BoardSize; row++)
            {
                isThereAnIdenticalRow = rowHorizontallWinning(row);

                if(isThereAnIdenticalRow)
                {
                    break;
                }
            }

            return isThereAnIdenticalRow;
        }

        private bool rowHorizontallWinning(int i_RowIndex)
        {
            bool isHorizontalIdenticals = r_isValid;
            byte firstResult = m_Board[i_RowIndex, 0];

            for ( int col = 0; col < BoardSize; col++)
            {
                byte horizontalElement = m_Board[i_RowIndex, col];

                if(firstResult != horizontalElement || firstResult == k_AvailableSlotSign)
                {
                    isHorizontalIdenticals = !r_isValid; 
                    break;
                }
            }

            return isHorizontalIdenticals;
        }

        private bool isVerticalWinning()
        {
            bool isThereAnIdenticalRow = !r_isValid;

            for (int col = 0; col < BoardSize; col++)
            {
                isThereAnIdenticalRow = colVerticalWinning(col);

                if (isThereAnIdenticalRow)
                {
                    break;
                }
            }

            return isThereAnIdenticalRow;
        }

        private bool colVerticalWinning(int i_ColIndex)
        {
            bool isVerticalIdenticals = r_isValid;
            byte firstElement = m_Board[0, i_ColIndex];

            for (int row = 1; row < BoardSize; row++)
            {
                byte verticalElement = m_Board[row, i_ColIndex];

                if (firstElement != verticalElement || firstElement == k_AvailableSlotSign)
                {
                    isVerticalIdenticals = !r_isValid;
                    break;
                }
            }

            return isVerticalIdenticals;
        }

        private bool isDiagonlWinning()
        {
            bool isElementsAreTheSame = r_isValid;
            byte firstElement = m_Board[0,0];

            for( int i = 1; i < BoardSize; i++)
            {
                byte diagonalElement = m_Board[i,i];
                
                if(firstElement != diagonalElement || firstElement == k_AvailableSlotSign) 
                { 
                    isElementsAreTheSame = !r_isValid; 
                    break;
                }
            }

            return isElementsAreTheSame;
        }

        private bool isSeconderyDiagonalWinning()
        {
            bool isElementsAreTheSame = r_isValid;
            byte firstElement = m_Board[0, BoardSize - 1];

            for( int i = 0; i < BoardSize; i++)
            {
                byte seconderyDiagonalElement = m_Board[i, BoardSize - i - 1];

                if(seconderyDiagonalElement != firstElement || firstElement == k_AvailableSlotSign)
                {
                    isElementsAreTheSame = !r_isValid;
                    break;
                }
            }

            return isElementsAreTheSame;
        }

        private byte getPlayerSign()
        {
            byte sign = m_FirstPlayerMove ? r_FirstPlayerSign : r_SecondPlayerSign;
            return sign;
        }
    }
}
