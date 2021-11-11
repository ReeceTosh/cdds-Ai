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
			//Something weird is going on through the code, i have check the proper results of the hashing and the expected is true.
			// Tested and succeded: Test1 with resultA, resultB included

			const char* Test1 = "HeLlO";
			unsigned int resultA = 850;
			unsigned int resultB = 1501;

			Assert::AreEqual(resultA, HashFunction::HashKey(Test1, 1));
			Assert::AreEqual(resultB, HashFunction::HashKey(Test1, 9));

			const char* Test2 = "Bob Lighter";
			unsigned int resultC = 1594219837;

			const char* Test3 = "Jeffery";
			unsigned int resultD = 1839057095;

			Assert::AreEqual(resultC, HashFunction::StringHash(Test2, sizeof(Test2)));
			Assert::AreEqual(resultD, HashFunction::StringHash(Test3, sizeof(Test3)));
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

			unsigned int TestFunctionA = HashingFunctions::HashKey(Test1, sizeof(Test1));
			unsigned int TestFunctionB = HashingFunctions::HashKey(Test2, sizeof(Test2));

			int resultA = 151;
			int resultB = 875;

			table[Test1] = TestFunctionA;
			table[Test2] = TestFunctionB;

			//Test 1
			Assert::AreEqual(resultA, table[Test1]);

			//Test 2
			Assert::AreEqual(resultB, table[Test2]);

		}
	};
}
