#pragma once

#include <WinSock2.h>
#include <Windows.h>
#include <queue>
#include <thread>
#include <mutex>
#include <iostream>
#include <map>
#include <vector>
#include <algorithm>
#include <iterator>
#include <iostream>
#include <sstream>

#define BUFFER_SIZE 16

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

using std::queue;
using std::string;
using std::cin;
using std::cout;
using std::thread;
using std::unique_lock;
using std::mutex;




class Server
{
public:
	Server();
	~Server();
	void serve(int port);

private:

	void accept();
	void clientHandler(SOCKET clientSocket);
	void handleMessage();

	SOCKET _serverSocket;
	queue<string> _msg;
	mutex mtx;
	//map<string, string> _users;
	unique_lock<mutex> _ul = unique_lock<mutex>(mtx, std::defer_lock);
};

