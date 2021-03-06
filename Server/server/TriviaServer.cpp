#include "TriviaServer.h"

TriviaServer::TriviaServer()
{
	_socket = ::socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

	if (_socket == INVALID_SOCKET)
	{
		throw std::exception(__FUNCTION__ " - socket");
	}

	_db = new DataBase();
	_roomIdSequence = _db->getMaxID();
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
	thread handleRcv(&TriviaServer::handleReceivedMessages, this);
	handleRcv.detach();

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
	sendAndRecieveKey(sock);
	int code = Helper::getMessageTypeCode(sock);

	try
	{
		while (code != EXIT)
		{
			if (code > 100 && code < 300)
			{
				addReceivedMessages(buildReceivedMessage(sock, code));
			}
			code = Helper::getMessageTypeCode(sock);
		}
	}
	catch(...){}//exit mesasge afterwards anyway

	addReceivedMessages(buildReceivedMessage(sock, EXIT));
}

void TriviaServer::safeDeleteUserMessage(ReceivedMessage* msg)
{
	//no need for signout because of our build - can send '299' only when logged out
	::closesocket(msg->getSock());
}

User* TriviaServer::handleSignIn(ReceivedMessage* msg)
{
	bool check = true;
	vector<string> unameAndPass = msg->getValues();
	if (_db->isUserAndPassMatch(unameAndPass[0], unameAndPass[1]))
	{
		for (auto it = _connectedUsers.begin(); it != _connectedUsers.end(); it++)
		{
			if (it->second->getUsername() == unameAndPass[0])
			{
				::send(msg->getSock(), encrypto("1022", msg->getSock()), 4, 0);
				return nullptr;
			}
		}

		::send(msg->getSock(), encrypto("1020", msg->getSock()), 4, 0);
		return new User(string(unameAndPass[0]), msg->getSock());
	}
	else
	{
		::send(msg->getSock(), encrypto("1021", msg->getSock()), 4, 0);
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
			if (_db->isUserExists(values[0]))
			{
				if (Validator::isEmailValid(values[2]))
				{
					//_connectedUsers.insert(std::make_pair(sock, new User(values[0], sock)));
					_db->addNewUser(values[0], values[1], values[2]);
					::send(sock, encrypto("1040", msg->getSock()), 4, 0);
					return true;
				}
				else
				{
					::send(sock, encrypto("1044", msg->getSock()), 4, 0);
				}
			}
			else
			{
				::send(sock, encrypto("1042", msg->getSock()), 4, 0);
			}
		}
		else
		{
			::send(sock, encrypto("1043", msg->getSock()), 4, 0);
		}
	}
	else
	{
		::send(sock, encrypto("1041", msg->getSock()), 4, 0);
	}
	return false;
}

void TriviaServer::handleSignOut(ReceivedMessage* msg)
{
	if (msg->getUser() != nullptr)
	{
		try
		{
			if (msg->getUser()->getRoom() != nullptr)
			{
				msg->getUser()->closeRoom();
				msg->getUser()->leaveRoom();
				msg->getUser()->leaveGame();
			}
		}
		catch (exception e)
		{
			cout << e.what() <<endl;
		}

		for (auto it = _connectedUsers.begin(); it != _connectedUsers.end(); it++)
		{
			if (it->second->getUsername() == msg->getUser()->getUsername())
			{
				delete it->second;
				_connectedUsers.erase(it);
				break;//sometimes there is only one solution
			}
		}
	}
}

void TriviaServer::handleLeaveGame(ReceivedMessage* msg)
{
	msg->getUser()->leaveGame();
}

void TriviaServer::handleStartGame(ReceivedMessage* msg)
{
	msg->getUser()->getRoom()->startGame();
}

void TriviaServer::handlePlayerAnswer(ReceivedMessage* msg)
{
	if (msg->getUser()->getGame() != nullptr)
	{
		vector<string> values = msg->getValues();
		if (!msg->getUser()->getGame()->handleAnswerFromUser(msg->getUser(), stoi(values[0]), stoi(values[1])))
		{
			delete msg->getUser()->getGame();
			msg->getUser()->setGame(nullptr);
		}
	}
}

void TriviaServer::handleGetBestScores(ReceivedMessage* msg)
{
	string toSend = "124";
	vector<string> answer = _db->getBestScores();

	for (unsigned int i = 0; i < answer.size(); i++)
	{
		toSend += "#" + answer[i];
	}

	::send(msg->getSock(), encrypto(toSend.c_str(), msg->getSock()), toSend.length(), 0);
}

void TriviaServer::handleGetPersonalStatus(ReceivedMessage* msg)
{
	string toSend = "126";
	vector<string> answer = _db->getPersonalStatus(msg->getUser()->getUsername());

	for (unsigned int i = 0; i < answer.size(); i++)
	{
		toSend += "#" + answer[i];
	}

	::send(msg->getSock(), encrypto(toSend.c_str(), msg->getSock()), toSend.length(), 0);
}

