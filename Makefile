binaries: streamvis
	mkdir -p Binaries
	cp -r Source/Binaries/* Binaries

streamvis:
	$(MAKE) -C Source

.PHONY: streamvis
