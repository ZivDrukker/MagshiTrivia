#pragma once

#include <cstring>
#include "TriviaServer.h"
#include <stdlib.h>
#include <time.h> 
#include <math.h>

const char* encrypto(std::string msg, SOCKET sock);
char* decrypto(std::string msg, SOCKET sock);

bool isPrime(int);
void sendAndRecieveKey(SOCKET sock);