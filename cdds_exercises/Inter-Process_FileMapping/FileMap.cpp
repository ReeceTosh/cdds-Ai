#include "FileMap.h"
#include <iostream>

using namespace std;

class FileMap
{


	HANDLE fileHandle = CreateFileMapping(INVALID_HANDLE_VALUE, nullptr, PAGE_READWRITE, 0, sizeof(MyData), L"MyShared");

};
