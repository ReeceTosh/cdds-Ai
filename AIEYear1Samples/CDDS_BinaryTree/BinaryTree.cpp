/*----------------------------------------
Author: Richard Stern
Description: A simple binary search tree
Date: 17/4/2015
----------------------------------------*/
#include "BinaryTree.h"
#include "TreeNode.h"
#include "raylib.h"
#include <iostream>
#include <cstdlib>
using namespace std;


BinaryTree::BinaryTree()
{
	m_pRoot = nullptr;
}

BinaryTree::~BinaryTree()
{
	while (m_pRoot)
	{
		Remove(m_pRoot->GetData());
	}
}

// Return whether the tree is empty
bool BinaryTree::IsEmpty() const
{
	return (m_pRoot == nullptr);
}

// Insert a new element into the tree.
// Smaller elements are placed to the left, larger ones are placed to the right.
void BinaryTree::Insert(int a_nValue)
{
	//If the root is empty then set the root equal to the value aka. create a new root.
	if (IsEmpty())
	{
		m_pRoot = new TreeNode(a_nValue);
	}
	else
	{
		//Else if the parent(currently root) is not empty, then insert the new value to either the left or right 
		//if it is greater/less than the parent value.

		TreeNode* parent = nullptr;
		TreeNode* pCurrent = m_pRoot;

		while (pCurrent != nullptr)
		{
			//parent will change to the leaf node and the leaf node will change to one of the nodes attached to it.
			parent = pCurrent;

			//dulpicate values, if not then continue on. 
			if (a_nValue == pCurrent->GetData())
			{
				return;
			}

			else if (a_nValue > pCurrent->GetData())
			{
				pCurrent = pCurrent->GetRight();
			}

			else
			{
				pCurrent = pCurrent->GetLeft();
			}
		}

		//Case 1: if the value is greater than the current pointer 
		if (a_nValue > parent->GetData())
		{
			parent->SetRight(new TreeNode(a_nValue));
		}
		//Case 2: if the value is less than the current pointer.
		else
		{
			parent->SetLeft(new TreeNode(a_nValue));
		}
	}
}

//Find the value in the binary tree.
TreeNode* BinaryTree::Find(int a_nValue)
{
	TreeNode* pCurrent = nullptr;
	TreeNode* pParent = nullptr;

	return FindNode(a_nValue, pCurrent, pParent) ? pCurrent : nullptr;
}

//Find the node with the same value in the binary tree.
bool BinaryTree::FindNode(int a_nSearchValue, TreeNode*& ppOutNode, TreeNode*& ppOutParent)
{
	//If there is no root.
	if (m_pRoot == nullptr)
	{
		return false;
	}
	else
	{
		TreeNode* currentNode = m_pRoot;
		ppOutParent = nullptr;
		while (true)
		{
			if (currentNode->GetData() == a_nSearchValue)
			{
				ppOutNode = currentNode;
				return true;
			}
			
			//If the value is less than the root, then check the left side.
			if (a_nSearchValue < currentNode->GetData()) 
			{
				if (currentNode->HasLeft())
				{
					ppOutParent = currentNode;
					currentNode = currentNode->GetLeft();
				}
				else
				{
					return false;
				}
			}
			//If the value is greater than the root, then check the right side.
			else
			{
				if (currentNode->HasRight())
				{
					ppOutParent = currentNode;
					currentNode = currentNode->GetRight();
				}
				else
				{
					return false;
				}
			}
		}
	}

	return false;
}

//Remove the node with the same value and rearrange/update the binary tree.
void BinaryTree::RemoveNode(int a_nValue, TreeNode* parent, TreeNode* node)
{
	if (!node->HasLeft() && !node->HasRight()) // Has no children (easy)
	{
		if (node == m_pRoot)
		{
			m_pRoot = nullptr;
		}
		else if (parent->GetLeft() == node)
		{
			parent->SetLeft(nullptr);
		}
		else if (parent->GetRight() == node)
		{
			parent->SetRight(nullptr);
		}

		delete node;
	}
	else if (node->HasLeft() && !node->HasRight()) // Has one child (left)
	{
		if (node == m_pRoot)
		{
			m_pRoot = node->GetLeft();
		}
		else if (parent->GetLeft() == node)
		{
			parent->SetLeft(node->GetLeft());
		}
		else if (parent->GetRight() == node)
		{
			parent->SetRight(node->GetLeft());
		}

		delete node;
	}
	else if (node->HasRight() && !node->HasLeft()) // Has one child (right)
	{
		if (node == m_pRoot)
		{
			m_pRoot = node->GetRight();
		}
		else if (parent->GetLeft() == node)
		{
			parent->SetLeft(node->GetRight());
		}
		else if (parent->GetRight() == node)
		{
			parent->SetRight(node->GetRight());
		}

		delete node;
	}
	else
	{
		// Find the smallest node greater than the current node
		TreeNode* target = node->GetRight();
		while (target->HasLeft())
		{
			target = target->GetLeft();
		}

		node->SetData(target->GetData());

		RemoveNode(0, node, target);
	}
}

//Remove the node with a specific value.
void BinaryTree::Remove(int a_nValue)
{
	TreeNode* pParent = nullptr;
	TreeNode* pCurrent = nullptr;

	// if we fail to find the value in tree, then stop.
	if (!FindNode(a_nValue, pCurrent, pParent))
	{
		return;
	}
	else
	{
		RemoveNode(a_nValue, pParent, pCurrent);
	}

}

