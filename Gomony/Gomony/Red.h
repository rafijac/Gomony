#pragma once
#include "Piece.h"
#include <string>
using namespace std;

//going down
class Red : public Piece
{
public:
	Red();
	~Red();
	char piece = 'O';
};
