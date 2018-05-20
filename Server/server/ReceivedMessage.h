#pragma once
#include "user.h"
#include "Helper.h"
#include <vector>

#define BUFFER_SIZE 4096

using std::vector;

class ReceivedMessage
{
public:
	ReceivedMessage(SOCKET, int);
	ReceivedMessage(SOCKET, int, vector<string>);
	SOCKET getSock();
	User* getUser();
	void setUser(User*);
	int getMessageCode();
	vector<string>& getValues();

private:
	SOCKET _sock;
	User* _user;
	int _messageCode;
	vector<string> _values;

};