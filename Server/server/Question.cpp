#include "Question.h"

Question::Question(int id, string question, string a1, string a2, string a3, string a4, int correctIndex)
{
	_id = id;
	_question = question;
	_answers = new string[4];
	_answers[0] = a1;
	_answers[1] = a2;
	_answers[2] = a3;
	_answers[3] = a4;
	_correctAnswerIndex = correctIndex;
}

Question::~Question()
{
	delete[] _answers;
}

string Question::getQuestion()
{
	return _question;
}

string * Question::getAnswers()
{
	return _answers;
}

int Question::getCorrectAnswerIndex()
{
	return _correctAnswerIndex;
}

int Question::getId()
{
	return _id;
}
