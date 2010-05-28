binaries: wrapper
	mkdir -p bin
	cp build/libstreamvis-wrappers-yarp.so bin

wrapper:
	mkdir -p build
	cd build && cmake ..
	$(MAKE) -C build

.PHONY: wrapper
