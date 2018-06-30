#include "crypto.h"



map<SOCKET, int> keys;

bool isPrime(int x)
{
	int i = 3;
	if (x == 2 || x == 3)
	{
		return true;
	}

	if (x % 2 == 0 || x < 2)
	{
		return false;
	}

	while (i != sqrt(x))
	{
		if (x % i == 0)
		{
			return false;
		}
		i += 2;
	}
	return true;

}

const char* encrypto(std::string msg, SOCKET sock)
{
	if (keys.find(sock) != keys.end())
	{
		int key = keys[sock];
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

	char *a = new char[msg.size() + 1];

	for (unsigned int i = 0; i < msg.length(); i++)
	{
		a[i] = msg[i];
	}

	return a;
}

char* decrypto(std::string msg, SOCKET sock)
{
	if (keys.find(sock) != keys.end())
	{
		int key = keys[sock];
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

	char *a = new char[msg.size() + 1];

	for (unsigned int i = 0; i < msg.length(); i++)
	{
		a[i] = msg[i];
	}

	return a;
}



void sendAndRecieveKey(SOCKET sock)
{
	srand(time(NULL));
	int g = 0, p = 0, s1 = 0;

	while (s1 == 0)
	{
		while (!isPrime(g))
		{
			g = rand();
			cout << g << endl;
		}

		while (!isPrime(p))
		{
			p = rand();
			cout << p << endl;
		}

		string toSend = std::to_string(g) + "#" + std::to_string(p);
		::send(sock, toSend.c_str(), toSend.length(), 0);

		int r1 = rand();

		int num1 = int(pow(g, r1)) % p;

		::send(sock, std::to_string(num1).c_str(), std::to_string(num1).length(), 0);

		string num2Str = Helper::getStringPartFromSocket(sock, 4096);
		bool endFound = false;
		for (unsigned int i = 0; i < num2Str.length() && !endFound; i++)
		{
			if (num2Str[i] == -51)
			{
				endFound = true;
				num2Str = num2Str.substr(0, i);
			}
		}

		int num2 = stoi(num2Str);

		s1 = int(pow(num2, r1)) % p;
		s1 = s1 % 100;
	}

	cout << "key: " << abs(s1) << endl;

	keys.insert(std::make_pair(sock, abs(s1)));
}
