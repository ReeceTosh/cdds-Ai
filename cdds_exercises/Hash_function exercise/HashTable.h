#pragma once
#include <functional>;
#include <iostream>;
#include <string>;

using namespace std;

template<typename KeyType, typename T>
class HashTable
{
public:

	typedef std::function<unsigned int(const KeyType&)> HashFunc;

	//Constructor sets up the size of the hash table with what function is being used;
	HashTable(unsigned int size, HashFunc hashFunc) : m_size(size), m_data(new T[size]), m_hash(hashFunc)
	{
	};

	//Destructor clears all the data that exists in the hash table
	~HashTable()
	{
		delete[] m_data;
	};

	//Overload operator for the hash table to work: typePointer of an array with they hash key.
	T& operator[] (const KeyType& key)
	{
		auto hashedKey = m_hash(key) % m_size;
		return m_data[hashedKey];
	}

	const T& operator[] (const KeyType& key) const
	{
		auto hashedKey = m_hash(key) % m_size;
		return m_data[hashedKey];
	}

	T& operator[] (unsigned int key)
	{
		 return m_data[key];
	}


	HashFunc m_hash;
	T* m_data;
	unsigned int m_size;
};
