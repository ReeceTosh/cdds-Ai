#include "HashingFunctions.h"
#include "HashTable.h"
#include <unordered_map>
#include <functional>;
#include <iostream>;
#include <string>;

using namespace std;

//General idea of what a hashtable looks like: GameObject** hashTable= new GameObject* [ tableSize];

//LOOK INTO M_DATA IN THE HASHTABLE.H FILE!!!!

int main()
{
	//m_size / function being used
	HashTable<const char*, int> table(9999999, [](const std::string& key) -> unsigned int
		{
			return HashingFunctions::basic(key.c_str(), key.size());
		}
	);

	const char* Test1 = "Testing one";
	auto TestFunction1 = HashingFunctions::StringHash(Test1, sizeof(Test1));
	//unsigned int key;

	unsigned int Test2[] = { 25, 3, 2, 6, 8 };
	//unsigned int TestFunction2 = HashingFunctions::HashFunction(Test2);

	

	table[Test1] = TestFunction1;

	cout << "Displaying Test: " << table[Test1] << endl;

	return 0;
}

unsigned int HashingFunctions::badHash(const char* data, unsigned int length)
{
	unsigned int hash = 0;

	for (unsigned int i = 0; i < length; ++i)
		hash += data[i];

	return hash;
}

unsigned int HashingFunctions::HashFunction(const char* data, unsigned int length)
{
	unsigned int hash = 0;

	for (unsigned int i = 0; i < length; ++i)
	{
		hash = (hash * 15) + data[i];
	}

	return (hash & 0x7FF);
}

unsigned int HashingFunctions::HashsFunction(const char* data, unsigned int length)
{
	unsigned int hash = 0;

	for (unsigned int i = 0; i < length; ++i)
	{
		hash = (hash * 15) + data[i];
	}

	return (hash & 0x7FF);
}

unsigned int HashingFunctions::HashKey(const char* data, unsigned int multiplier)
{
	unsigned int hash = 0;
	unsigned int length = sizeof(data);
	for (unsigned int i = 0; i < length; i++)
	{
		hash += data[i] * 84 * (i + 1) * multiplier;
		hash += data[i] % 16;
	}

	return hash % 0x777;
}

unsigned int HashingFunctions::StringHash(const char* data, unsigned int size)
{
	unsigned int hash = 0;

	for (unsigned int i = 0; i < size; ++i)
	{
		hash = (hash * 1515) + data[i];
	}

	return (hash & 0x7FFFFFFF);
}
