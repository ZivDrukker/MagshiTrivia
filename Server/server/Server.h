#pragma once

#include <WinSock2.h>
#include <Windows.h>
#include <queue>
#include <thread>
#include <mutex>

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

	SOCKET _serverSocket;
	queue<string> _msg;
	mutex mtx;
	unique_lock<mutex> _ul = unique_lock<mutex>(mtx, defer_lock);
};

