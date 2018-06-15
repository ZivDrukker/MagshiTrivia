#pragma once
#include <vector>
#include "Question.h"
#include "sqlite3.h"

using std::vector;
using std::string;
using std::cout;
using std::endl;

class DataBase
{
public:
	DataBase();
	~DataBase();
	bool isUserExists(string);
	bool addNewUser(string, string, string);
	bool isUserAndPassMatch(string, string);
	vector<Question*> initQuestions(int);
	vector<string> getBestScores();
	vector<string> getPersonalStatus(string);
	int insertNewGame();
	bool updateGameStatus(int);
	bool addAnswerToPlayer(int, string, int, string, bool, int);
	int getNewGameID();



private:
	static int callbackCount(void*, int, char**, char**);
	static int callback(void*, int, char**, char**);
	void checkErr();

	sqlite3* _db;
	char* _zErrMsg;
	int _rc;

};