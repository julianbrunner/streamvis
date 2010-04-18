#include "main.h"

#include <ros/ros.h>

void Callback(const unsigned char* data)
{
}

void Ros_Init()
{
	ros::init(0, NULL, "ros.wrapper");
}