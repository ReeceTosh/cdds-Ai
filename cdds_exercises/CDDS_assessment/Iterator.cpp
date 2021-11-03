#include "Iterator.h"


void Iterator::Iterator::operator++()
{
	currentNode = currentNode->next;
}
void Iterator::Iterator::operator--()
{
	currentNode = currentNode->next;
}
ListNode Iterator::Iterator::operator*()
{
	return *currentNode;
}