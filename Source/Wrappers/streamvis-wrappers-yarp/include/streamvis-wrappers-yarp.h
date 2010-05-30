// Copyright © Julian Brunner 2009 - 2010

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Stream Visualizer.  If not, see <http://www.gnu.org/licenses/>.

#ifndef __STREAMVIS_WRAPPERS_YARP_H__
#define __STREAMVIS_WRAPPERS_YARP_H__

#include <yarp/os/all.h>

using namespace yarp::os;

extern "C"
{
	Network*	Network_New();
	void		Network_Dispose(Network* network);
	void		Network_Connect(const char* source, const char* destination);
	void		Network_Disconnect(const char* source, const char* destination);
	bool		Network_Exists(const char* name);
	
	BufferedPort<Bottle>*	BufferedPort_Bottle_New();
	void					BufferedPort_Bottle_Dispose(BufferedPort<Bottle>* port);
	void					BufferedPort_Bottle_Open(BufferedPort<Bottle>* port, const char* name);
	void					BufferedPort_Bottle_Close(BufferedPort<Bottle>* port);
	Bottle*					BufferedPort_Bottle_Prepare(BufferedPort<Bottle>* port);
	Bottle*					BufferedPort_Bottle_Read(BufferedPort<Bottle>* port);
	void					BufferedPort_Bottle_Write(BufferedPort<Bottle>* port);
	
	void	Bottle_Clear(Bottle* bottle);
	int		Bottle_Size(Bottle* bottle);
	Value*	Bottle_GetValue(Bottle* bottle, int index);
	Bottle*	Bottle_AddList(Bottle* bottle);
	void	Bottle_AddDouble(Bottle* bottle, double value);
	
	bool	Value_IsList(Value* value);
	bool	Value_IsInt(Value* value);
	bool	Value_IsDouble(Value* value);
	Bottle*	Value_AsList(Value* value);
	int		Value_AsInt(Value* value);
	double	Value_AsDouble(Value* value);
}

#endif
