// Copyright © Julian Brunner 2009 - 2011

// This file is part of Stream Visualizer (streamvis).
// 
// Stream Visualizer is free software: you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation, either version 3 of the License, or (at your option) any
// later version.
// 
// Stream Visualizer is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
// FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
// details.
// 
// You should have received a copy of the GNU General Public License along with
// Stream Visualizer. If not, see <http://www.gnu.org/licenses/>.

#include "streamvis-wrappers-ros/streamvis-wrappers-ros.h"

#include <string>

#include <ros/ros.h>
#include <ros/serialization.h>
#include <topic_tools/shape_shifter.h>

using namespace std;
using namespace ros;
using namespace ros::serialization;
using namespace topic_tools;

void InitializeRos()
{
	int argc = 0;
	char** argv = NULL;
	init(argc, argv, "streamvis_wrappers_ros");
}
void ShutdownRos()
{
	shutdown();
}
void RosSpin()
{
	spin();
}

NodeHandle* CreateNode()
{
	return new NodeHandle();
}
void DisposeNode(NodeHandle* node)
{
	delete node;
}

Subscriber* CreateSubscriber(NodeHandle* node, const char* topicName, unsigned int queueLength, void (*callback)(ShapeShifter::ConstPtr))
{
	return new Subscriber(node->subscribe<ShapeShifter>(topicName, queueLength, callback));
}
void DisposeSubscriber(Subscriber* subscriber)
{
	delete subscriber;
}

const char* ShapeShifterGetDataType(const ShapeShifter::ConstPtr message)
{
	string info = message->getDataType();

	char* result = new char[info.size() + 1];
	strcpy(result, info.c_str());
	return result;	
}
const char* ShapeShifterGetDefinition(const ShapeShifter::ConstPtr message)
{
	string info = message->getMessageDefinition();

	char* result = new char[info.size() + 1];
	strcpy(result, info.c_str());
	return result;
}
unsigned char* ShapeShifterGetData(const ShapeShifter::ConstPtr message)
{
	unsigned char* data = new unsigned char[message->size()];

	OStream stream(data, message->size());
	message->write(stream);

	return data;
}
unsigned int ShapeShifterGetDataLength(const ShapeShifter::ConstPtr message)
{
	return message->size();
}

