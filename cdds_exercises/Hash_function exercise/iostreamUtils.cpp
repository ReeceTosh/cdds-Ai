#include "iostreamUtils.h"
#include <iostream>

using namespace std;

void cinclear()
{	
	cin.clear();
	cin.ignore(cin.rdbuf()->in_avail());
}
