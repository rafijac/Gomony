#include <iostream>
#include <string>
#include <queue>
#include "Piece.h"
#include <vector>
using namespace std;

void buildBoard(char** &  board);
//builds the board multiD array without placing the pieces

void placePieces(char** &  board, vector<queue<Piece>> & piece);
//places all the pieces on the board to start the game

void print(char** & board);

void delAll(char** &  board, vector<queue<Piece>> piece);
//clears the board

void move(char** &  board, string & currLocation, string & nexLocation, vector<queue<Piece>> piece);
//moves the piece the user chose to its desired location

int findPieceNum(string location, vector<queue<Piece>> piece);
//based on the locations user chose the function returns the corresponding pieceNum in array of pieces

void checkNextLocation(char** &  board, Piece piece, string currLocation, string & nexLocation);
//checks to make sure placing peice in an allowed area

void changeNextLocation(char** &  board, Piece piece, string currLocation, string & nexLocation);
//changes the next location and makes sure its a valid location

int jumpability(char** & board, Piece piece, string currLocation);
//detects it there is a potention jump, not bool bc if there's two jumps than user can decide which jump

bool blocker(char** & board, string currLocation, string nexLocation, Piece p);
//checks to see that if there are two pieces which would block a potential jump, or if there is a wall in the way, or if its own teamate is in its way

char findLocationPiece(char** & board, string location);
//returns the peice in any given location on a board or returns 'N'

bool exmptyLanding(char** & board, string currLocation, string nexLocation);
//returns true if after the piece does a jump it has somewhere to land


int main() {
	//build board here----------------------------------------------------------------------------
	char** board = new char*[11];

	//step 1) build board
	buildBoard(board);

	//step 2) place pieces
	vector<queue<Piece>> piece(24); 	//create all the pieces 
	placePieces(board, piece);

	print(board);

	//gameplay here-------------------------------------------------------------------------------
	int choice = 1;
	string currLocation, nextLocation;//cin the piece the location of the piece the player would like to move and where he wants to move it
	cout << "would you like to continue playing 1 for yes 0 for no" << endl;

	while (choice != 0) {
		cout << "red turn" << endl;
		//find location of the piece
		cout << "which piece are you moving?" << endl;
		cin >> currLocation;

		//found out where the player would like to move it
		cout << "where do you wanna move it?" << endl;
		cin >> nextLocation;

		//move the piece
		move(board, currLocation, nextLocation, piece);
		print(board);

		cout << "blacks turn" << endl;
		//find location of the piece
		cout << "which piece are you moving?" << endl;
		cin >> currLocation;

		//found out where the player would like to move it
		cout << "where do you wanna move it?" << endl;
		cin >> nextLocation;

		//move the piece
		move(board, currLocation, nextLocation, piece);
		print(board);

		cout << "would you like to continue playing 1 for yes 0 for no \n";
		cin >> choice;
	}

	system("pause");
	delAll(board, piece);
	return 0;
}



void buildBoard(char** & board)
{
	for (int i = 0; i < 9; i++) {//rows
		board[i] = new char[11];
		if (i > 0)
			board[i][0] = i + 'A' - 1;// -1 becuase skips the first one
		for (int j = 0; j < 9; j++) {//colums
			if (i == 0) //on first row add 1-10
				if (j > 0)
					board[i][j] = j - 1 + '0';
				else//makes the board[0][0] = ' '
					board[i][j] = ' ';
			if (i != 0 && j > 0)//fills the board with ' '
				board[i][j] = ' ';
		}
	}
}

void placePieces(char** & board, vector<queue<Piece>> & piece)
{
	int count = 0;
	for (int i = 1; i < 9; i++) {
		//in order to checker the board offset the j when i is even
		int j = 1;
		if (i % 2 == 0) 
			j++;
		for (; j < 9; j+=2) {
			if (i < 4)//place red pieces
			{
				//create the piece with the info
				Piece p(count, 'X');
				p.setLocation(i - 1, j - 1);
				piece[count++].push(p);
				//represent the piece on the board
				board[i][j] = 'X';
			}
			if (i > 5)//places black pieces
			{
				//create the piece with the info
				Piece p(count, 'O');
				p.setLocation(i - 1, j - 1);
				piece[count++].push(p);
				//represent the piece on the board
				board[i][j] = 'O';
			}
		}
	}
}

void print(char** &  board) {
	for (int i = 0; i < 9; i++) {//rows
		for (int j = 0; j < 9; j++) {//colums
			cout << board[i][j];
		}
		cout << endl;
	}
}

void delAll(char** & board, vector<queue<Piece>> piece)
{
	//delete pieces
	for (unsigned int i = 0; i < piece.size(); i++){
		piece[i].pop();
	}

	//delete board
	for (unsigned int i = 0; i < 9; i++)
	{
		delete board[i];
	}
	delete[]  board;
}


