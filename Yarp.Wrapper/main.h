// project created on 7/21/2009 at 2:04 PM
#ifndef __MAIN_H__
#define __MAIN_H__

#include <yarp/os/all.h>

using namespace yarp::os;

extern "C"
{
	Network* Network_New();
	void Network_Dispose(Network* network);
	void Network_Connect(const char* source, const char* destination);
	
	BufferedPort<Bottle>* BufferedPort_Bottle_New();
	void BufferedPort_Bottle_Dispose(BufferedPort<Bottle>* port);
	void BufferedPort_Bottle_Open(BufferedPort<Bottle>* port, const char* name);
	void BufferedPort_Bottle_Close(BufferedPort<Bottle>* port);
	Bottle* BufferedPort_Bottle_Read(BufferedPort<Bottle>* port);
	
	double Bottle_GetDouble(Bottle* bottle, int index);
}

#endif
