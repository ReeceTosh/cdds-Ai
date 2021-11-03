/*----------------------------------------
Author: Richard Stern
Description: A simple binary search tree
Date: 17/4/2015
----------------------------------------*/
#ifndef _BINARYTREE_H_
#define _BINARYTREE_H_

class TreeNode;

class BinaryTree
{
public:
	BinaryTree();
	~BinaryTree();

	bool IsEmpty() const;
	void Insert(int a_nValue);
	void Remove(int a_nValue);
	TreeNode* Find(int a_nValue);

	void PrintOrdered();
	void PrintUnordered();

	void Draw(TreeNode* selected = nullptr);

private:
	//Find the node with the specified value.
	bool FindNode(int a_nSearchValue, TreeNode*& ppOutNode, TreeNode*& ppOutParent);

	//Used to recurse through the nodes in value order and print their values.
	void PrintOrderedRecurse(TreeNode*);
	void PrintUnorderedRecurse(TreeNode*);

	//Remove functions used with in the Remove() when a condition is met to remove all nodes.
	void RemoveRoot();
	void RemoveMatch(TreeNode* parent, TreeNode* removeNode, bool left);
	void RemoveNode(int a_nValue, TreeNode* parent);
	int FindSmallest(TreeNode* ptr);

	void Draw(TreeNode*, int x, int y, int horizontalSpacing, TreeNode* selected = nullptr);

	//The root node of the tree
	TreeNode* m_pRoot;
};

#endif //_BINARYTREE_H_