#include "Game.h"

Game::Game(const vector<User*>& users, int questionsNo, DataBase* db, User* admin, int id)
{
	_id = id;
	_db = db;
	_players = users;
	_questions_no = questionsNo;
	_admin = admin;
	_currentTurnAnswer = 0;
	if (!_db->insertNewGame())
	{
		throw(std::invalid_argument("The game isnt starting"));
	}

	_questions = db->initQuestions(questionsNo);

	for (unsigned int i = 0; i < users.size(); i++)
	{
		_results.insert(std::make_pair(users[i]->getUsername(), 0));
		users[i]->setGame(this);
	}

	sendFirstQuestion();
}

Game::~Game()
{
	_questions.clear();
	_players.clear();
}

void Game::sendFirstQuestion()
{
	string toSend = "118##";
	Question* q = _questions.front();

	string question = q->getQuestion();

	if (question.length() == 0)
	{
		::send(_admin->getSocket(), "1180", 4, 0);
		return;
	}

	toSend += question;

	string* answers = q->getAnswers();

	for (unsigned int i = 0; i < 4; i++)
	{
		toSend += "##";
		toSend += answers[i];
	}

	for (auto it = _players.begin(); it != _players.end(); it++)
	{
		::send((*it)->getSocket(), toSend.c_str(), toSend.length(), 0);
	}

}

void Game::handleFinishGame()
{
	_db->updateGameStatus(1);

	string toSend = "121";

	for (auto it = _results.begin(); it != _results.end(); it++)
	{
		toSend += "##";
		toSend += it->first;
		toSend += "##";
		toSend += std::to_string(it->second);
	}

	for (unsigned int i = 0; i < _players.size(); i++)
	{
		::send(_players[i]->getSocket(), toSend.c_str(), toSend.length(), 0);
	}

	_players.clear();

}

bool Game::handleNextTurn()
{

	if (_currentTurnAnswer < _players.size())
	{
		return true;
	}

	if (_questions.size() == 0)
	{
		handleFinishGame();
		return false;
	}
	else
	{
		_questions.erase(_questions.begin());
		sendQuestionsToAllUsers();
	}

	return true;
}


bool Game::handleAnswerFromUser(User* usr, int index, int time)
{
	_currentTurnAnswer++;

	Question* q = _questions.front();
	int correctIndex = q->getCorrectAnswerIndex();
	string* ans = q->getAnswers();

	if (index < 5)
	{
		_db->addAnswerToPlayer(_id, usr->getUsername(), _questions_no - _questions.size(), ans[index - 1], correctIndex == index, time);
	}
	else
	{
		_db->addAnswerToPlayer(_id, usr->getUsername(), _questions_no - _questions.size(), "", correctIndex == index, time);
	}


	if (correctIndex == index)
	{
		::send(usr->getSocket(), "120#1", 5, 0);
		_results[usr->getUsername()]++;
	}
	else
	{
		::send(usr->getSocket(), "120#0", 5, 0);
	}

	return handleNextTurn();
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



void Game::sendQuestionsToAllUsers()
{
	if (_questions.size() != 0)
	{
		_currentTurnAnswer = 0;
		Question* q = _questions.front();
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
	else
	{
		handleFinishGame();
	}
}
