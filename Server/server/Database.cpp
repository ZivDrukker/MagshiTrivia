#include "Database.h"

vector<string*> _tableSave;

DataBase::DataBase()
{
	_rc = sqlite3_open("trivia.db", &_db);

	checkErr();
}

DataBase::~DataBase()
{
	_rc = sqlite3_close(_db);

	checkErr();
}

void DataBase::checkErr()
{
	if (_rc)
	{
		cout << "Can't open databse 'trivia.db' errMsg = " << sqlite3_errmsg(_db) << endl;
		sqlite3_close(_db);
		system("pause");
	}
}

bool DataBase::isUserExists(string username)
{
	_rc = sqlite3_exec(_db, "Select username from t_users;", callback, 0, &_zErrMsg);

	if (_tableSave.size() == 0)
	{
		return false;
	}

	checkErr();
	return true;
}

bool DataBase::addNewUser(string uname, string password, string email)
{
	_rc = sqlite3_exec(_db, string("insert into t_users(username, password, email) values('" + uname + ", '" + password + "' + '" + email + "'');").c_str(), NULL, 0, &_zErrMsg);

	checkErr();
	return false;
}

bool DataBase::isUserAndPassMatch(string uname, string pass)
{
	string toCheck = "select * from t_users where username == '" + uname + "' and password == '" + pass + "';";
	_rc = sqlite3_exec(_db, toCheck.c_str() , callback, 0, &_zErrMsg);

	if (_tableSave.size() >= 1)
	{
		_tableSave.clear();
		return true;
	}

	_tableSave.clear();
	checkErr();
	return false;
}


vector<Question*> DataBase::initQuestions(int num)
{
	_rc = sqlite3_exec(_db, "Select * from t_questions;", callback, 0, &_zErrMsg);
	vector<Question*>* qs = new vector<Question*>();

	for (int i = 0; i < num; i++)
	{
		string* line = _tableSave[i];
		qs->push_back(new Question(i, line[1], line[3], line[4], line[5], line[6], atoi(line[2].c_str())));
	}

	_tableSave.clear();
	checkErr();
	return *qs;
}

bool DataBase::updateGameStatus(int gameId)
{
	_rc = sqlite3_exec(_db, string("update t_games set status='1', end_time=datetime('now', 'localtime') where game_id='" + std::to_string(gameId) + "';").c_str(), callback, 0, &_zErrMsg);

	checkErr();
	return !_rc;
}

bool DataBase::addAnswerToPlayer(int gameId, string username, int questionId, string answer, bool isCorrect, int answerTime)
{
	string toSend = "";
	if (isCorrect)
	{
		toSend += "insert into t_players_answers(game_id, username, question_id, player_answer, is_correct, answer_time) values(";
		toSend += std::to_string(gameId);
		toSend += ", '" + username;
		toSend += "', " + questionId;
		toSend += ", '" + answer;
		toSend += "', 1";
		toSend += "', " + std::to_string(answerTime);
	}
	else
	{
		toSend += "insert into t_players_answers(game_id, username, question_id, player_answer, is_correct, answer_time) values(";
		toSend += std::to_string(gameId);
		toSend += ", '" + username;
		toSend += "', " + questionId;
		toSend += ", '" + answer;
		toSend += "', 0";
		toSend += "', " + std::to_string(answerTime);
	}

	_rc = sqlite3_exec(_db, toSend.c_str(), callback, 0, &_zErrMsg);

	checkErr();

	return !_rc;

}

vector<string> DataBase::getBestScores()
{
	
	vector<string>* users = new vector<string>();

	_rc = sqlite3_exec(_db, "select username, sum(is_correct) from t_players_answers group by username order by sum(is_correct) desc;", callback, 0, &_zErrMsg);

	if (_tableSave.size() != 0)
	{
		for (unsigned int i = 0; i < (_tableSave.size() > 5 ? 5 : _tableSave.size()); i++)
		{
			users->push_back(_tableSave[i][0] + ": " + _tableSave[i][1]);
		}
	}

	checkErr();
	return *users;
}

vector<string> DataBase::getPersonalStatus(string username)
{
	vector<string>* stats = new vector<string>();

	_rc = sqlite3_exec(_db, string("select count(distinct game_id), sum(is_correct), (count(is_correct) - sum(is_correct)), round(avg(answer_time), 3) from t_players_answers where username='" + username + "';").c_str(), callback, 0, &_zErrMsg);

	stats->push_back("Number of games: " + _tableSave[0][0]);
	stats->push_back("Number of correct answers: " + _tableSave[0][1]);
	stats->push_back("Number of worng answers: " + _tableSave[0][2]);
	stats->push_back("Avarage time per answer: " + _tableSave[0][3]);

	checkErr();
	return *stats;
}

int DataBase::insertNewGame()
{
	string toSend = "insert into t_games(status) values(0);";
	_rc = sqlite3_exec(_db, toSend.c_str(), NULL, 0, &_zErrMsg);

	checkErr();

	if (_rc)
	{
		return -1;
	}

	toSend = "select * from t_games where game_id = (select max(game_id) from t_games);";

	_rc = sqlite3_exec(_db, toSend.c_str(), callback, 0, &_zErrMsg);

	checkErr();
	if (_rc)
	{
		return -1;
	}

	return stoi(_tableSave[0][0]);
}

int DataBase::callbackCount(void* notUsed, int argc, char** argv, char** azCol)
{
	return atoi(argv[0]);
}

int DataBase::callback(void *, int argc, char** data, char **)
{
	string* save = new string[argc];

	for (int i = 0; i < argc; i++)
	{
		save[i] = string(data[i]);
	}

	_tableSave.push_back(save);
	return 0;
}
