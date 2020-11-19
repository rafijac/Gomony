#include <iostream>
#include <string>
#include "Black.h"
#include "Red.h"
using namespace std;

void buildBoard(char** &  board);
//builds the board multiD array without placing the pieces

void placePieces(char** &  board, Red** red, Black** black);
//places all the pieces on the board to start the game

void print(char** & board);

void delAll(char** &  board, Red** red, Black** black);
//clears the board

void move(char** &  board, string currLocation, string nexLocation, Red** red, Black** black, string color);
//moves the piece the user chose to its desired location

int findPiece(string location, Red** red, Black** black, string color);
//based on the locations user chose the function returns the corresponding pieceNum in array of pieces

int main() {
	//build board here----------------------------------------------------------------------------
	char** board = new char*[11];

	//step 1) build board
	buildBoard(board);

	//step 2) place pieces
	//create all the pieces of each color
	Red** red = new Red*[12];
	Black** black = new Black*[12];
	for (int i = 0; i < 12; i++)
	{
		red[i] = new Red();
		red[i]->setPieceNum(i);
		black[i] = new Black();
//		black[i]->setPieceNum(i);
	}

	placePieces(board, red, black);

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
		move(board, currLocation, nextLocation, red, black, "red");
		print(board);

		cout << "blacks turn" << endl;
		//find location of the piece
		cout << "which piece are you moving?" << endl;
		cin >> currLocation;

		//found out where the player would like to move it
		cout << "where do you wanna move it?" << endl;
		cin >> nextLocation;

		//move the piece
		move(board, currLocation, nextLocation, red, black, "red");
		print(board);

		cout << "would you like to continue playing 1 for yes 0 for no \n";
		cin >> choice;
	}

	system("pause");
	delAll(board, red, black);
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

void placePieces(char** & board, Red** red, Black** black)
{
	//counts the num of peices for colors
	int pRed = 0;
	int pBlack = 0;

	for (int i = 1; i < 9; i++) {
		//in order to checker the board offset the j when i is even
		int j = 1;
		if (i % 2 == 0) 
			j++;
		for (; j < 9; j+=2) {
			if (i < 4)//place red pieces
			{
				red[pRed]->setLocation(i, j);
				board[i][j] = red[pRed++]->piece;

			}
			if (i > 5)//places black pieces
			{
				board[i][j] = black[pBlack++]->piece;
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

void delAll(char** & board, Red** red, Black** black)
{
	for (int i = 0; i < 12; i++){
		delete red[i];
		delete black[i];
	}
	delete[] red;
	delete[] black;

	for (int i = 0; i < 9; i++)
	{
		delete board[i];
	}
	delete[]  board;
}


void move(char** &  board, string currLocation, string nexLocation, Red** red, Black** black, string color)
{
	if (color == "red") {
		if (findPiece(currLocation, red, black, color) == 100) {
			cout << "invalid selection" << endl;
			return;
		}
		else {
			//sets the peices internal location to the next locations
			red[findPiece(currLocation, red, black, color)]->setLocation(nexLocation[0] - 'A', nexLocation[1] - 0);

			//change the location on the board
			board[currLocation[0] - 'A'][currLocation[1] - 0] = ' ';
			board[nexLocation[0] - 'A'][nexLocation[1] - 0] = red[findPiece(currLocation, red, black, color)]->piece;
		}
	}
	if (color == "black") {
		if (findPiece(currLocation, red, black, color) == 100) {
			cout << "invalid selection" << endl;
			return;
		}
		else {
			//sets the peices internal location to the next locations
			red[findPiece(currLocation, red, black, color)]->setLocation(nexLocation[0] - 'A', nexLocation[1] - 0);

			//change the location on the board
			board[currLocation[0] - 'A'][currLocation[1] - 0] = ' ';
			board[nexLocation[0] - 'A'][nexLocation[1] - 0] = red[findPiece(currLocation, red, black, color)]->piece;
		}
	}
}


int findPiece(string location, Red** red, Black** black, string color)
{
	if (color == "red") {
		for (int i = 0; i < 12; i++) {
			if (red[i]->getLocation() == location) {
				return red[i]->getPieceNum();
			}
		}
	}
	else if (color == "black") {
		for (int i = 0; i < 12; i++) {
			if (black[i]->getLocation() == location) {
				return black[i]->getPieceNum();
			}
		}
	}

	return 100;//if not found
}
