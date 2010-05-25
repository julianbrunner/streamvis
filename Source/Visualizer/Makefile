all: visualizer

.PHONY: visualizer

visualizer:
	xbuild /target:Rebuild /property:Configuration=Release
	mkbundle2 -z -o Visualizer/bin/Release/streamvis Visualizer/bin/Release/*.exe Visualizer/bin/Release/*.dll
