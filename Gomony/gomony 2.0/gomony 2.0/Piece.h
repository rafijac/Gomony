#pragma once
using namespace std;
#include <string>

class Piece
{
public:
	Piece();
	~Piece();
	Piece(const Piece &other);
	Piece(int num, char col) { pieceNum = num; color = col;king = false; };
	void setLocation(int row, int col);
	void setLocation(string loc) { myLocation = loc; }
	string getLocation() { return myLocation; };
	void setPieceNum(int x) { pieceNum = x; };
	int getPieceNum() { return pieceNum; };
	char getColor() { return color; };
	bool isKing() { return king; };

	void operator=(Piece other);//only using this to change location so only need to copy that

private:
	int pieceNum;
	string myLocation;
	bool king;
	char color;

};
