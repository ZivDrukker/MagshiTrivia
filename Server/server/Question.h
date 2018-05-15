#pragma once
#include <string>
#include <iostream>

using std::string;

class Question
{
public:
	Question(int, string, string, string, string, string, int);
	~Question();
	string getQuestion();
	string* getAnswers();
	int getCorrectAnswerIndex();
	int getId();

private:
	string _question;
	string* _answers;
	int _correctAnswerIndex;
	int _id;
};