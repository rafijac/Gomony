#include <iostream>
#include <string>
#include <queue>
#include <vector>
using namespace std;

enum Color { Red, Black };
int main() {

	//gameplay here-------------------------------------------------------------------------------
	string currLocation, nextLocation;
	Color color = Red;
	int moveNum = 0;
	int choice = 1;
	cout << "GOOD LUCK!!!" << endl;

	while (choice != 0) {
		cout << color << "turn" << endl;
		//find location of the piece
		cout << "which piece are you moving?" << endl;
		cin >> currLocation;

		//found out where the player would like to move it
		cout << "where do you wanna move it?" << endl;
		cin >> nextLocation;

		//move the piece
		move(board, currLocation, nextLocation, piece);
		print(board);

		//change whos turn it is -----------------------------------------------------------------------------------

		moveNum++;
		if (moveNum % 2 == 0) {
			color = Red;
		}
		else
			color = Black;

		//----------------------------------------------------------------------------------------------------------

		cout << "would you like to continue playing 1 for yes 0 for no \n";
		cin >> choice;
	}

	system("pause");
	delAll(board, piece);
	return 0;
}
