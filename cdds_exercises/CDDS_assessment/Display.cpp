#include "Display.h"
#include "doubleLinkedList.h"
#include "iostreamUtils.h"
#include <iostream>
#include <algorithm>


using namespace std;

std::string Display::getMenuOption()
{
    std::string userInput;
    cinclear();
    cin >> userInput;

    std::transform(userInput.begin(), userInput.end(), userInput.begin(), ::tolower);
    return userInput;
}

void Display::displayMenu()
{
    cout << endl << "=== Menu ===" << endl;
    cout << "S)ort" << endl;
    cout << "C)lear" << endl;
    cout << "Q)uit program" << endl;
    cout << "--------------" << endl;
    cout << "> ";
}

void Display::Update()
{
    doubleLinkedList doubleList;

    displayMenu();
    std::string menuOption = getMenuOption();

    if (menuOption == "c")
    {
        //clear function:
        doubleList.clear();
    }
    else if (menuOption == "s")
    {
        //bubble sort function:
        doubleList.printAllList();
    }
    else if (menuOption == "q")
    {
        //shutdown application:

    }
}
