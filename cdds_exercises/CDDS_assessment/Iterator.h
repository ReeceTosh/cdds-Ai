#pragma once
#include "ListNode.h"
#include <iostream>

using namespace std;

class Iterator
{
private:



	ListNode* currentNode;

public:
	void moveNext()
	{
		if (currentNode == nullptr)
		{
			return;
		}

		currentNode = currentNode->next;
	};

	void movePrevious()
	{
		if (currentNode == nullptr)
		{
			return;
		}

		currentNode = currentNode->previous;

	};

	bool isValid()
	{
		return currentNode != nullptr;
	};

	ListNode* GetCurrent()
	{
		//checking that currentNode != nullptr.
		//if it is then throw exception.
		if (currentNode == nullptr)
		{
			throw exception ("Invalid iterator");
			cout << "invalid iterator" << endl;
			return 0;
		}

		return currentNode;
	};

	void operator++();
	void operator--();
	ListNode operator*();

	Iterator(ListNode* node)
	{
		currentNode = node;
	}
};

