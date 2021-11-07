#pragma once	
#include <functional>
#include <iostream>

//template<typename KeyType, typename T>
namespace HashingFunctions 
{

	typedef std::function< unsigned int(const char*, unsigned int)> HashFunc;
	// implementation of a basic addition hash
	unsigned int badHash( const char* data, unsigned int length );
	
	// ADD YOUR FUNCTIONS HERE
	
	//unsigned int HashKeys(unsigned int data[], unsigned int key_iter = 4)
	//{
	//	unsigned int hash = 0;
	//	
	//	for (int i = 0; i < sizeof(data); i++)
	//	{
	//		data[i] = data[i] * sizeof(data);
	//	}
	//
	//	return hash;
	//}
	
	unsigned int HashFunction(const char* data, unsigned int length);

	static unsigned int HashsFunction(const char* data, unsigned int length);

	unsigned int HashKey(const char* data, unsigned int multiplier = 4);

	unsigned int StringHash(unsigned char* data, unsigned int size)
	{
		unsigned int hash = 0;

		for (unsigned int i = 0; i < size; ++i)
		{
			hash = (hash * 1515) + data[i];
		}

		return (hash & 0x7FFFFFFF);
	}
	// a helper to access a default hash function
	static HashFunc basic = HashFunction;
};

class HashFunction
{
public:
	static unsigned int HashKey(const char* data, unsigned int multiplier = 4)
	{
		unsigned int hash = 0;
		unsigned int length = sizeof(data);
		for (int i = 0; i < length; i++)
		{
			hash += data[i] * 84 * (i + 1) * multiplier;
			hash += data[i] % 16;
		}

		return hash % 0x777;
	}

	static unsigned int StringHash(unsigned char* data, unsigned int size)
	{
		unsigned int hash = 0;

		for (unsigned int i = 0; i < size; ++i)
		{
			hash = (hash * 1515) + data[i];
		}

		return (hash & 0x7FFFFFFF);
	}
};
