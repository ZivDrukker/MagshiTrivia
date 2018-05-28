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
	bool handleAnswerFromUser(User*, int, int);
	bool leaveGame(User*);

private:
	void sendQuestionsToAllUsers();

	vector<Question*> _questions;
	vector<User*> _players;
	int _questions_no;
	int _currQuestionIndex;
	DataBase* _db;// needed to be & but not working
	map<string, int> _results;
	unsigned int _currentTurnAnswer;
	User* _admin;
	int _id;
};