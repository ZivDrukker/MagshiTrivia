#include "TriviaServer.h"

TriviaServer::TriviaServer()
{
	_socket = ::socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

	if (_socket == INVALID_SOCKET)
	{
		throw std::exception(__FUNCTION__ " - socket");
	}

	//DB constractor activation !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
}


TriviaServer::~TriviaServer()
{
	for (auto it = _connectedUsers.begin(); it != _connectedUsers.end(); it++)
	{
		delete it->second;

		try
		{
			::closesocket(it->first);
		}
		catch (...) {}
	}

	for (auto it = _roomsList.begin(); it != _roomsList.end(); it++)
	{
		delete it->second;
	}

	try
	{
		::closesocket(_socket);
	}
	catch (...) {}

}

void TriviaServer::Server()
{
	bindAndListen();

	while (true)
	{
		cout << "Waiting for client connection request" << endl;
		accept();
	}
}

void TriviaServer::bindAndListen()
{
	struct sockaddr_in sa = { 0 };

	sa.sin_port = htons(PORT);
	sa.sin_family = AF_INET;
	sa.sin_addr.s_addr = INADDR_ANY;


	if (::bind(_socket, (struct sockaddr*)&sa, sizeof(sa)) == SOCKET_ERROR)
	{
		throw std::exception(__FUNCTION__ " - bind");
	}

	if (::listen(_socket, SOMAXCONN) == SOCKET_ERROR)
	{
		throw std::exception(__FUNCTION__ " - listen");
	}

	cout << "Listening on port " << PORT << endl;
}

void TriviaServer::accept()
{
	SOCKET _clientSocket = ::accept(_socket, NULL, NULL);

	if (_clientSocket == INVALID_SOCKET)
	{
		throw std::exception(__FUNCTION__);
	}

	cout << "Client accepted. Server and client can speak" << endl;

	thread clientHndlr(&TriviaServer::clientHandler, this, _clientSocket);

	clientHndlr.detach();
}

void TriviaServer::clientHandler(SOCKET sock)
{
	int code = Helper::getMessageTypeCode(sock);

	try
	{
		while (code != 0 && code != EXIT)
		{
			addReceivedMessages(buildReceivedMessage(sock, code));

			code = Helper::getMessageTypeCode(sock);
		}
	}
	catch(...){}//exit mesasge afterwards anyway

	addReceivedMessages(buildReceivedMessage(sock, EXIT));
}

void TriviaServer::addReceivedMessages(ReceivedMessage* msg)
{
	_mtxReceivedMessage.lock();

	_queReceivedMessages.push(msg);

	_mtxReceivedMessage.unlock();
}

ReceivedMessage * TriviaServer::buildReceivedMessage(SOCKET sock, int code)
{
	return new ReceivedMessage(sock, code);
}
