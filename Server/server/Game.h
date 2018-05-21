#pragma once
#include <vector>
#include "user.h"
#include <map>
#include "Question.h"
#include "Database.h"

using std::vector;
using std::map;

class Game
{
public:
	Game(const vector<User*>&, int, DataBase*, User*);
	~Game();
	void sendFirstQuestion();
	void handleFinishGame();
	bool handleNextTurn();
	bool handleAnswerFromUser();
	bool leaveGame(User*);
	//int getId();

private:
	bool insertGameToDb();
	void initQuestionsFromDb();
	void sendQuestionsToAllUsers();

	vector<Question*> _questions;
	vector<User*> _players;
	int _questions_no;
	int _currQuestionIndex;
	DataBase* _db;// needed to be & but not working
	map<string, int> _results;
	int _currentTurnAnswer;
	User* _admin;
};