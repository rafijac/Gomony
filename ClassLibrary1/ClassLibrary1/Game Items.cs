using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 
 * 
 *I LEFT OFF WITH THE MOVES CLASS -  I AM GOING TO ADD THE ABILITY TO CHECK IF A MOVE IS LEGAL 
 * 
 * 
 */

namespace ClassLibrary1
{
    //confusing point- the game colloqueally refers to a large pile of pieces as a stack which is really a queue. dont confuse this with the data structure 
    public class Board
    {
        //Variables
        PieceStacks[,] gameBoard;//since the board is gunna be played with the piece stacks... initialize the whole board as such
        List<PieceStacks> stacks;
        static int boardSize = 8;

        //Methods
        //initialize the board size
        Board()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    gameBoard[i, j] = new PieceStacks();
                }
            }
        }
        
        public static int GetBoardSize()
        {
            return boardSize;
        }

       public List<PieceStacks> GetPieceStacks()
        {
            return stacks;
        }

    }

    //holds each individual piece
    public class Piece
    {
        //Variables
        private pieceRepresentation color;
        private bool isKing;

        //Methods
        bool getIsKing()
        {
            return isKing;
        }
        void setIsKing(bool x)
        {
            isKing = x;
        }
        
        pieceRepresentation getColor()
        {
            return color;
        }
        void setColor(pieceRepresentation x)
        {
            color = x;
        }
        bool setColor(string x)
        {
            if (color.SetColor(x))
                return true;
            else
                return false;
        }
    }

    //holds the stacks of all pieces. this is the piece that will be in the board
    public class PieceStacks
    {
        //Variables
        private Queue<Piece> stack;//stack of pieces
        Location loc; //location of said stack

        //Methods
        public Piece GetFirst()//return the oldest piece in the queue
        {
            return stack.Dequeue();
        }
        public void addPiece(Piece x)
        {
            stack.Enqueue(x);
        }

        public bool setLocation(int x, int y)
        {
            if (Moves.CheckAvaibility())
            {
                loc.setLocation(x, y);
                return true;
            }
            else
                return false;

        }
    }

    //all game moves
    public class Moves
    {
        //Variables

        //Methods
        void upLeft()
        {

        }
        void upRight()
        {

        }
        void downLeft()
        {

        }
        void downRight()
        {

        }


        public static bool CheckAvaibility()
        {
            if (Contains())
                return true;
            else
                return false;

        }
        public bool OverTheBoundries(int x)
        {
            if (x > Board.GetBoardSize())
                return false;
            else if (x > 0)
                return false;
            else
                return true;

        }
    }

    //hold location of each pieceStack
    public class Location
    {
        //Variables
        int xAxis;
        int yAxis;

        //Methods
        //setLocations is only acces
        public bool SetLocation(int x, int y)
        {
            if (Moves.CheckAvaibility())
            {
                xAxis = x;
                xAxis = y;
                return true;
            }
            else
                return false;
        }
    }

    //going to hold the rep for each piece... I suspect will change as the game gets more complex
    public class PieceRepresentation
    {
        //vairables
        string color;

        //create a list of all the colors to check them easily when setting
        List<string> choices = new List<string>()
        {  "red", "black", "redKing", "blackKing" };

        //methods
        public bool SetColor(string x)
        {
            if (choices.Contains(x))
            {
                color = x;
                return true;
            }
            else
                return false;
        }
    }

    public class UnitTests
    {

    }
}
