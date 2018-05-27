#include "Database.h"

vector<string*> _tableSave;

DataBase::DataBase()
{
	_rc = sqlite3_open("database.db", &_db);

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
		cout << "Can't open databse 'database.db' errMsg = " << sqlite3_errmsg(_db) << endl;
		sqlite3_close(_db);
		system("pause");
	}
}

bool DataBase::isUserExists(string username)
{
	_rc = sqlite3_exec(_db, "Select uname from Users;", callback, 0, &_zErrMsg);

	if (_tableSave.size() == 0)
	{
		return false;
	}

	checkErr();
	return true;
}

bool DataBase::addNewUser(string uname, string password, string email)
{
	_rc = sqlite3_exec(_db, string("insert into users(uname, password, email) values('" + uname + ", '" + password + "' + '" + email + "'');").c_str(), NULL, 0, &_zErrMsg);

	checkErr();
	return false;
}

bool DataBase::isUserAndPassMatch(string uname, string pass)
{
	string toCheck = "select * from User where uname == '" + uname + "' and pass == '" + pass + "';";
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
	_rc = sqlite3_exec(_db, "Select * from questions;", callback, 0, &_zErrMsg);
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
