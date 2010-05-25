all: yarp ros

.PHONY: yarp ros

yarp:
	-$(MAKE) -C streamvis-wrappers-yarp

ros:
	-ROS_PACKAGE_PATH=$$ROS_PACKAGE_PATH:$$PWD/streamvis-wrappers-ros $(MAKE) -C streamvis-wrappers-ros
