#include "Piece.h"



Piece::Piece()
{
	isKing = false;
}


Piece::~Piece()
{
}

void Piece::setLocation(int row, int col)
{
	myLocation += row + 'A';
	myLocation += (col + '0');
}

