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

#include "streamvis-wrappers-ros.h"

#include <ros/ros.h>
#include <topic_tools/shape_shifter.h>

																	#include <stdio.h>

ros::NodeHandle* InitializeNode()
{
	int argc = 0;
	char** argv = NULL;
	ros::init(argc, argv, "streamvis_wrappers_ros");

	return new ros::NodeHandle;
}
void DisposeNode(ros::NodeHandle* node)
{
	delete node;
}
bool RosOk()
{
	return ros::ok();
}
void RosSpinOnce()
{
	ros::spinOnce();
}
ros::Publisher* Advertise(ros::NodeHandle* node, const char* topicName, unsigned int queueLength)
{
	return new ros::Publisher(node->advertise<topic_tools::ShapeShifter>(topicName, queueLength));
}
void DisposePublisher(ros::Publisher* publisher)
{
	delete publisher;
}
ros::Subscriber* Subscribe(ros::NodeHandle* node, const char* topicName, unsigned int queueLength, void (*callback)(topic_tools::ShapeShifter::ConstPtr))
{
	return new ros::Subscriber(node->subscribe<topic_tools::ShapeShifter>(topicName, queueLength, callback));
}
void DisposeSubscriber(ros::Subscriber* subscriber)
{
	delete subscriber;
}
const char* ShapeShifterGetDataType(topic_tools::ShapeShifter::ConstPtr message)
{
	std::string info = message->datatype;

	char* result = new char[info.size() + 1];
	strcpy(result, info.c_str());
	return result;	
}
const char* ShapeShifterGetDefinition(topic_tools::ShapeShifter::ConstPtr message)
{
	std::string info = message->msg_def;

	char* result = new char[info.size() + 1];
	strcpy(result, info.c_str());
	return result;
}
