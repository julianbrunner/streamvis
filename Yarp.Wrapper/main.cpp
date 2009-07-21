#include "main.h"

Network* Network_New()
{
	return new Network;
}
void Network_Dispose(Network* network)
{
	delete network;
}
void Network_Connect(const char* source, const char* destination)
{
	Network::connect(source, destination);
}

BufferedPort<Bottle>* BufferedPort_Bottle_New()
{
	return new BufferedPort<Bottle>;
}
void BufferedPort_Bottle_Dispose(BufferedPort<Bottle>* port)
{
	delete port;
}	
void BufferedPort_Bottle_Open(BufferedPort<Bottle>* port, const char* name)
{
	port->open(name);
}
void BufferedPort_Bottle_Close(BufferedPort<Bottle>* port)
{
	port->close();
}
Bottle* BufferedPort_Bottle_Read(BufferedPort<Bottle>* port)
{
	return port->read();
}

double Bottle_GetDouble(Bottle* bottle, int index)
{
	return bottle->get(index).asDouble();
}