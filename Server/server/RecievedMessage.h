#pragma once
#include "user.h"
#include <vector>

using std::vector;

class RecievedMessage
{
public:
	RecievedMessage(SOCKET, int);
	RecievedMessage(SOCKET, int, vector<string>);
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