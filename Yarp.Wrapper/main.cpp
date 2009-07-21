#include "main.h"
#include <iostream>
#include <yarp/os/all.h>

using namespace std;
using namespace yarp::os;

Network *network = NULL;

void Network_Initialize()
{
	network = new Network();
	cout << network;
}
void Network_Dispose()
{
	// TODO: Throw exception if network == NULL
	delete network;
	network = NULL;
}