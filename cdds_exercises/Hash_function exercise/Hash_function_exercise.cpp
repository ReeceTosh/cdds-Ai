#include "Hash_function_exercise.h"
#include <functional>;
#include <iostream>;
#include <string>;

using namespace std;
//class Hash_function_exercise
//{
	//unsigned int badHash(const char* data, unsigned int length)
	//{
	//	unsigned int hash = 0;
	//	for (unsigned int i = 0; i < length; ++i)
	//		hash += data[i];

	//	return hash;
	//}

//};


//Hashes a string name/data and returns a hash key.
unsigned int StringHash(unsigned char* data, unsigned int size)
{
	unsigned int hash = 0;

	for (unsigned int i = 0; i < size; ++i)
	{
		hash = (hash * 1515) + data[i];
	}

	return (hash & 0x7FFFFFFF);
}



void _interaction()
{
	cout << "Welcome to the hashing program would you like to: " << endl;
	cout << "T)est" << endl;
	cout << "Q)uit" << endl;
	cout << "> " << endl;
	char input;
	cin >> input;

	switch (input)
	{
	case 't':

		break;
	}
}

void Test()
{

	cout << "Please type a number or word which will be hashed by bytes(4): " << endl;
	//cin >> 
}
