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

#ifndef __STREAMVIS_WRAPPERS_ROS_H__
#define __STREAMVIS_WRAPPERS_ROS_H__

#include <ros/ros.h>
#include <topic_tools/shape_shifter.h>

using namespace ros;
using namespace topic_tools;

extern "C"
{
	void InitializeRos();
	void ShutdownRos();
	void RosSpin();

	NodeHandle* CreateNode();
	void DisposeNode(NodeHandle* node);

	Subscriber* CreateSubscriber(NodeHandle* node, const char* topicName, unsigned int queueLength, void (*callback)(const ShapeShifter::ConstPtr));
	void DisposeSubscriber(Subscriber* subscriber);

	const char* ShapeShifterGetDataType(const ShapeShifter::ConstPtr message);
	const char* ShapeShifterGetDefinition(const ShapeShifter::ConstPtr message);
	unsigned char* ShapeShifterGetData(const ShapeShifter::ConstPtr message);
	unsigned int ShapeShifterGetDataLength(const ShapeShifter::ConstPtr messsage);
}

#endif
