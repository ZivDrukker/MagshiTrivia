#include "ReceivedMessage.h"

ReceivedMessage::ReceivedMessage(SOCKET sock, int msgCode)
{
	_sock = sock;
	_messageCode = msgCode;
	_values = getValues();
}

ReceivedMessage::ReceivedMessage(SOCKET sock, int msgCode, vector<string> values)
{
	_sock = sock;
	_messageCode = msgCode;
	_values = values;
}

SOCKET ReceivedMessage::getSock()
{
	return _sock;
}

User * ReceivedMessage::getUser()
{
	return _user;
}

void ReceivedMessage::setUser(User* usr)
{
	_user = usr;
}

int ReceivedMessage::getMessageCode()
{
	return _messageCode;
}

vector<string>& ReceivedMessage::getValues()
{
	if (_values.size() != 0)
	{
		return _values;
	}

	//ignoring messageg codes with don't need to handle HERE
	if (_messageCode != 205 && _messageCode != 217 && _messageCode != 215 && _messageCode != 225 && _messageCode != 299 && _messageCode != 223 && _messageCode != 222 && _messageCode != 211 && _messageCode != 201)
	{
		_values = Helper::split(Helper::getStringPartFromSocket(_sock, BUFFER_SIZE), '#');
	}

	return _values;
}
