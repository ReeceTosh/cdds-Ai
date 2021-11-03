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
		TreeNode* parent = nullptr;
		TreeNode* pCurrent = m_pRoot;

		while (pCurrent != nullptr)
		{
			//parents will change to the leaf node and the leaf node will change to one of the nodes attached to it.
			parent = pCurrent;

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

		if (a_nValue > parent->GetData())
		{
			parent->SetRight(new TreeNode(a_nValue));
		}
		else
		{
			parent->SetLeft(new TreeNode(a_nValue));
		}
	}
}

TreeNode* BinaryTree::Find(int a_nValue)
{
	TreeNode* pCurrent = nullptr;
	TreeNode* pParent = nullptr;

	return FindNode(a_nValue, pCurrent, pParent) ? pCurrent : nullptr;
}

//Check code with pseudo code
bool BinaryTree::FindNode(int a_nSearchValue, TreeNode*& ppOutNode, TreeNode*& ppOutParent)
{
	ppOutNode = ppOutParent;
	while (ppOutNode != nullptr)
	{
		if (a_nSearchValue == ppOutNode->GetData())
		{
			return ppOutNode;
		}
		else
		{
			if (a_nSearchValue < ppOutNode->GetData())
			{
				ppOutNode = ppOutNode->GetLeft();
			}
			else
			{
				ppOutNode = ppOutNode->GetRight();
			}
		}
	}

	return false;
}

void BinaryTree::RemoveNode(int a_nValue, TreeNode* parent)
{
	
	TreeNode* pParent = parent;
	TreeNode* pCurrent = nullptr;

	// if we fail to find the value in tree, then stop.
	if (!FindNode(a_nValue, pCurrent, pParent))
	{
		return;
	}
	else
	{
		//If the root contains the value
		if (m_pRoot->GetData() == a_nValue)
		{
			RemoveRoot();
		}

		if (a_nValue < pCurrent->GetRight()->GetData() && pCurrent->GetLeft() != nullptr)
		{
			if (pCurrent->GetLeft()->GetData() == a_nValue)
			{
				RemoveMatch(pCurrent, pCurrent->GetLeft(), true);
			}
			else
			{
				Remove(a_nValue);
			}
		}
		else if (a_nValue > pCurrent->GetLeft()->GetData() && pCurrent->GetRight() != nullptr)
		{
			if (pCurrent->GetRight()->GetData() == a_nValue)
			{
				RemoveMatch(pCurrent, pCurrent->GetRight(), false);
			}
			else
			{
				Remove(a_nValue);
			}
		}
		else
		{
			cout << a_nValue << " was not found!!!";
		}
	}

	//TreeNode* pCurrent = m_pRoot;
	//if (pCurrent->GetRight())
	//{
	//	// find minimum value via iteration
	//	
	//}
}

//READ AND DEBUG FUNCTION IF THERE IS ANY ISSUE.
//Check code with pseudo code
void BinaryTree::Remove(int a_nValue)
{
	RemoveNode(a_nValue, m_pRoot);
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
	//if the root node is empty, remove any/all nodes and the root node.

	if (m_pRoot != NULL)
	{
		//m_pRoot is the parent
		//delPtr is the last reference to the previous parent node

		//TreeNode* parent = m_pRoot;

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
			//delPtr->GetRight() = NULL;
			delete delPtr;

			cout << "The root with value: " << rootValue << "\nThe new root contains the value of: " << m_pRoot->GetData() << endl;
		}
		else if (m_pRoot->GetLeft() != NULL && m_pRoot->GetRight() == NULL)
		{
			m_pRoot = m_pRoot->GetLeft();
			//delPtr->GetRight() = NULL;
			delete delPtr;

			cout << "The root with value: " << rootValue << "\nThe new root contains the value of: " << m_pRoot->GetData() << endl;
		}

		//If the root has two children.
		else
		{
			smallestInRightTree = FindSmallest(m_pRoot->GetRight());
			RemoveNode(smallestInRightTree, m_pRoot);
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
		    RemoveNode(smallestInRightSubtree, removeNode);
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