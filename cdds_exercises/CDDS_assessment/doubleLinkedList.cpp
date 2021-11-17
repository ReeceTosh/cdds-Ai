#include "doubleLinkedList.h"
#include "Display.h"
#include <iostream>

using namespace std;
//empty base constructor.
doubleLinkedList::doubleLinkedList()
{
}

doubleLinkedList::~doubleLinkedList()
{
	//free any allocated memory
	clear();
}

void doubleLinkedList::insert(ListNode* n, doubleLinkedList node)
{
	ListNode* newNode = new ListNode(n->data, n->next);
	if (head->next == tail->previous)
	{
		return;
	}

	if (newNode->next == NULL)
	{
		cout << "invalid, cannot be null." << endl;
	}
}



//remove function
void doubleLinkedList::printList(ListNode* n)
{
	while (n != NULL)
	{
		cout << n->data << " ";
		n = n->next;
	}
}


//checked...
void doubleLinkedList::printListReverse()
{
	ListNode* n = tail;
	ListNode* h = head;
	BubbleSort(h);

	for (int i = count()-1; i >= 0; i--)
	{
		if (n != nullptr || n->data != NULL)
		{
			cout << n->data << " ";
			n = n->previous;
		}
	}
}

void doubleLinkedList::Display(bool isBackwards)
{
	if (isBackwards)
	{
		printListReverse();
	}
	else
	{
		printAllList();
	}
}

void doubleLinkedList::printAllList()
{
	ListNode* n = head;
	BubbleSort(n);

	for (int i = 0; i < count(); i++)
	{
		if (n != nullptr || n->data != NULL)
		{
			cout << n->data << " ";
			n = n->next;		
		}
	}
}

int& doubleLinkedList::first()
{
	doubleLinkedList f;
	if (f.empty())
		throw std::exception("List is empty");

	return head->data;
}

int& doubleLinkedList::last()
{
	doubleLinkedList f;
	if (f.empty())
		throw std::exception("List is empty");

	return tail->data;
}

int doubleLinkedList::double_linked_function()
{
	ListNode* head = NULL;
	head = new ListNode();

	ListNode* one = NULL;
	one = new ListNode();

	ListNode* two = NULL;
	two = new ListNode();


	ListNode* three = NULL;
	three = new ListNode();


	ListNode* tail = NULL;
	tail = new ListNode();

//---------------------------------
	one->data = 4;
	one->next = two;

	two->data = 0;
	two->next = three;
	two->previous = one;

	three->data = 8;
	three->next = tail;
	three->previous = two;

//--------------------------------

	pushFront(83);
	pushBack(3);
	pushBack(8384);
	pushFront(76);
	pushFront(3);

	printAllList();

	cout << endl;
	cout << endl;

	printListReverse();

	cout << endl;
	cout << endl;

	popFront();

	printListReverse();

	cout << endl;
	cout << endl;

	remove(3);

	printAllList();

	return 0;
}

//todo:
void doubleLinkedList::erase(Iterator it)
{
	if ((*it).data == (*it).data)
	{
       removeOne(it.GetCurrent());
	}
}

//todo:
void doubleLinkedList::remove(unsigned int value)
{
	ListNode* node = head;
	for (int i = 0; i < count(); i++)
	{

		if (node->data == value)
		{
			if (node->next != NULL)
				node->next->previous = node->previous;
			else
				popBack();

			if (node->previous != NULL)
				node->previous->next = node->next;
			
			node->data = NULL;
			i++;


		}

	    node = node->next;

	}
	
}


//remove:
void doubleLinkedList::removeOne(ListNode* node)
{
	if (node->data == NULL)
	{
		return;
	}
	node->data = NULL;
}


//checked...
void doubleLinkedList::popBack()
{
	if (this->tail == NULL)
	{
		return;
	}
	ListNode* temp = this->tail;
	this->tail = this->tail->previous;

	delete temp;

	if (this->tail)
	{
		this->tail->next = nullptr;
	}
	else
	{
		this->head = nullptr;
		this->tail = nullptr;
	}
}

//checked...
void doubleLinkedList::popFront()
{
	if (this->head == NULL)
	{
		return;
	}
	ListNode* temp = this->head;
	this->head = this->head->next;

	delete temp;

	if (this->head)
	{
		this->head->previous = nullptr;
	}
	else
	{
		this->head = nullptr;
		this->tail = nullptr;
	}
}

const bool doubleLinkedList::empty()
{
	return (head == nullptr) && (tail == nullptr);
}

//checked...
void doubleLinkedList::clear()
{
	ListNode* node = head;
	ListNode* nextNode;

	while (node != nullptr)
	{
		nextNode = node->next;
		delete node;

		node = nextNode;
	}
}

//checked...
void doubleLinkedList::pushFront(int value)
{
	ListNode* newNode = new ListNode(value, head);
	if (head != nullptr)
	{
		head->previous = newNode;
	}

	head = newNode;

	if (tail == nullptr)
	{
		tail = newNode;
	}
}

//checked...
void doubleLinkedList::pushBack(int value)
{
	ListNode* newNode = new ListNode(value, nullptr, tail);
	if (tail != nullptr)
	{
		tail->next = newNode;
	}

	tail = newNode;

	if (head == nullptr)
	{
		head = newNode;
	}
}

void doubleLinkedList::BubbleSort(ListNode* startNode)
{
	bool swapped;
	ListNode* ptrStart;
	ListNode* ptrEnd = nullptr;

	//If empty, don't do the sort
	if (startNode == nullptr)
	{
		return;
	}

	//The sort will run at least once.
	do
	{
		//It will receive the parameter and set it to a local variable
		//for the remainder of the loop.
		swapped = false;
		ptrStart = startNode;

		//While the next node isn't empty/NULL
		while (ptrStart->next != ptrEnd)
		{
			//Does a check/comparison of values and sorts them in ascending order.
			if (ptrStart->data > ptrStart->next->data)
			{
				swap(ptrStart->data, ptrStart->next->data);
				swapped = true;
			}
			ptrStart = ptrStart->next;
		}
		ptrEnd = ptrStart;
	} while (swapped);
}

//checked...
unsigned int doubleLinkedList::count()
{
	unsigned int counter = 0;

	ListNode* node = head;
	while (node != nullptr && counter < 100)
	{
		counter++;
		node = node->next;
	}
	return counter;
}

Iterator doubleLinkedList::begin()
{
	return Iterator(head);
}

Iterator doubleLinkedList::end()
{
	return Iterator(tail);
}

