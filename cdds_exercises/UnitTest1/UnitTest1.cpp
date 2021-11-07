#include "pch.h"
#include "CppUnitTest.h"
#include "HashingFunctions.h"

using namespace Microsoft::VisualStudio::CppUnitTestFramework;

namespace UnitTest1
{
	TEST_CLASS(HashTest)
	{
	public:
		//Look at previous code for more information

		TEST_METHOD(HashCharTest)
		{
			// hash;
			const char* Test1 = "HeLlO";
			unsigned int key = 0;
			
			//Assert::AreEqual(key, );
			//Test on char* array's has value.
		}

		TEST_METHOD(HashTableTest)
		{
			//Test on the HashTable.
		}

		TEST_METHOD(HashArrayTest)
		{
			//Test on an array of numbers
		}
	};
}
