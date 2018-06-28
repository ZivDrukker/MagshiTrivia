#pragma once

#include <string>

std::string base64_encode(char* bytes_to_encode, unsigned int in_len);
std::string base64_decode(std::string encoded_string);