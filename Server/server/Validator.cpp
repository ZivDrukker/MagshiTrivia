#include "Validator.h"

static bool isPasswordValid(string pass)
{
	if (pass.length() >= 4)
	{
		return true;
	}
	return false;
}

static bool isUsernameValid(string uname)
{
	if (uname.length() >= 4)
	{
		return true;
	}
	return false;
}

static bool isEmailValid(string email)
{
	if (email.length() > 5 && email.find("@") != std::string::npos && email.find(".") != std::string::npos)
	{
		return true;
	}
	return false;
}