void BinaryTree::PrintOrdered()
{
	PrintOrderedRecurse(m_pRoot);
	cout << endl;
}

//Empty function
void BinaryTree::PrintOrderedRecurse(TreeNode* pNode)
{

}

void BinaryTree::PrintUnordered()
{
	PrintUnorderedRecurse(m_pRoot);
	cout << endl;
}

//Empty function
void BinaryTree::PrintUnorderedRecurse(TreeNode* pNode)
{

}


//READ AND DEBUG FUNCTION IF THERE IS ANY ISSUE.
void BinaryTree::RemoveRoot()
{
	//If the root node is empty, remove any/all nodes and the root node.
	if (m_pRoot != NULL)
	{
		//m_pRoot is the parent
		//delPtr is the last reference to the previous parent node

		TreeNode* delPtr = m_pRoot;
		int rootValue = m_pRoot->GetData();
		int smallestInRightTree;

		//if no children exists.
		if (m_pRoot->GetLeft() == NULL && m_pRoot->GetRight() == NULL)
		{
			m_pRoot = NULL;
			delete delPtr;
		}

		//Root has 1 child root
		else if (m_pRoot->GetLeft() == NULL && m_pRoot->GetRight() != NULL)
		{
			m_pRoot = m_pRoot->GetRight();
			delete delPtr;

			cout << "The root with value: " << rootValue << "\nThe new root contains the value of: " << m_pRoot->GetData() << endl;
		}
		else if (m_pRoot->GetLeft() != NULL && m_pRoot->GetRight() == NULL)
		{
			m_pRoot = m_pRoot->GetLeft();
			delete delPtr;

			cout << "The root with value: " << rootValue << "\nThe new root contains the value of: " << m_pRoot->GetData() << endl;
		}

		//If the root has two children.
		else
		{
			smallestInRightTree = FindSmallest(m_pRoot->GetRight());
			RemoveNode(smallestInRightTree, m_pRoot, NULL);
			m_pRoot->GetData() == smallestInRightTree;
			cout << "new root value: " << m_pRoot->GetData();
		}
	}
	else
	{
		cout << "Can't remove root, already empty";
	}
}

//READ AND DEBUG FUNCTION IF THERE IS ANY ISSUE.
void BinaryTree::RemoveMatch(TreeNode* parent, TreeNode* removeNode, bool left)
{
	//Check the parent variable to see if it can be used, it is complaining that the variable
	//needs to be modifiable. to start check on how the == symbol works properly.
	//Example: parent->GetRight() == NULL;

	if (m_pRoot != NULL)
	{
		TreeNode* delPtr;
		int nodeMatchValue = removeNode->GetData();
		int smallestInRightSubtree;

		//There are no children in the node
		if (parent->GetLeft() == NULL && parent->GetRight() == NULL)
		{
			delPtr = removeNode;
			if (left == true)
			{
				parent->GetLeft() == NULL;
			}
			else
			{
				parent->GetRight() == NULL;
			}
			delete delPtr;

			cout << "The node containing value " << nodeMatchValue << " was removed.";
		}

		//Node to remove has 1 child 
		else if (parent->GetLeft() == NULL && parent->GetRight() != NULL)
		{
			if (left == true)
			{
				parent->GetLeft() == removeNode->GetRight();
			}
			else
			{
				parent->GetRight() == removeNode->GetRight();
			}
			removeNode->GetRight() == NULL;
			delPtr = removeNode;
			delete delPtr;

			cout << "The node containing value " << nodeMatchValue << " was removed.";
		}
		else if (parent->GetLeft() == NULL && parent->GetRight() != NULL)
		{
			if (left == true)
			{
				parent->GetLeft() == removeNode->GetLeft();
			}
			else
			{
				parent->GetRight() == removeNode->GetLeft();
			}
			removeNode->GetLeft() == NULL;
			delPtr = removeNode;
			delete delPtr;

			cout << "The node containing value " << nodeMatchValue << " was removed.";
		}

		//Node to remove has two children
		else
		{
			smallestInRightSubtree = FindSmallest(removeNode->GetRight());
			RemoveNode(smallestInRightSubtree, removeNode, NULL);
			removeNode->GetData() == smallestInRightSubtree;
		}


	}
	else
	{
		cout << "Can't remove node. The root tree is empty, my friend.";
	}
}

int BinaryTree::FindSmallest(TreeNode* ptr)
{
	if (m_pRoot == NULL)
	{
		cout << "Tree is empty";
		return 0;
	}
	else
	{
		if (ptr->GetLeft() != NULL)
		{
			return FindSmallest(ptr->GetLeft());
		}
		else
		{
			return ptr->GetData();
		}
	}
}

void BinaryTree::Draw(TreeNode* selected)
{
	Draw(m_pRoot, 400, 40, 400, selected);
}

void BinaryTree::Draw(TreeNode* pNode, int x, int y, int horizontalSpacing, TreeNode* selected)
{

	horizontalSpacing /= 2;

	if (pNode)
	{
		if (pNode->HasLeft())
		{
			DrawLine(x, y, x - horizontalSpacing, y + 80, RED);

			Draw(pNode->GetLeft(), x - horizontalSpacing, y + 80, horizontalSpacing, selected);
		}

		if (pNode->HasRight())
		{
			DrawLine(x, y, x + horizontalSpacing, y + 80, RED);

			Draw(pNode->GetRight(), x + horizontalSpacing, y + 80, horizontalSpacing, selected);
		}

		pNode->Draw(x, y, (selected == pNode));
	}
}