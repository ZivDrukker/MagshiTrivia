#pragma once
#include <string>
#include <iostream>
#include <WinSock2.h>

class Game;
class Room;

using std::string;

class User
{
public:
	User(string username, SOCKET sock);
	~User() = default;
	void send(string);
	string getUsername();
	SOCKET getSocket();
	Room* getRoom();
	Game* getGame();
	void setGame(Game*);
	void clearRoom();
	bool createRoom(string, int, int, int);
	bool joinRoom(Room*);
	void leaveRoom();
	int closeRoom();
	bool leaveGame();


private:
	string _username;
	Room* _currRoom;
	Game* _currGame;
	SOCKET _sock;
};