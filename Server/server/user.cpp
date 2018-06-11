#include "user.h"
#include "Room.h"

User::User(string username, SOCKET sock)
{
	this->_username = username;
	this->_sock = sock;
}

void User::send(string msg)
{
	::send(_sock, msg.c_str(), msg.size(), 0);
}

string User::getUsername()
{
	return _username;
}

SOCKET User::getSocket()
{
	return _sock;
}

Room * User::getRoom()
{
	return _currRoom;
}

Game * User::getGame()
{
	return _currGame;
}

void User::setGame(Game* game)
{
	_currGame = game;
}

void User::clearRoom()
{
	_currRoom = nullptr;
}

bool User::createRoom(string, int, int, int)
{
	return false;
}

bool User::joinRoom(Room* r)
{
	if (r->joinRoom(this))
	{
		return true;
	}
	return false;
}

void User::leaveRoom()
{
	if (_currRoom != nullptr)
	{
		_currRoom->leaveRoom(this);
		clearRoom();
	}
}

int User::closeRoom()
{
	int id = _currRoom->closeRoom(this);

	if (id != -1)
	{
		_currRoom = nullptr;
	}

	return id;
}

bool User::leaveGame()
{
	if (_currGame->leaveGame(this))
	{
		_currGame = nullptr;
		return false;
	}
	return true;
}

void User::setRoom(Room* room)
{
	_currRoom = room;
}