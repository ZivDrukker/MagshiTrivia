#include "crypto.h"


map<SOCKET, int> keys;

//checks if num is prime
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

//encrypts a given message
const char* encrypto(std::string msg, SOCKET sock)
{
	if (keys.find(sock) != keys.end())
	{
		int key = keys[sock];//find the key per client
		std::string t = "";
		for (unsigned int i = 0; i < msg.length(); i++)
		{
			t += char(int(msg[i]) ^ key);//building the string by XORing with key
		}

		char *a = new char[t.size() + 1];

		a[t.size()] = 0;//making it a string
		memcpy(a, t.c_str(), t.size());//actually puting the encrypted text into a

		return a;
	}

	char *a = new char[msg.size() + 1];

	for (unsigned int i = 0; i < msg.length(); i++)
	{
		a[i] = msg[i];
	}

	return a;
}

//decrypts a given message
char* decrypto(std::string msg, SOCKET sock)
{
	if (keys.find(sock) != keys.end())
	{
		int key = keys[sock];
		std::string t = "";
		for (unsigned int i = 0; i < msg.length(); i++)
		{
			t += char(int(msg[i]) ^ key);//XORing the message to get the plain message again
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

//transfering the initial key between the server and client
void sendAndRecieveKey(SOCKET sock)
{
	srand(time(NULL));
	int g = 0, p = 0, s1 = 0;

	while (s1 == 0)
	{
		//finding two public keys
		cout << "Calculating two public keys - prime numbers" << endl;
		while (!isPrime(g))
		{
			g = rand();
		}

		while (!isPrime(p))
		{
			p = rand();
		}

		string toSend = std::to_string(g) + "#" + std::to_string(p);//giving client public keys
		::send(sock, toSend.c_str(), toSend.length(), 0);

		int r1 = rand();

		int num1 = int(pow(g, r1)) % p;//send num for client to retrieve key from

		::send(sock, std::to_string(num1).c_str(), std::to_string(num1).length(), 0);//sending the generated number

		string num2Str = Helper::getStringPartFromSocket(sock, 4096);//getting the client's generated num
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

		s1 = int(pow(num2, r1)) % p;//finding the key buy the private key and the client's generated number
		s1 = s1 % 100;//restricting key to 100 for faster debugging runtime
	}

	cout << "key: " << abs(s1) << endl;

	keys.insert(std::make_pair(sock, abs(s1)));//save key with client's socket
}
