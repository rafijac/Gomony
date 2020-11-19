#pragma once
#include "Piece.h"
#include <string>
using namespace std;

//going up
class Black : public Piece
{
public:
	Black();
	~Black();
	char piece = 'X';
};