void move(char** &  board, string & currLocation, string & nexLocation, vector<queue<Piece>> piece)
{
	if (findPieceNum(currLocation, piece) == 100) {
		cout << "invalid selection" << endl;
		return;
	}
	else {
		Piece  p = piece[findPieceNum(currLocation, piece)].front();

		checkNextLocation(board, p, currLocation, nexLocation );

		//sets the peices internal location to the next locations
		p.setLocation(nexLocation);
		piece[findPieceNum(currLocation, piece)].front() = p;

		//change the location on the board
		board[currLocation[0] - 'A' + 1][currLocation[1] - '0' + 1] = ' ';
		board[nexLocation[0] - 'A' + 1][nexLocation[1] - '0' + 1] = p.getColor();
	}
}


int findPieceNum(string location, vector<queue<Piece>> piece)
{
	for (unsigned int i = 0; i < piece.size(); i++){
		Piece p = piece[i].front();
		if (p.getLocation() == location)
			return p.getPieceNum();
	}

	return 100;//if not found
}

void checkNextLocation(char** &  board, Piece p, string currLocation, string & nexLocation)
{
	//error checking to make sure moving to an allowed location
	if (nexLocation[0] - 'A' > 0 && nexLocation[0] - 'A' < 9 || nexLocation[1] - '0' > 0 && nexLocation[1] - '9' < 9) {//first of all make sure the nexlocation is on the board
		// 1) if king can go up or down as long as doesnt touch wall
		if (p.isKing()) {

		}
		// 2) if not king and X can only go down one and one to the left or right and cant jump its own teamate
		if (p.getColor() == 'X')
			if (currLocation[0] + 1 == nexLocation[0] && (currLocation[1] + 1 == nexLocation[1] || currLocation[1] - 1 == nexLocation[1]) && !blocker(board, currLocation, nexLocation))
				return;

		// 3) if not king and O can only go down one and one to the left or right and cant jump itself
		if (p.getColor() == 'O')
			if (currLocation[0] - 1 == nexLocation[0] && (currLocation[1] + 1 == nexLocation[1] || currLocation[1] - 1 == nexLocation[1]) && !blocker(board, currLocation, nexLocation))
				return;
	}
	changeNextLocation(board, p, currLocation, nexLocation);
}

void changeNextLocation(char** &  board, Piece piece, string currLocation, string & nexLocation)
{
	cout << "invalid move, please try again\n";
	cin >> nexLocation;
	checkNextLocation(board, piece, currLocation, nexLocation);

	return;
}

int jumpability(char** & board, Piece piece, string currLocation)
{
	//already checked to make sure theres nothing blocking it so just need to see if there is a piece infront of him he is able to jump

	int count = 0;//num of jump - max 2
	string loc1, loc2; //searches both potention jump locations

	//need to check seperately for red and black
	if (piece.getColor() == 'X') {
		loc1[0] = currLocation[0] + 1;
		loc1[1] = currLocation[1] + 1;
		loc2[0] = currLocation[0] + 1;
		loc2[1] = currLocation[1] - 1;

		if(findLocationPiece(board, loc1) == 'O' && exmptyLanding(board, currLocation, loc1))
			count++;
		if(findLocationPiece(board, loc2) == 'O'  && exmptyLanding(board, currLocation, loc2))
			count++;
	}

	if (piece.getColor() == 'O') {
		loc1[0] = currLocation[0] - 1;
		loc1[1] = currLocation[1] + 1;
		loc2[0] = currLocation[0] - 1;
		loc2[1] = currLocation[1] - 1;

		if (findLocationPiece(board, loc1) == 'X' && exmptyLanding(board, currLocation, loc1))
			count++;
		if (findLocationPiece(board, loc2) == 'X'  && exmptyLanding(board, currLocation, loc2))
			count++;
	}

	return count;
}

bool blocker(char** & board, string currLocation, string nexLocation, Piece p)
{
/*--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
																SO NOT DONE WITH THIS ITS CRAZY
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------*/

	string finalLocation;
	//1) there isnt a wall in the way
	if (nexLocation[0] - 'a' > 0 && nexLocation[0] - 'a' < 9 && nexLocation[1] - '0' > 0 && nexLocation[0] - '0' < 9)
		if (findLocationPiece(board, nexLocation) != p.getColor())	//2) its own piece isnt in the way
			//create the location for the location after the jump so you can see if its empty in the upcoming if
			if (p.getColor() == 'X') {
				if (nexLocation[1] > currLocation[1]) {
					finalLocation[0] += 1;
					finalLocation[1] += 1;
					if (findLocationPiece(board, finalLocation) == 'N')//checks to see its empty
						return true;
				}
				else if (nexLocation[1] < currLocation[1]) {
					finalLocation[0] += 1;
					finalLocation[1] -= 1;
					if (findLocationPiece(board, finalLocation) == 'N')//checks to see its empty
						return true;
				}
			}
			else if (p.getColor() == 'O') {

			}
			if (nexLocation[0] && nexLocation[1])	//3) the location after the jump is empty
					return true;
	return false;
}
