#include "Game.h"

Game::Game(const vector<User*>& users, int questionsNo, DataBase* db)
{
	_db = db;
	_players = users;
	_questions_no = questionsNo;
}

Game::~Game()
{
	_questions.clear();
	_players.clear();
}

void Game::sendFirstQuestion()
{
}

void Game::handleFinishGame()
{
}

bool Game::handleNextTurn()
{
	return false;
}

bool Game::handleAnswerFromUser()
{
	return false;
}

bool Game::leaveGame(User* user)
{
	for (unsigned int i = 0; i < _players.size(); i++)
	{
		if (user == _players[i])
		{
			_players.erase(_players.begin() + i);
			if (_players.size() > 0)
			{
				handleNextTurn();
				return true;
			}
		}
	}
	return false;
}
//
//int Game::getId()
//{
//	return _id;
//}

bool Game::insertGameToDb()
{
	return false;
}

void Game::initQuestionsFromDb()
{
}

void Game::sendQuestionsToAllUsers()
{
	_currentTurnAnswer = 0;
	Question* q = _questions.front();
	_questions.erase(_questions.begin());
	string* ans = q->getAnswers();
	string msg = "118###" + q->getQuestion();
	for (unsigned int i = 0; i < 4; i++)
	{
		msg += "###";
		msg += ans[i];
	}

	for (unsigned int i = 0; i < _players.size(); i++)
	{
		::send(_players[i]->getSocket(), msg.c_str(), msg.size(), 0);

	}
}
