//#include "Server.h"
//
//
////using namespace std;
//
//
//Server::Server()
//{
//	// notice that we step out to the global namespace
//	// for the resolution of the function socket
//	
//	// this server use TCP. that why SOCK_STREAM & IPPROTO_TCP
//	// if the server use UDP we will use: SOCK_DGRAM & IPPROTO_UDP
//	_serverSocket = ::socket(AF_INET,  SOCK_STREAM,  IPPROTO_TCP); 
//
//	if (_serverSocket == INVALID_SOCKET)
//		throw std::exception(__FUNCTION__ " - socket");
//
//	thread msgHandler(&Server::handleMessage, this);
//}
//
//Server::~Server()
//{
//	try
//	{
//		// the only use of the destructor should be for freeing 
//		// resources that was allocated in the constructor
//		::closesocket(_serverSocket);
//	}
//	catch (...) {}
//}
//
//void Server::serve(int port)
//{
//	
//	struct sockaddr_in sa = { 0 };
//	
//	sa.sin_port = htons(port); // port that server will listen for
//	sa.sin_family = AF_INET;   // must be AF_INET
//	sa.sin_addr.s_addr = INADDR_ANY;    // when there are few ip's for the machine. We will use always "INADDR_ANY"
//
//	/*
//	// Get the local host information
//localHost = gethostbyname("");
//localIP = inet_ntoa (*(struct in_addr *)*localHost->h_addr_list);
//	*/
//
//	// again stepping out to the global namespace
//	// Connects between the socket and the configuration (port and etc..)
//	if (::bind(_serverSocket, (struct sockaddr*)&sa, sizeof(sa)) == SOCKET_ERROR)
//		throw std::exception(__FUNCTION__ " - bind");
//	
//	// Start listening for incoming requests of client
//	/*
//	input:
//	socket
//	backlog:The maximum length of the queue of pending connections.
//	*/
//	if (::listen(_serverSocket, SOMAXCONN) == SOCKET_ERROR)
//		throw std::exception(__FUNCTION__ " - listen");
//	cout << "Listening on port " << port << endl;
//
//	//main msg func !!!!!!!!!!!
//
//	while (true)
//	{
//		// the main thread is only accepting clients 
//		// and add then to the list of handlers
//		cout << "Waiting for client connection request" << endl;
//		accept();
//	}
//}
//
//
//void Server::accept()
//{
//	// this accepts the client and create a specific socket from server to this client
//
//	//input: socket , sockaddr* addr,int* addrlen
//	//	output: socket -- wait(block) until client connected
//	//
//	client_socket = ::accept(_serverSocket, NULL, NULL);
//
//	if (client_socket == INVALID_SOCKET)
//		throw std::exception(__FUNCTION__);
//
//	cout << "Client accepted. Server and client can speak" << endl;
//
//	// the function that handle the conversation with the client
//	thread handler(&Server::clientHandler, this, client_socket);
//
//	handler.detach();
//}
//
//
//void Server::clientHandler(SOCKET clientSocket)
//{
//
//	try
//	{
//		/*
//		string s = "Welcome! What is your name (4 bytes)? ";
//		::send(clientSocket, s.c_str(), s.size(), 0);  // last parameter: flag. for us will be 0.
//		*/
//		char m[BUFFER_SIZE];
//		::recv(clientSocket, m, 4, 0);
//
//		_ul.lock();
//		_msg.push(std::make_pair(m, clientSocket));
//		_ul.unlock();
//
//		//m[4] = 0;
//		//cout << "Client name is: " << m << endl;
//
//		//s = "Bye";
//		//::send(clientSocket, s.c_str(), s.size(), 0);
//		
//		// Closing the socket (in the level of the TCP protocol)
//		::closesocket(clientSocket); 
//	}
//	catch (const std::exception& e)
//	{
//		cout << e.what() << endl;
//		::closesocket(clientSocket);
//	}
//
//}
//
//
//void Server::handleMessage()
//{
//	while (true)
//	{
//		_ul.lock();
//
//		if (!_msg.empty())
//		{
//			pair<string,SOCKET> message = _msg.front();
//			_msg.pop();
//			string msg = message.first;
//			SOCKET socket = message.second;
//			int code = atoi(msg.substr(0, 3).c_str());
//
//			vector<string> msgs = split(msg, '#');
//			
//			switch (code)
//			{
//			case SIGN_IN:
//				signinHandler(msgs, socket);
//				break;
//
//			case SIGN_OUT:
//				break;
//
//			case SIGN_UP:
//				signupHandler(msgs, socket);
//				break;
//
//			case ROOMS_REQ:
//				break;
//
//			case ROOMS_USER:
//				break;
//
//			case ROOM_JOIN_REQ:
//				break;
//
//			case ROOM_LEAVE_REQ:
//				break;
//
//			case ROOM_CREATE_REQ:
//				break;
//
//			case ROOM_CREATE_REPLY:
//				break;
//
//			case ROOM_CLOSE_REQ:
//				break;
//
//			case GAME_LEAVE_MSG:
//				break;
//
//			case GAME_SATRT:
//				break;
//			
//			case ANSWER_SEND:
//				break;
//
//			case HIGH_SCORES_REQ:
//				break;
//			
//			case PERSONAL_STATUS_REQ:
//				break;
//			
//			case EXIT:
//				break;
//
//			default:
//				cout << "hey ucf if you get here you are the best!!!\n https://www.youtube.com/watch?v=X9QfZU3xbDk" << endl;
//				break;
//			}
//		}
//	}
//}
//
//vector<string> Server::split(string& str, char delim)
//{
//	vector<string> words;
//	std::stringstream ss(str);
//	std::string token;
//	while (getline(ss, token, delim)) {
//		words.push_back(token);
//	}
//
//	for (unsigned int i = 0; i < words.size(); i++)
//	{
//		if (words[i] == "")
//		{
//			words.erase(words.begin() + i);
//		}
//	}
//	return words;
//}
//
//void Server::signupHandler(vector<string> msgs, SOCKET socket)
//{
//	if(_users.find(msgs[1]) != _users.end())
//	{
//		if (msgs[1].length() >= 4)
//		{
//			if (msgs[2].length() >= 4)
//			{
//				if (msgs[3].length() > 5 && msgs[3].find("@") != std::string::npos && msgs[3].find(".") != std::string::npos)
//				{
//					_users[msgs[1]] = make_pair(msgs[2], msgs[3]);
//					connectedUsers.push_back(msgs[1]);
//					sendMsg("1040", socket);
//				}
//				else
//				{
//					sendMsg("1044", socket);
//				}
//			}
//			else
//			{
//				sendMsg("1041", socket);
//			}
//		}
//		else
//		{
//			sendMsg("1043", socket);
//		}
//	}
//	else
//	{
//		sendMsg("1042", socket);
//	}
//}
//
//void Server::signinHandler(vector<string> msgs, SOCKET socket)
//{
//	if (_users.find(msgs[1]) != _users.end())
//	{
//		if (_users[msgs[1]].first == msgs[2])
//		{
//			if (std::find(connectedUsers.begin(), connectedUsers.end(), msgs[1]) != connectedUsers.end())
//			{
//				sendMsg("1020", socket);
//			}
//			else
//			{
//				sendMsg("1022", socket);
//			}
//		}
//		else
//		{
//			sendMsg("1021", socket);
//		}
//	}
//}
//
//void Server::sendMsg(string toSend, SOCKET client_socket)
//{
//	::send(client_socket, toSend.c_str(), toSend.size(), 0);
//}