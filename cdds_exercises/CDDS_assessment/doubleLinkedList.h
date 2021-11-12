#pragma once
#include "ListNode.h"
#include "Iterator.h"
class doubleLinkedList
{
public:
	doubleLinkedList();
	~doubleLinkedList();
	//display function.
	int double_linked_function();

	void printAllList();
	void printList(ListNode* n);
	void printListReverse();
	void Display(bool isBackwards);

	//return iterator at first and end of element.
	Iterator begin();
	Iterator end();

	int& first();
	int& last();

	unsigned int count();
	void insert(ListNode* n, doubleLinkedList node);

	//remove an element by its iterator
	void erase(Iterator it);

	void remove(unsigned int value);

	//testing function.
	void removeOne(ListNode* node);

	void popBack();
	void popFront();

	const bool empty();
	void clear();

	void pushFront(int value);
	void pushBack(int value);

private:

	ListNode* head;
	ListNode* tail;
};

