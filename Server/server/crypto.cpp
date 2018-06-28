#include "crypto.h"
#include "base64.h"
#include <cstring>
#include <iostream>


 //key is 1337 % UcfSecretEnc % 42 = 1337 % 1178 % 42= 159 % 42 = 33
int key = 33;

const char* encrypto(std::string msg)
{
	std::string t = "";
	for (unsigned int i = 0; i < msg.length(); i++)
	{
		t += char(int(msg[i]) ^ key);
	}

	char *a = new char[t.size() + 1];

	a[t.size()] = 0;
	memcpy(a, t.c_str(), t.size());

	return a;
}

char* decrypto(std::string msg)
{
	std::string t = "";
	for (unsigned int i = 0; i < msg.length(); i++)
	{
		t += char(int(msg[i]) ^ key);
	}

	char *a = new char[t.size() + 1];
	a[t.size()] = 0;
	memcpy(a, t.c_str(), t.size());

	return a;
}
