#pragma once
#include "user.h"
#include "Database.h"
#include "ReceivedMessage.h"
#include "Helper.h"
#include <map>
#include <mutex>
#include <queue>
#include <thread>
#include "Validator.h"
#include "Room.h"
#include "Database.h"

#define PORT 1337

#define BUFFER_SIZE 4096

#define SIGN_IN 200
#define SIGN_OUT 201
#define SIGN_IN_REPLY 102
#define SIGN_UP 203
#define SIGN_UP_REPLY 104

#define USERS_SEND 108

#define ROOMS_REQ 205
#define ROOMS_SEND 106
#define ROOMS_USER 207
#define ROOM_JOIN_REQ 209
#define ROOM_JOIN_REPLY 110
#define ROOM_LEAVE_REQ 211
#define ROOM_LEAVE_REPLY 112
#define ROOM_CREATE_REQ 213
#define ROOM_CREATE_REPLY 114
#define ROOM_CLOSE_REQ 215
#define ROOM_CLOSE_REPLY 116

#define GAME_END 121
#define GAME_LEAVE_MSG 222
#define GAME_SATRT 217

#define QUESTION_SEND 118
#define ANSWER_SEND 219
#define ANSWER_CORRECTNESS_REPLY 120

#define HIGH_SCORES_REQ 223
#define HIGH_SCORES_REPLY 124

#define PERSONAL_STATUS_REQ 225
#define PERSONAL_STATUS_REPLY 126

#define EXIT 299

using std::map;
using std::mutex;
using std::queue;
using std::cout;
using std::cin;
using std::endl;
using std::thread;
using std::pair;

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

	void safeDeleteUserMessage(ReceivedMessage*);

	User* handleSignIn(ReceivedMessage*);
	bool handleSignUp(ReceivedMessage*);
	void handleSignOut(ReceivedMessage*);

	void handleLeaveGame(ReceivedMessage*);
	void handleStartGame(ReceivedMessage*);
	void handlePlayerAnswer(ReceivedMessage*);

	bool handleCreateRoom(ReceivedMessage*);
	bool handleCloseRoom(ReceivedMessage*);
	bool handleJoinRoom(ReceivedMessage*);
	bool handleLeaveRoom(ReceivedMessage*);
	void handleGetUserInRoom(ReceivedMessage*);
	void handleGetRooms(ReceivedMessage*);

	void handleGetBestScores(ReceivedMessage*);
	void handleReceivedMessages();
	void addReceivedMessages(ReceivedMessage*);
	ReceivedMessage* buildReceivedMessage(SOCKET ,int);

	User* getUserByName(string);
	User* getUserBySocket(SOCKET);
	Room* getRoomById(int);

	SOCKET _socket;
	map<SOCKET, User*> _connectedUsers;
	map<string, string> _users;
	DataBase* _db;
	map<int, Room*> _roomsList;
	mutex _mtxReceivedMessage;
	queue<ReceivedMessage*> _queReceivedMessages;

	int _roomIdSequence = 0;

};