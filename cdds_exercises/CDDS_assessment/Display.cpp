#include "Display.h"
#include "doubleLinkedList.h"
#include "iostreamUtils.h"
#include <iostream>
#include <algorithm>


using namespace std;

//Interaction receiver
std::string Display::getMenuOption()
{
	std::string userInput;
	cinclear();
	cin >> userInput;

	//converts response to lowercase.
	std::transform(userInput.begin(), userInput.end(), userInput.begin(), ::tolower);
	return userInput;
}

//Displays available options
void Display::displayMenu()
{
	cout << endl << "=== Menu ===" << endl;
	cout << "A)dd a node" << endl;
	cout << "D)isplay all existing nodes" << endl;
	cout << "R)emove a node" << endl;
	cout << "S)ort" << endl;
	cout << "C)lear" << endl;
	cout << "Q)uit program" << endl;
	cout << "--------------" << endl;
	cout << "> ";
}

//This will check the interaction and perform a task when a certain button is used.
void Display::Update()
{
	doubleLinkedList doubleList;
	bool isActive = true;
	bool isBackwards = false;
	while (isActive)
	{

		displayMenu();
		std::string menuOption = getMenuOption();

		if (menuOption == "c")
		{
			system("cls");
			if (doubleList.empty())
			{
				cout << "The list is already empty.";
			}

			//clear function:
			doubleList.clear();
		}
		else if (menuOption == "s")
		{
			system("cls");
			if (doubleList.empty())
			{
				cout << "Sorry, the list is empty, sorting cannot be done at this time." << endl;
			}
			else
			{
				string input;
				cout << "Sort Orders: \n\n- H)ead-tail \n- T)ail-head \n\nInput: ";
				cin >> input;
				std::transform(input.begin(), input.end(), input.begin(), ::tolower);;

				if (input == "h")
				{
					//bubble sort function:
					doubleList.printAllList();
					isBackwards = false;
				}
				else if (input == "t")
				{
					doubleList.printListReverse();
					isBackwards = true;
				}
				else
				{
					cout << "That is not the correct prompt." << endl;
				}
			}

		}
		else if (menuOption == "q")
		{
			//shutdown application:
			isActive = false;
			system("exit");
		}
		else if (menuOption == "a")
		{
			system("cls");
			string input;
			cout << "What number value would you like to add: ";
			cin >> input;

			int value = stoi(input);

			if (doubleList.empty())
			{
				doubleList.pushFront(value);
			}
			else
			{
				string choice;
				cout << "\n\nadd the node at the F)ront or B)ack: ";
				cin >> choice;

				std::transform(choice.begin(), choice.end(), choice.begin(), ::tolower);;

				if (choice == "f")
				{
					doubleList.pushFront(value);
				}
				else if (choice == "b")
				{
					doubleList.pushBack(value);
				}
			}
		}
		else if (menuOption == "r")
		{
			system("cls");
			if (doubleList.empty())
			{
				cout << "The list is empty. There is nothing to remove." << endl;
				system("exit");
			}

			else
			{
				cout << "Current List of nodes: ";
				doubleList.printAllList();
				cout << endl;

				string input;
				cout << "\nWhat number value would you like to remove: ";
				cin >> input;

				int value = stoi(input);
				try
				{
					doubleList.remove(value);
				}
				catch (system_error error)
				{
					cout << "Value does not exist, this program will now shutdown." << endl;
					system("exit");
				}

			}
		}
		else if (menuOption == "d")
		{
			system("cls");
			if (doubleList.empty())
			{
				cout << "Sorry, there is no list to display at this time, please create a node and try again." << endl;
			}
			else
			{
				doubleList.Display(isBackwards);
			}
		}
	}
}
