#pragma once
#include "user.h"
#include "Database.h"
#include "RecievedMessage.h"
#include <map>
#include <mutex>
#include <queue>


using std::map;
using std::mutex;
using std::queue;

class TriviaServer
{
public:
	TriviaServer();
	~TriviaServer();
	void Server();

private:
	void bindAndListen();
	void accept();
	void clientHandler(SOCKET);

	void safeDeleteUserMessage(RecievedMessage*);

	User* handleSignIn(RecievedMessage*);
	bool handleSignUp(RecievedMessage*);

	void handleLeaveGame(RecievedMessage*);
	void handleStartGame(RecievedMessage*);
	void handlePlayerAnswer(RecievedMessage*);

	bool handleCreateRoom(RecievedMessage*);
	bool handleCloseRoom(RecievedMessage*);
	bool handleJoinRoom(RecievedMessage*);
	bool handleLeaveRoom(RecievedMessage*);
	void handleGetUserInRoom(RecievedMessage*);

	void handleGetBestScores(RecievedMessage*);
	void handleRecievedMessages();
	void addRecievedMessages(RecievedMessage*);
	RecievedMessage* buildRecieveMessage(SOCKET ,int);

	User* getUserByName(string);
	User* getUserBySocket(SOCKET);
	Room* getRoomById(int);

	SOCKET _socket;
	map<SOCKET, User*>_connectedUsers;
	DataBase _db;
	map<int, Room*> _roomsList();
	mutex _mtxRecievedMessage;
	queue<RecievedMessage*> _queRecievedMessages;

};