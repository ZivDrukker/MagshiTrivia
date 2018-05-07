#include "Server.h"
#include <exception>
#include <iostream>
#include <string>

using namespace std;

Server::Server()
{
	// notice that we step out to the global namespace
	// for the resolution of the function socket
	
	// this server use TCP. that why SOCK_STREAM & IPPROTO_TCP
	// if the server use UDP we will use: SOCK_DGRAM & IPPROTO_UDP
	_serverSocket = ::socket(AF_INET,  SOCK_STREAM,  IPPROTO_TCP); 

	if (_serverSocket == INVALID_SOCKET)
		throw std::exception(__FUNCTION__ " - socket");
}

Server::~Server()
{
	try
	{
		// the only use of the destructor should be for freeing 
		// resources that was allocated in the constructor
		::closesocket(_serverSocket);
	}
	catch (...) {}
}

void Server::serve(int port)
{
	
	struct sockaddr_in sa = { 0 };
	
	sa.sin_port = htons(port); // port that server will listen for
	sa.sin_family = AF_INET;   // must be AF_INET
	sa.sin_addr.s_addr = INADDR_ANY;    // when there are few ip's for the machine. We will use always "INADDR_ANY"

	/*
	// Get the local host information
localHost = gethostbyname("");
localIP = inet_ntoa (*(struct in_addr *)*localHost->h_addr_list);
	*/

	// again stepping out to the global namespace
	// Connects between the socket and the configuration (port and etc..)
	if (::bind(_serverSocket, (struct sockaddr*)&sa, sizeof(sa)) == SOCKET_ERROR)
		throw std::exception(__FUNCTION__ " - bind");
	
	// Start listening for incoming requests of client
	/*
	input:
	socket
	backlog:The maximum length of the queue of pending connections.
	*/
	if (::listen(_serverSocket, SOMAXCONN) == SOCKET_ERROR)
		throw std::exception(__FUNCTION__ " - listen");
	cout << "Listening on port " << port << endl;

	//main msg func !!!!!!!!!!!

	while (true)
	{
		// the main thread is only accepting clients 
		// and add then to the list of handlers
		cout << "Waiting for client connection request" << endl;
		accept();
	}
}


void Server::accept()
{
	// this accepts the client and create a specific socket from server to this client

	//input: socket , sockaddr* addr,int* addrlen
	//	output: socket -- wait(block) until client connected
	//
	SOCKET client_socket = ::accept(_serverSocket, NULL, NULL);

	if (client_socket == INVALID_SOCKET)
		throw std::exception(__FUNCTION__);

	cout << "Client accepted. Server and client can speak" << endl;

	// the function that handle the conversation with the client
	thread handler(&clientHandler, client_socket);

	handler.detach();
}


void Server::clientHandler(SOCKET clientSocket)
{
	_ul.lock();

	try
	{
		string s = "Welcome! What is your name (4 bytes)? ";
		::send(clientSocket, s.c_str(), s.size(), 0);  // last parameter: flag. for us will be 0.

		char m[5];
		::recv(clientSocket, m, 4, 0);
		m[4] = 0;
		cout << "Client name is: " << m << endl;

		s = "Bye";
		::send(clientSocket, s.c_str(), s.size(), 0);
		
		// Closing the socket (in the level of the TCP protocol)
		::closesocket(clientSocket); 
	}
	catch (const std::exception& e)
	{
		::closesocket(clientSocket);
	}

	_ul.lock();
}

