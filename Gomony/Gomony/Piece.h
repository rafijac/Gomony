#pragma once
using namespace std;
#include <string>

class Piece
{
public:
	Piece();
	~Piece();
	void setLocation(int row, int col);
	string getLocation() { return myLocation; };
	void setPieceNum(int x) { pieceNum = x; };
	int getPieceNum() { return pieceNum; };
	void setColor(string x) { color = x; };
	string getColor() { return color; };

private:
	bool isKing;
	string myLocation;
	int pieceNum;
	string color;
};
