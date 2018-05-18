#include "RecievedMessage.h"

RecievedMessage::RecievedMessage(SOCKET sock, int msgCode)
{
	_sock = sock;
	_messageCode = msgCode;
}

RecievedMessage::RecievedMessage(SOCKET sock, int msgCode, vector<string> values)
{
	_sock = sock;
	_messageCode = msgCode;
	_values = values;
}

SOCKET RecievedMessage::getSock()
{
	return _sock;
}

User * RecievedMessage::getUser()
{
	return _user;
}

void RecievedMessage::setUser(User* usr)
{
	_user = usr;
}

int RecievedMessage::getMessageCode()
{
	return _messageCode;
}

vector<string>& RecievedMessage::getValues()
{
	return _values;
}
