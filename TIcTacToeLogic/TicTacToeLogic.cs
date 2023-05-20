using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex02.ConsoleUtils;



namespace TicTacToeLogic
{
    
    internal class TicTacToeLogic
    {
        private byte[,] m_Board;
        private const byte k_AvailableSlotSign = 0;

        private const Boolean m_isValid = true;
        private int m_NumberOfMovesRemains = 0;
        private Boolean FirstPlayerMove = true;
        private readonly byte r_FirstPlayerSign = 1;
        private readonly byte r_SecondPlayerSign = 2;

        public TicTacToeLogic(int i_BoardSize)
        {
            m_NumberOfMovesRemains = i_BoardSize * i_BoardSize;
            m_Board = new byte[i_BoardSize, i_BoardSize];
            FirstPlayerMove = true;

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
            bool response = m_isValid;

            if (isThereAWin() && !isSlotAvailable(i_Row, i_Column))
            {
                response = !m_isValid;
            }

            return response;

        }

        public void PlayMove(int i_Row, int i_Column)
        {
            if (CanIMakeAMove(i_Row, i_Column))
            {

                m_Board[i_Row, i_Column] = getPlayerSign();
                FirstPlayerMove = (FirstPlayerMove) ? !FirstPlayerMove : FirstPlayerMove;
                m_NumberOfMovesRemains--;
            }
        }

        private Boolean isSlotAvailable(int i_Row, int i_Column) 
        {
            bool isSlotAvailable = true;

            if (m_Board[i_Row, i_Column] != k_AvailableSlotSign)
            {

                isSlotAvailable = false;
            } 

            return isSlotAvailable;
        }

        public bool IsThereTie()
        {
            bool isATie = false;
            
            if(m_NumberOfMovesRemains == 0)
            {
                isATie = m_isValid;
            }

            return isATie;
        }

        public bool isThereAWin() 
        {
            bool isWinning = !m_isValid;
            bool horizontalWinning = isHorizontalWinning();
            bool verticalWinning = isVerticalWinning();
            bool diagonalWinning = isDiagonlWinning();

            if(horizontalWinning || verticalWinning || diagonalWinning)
            {
                isWinning = m_isValid;
            }

            return isWinning;
        }

        private bool isHorizontalWinning()
        {
            bool isThereAnIdenticalRow = !m_isValid;

            for( int row = 0; row < m_Board.GetLength(0); row++)
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
            bool isHorizontalIdenticals = m_isValid;
            byte lastResult = m_Board[i_RowIndex, 0];

            for ( int col = 0; col < m_Board.GetLength(1); col++)
            {
                if(lastResult != m_Board[i_RowIndex, col])
                {
                    isHorizontalIdenticals = m_isValid; 
                    break;
                }
            }

            return isHorizontalIdenticals;
        }

        private bool isVerticalWinning()
        {
            bool isThereAnIdenticalRow = !m_isValid;

            for (int row = 0; row < m_Board.GetLength(0); row++)
            {
                isThereAnIdenticalRow = rowHorizontallWinning(row);

                if (isThereAnIdenticalRow)
                {
                    break;
                }
            }

            return isThereAnIdenticalRow;
        }

        private bool colVerticalWinning(int i_ColIndex)
        {
            bool isVerticalIdenticals = m_isValid;
            byte lastResult = m_Board[0, i_ColIndex];

            for (int row = 0; row < m_Board.GetLength(1); row++)
            {
                if (lastResult != m_Board[row, i_ColIndex])
                {
                    isVerticalIdenticals = m_isValid;
                    break;
                }
            }

            return isVerticalIdenticals;
        }

        private bool isDiagonlWinning()
        {
            bool isElementsAreTheSame = m_isValid;
            byte lastResult = m_Board[0,0];

            for( int i = 0; i < m_Board.GetLength(0); i++)
            {
                byte diagonalElement = m_Board[i,i];
                
                if(lastResult != diagonalElement) 
                { 
                    isElementsAreTheSame = !m_isValid; 
                    break;
                }
            }

            return isElementsAreTheSame;
        }

        private byte getPlayerSign()
        {
            byte sign = FirstPlayerMove ? r_FirstPlayerSign : r_SecondPlayerSign;
            return sign;
        }
    }
}
