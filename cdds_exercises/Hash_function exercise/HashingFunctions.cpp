#include "HashingFunctions.h"
#include "HashTable.h"
#include <unordered_map>
#include <functional>;
#include <iostream>;
#include <string>;

using namespace std;

int main()
{
	//m_size / function being used
	HashTable<const char*, int> table(9999999, [](const std::string& key) -> unsigned int
		{
			return HashingFunctions::basic(key.c_str(), key.size());
		}
	);

	const char* Test1 = "HeLlO";
	auto TestFunction1 = HashingFunctions::HashKey(Test1, 1);
	//unsigned int key;

	unsigned int Test2[] = { 25, 3, 2, 6, 8 };
	//unsigned int TestFunction2 = HashingFunctions::HashFunction(Test2);


	const char* test1 = "General";
	unsigned int TestFunction2 = HashingFunctions::HashKey(test1, sizeof(test1));

	const char* test2 = "Kenobi";
	unsigned int TestFunction3 = HashingFunctions::HashKey(test2, sizeof(test2));

	const char* test3 = "Jeffery";
	
	table[Test1] = TestFunction1;
	//111688

	table[test1] = TestFunction2;
	//1586

	table[test2] = TestFunction3;
	//480298

	cout << "Displaying Test, HashKey(General): " << TestFunction2 << "\n" << endl;
	cout << "Displaying Test, HashKey(Kenobi): " << TestFunction3 << "\n" << endl;

	cout << "Displaying Test, table[Kenobi]: " << table[test2] << "\n" << endl;
	cout << "Displaying Test, table[General]: " << table[test1] << "\n" << endl;
	
	system("pause");
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

	return hash % 0x77777;
}
