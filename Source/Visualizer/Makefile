binaries: bundle
	mkdir -p Binaries
	cp Visualizer/bin/Release/streamvis Binaries

bundle: visualizer
	mkbundle2 -z -o Visualizer/bin/Release/streamvis Visualizer/bin/Release/*.exe Visualizer/bin/Release/*.dll

visualizer:
	xbuild /property:Configuration=Release

.PHONY: visualizer
