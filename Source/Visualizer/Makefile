# Copyright © Julian Brunner 2009 - 2011

# This file is part of Stream Visualizer (streamvis).
# 
# Stream Visualizer is free software: you can redistribute it and/or modify it
# under the terms of the GNU General Public License as published by the Free
# Software Foundation, either version 3 of the License, or (at your option) any
# later version.
# 
# Stream Visualizer is distributed in the hope that it will be useful, but
# WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
# FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more
# details.
# 
# You should have received a copy of the GNU General Public License along with
# Stream Visualizer. If not, see <http:#www.gnu.org/licenses/>.

all: bundle
	-mkdir Binaries
	cp Visualizer/bin/Release/streamvis Binaries

bundle: visualizer
	mkbundle -z -o Visualizer/bin/Release/streamvis Visualizer/bin/Release/*.exe Visualizer/bin/Release/*.dll

visualizer:
	xbuild /property:Configuration=Release

clean: clean-visualizer
	-rm -r Binaries

clean-bundle:
	-rm Visualizer/bin/Release/streamvis

clean-visualizer: clean-bundle
	xbuild /target:Clean

.PHONY: bundle visualizer clean clean-bundle clean-visualizer
