binaries: wrapper
	mkdir -p Binaries
	cp build/libstreamvis-wrappers-yarp.so Binaries

wrapper:
	mkdir -p build
	cd build && cmake ..
	$(MAKE) -C build

.PHONY: wrapper
