#include "Room.h"

Room::Room(int id, User* admin, string name, int maxUser, int qNo, int qTime, DataBase* db)
{
	_id = id;
	_admin = admin;
	_name = name;
	_maxUsers = maxUser;
	_questionTime = qTime;
	_questionNo = qNo;
	_users.push_back(admin);
	_game = nullptr;
	_db = db;
}

bool Room::joinRoom(User* user)
{
	for (unsigned int i = 0; i < _users.size(); i++)
	{
		if (_users[i] == user)
		{
			return false;
		}
	}
	if (_users.size() == _maxUsers)
	{
		return false;
	}

	_users.push_back(user);

	string toSend = getUsersListMessage();

	sendMessage(toSend);
	
	return true;
}

void Room::leaveRoom(User* user)
{
	for (unsigned int i = 0; i < _users.size(); i++)
	{
		if (_users[i] == user)
		{
			_users.erase(_users.begin() + i);
			sendMessage(user, "1120");
		}
	}
	sendMessage(user, "The user: " + user->getUsername() + " has left the room!");
}

int Room::closeRoom(User* admin)
{
	if (_admin == admin)
	{
		sendMessage("116");
		for (unsigned int i = 0; i < _users.size(); i++)
		{
			_users.erase(_users.begin() + i);
		}
		return _id;
	}
	return -1;
}

vector<User*> Room::getUsers()
{
	return _users;
}

string Room::getUsersListMessage()
{
	string names = "108" + _users.size();
	for (unsigned int i = 0; i < _users.size(); i++)
	{
		names += _users[i]->getUsername();
		if (i + 1 != _users.size())
		{
			names += "##";
		}
	}
	return names;
}

int Room::getQuestionsNo()
{
	return _questionNo;
}

int Room::getQuestionTime()
{
	return _questionTime;
}

string Room::getName()
{
	return _name;
}

void Room::startGame()
{
	_game = new Game(_users, _questionNo, _db, _admin);
}

void Room::sendMessage(string msg)
{
	sendMessage(nullptr, msg);
}

void Room::sendMessage(User* user, string msg)
{
	for (unsigned int i = 0; i < _users.size(); i++)
	{
		if (_users[i] != user)
		{
			::send(_users[i]->getSocket(), msg.c_str(), msg.size(), 0);
		}
	}
}

