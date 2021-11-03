#pragma once

#include <functional>
#include <iostream>

using namespace std;


// look at the hash table slides for redo of this but keep hash functions
namespace Hash_function_exercise
{
	//Hash Table
	unsigned static int tableSize = 100;
	unsigned int** hashTableNumber = new unsigned int * [tableSize];
	unsigned int ref;

	typedef std::function< unsigned int(const char*, unsigned int)> HashFunc;
	// implementation of a basic addition hash
	unsigned int badHash(const char* data, unsigned int length);

	// ADD YOUR FUNCTIONS HERE
	unsigned int key(unsigned int data, unsigned int key_iter = 4)
	{
		return data;
	}
	unsigned int key(const char* data, unsigned int multiplier = 4)
	{
		unsigned int hash = 0;
		unsigned int length = sizeof(data);
		for (int i = 0; i < length; i++)
		{
			hash += data[i] * 84 * (i+1) * multiplier;
			hash += data[i] % 16;
		}

		return hash % 100000;
	}

	void HashTable(unsigned int numberKey)
	{
		unsigned int hashKey = numberKey;
		ref = numberKey;
		hashKey = hashKey % tableSize;

		hashTableNumber[hashKey] = new unsigned int(numberKey);
	}

	// a helper to access a default hash function
	static HashFunc def;
};

