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

ros::NodeHandle* Initialize()
{
	int argc = 0;
	char** argv = NULL;
	ros::init(argc, argv, "streamvis-wrappers-ros");

	return new ros::NodeHandle;
}
void Dispose(ros::NodeHandle* nodeHandle)
{
	delete nodeHandle;
}
ros::Publisher* Advertise(ros::NodeHandle* nodeHandle, const char* topicName, unsigned int queueLength)
{
	return new ros::Publisher(nodeHandle->advertise<topic_tools::ShapeShifter>(topicName, queueLength));
}
void DisposePublisher(ros::Publisher* publisher)
{
	delete publisher;
}
ros::Subscriber* Subscribe(ros::NodeHandle* nodeHandle, const char* topicName, unsigned int queueLength, void (*callback)(topic_tools::ShapeShifter::ConstPtr))
{
	return new ros::Subscriber(nodeHandle->subscribe<topic_tools::ShapeShifter>(topicName, queueLength, callback));
}
void DisposeSubscriber(ros::Subscriber* subscriber)
{
	delete subscriber;
}
bool ros_ok()
{
	return ros::ok();
}
void ros_spinOnce()
{
	ros::spinOnce();
}
