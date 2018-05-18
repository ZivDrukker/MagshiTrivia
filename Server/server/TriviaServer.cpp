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

User* TriviaServer::handleSignIn(ReceivedMessage* msg)
{
	bool check = true;
	vector<string> unameAndPass = msg->getValues();
	if (_users.find(unameAndPass[0]) != _users.end())
	{
		if (_users[unameAndPass[0]] == unameAndPass[1])
		{
			for (auto it = _connectedUsers.begin(); it != _connectedUsers.end() && check; it++)
			{
				if (it->second->getUsername() == unameAndPass[0])
				{
					check = false;
				}
			}

			if (check)
			{
				return new User(unameAndPass[0], msg->getSock());
			}
		}
	}
	return nullptr;
}

bool TriviaServer::handleSignUp(ReceivedMessage* msg)
{
	vector<string> values = msg->getValues();
	SOCKET sock = msg->getSock();
	if (Validator::isPasswordValid(values[1]))
	{
		if (Validator::isUsernameValid(values[0]))
		{
			if (_users.find(values[0]) == _users.end())
			{
				if (Validator::isEmailValid(values[2]))
				{
					::send(sock, "1040", 4, 0);
					return true;
				}
				else
				{
					::send(sock, "1044", 4, 0);
				}
			}
			else
			{
				::send(sock, "1042", 4, 0);
			}
		}
		else
		{
			::send(sock, "1043", 4, 0);
		}
	}
	else
	{
		::send(sock, "1041", 4, 0);
	}
	return false;
}

void TriviaServer::handleReceivedMessages()
{
	User* usr;
	while (true)
	{
		_mtxReceivedMessage.lock();

		if (!_queReceivedMessages.empty())
		{
			ReceivedMessage* message = _queReceivedMessages.front();
			_queReceivedMessages.pop();
			vector<string> msg = message->getValues();
			int code = message->getMessageCode();

			switch (code)
			{
			case SIGN_IN:
				usr = handleSignIn(message);
				if (usr != nullptr)
				{
					_connectedUsers.insert(std::make_pair(message->getSock(), usr));
				}
				break;

			case SIGN_OUT:
				break;

			case SIGN_UP:
				handleSignUp(message);
				break;

			case ROOMS_REQ:
				break;

			case ROOMS_USER:
				break;

			case ROOM_JOIN_REQ:
				break;

			case ROOM_LEAVE_REQ:
				break;

			case ROOM_CREATE_REQ:
				break;

			case ROOM_CREATE_REPLY:
				break;

			case ROOM_CLOSE_REQ:
				break;

			case GAME_LEAVE_MSG:
				break;

			case GAME_SATRT:
				break;

			case ANSWER_SEND:
				break;

			case HIGH_SCORES_REQ:
				break;

			case PERSONAL_STATUS_REQ:
				break;

			case EXIT:
				break;

			default:
				cout << "hey ucf if you get here you are the best!!!\n https://www.youtube.com/watch?v=X9QfZU3xbDk" << endl;
				break;
			}
		}
	}
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
