// Copyright © Julian Brunner 2009

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

#include "main.h"

Network*	Network_New()													{ return new Network;							}
void		Network_Dispose(Network* network)								{ delete network;;								}
void		Network_Connect(const char* source, const char* destination)	{ Network::connect(source, destination);		}
void		Network_Disconnect(const char* source, const char* destination)	{ Network::disconnect(source, destination);		}
bool		Network_Exists(const char* name)								{ return Network::queryName(name).isValid();	}

BufferedPort<Bottle>*	BufferedPort_Bottle_New()												{ return new BufferedPort<Bottle>;		}
void					BufferedPort_Bottle_Dispose(BufferedPort<Bottle>* port)					{ delete port;							}
void					BufferedPort_Bottle_Open(BufferedPort<Bottle>* port, const char* name)	{ port->open(name);	port->setStrict();	}
void					BufferedPort_Bottle_Close(BufferedPort<Bottle>* port)					{ port->close();						}
Bottle*					BufferedPort_Bottle_Prepare(BufferedPort<Bottle>* port)					{ return &port->prepare();				}
Bottle*					BufferedPort_Bottle_Read(BufferedPort<Bottle>* port)					{ return port->read();					}
void					BufferedPort_Bottle_Write(BufferedPort<Bottle>* port)					{ port->writeStrict();					}

void	Bottle_Clear(Bottle* bottle)					{ bottle->clear();				}
int		Bottle_Size(Bottle* bottle)						{ return bottle->size();		}
Value*	Bottle_GetValue(Bottle* bottle, int index)		{ return &bottle->get(index);	}
Bottle*	Bottle_AddList(Bottle* bottle)					{ return &bottle->addList();	}
void	Bottle_AddDouble(Bottle* bottle, double value)	{ bottle->addDouble(value);		}
	
bool	Value_IsList(Value* value)		{ return value->isList();	}
bool	Value_IsDouble(Value* value)	{ return value->isDouble();	}
Bottle*	Value_AsList(Value* value)		{ return value->asList();	}
double	Value_AsDouble(Value* value)	{ return value->asDouble();	}