#pragma once
class ListNode
{
public:
	ListNode(int value = 0, ListNode* _next = nullptr, ListNode* _prev = nullptr)
	{
		data = value;
		next = _next;
		previous = _prev;
	}
	int data;
	ListNode* next = nullptr;
	ListNode* previous = nullptr;
};

