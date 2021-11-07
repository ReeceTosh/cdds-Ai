#include "CppUnitTest.h"
#include "HashingFunctions.h"
#include "HashTable.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace UnitTestHash
{
	TEST_CLASS(HashTest)
	{
	public:
		//Look at previous code for more information

		TEST_METHOD(HashCharTest)
		{
			const char* Test1 = "HeLlO";
			unsigned int result = 850;

			Assert::AreEqual(result, HashFunction::HashKey(Test1, 1));
			Assert::AreEqual(result, HashFunction::HashKey(Test1, 1));
			//Test on char* array's has value.
		}

		//Test on the HashTable.
		TEST_METHOD(HashTableTest)
		{
			//constructor to set up table.
			HashTable<const char*, int> table(100, [](const std::string& key) -> unsigned int
				{
					return HashingFunctions::basic(key.c_str(), key.size());
				}
			);

			const char* Test1 = "General";
			const char* Test2 = "Kenobi";

			table[Test1] = 1;
			table[Test2] = 2;

			//Test 1
			Assert::AreEqual(table[Test1], 1);
			Assert::AreEqual(table[Test1], 1);

			//Test 2
			Assert::AreEqual(table[Test2], 2);
			Assert::AreEqual(table[Test2], 2);
		}
	};
}