void TriviaServer::handleReceivedMessages()
{
	User* usr;

	while (true)
	{

		if (!_queReceivedMessages.empty())
		{
			_mtxReceivedMessage.lock();

			ReceivedMessage* message = _queReceivedMessages.front();

			_queReceivedMessages.pop();
			
			_mtxReceivedMessage.unlock();

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
				handleSignOut(message);
				break;

			case SIGN_UP:
				handleSignUp(message);
				break;

			case ROOMS_REQ:
				handleGetRooms(message);
				break;

			case ROOMS_USER:
				handleGetUserInRoom(message);
				break;

			case ROOM_JOIN_REQ:
				handleJoinRoom(message);
				break;

			case ROOM_LEAVE_REQ:
				handleLeaveRoom(message);
				break;

			case ROOM_CREATE_REQ:
				if (handleCreateRoom(message))
				{
					::send(message->getSock(), encrypto("1140", message->getSock()), 4, 0);
				}
				else
				{
					::send(message->getSock(), encrypto("1141", message->getSock()), 4, 0);
				}
				break;

			case ROOM_CLOSE_REQ:
				handleCloseRoom(message);
				break;

			case GAME_LEAVE_MSG:
				handleLeaveGame(message);
				break;

			case GAME_START:
				handleStartGame(message);
				break;

			case ANSWER_SEND:
				handlePlayerAnswer(message);
				break;

			case HIGH_SCORES_REQ:
				handleGetBestScores(message);
				break;

			case PERSONAL_STATUS_REQ:
				handleGetPersonalStatus(message);
				break;

			case EXIT:
				safeDeleteUserMessage(message);
				break;

			default:
				cout << "Hey Ucf, if you get here you are the best!!!\n https://www.youtube.com/watch?v=X9QfZU3xbDk" << endl;
				break;
			}
		}
	}
}

void TriviaServer::addReceivedMessages(ReceivedMessage* msg)
{

	cout << msg->getMessageCode() << " ";

	for (unsigned int i = 0; i < msg->getValues().size(); i++)
	{
		cout << msg->getValues()[i] << " ";
	}

	cout << endl;
	_mtxReceivedMessage.lock();

	_queReceivedMessages.push(msg);

	_mtxReceivedMessage.unlock();
}

ReceivedMessage * TriviaServer::buildReceivedMessage(SOCKET sock, int code)
{
	ReceivedMessage* msg = new ReceivedMessage(sock, code);
	msg->setUser(getUserBySocket(sock));
	return msg;
}

User* TriviaServer::getUserByName(string name)
{
	for (auto it = _connectedUsers.begin(); it != _connectedUsers.end(); it++)
	{
		if (it->second->getUsername() == name)
		{
			return it->second;
		}
	}
	return nullptr;
}

User * TriviaServer::getUserBySocket(SOCKET sock)
{
	for (auto it = _connectedUsers.begin(); it != _connectedUsers.end(); it++)
	{
		if (it->first == sock)
		{
			return it->second;
		}
	}
	return nullptr;
}

Room* TriviaServer::getRoomById(int id)
{
	if (id <= _roomIdSequence && id > 0)
	{
		return _roomsList[id];
	}
	return nullptr;
}

bool TriviaServer::handleCreateRoom(ReceivedMessage* msg)
{
	if (msg->getUser() == nullptr)
	{
		return false;
	}

	vector<string> values = msg->getValues();

	_roomIdSequence++;

	Room* r = new Room(_roomIdSequence, msg->getUser(), values[0], atoi(values[1].c_str()), atoi(values[2].c_str()), atoi(values[3].c_str()), _db);

	_roomsList.insert(std::make_pair(_roomIdSequence, r));

	return true;
}

bool TriviaServer::handleCloseRoom(ReceivedMessage* msg)
{
	int id = msg->getUser()->getRoom()->closeRoom(msg->getUser());
	if (id == -1)
	{
		return false;
	}
	
	Room* r = getRoomById(id);
	for (auto it = _roomsList.begin(); it != _roomsList.end(); ++it)
	{
		if (it->second == r)
		{
			_roomsList.erase(it);
			break; // need to exit the loop
		}

	}
	delete r;
	return true;
}

bool TriviaServer::handleJoinRoom(ReceivedMessage* msg)
{
	if (msg->getUser() == nullptr)
	{
		return false;
	}
	
	vector<string> values = msg->getValues();

	Room* r = getRoomById(atoi(values[0].c_str()));

	if (r == nullptr)
	{
		::send(msg->getSock(), encrypto("1102", msg->getSock()), 101, 0);
		return false;
	}
	string toSend = "1100#" + std::to_string(r->getQuestionsNo()) + "#" + std::to_string(r->getQuestionTime());

	if (!msg->getUser()->joinRoom(r))
	{
		::send(msg->getSock(), encrypto("1101", msg->getSock()), 101, 0);
		return false;

	}
	else
	{
		::send(msg->getSock(), encrypto(toSend.c_str(), msg->getSock()), toSend.length(), 0);
	}

	return true;
}

bool TriviaServer::handleLeaveRoom(ReceivedMessage* msg)
{
	msg->getUser()->leaveRoom();
	return false;
}

void TriviaServer::handleGetUserInRoom(ReceivedMessage* msg)
{
	vector<string> values = msg->getValues();

	Room* r = getRoomById(atoi(values[0].c_str()));
	string toSend = r->getUsersListMessage();

	::send(msg->getSock(), encrypto(toSend.c_str(), msg->getSock()), toSend.length(), 0);
}

void TriviaServer::handleGetRooms(ReceivedMessage* msg)
{
	string toSend = "106";
	string count = std::to_string(_roomIdSequence);
	toSend += "##";
	toSend += count;

	for (auto it = _roomsList.begin(); it != _roomsList.end(); it++)
	{
		toSend += "##";
		toSend += std::to_string(it->first);
		toSend += "##";
		toSend += it->second->getName();
	}

	::send(msg->getSock(), encrypto(toSend.c_str(), msg->getSock()), toSend.length(), 0);
}
