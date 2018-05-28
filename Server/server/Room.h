#pragma once
#include "user.h"
#include <vector>
#include "Database.h"
#include "Game.h"

using std::vector;

class Room
{
public:
	Room(int, User*, string, int, int, int, DataBase*);
	~Room() = default;
	bool joinRoom(User*);
	void leaveRoom(User*);
	int closeRoom(User*);
	vector<User*> getUsers();
	string getUsersListMessage();
	int getQuestionsNo();
	int getQuestionTime();
	string getName();
	void startGame();

private:
	void sendMessage(string);
	void sendMessage(User*, string);

	vector<User*> _users;
	User* _admin;
	int _maxUsers;
	int _questionTime;
	int _questionNo;
	string _name;
	int _id;
	DataBase* _db;
	Game* _game;
};