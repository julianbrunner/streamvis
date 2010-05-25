Binaries/streamvis: streamvis
	mkdir -p Binaries
	-cp Source/Wrappers/streamvis-wrappers-yarp/bin/libstreamvis-wrappers-yarp.so Binaries
	-cp Source/Wrappers/streamvis-wrappers-ros/bin/libstreamvis-wrappers-ros.so Binaries
	cp Source/Visualizer/Visualizer/bin/Release/streamvis Binaries

streamvis:
	$(MAKE) -C Source
