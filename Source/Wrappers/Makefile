# Copyright © Julian Brunner 2009 - 2010

# This file is part of Stream Visualizer (streamvis).
# 
# Stream Visualizer is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
# 
# Stream Visualizer is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
# 
# You should have received a copy of the GNU General Public License
# along with Stream Visualizer.  If not, see <http://www.gnu.org/licenses/>.

binaries: yarp ros
	mkdir -p Binaries
	-cp streamvis-wrappers-yarp/bin/libstreamvis-wrappers-yarp.so Binaries
	-cp streamvis-wrappers-ros/bin/libstreamvis-wrappers-ros.so Binaries

yarp:
	-$(MAKE) -C streamvis-wrappers-yarp

ros:
	-ROS_PACKAGE_PATH=$$ROS_PACKAGE_PATH:$$PWD/streamvis-wrappers-ros $(MAKE) -C streamvis-wrappers-ros

.PHONY: yarp ros
