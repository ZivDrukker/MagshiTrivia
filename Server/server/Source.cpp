#pragma comment (lib, "ws2_32.lib")

#include "WSAInitializer.h"
#include "TriviaServer.h"
#include <iostream>
#include <exception>

using namespace std;

int main()
{
	_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);
	try
	{
		WSAInitializer wsaInit;
		TriviaServer myServer;

		myServer.Server();
	}
	catch (exception& e)
	{
		cout << "Error occured: " << e.what() << endl;
	}
	system("PAUSE");
	return 0;
}