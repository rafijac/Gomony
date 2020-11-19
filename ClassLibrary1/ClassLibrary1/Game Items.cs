using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * 
 * 
 *I LEFT OFF WITH THE MOVES CLASS -  add perform jump to the basic moves
 * 
 * 
 */

namespace ClassLibrary1
{
    //confusing point- the game colloqueally refers to a large pile of pieces as a stack which is really a queue. dont confuse this with the data structure 
    public class Board
    {
        //Variables
        PieceStack[,] gameBoard;//since the board is gunna be played with the piece stacks... initialize the whole board as such
        List<PieceStack> stacks;
        static int boardSize = 8;

        //Methods
        //initialize the board size
        Board()
        {
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    gameBoard[i, j] = new PieceStack();
                }
            }
        }

        public static int GetBoardSize()
        {
            return boardSize;
        }

        public static List<PieceStack> GetPieceStack()
        {
            return stacks;
        }

    }

    //holds each individual piece
    public class Piece
    {
        //Variables
        private static PieceRepresentation color;

        public PieceRepresentation getColor()
        {
            return color;
        }
        void setColor(PieceRepresentation x)
        {
            color = x;
        }
    }

    //holds the stacks of all pieces. this is the piece that will be in the board
    public class PieceStack
    {
        //Variables
        private Queue<Piece> stack;//stack of pieces
        Location loc; //location of said stack

        //Methods
        public Location GetLocation()
        {
            return loc;
        }
        public Piece DequeueFirst()//return the oldest piece in the queue
        {
            return stack.Dequeue();
        }
        public void addPiece(Piece x)
        {
            stack.Enqueue(x);
        }
        public void setLocation(int x, int y)
        {
            loc.SetLocation(x, y);
        }
        public Piece GetFirst()//return the first peiece in the queue
        {
            return stack.First();
        }
        public PieceRepresentation GetTeam()
        {
            return GetFirst().getColor();
        }
        //check to see if the piece is a king
        public void TurnIntoKing()
        {
            if()
        }
    }

    //all game moves
    public interface IMoves
    {
        //Variables

        //Methods
        bool Left(PieceStack ps, int x, int y);
        bool Right(PieceStack ps, int x, int y);

        //CHECK TO MAKE SURE A MOVE IS LEGAL- i.e. -
        //1. not over the boundarys of the board
        //2. there isnt a piece blocking it
        //sends in the piece and where you want it to go
        int CheckLegaility(PieceStack loc, int x, int y);
        bool TheresAPieceThere(int x, int y);
        bool TheresAPieceThere(Location x);
        bool PieceOrWallInTheWay(Location x, Enums.Directions y, Enums.Directions z);
        bool DoubleBlocked(Location ps, Enums.Directions x, Enums.Directions y);
        bool OverTheBoundries(int x);
        Enums.Directions DirectionExtractorHorizontal(Location x, int y);
        Enums.Directions DirectionExtractorVertical(Location x, int y);
        bool PerformJump(PieceStack x, int y, int z);
        PieceStack StackLocationFinder(int x, int y);
        PieceStack StackLocationFinder(Location x);
        int DirectionIncreaser(int x, Enums.Directions y);

    }

    public class KingMoves : IMoves
    {
        public bool Left(PieceStack ps, int x, int y)
        {
            //going up to the left
            if (ps.GetLocation().GetYaxis() - y < 0)
            {
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() - 1, (ps.GetLocation().GetYaxis() - 1)) == 1)
                {
                    ps.setLocation(ps.GetLocation().GetXaxis() - 1, ps.GetLocation().GetYaxis() - 1);
                    return true;
                }
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() - 1, (ps.GetLocation().GetYaxis() - 1)) == 2)
                {
                    //since in this function what was sent in arg2 we need increade the direction by one in the direction it is going
                    PerformJump(ps, DirectionIncreaser(x, DirectionExtractorHorizontal(ps.GetLocation(), x)), DirectionIncreaser(y, DirectionExtractorVertical(ps.GetLocation(), y)));
                    return true;
                }
            }
            //going down to the left
            else
            {
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() - 1, (ps.GetLocation().GetYaxis() + 1)) == 1)
                {
                    ps.setLocation(ps.GetLocation().GetXaxis() - 1, ps.GetLocation().GetYaxis() + 1);
                    return true;
                }
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() - 1, (ps.GetLocation().GetYaxis() + 1)) == 2)
                {
                    PerformJump(ps, DirectionIncreaser(x, DirectionExtractorHorizontal(ps.GetLocation(), x)), DirectionIncreaser(y, DirectionExtractorVertical(ps.GetLocation(), y)));
                    return true;
                }
            }
            return false;
        }
        public bool Right(PieceStack ps, int x, int y)
        {
            if (ps.GetLocation().GetYaxis() - y < 0)
            {
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() - 1, (ps.GetLocation().GetYaxis() + 1)) == 1)
                {
                    ps.setLocation(ps.GetLocation().GetXaxis() + 1, ps.GetLocation().GetYaxis() + 1);
                    return true;
                }
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() - 1, (ps.GetLocation().GetYaxis() + 1)) == 2)
                {
                    PerformJump(ps, DirectionIncreaser(x, DirectionExtractorHorizontal(ps.GetLocation(), x)), DirectionIncreaser(y, DirectionExtractorVertical(ps.GetLocation(), y)));
                    return true;
                }
            }
            else
            {
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() + 1, (ps.GetLocation().GetYaxis() + 1)) == 1)
                {
                    ps.setLocation(ps.GetLocation().GetXaxis() + 1, ps.GetLocation().GetYaxis() - 1);
                    return true;
                }
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() + 1, (ps.GetLocation().GetYaxis() + 1)) == 2)
                {
                    PerformJump(ps, DirectionIncreaser(x, DirectionExtractorHorizontal(ps.GetLocation(), x)), DirectionIncreaser(y, DirectionExtractorVertical(ps.GetLocation(), y)));
                    return true;
                }
            }
            return false;
        }

        //CHECK TO MAKE SURE A MOVE IS LEGAL- i.e. -
        //1. not over the boundarys of the board
        //2. there isnt a piece blocking it
        //sends in the piece and where you want it to go
        public int CheckLegaility(PieceStack startingPoint, int x, int y)//if returns 0 then cant be done, 1 means can, 2 means need to jumpp
        {
            //2 scenarios - spot is free in which chase the location it returns 1
            //1.spot is free
            if (!TheresAPieceThere(x, y))
                return 1;
            //scenario 2  has 3 cases - 
            else
            {
                //A) is that theres a piece where it wants to go followed by another piece or wall
                //in this scenario we need to extract the horizantal and vertical directions and the location of the spot where he wanted to go originally
                Enums.Directions xAxis = DirectionExtractorHorizontal(startingPoint.GetLocation(), x);
                Enums.Directions yAxis = DirectionExtractorVertical(startingPoint.GetLocation(), y);
                Location loc = new Location(x, y);

                if (PieceOrWallInTheWay(loc, xAxis, yAxis))
                    return 0;
                //B the Piece directly infront of it is from his own team
                else if (startingPoint.GetTeam() == StackLocationFinder(x, y).GetTeam())
                    return 0;
                //C) it isnt double blocked in which case it can be jump and its not its own team
                else
                    return 2;
            }
        }
        public bool TheresAPieceThere(Location loc)
        {
            List<PieceStack> stack = Board.GetPieceStack();
            for (int i = 0; i < stack.Capacity; i++)
            {
                if (loc == stack[i].GetLocation())
                    return true;
            }
            return false;
        }
        public bool TheresAPieceThere(int x, int y)
        {
            Location loc = new Location(x, y);
            return TheresAPieceThere(loc);
        }

        //arg1 = starting point, arg2 = where were headed
        public bool PieceOrWallInTheWay(Location loc, Enums.Directions xAxis, Enums.Directions yAxis)
        {
            //2 conditions for a piece to be in the way and not allow a jump
            // A) theres piece followed by a wall in that direction
            if ((OverTheBoundries(loc.GetXaxis() - 1) || OverTheBoundries(loc.GetXaxis() + 1)) || (OverTheBoundries(loc.GetYaxis() + 1)) || OverTheBoundries(loc.GetYaxis() - 1))
                return false;
            // B) there a piece after the piece which is blocking it
            else if (DoubleBlocked(loc, xAxis, yAxis))
                return false;
            else
                return true;
        }
        //checks that the location of teh  piece after the piece in the location its going is taken
        public bool DoubleBlocked(Location loc, Enums.Directions xAxis, Enums.Directions yAxis)
        {
            //4 directions 4 a double block based on the direction the stack is moving
            //A) if its going up to the right then the continuation up to the right needs to be free 
            if (yAxis == Enums.Directions.Up && xAxis == Enums.Directions.Right)
            {
                if (TheresAPieceThere(loc.GetXaxis() + 1, loc.GetYaxis() + 1))
                    return true;
                else
                    return false;
            }
            //B) if its going up to the left then the continuation up to the left needs to be free 
            if (yAxis == Enums.Directions.Up && xAxis == Enums.Directions.Left)
            {
                if (TheresAPieceThere(loc.GetXaxis() - 1, loc.GetYaxis() + 1))
                    return true;
                else
                    return false;
            }
            //C) if its going down to the right then the continuation down to the right needs to be free 
            if (yAxis == Enums.Directions.Down && xAxis == Enums.Directions.Right)
            {
                if (TheresAPieceThere(loc.GetXaxis() - 1, loc.GetYaxis() - 1))
                    return true;
                else
                    return false;
            }
            //D) if its going down to the right then the continuation down to the right needs to be free 
            if (yAxis == Enums.Directions.Down && xAxis == Enums.Directions.Right)
            {
                if (TheresAPieceThere(loc.GetXaxis() - 1, loc.GetYaxis() + 1))
                    return true;
                else
                    return false;
            }
            return true;

        }
        public bool OverTheBoundries(int x)
        {

            if (x > Board.GetBoardSize() - 1)
                return false;
            else if (x < 0)
                return false;
            else
                return true;
        }

        public Enums.Directions DirectionExtractorHorizontal(Location startingPoint, int x)
        {
            if (startingPoint.GetXaxis() - x < 0)
                return Enums.Directions.Right;
            else
                return Enums.Directions.Left;

        }
        public Enums.Directions DirectionExtractorVertical(Location startingPoint, int y)
        {
            if (startingPoint.GetYaxis() - y < 0)
                return Enums.Directions.Down;
            else
                return Enums.Directions.Up;
        }

        //arg1= the sp thats moving, arg2 = new location of the piece
        public bool PerformJump(PieceStack JumpingStack, int x, int y)
        {
            //1. pop the top of the stack were jumping and enqueu to the piece thats jump
            //A. find the location of the piece we are jumping
            Location loc = new Location(Math.Abs(JumpingStack.GetLocation().GetXaxis() - x), Math.Abs(JumpingStack.GetLocation().GetYaxis() - y));
            //B. find the stack that resides in that location 
            PieceStack JumpedStack = StackLocationFinder(loc);
            //check to make sure it returned a value
            if (JumpedStack == null)
                return false;
            //C. dequece that jumped stack to the stack doing the jumping
            JumpingStack.addPiece(JumpedStack.DequeueFirst());

            //2. set the new location
            JumpingStack.setLocation(x, y);

            return true;

        }
        //send in a location and return the stack that resides in that location if no stack there, return false;
        public PieceStack StackLocationFinder(int x, int y)
        {
            //first lets get the list of stacks and a location for the x and y
            List<PieceStack> stack = Board.GetPieceStack();
            Location loc = new Location(x, y);

            for (int i = 0; i < stack.Capacity; i++)
            {
                if (stack[i].GetLocation() == loc)
                    return stack[i];
            }
            return null;
        }
        public PieceStack StackLocationFinder(Location loc)
        {
            //first lets get the list of stacks and a location for the x and y
            List<PieceStack> stack = Board.GetPieceStack();
            //now find the stack
            for (int i = 0; i < stack.Capacity; i++)
            {
                if (stack[i].GetLocation() == loc)
                    return stack[i];
            }
            return null;
        }
        //takes the direction in which the pice is going and increases its location by one
        public int DirectionIncreaser(int startingLocation, Enums.Directions direction)
        {
            if (direction == Enums.Directions.Up || direction == Enums.Directions.Right)
                return startingLocation + 1; ;
            if (direction == Enums.Directions.Down || direction == Enums.Directions.Left)
                return startingLocation - 1;
            else
                return startingLocation;
        }
    }

    public class BasicMoves : IMoves
    {
        //Variables

        //Methods
        public bool Left(PieceStack ps, int x, int y)
        {
            //going up to the left
            if (ps.GetLocation().GetYaxis() - y < 0)
            {
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() - 1, (ps.GetLocation().GetYaxis() - 1)) == 1)
                {
                    ps.setLocation(ps.GetLocation().GetXaxis() - 1, ps.GetLocation().GetYaxis() - 1);
                    return true;
                }
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() - 1, (ps.GetLocation().GetYaxis() - 1)) == 2)
                {
                    //since in this function what was sent in arg2 we need increade the direction by one in the direction it is going
                    PerformJump(ps, DirectionIncreaser(x, DirectionExtractorHorizontal(ps.GetLocation(), x)), DirectionIncreaser(y, DirectionExtractorVertical(ps.GetLocation(), y)));
                    return true;
                }
            }
            //going down to the left
            else
            {
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() - 1, (ps.GetLocation().GetYaxis() + 1)) == 1)
                {
                    ps.setLocation(ps.GetLocation().GetXaxis() - 1, ps.GetLocation().GetYaxis() + 1);
                    return true;
                }
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() - 1, (ps.GetLocation().GetYaxis() + 1)) == 2)
                {
                    PerformJump(ps, DirectionIncreaser(x, DirectionExtractorHorizontal(ps.GetLocation(), x)), DirectionIncreaser(y, DirectionExtractorVertical(ps.GetLocation(), y)));
                    return true;
                }
            }
            return false;
        }
        public bool Right(PieceStack ps, int x, int y)
        {
            if (ps.GetLocation().GetYaxis() - y < 0)
            {
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() - 1, (ps.GetLocation().GetYaxis() + 1)) == 1)
                {
                    ps.setLocation(ps.GetLocation().GetXaxis() + 1, ps.GetLocation().GetYaxis() + 1);
                    return true;
                }
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() - 1, (ps.GetLocation().GetYaxis() + 1)) == 2)
                {
                    PerformJump(ps, DirectionIncreaser(x, DirectionExtractorHorizontal(ps.GetLocation(), x)), DirectionIncreaser(y, DirectionExtractorVertical(ps.GetLocation(), y)));
                    return true;
                }
            }
            else
            {
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() + 1, (ps.GetLocation().GetYaxis() + 1)) == 1)
                {
                    ps.setLocation(ps.GetLocation().GetXaxis() + 1, ps.GetLocation().GetYaxis() - 1);
                    return true;
                }
                if (CheckLegaility(ps, ps.GetLocation().GetXaxis() + 1, (ps.GetLocation().GetYaxis() + 1)) == 2)
                {
                    PerformJump(ps, DirectionIncreaser(x, DirectionExtractorHorizontal(ps.GetLocation(), x)), DirectionIncreaser(y, DirectionExtractorVertical(ps.GetLocation(), y)));
                    return true;
                }
            }
            return false;
        }

        //CHECK TO MAKE SURE A MOVE IS LEGAL- i.e. -
        //1. not over the boundarys of the board
        //2. there isnt a piece blocking it
        //sends in the piece and where you want it to go
        public int CheckLegaility(PieceStack startingPoint, int x, int y)//if returns 0 then cant be done, 1 means can, 2 means need to jumpp
        {
            //2 scenarios - spot is free in which chase the location it returns 1
            //1.spot is free
            if (!TheresAPieceThere(x, y))
                return 1;
            //scenario 2  has 3 cases - 
            else
            {
                //A) is that theres a piece where it wants to go followed by another piece or wall
                //in this scenario we need to extract the horizantal and vertical directions and the location of the spot where he wanted to go originally
                Enums.Directions xAxis = DirectionExtractorHorizontal(startingPoint.GetLocation(), x);
                Enums.Directions yAxis = DirectionExtractorVertical(startingPoint.GetLocation(), y);
                Location loc = new Location(x, y);

                if (PieceOrWallInTheWay(loc, xAxis, yAxis))
                    return 0;
                //B the Piece directly infront of it is from his own team
                else if (startingPoint.GetTeam() == StackLocationFinder(x, y).GetTeam())
                    return 0;
                //C) it isnt double blocked in which case it can be jump and its not its own team
                else
                    return 2;
            }
        }
        public bool TheresAPieceThere(Location loc)
        {
            List<PieceStack> stack = Board.GetPieceStack();
            for (int i = 0; i < stack.Capacity; i++)
            {
                if (loc == stack[i].GetLocation())
                    return true;
            }
            return false;
        }
        public bool TheresAPieceThere(int x, int y)
        {
            Location loc = new Location(x, y);
            return TheresAPieceThere(loc);
        }

        //arg1 = starting point, arg2 = where were headed
        public bool PieceOrWallInTheWay(Location loc, Enums.Directions xAxis, Enums.Directions yAxis)
        {
            //2 conditions for a piece to be in the way and not allow a jump
            // A) theres piece followed by a wall in that direction
            if ((OverTheBoundries(loc.GetXaxis() - 1) || OverTheBoundries(loc.GetXaxis() + 1)) || (OverTheBoundries(loc.GetYaxis() + 1)) || OverTheBoundries(loc.GetYaxis() - 1))
                return false;
            // B) there a piece after the piece which is blocking it
            else if (DoubleBlocked(loc, xAxis, yAxis))
                return false;
            else
                return true;
        }
        //checks that the location of teh  piece after the piece in the location its going is taken
        public bool DoubleBlocked(Location loc, Enums.Directions xAxis, Enums.Directions yAxis)
        {
            //4 directions 4 a double block based on the direction the stack is moving
            //A) if its going up to the right then the continuation up to the right needs to be free 
            if (yAxis == Enums.Directions.Up && xAxis == Enums.Directions.Right)
            {
                if (TheresAPieceThere(loc.GetXaxis() + 1, loc.GetYaxis() + 1))
                    return true;
                else
                    return false;
            }
            //B) if its going up to the left then the continuation up to the left needs to be free 
            if (yAxis == Enums.Directions.Up && xAxis == Enums.Directions.Left)
            {
                if (TheresAPieceThere(loc.GetXaxis() - 1, loc.GetYaxis() + 1))
                    return true;
                else
                    return false;
            }
            //C) if its going down to the right then the continuation down to the right needs to be free 
            if (yAxis == Enums.Directions.Down && xAxis == Enums.Directions.Right)
            {
                if (TheresAPieceThere(loc.GetXaxis() - 1, loc.GetYaxis() - 1))
                    return true;
                else
                    return false;
            }
            //D) if its going down to the right then the continuation down to the right needs to be free 
            if (yAxis == Enums.Directions.Down && xAxis == Enums.Directions.Right)
            {
                if (TheresAPieceThere(loc.GetXaxis() - 1, loc.GetYaxis() + 1))
                    return true;
                else
                    return false;
            }
            return true;

        }
        public bool OverTheBoundries(int x)
        {

            if (x > Board.GetBoardSize() - 1)
                return false;
            else if (x < 0)
                return false;
            else
                return true;
        }

        public Enums.Directions DirectionExtractorHorizontal(Location startingPoint, int x)
        {
            if (startingPoint.GetXaxis() - x < 0)
                return Enums.Directions.Right;
            else
                return Enums.Directions.Left;

        }
        public Enums.Directions DirectionExtractorVertical(Location startingPoint, int y)
        {
            if (startingPoint.GetYaxis() - y < 0)
                return Enums.Directions.Down;
            else
                return Enums.Directions.Up;
        }

        //arg1= the sp thats moving, arg2 = new location of the piece
        public bool PerformJump(PieceStack JumpingStack, int x, int y)
        {
            //1. pop the top of the stack were jumping and enqueu to the piece thats jump
            //A. find the location of the piece we are jumping
            Location loc = new Location(Math.Abs(JumpingStack.GetLocation().GetXaxis() - x), Math.Abs(JumpingStack.GetLocation().GetYaxis() - y));
            //B. find the stack that resides in that location 
            PieceStack JumpedStack = StackLocationFinder(loc);
            //check to make sure it returned a value
            if (JumpedStack == null)
                return false;
            //C. dequece that jumped stack to the stack doing the jumping
            JumpingStack.addPiece(JumpedStack.DequeueFirst());

            //2. set the new location
            JumpingStack.setLocation(x, y);

            return true;
        }

        //send in a location and return the stack that resides in that location if no stack there, return false;
        public PieceStack StackLocationFinder(int x, int y)
        {
            //first lets get the list of stacks and a location for the x and y
            List<PieceStack> stack = Board.GetPieceStack();
            Location loc = new Location(x, y);

            for (int i = 0; i < stack.Capacity; i++)
            {
                if (stack[i].GetLocation() == loc)
                    return stack[i];
            }
            return null;
        }
        public PieceStack StackLocationFinder(Location loc)
        {
            //first lets get the list of stacks and a location for the x and y
            List<PieceStack> stack = Board.GetPieceStack();
            //now find the stack
            for (int i = 0; i < stack.Capacity; i++)
            {
                if (stack[i].GetLocation() == loc)
                    return stack[i];
            }
            return null;
        }
        //takes the direction in which the pice is going and increases its location by one
        public int DirectionIncreaser(int startingLocation, Enums.Directions direction)
        {
            if (direction == Enums.Directions.Up || direction == Enums.Directions.Right)
                return startingLocation + 1; ;
            if (direction == Enums.Directions.Down || direction == Enums.Directions.Left)
                return startingLocation - 1;
            else
                return startingLocation;
        }
    }

    //hold location of each pieceStack
    public class Location
    {
        //Variables
        int xAxis;
        int yAxis;
        private Enums.Directions x;
        private Enums.Directions y;

        //Methods
        public Location(int x, int y)
        {
            this.xAxis = x;
            this.yAxis = y;
        }

        public int GetXaxis()
        {
            return xAxis;
        }
        public int GetYaxis()
        {
            return yAxis;
        }


        //setLocations is only accesses after chekcing move is ok... true weve checked alot but better safe than sorry
        public void SetLocation(int x, int y)
        {
            xAxis = x;
            xAxis = y;
        }
    }

    //going to hold the rep for each piece... I suspect will change as the game gets more complex
    public class PieceRepresentation
    {
        //vairables
        Enums.PieceTypes PieceTypes;

        //methods
        public void SetColor(Enums.PieceTypes x)
        {
            PieceTypes = x;
        }
    }

    public class Enums
    {
        public enum PieceTypes { Red, Black, RedKing, BlackKing };//active if no suitable hosting unit found
        public enum Directions { Up, Down, Left, Right };
    }

    public class UnitTests
    {

    }

}
