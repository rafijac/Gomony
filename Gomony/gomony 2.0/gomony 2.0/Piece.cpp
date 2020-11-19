#include "Piece.h"



Piece::Piece()
{
	king = false;
}


Piece::Piece(const Piece & other)
{
	this->pieceNum = other.pieceNum;
	this->myLocation = other.myLocation;
	this->king = other.king;
	this->color = other.color;
}

Piece::~Piece()
{
}

void Piece::setLocation(int row, int col)
{
	myLocation += row + 'A';
	myLocation += (col + '0');
}

void Piece::operator=(Piece other)
{
	this->pieceNum = other.pieceNum;
	this->myLocation = other.myLocation;
	this->king = other.king;
	this->color = other.color;
}

