binaries: yarp ros
	mkdir -p Binaries
	-cp streamvis-wrappers-yarp/bin/libstreamvis-wrappers-yarp.so Binaries
	-cp streamvis-wrappers-ros/bin/libstreamvis-wrappers-ros.so Binaries

yarp:
	-$(MAKE) -C streamvis-wrappers-yarp

ros:
	-ROS_PACKAGE_PATH=$$ROS_PACKAGE_PATH:$$PWD/streamvis-wrappers-ros $(MAKE) -C streamvis-wrappers-ros

.PHONY: yarp ros
