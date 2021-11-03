#include "Hash_function_exercise.h"
#include <functional>;
#include <iostream>;
#include <string>;

using namespace std;
namespace HashFunction
{
	unsigned int badHash(const char* data, unsigned int length)
	{
		unsigned int hash = 0;
		for (unsigned int i = 0; i < length; ++i)
			hash += data[i];

		return hash;
	}
}



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



int main()
{
	
	ref = Hash_function_exercise::key("John Lard");
	cout << "check one two...." << endl;

	cout << "Old hash function: badHash(string, unsigned int) -> " << HashFunction::badHash("dfwsg", 5) << endl;
	cout << endl;
	cout << "Hash function: key(const char* data) hskdjhfkhsalkdf -> " << Hash_function_exercise::key("hskdjhfkhsalkdf") << endl;
	cout << endl;
	cout << "Hash function: key(const char* data) hskdjhfkhsalkdf, 5 -> " << Hash_function_exercise::key("hskdjhfkhsalkdf", 16) << endl;
	cout << endl;
	cout << "Hash function: key(const char* data) John Lard -> " << Hash_function_exercise::key("John Lard") << endl;
	cout << endl;
	cout << "Hash Table: hash key of 'John Lard' in the hash table: " << ref << endl << "hashTableNumber[ref] -> " << Hash_function_exercise::hashTableNumber[ref] << endl;
	cout << endl;
	return 0;
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
