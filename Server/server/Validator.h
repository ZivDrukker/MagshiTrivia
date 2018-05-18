#pragma once
#include <string>
#include <iostream>

using std::string;
class Validator
{
public:
	static bool isPasswordValid(string);
	static bool isUsernameValid(string);
	static bool isEmailValid(string);
};