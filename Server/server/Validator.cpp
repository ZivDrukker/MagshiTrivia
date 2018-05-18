#include "Validator.h"

bool Validator::isPasswordValid(string pass)
{
	bool checkNum = false, checkUpper = false, checkLower = false;
	for (unsigned int i = 0; i < pass.length(); i++)
	{
		if (pass[i] == ' ')
		{
			return false;
		}

		if (pass[i] >= '0' && pass[i] <= '9')
		{
			checkNum = true;
		}

		if (pass[i] >= 'a' && pass[i] <= 'z')
		{
			checkLower = true;
		}

		if (pass[i] >= 'A' && pass[i] <= 'Z')
		{
			checkUpper = true;
		}
	}

	if (pass.length() >= 4 && checkNum && checkLower && checkUpper)
	{
		return true;
	}

	return false;
}

bool Validator::isUsernameValid(string uname)
{
	if (uname.size() == 0)
	{
		return false;
	}

	if ((uname[0] >= 'a' && uname[0] <= 'z') || (uname[0] >= 'A' && uname[0] <= 'Z'))
	{
		for (unsigned int i = 0; i < uname.length(); i++)
		{
			if (uname[i] == ' ')
			{
				return false;
			}
		}

		return true;
	}
	return false;
}

bool Validator::isEmailValid(string email)
{
	if (email.length() > 5 && email.find("@") != std::string::npos && email.find(".") != std::string::npos)
	{
		return true;
	}
	return false;
}