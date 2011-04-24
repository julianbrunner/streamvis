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

all: wrappers visualizer
	-mkdir Binaries
	-cp -r Wrappers/Binaries/* Binaries
	cp -r Visualizer/Binaries/* Binaries

wrappers:
	$(MAKE) -C Wrappers

visualizer:
	$(MAKE) -C Visualizer

clean: clean-wrappers clean-visualizer
	-rm -r Binaries

clean-wrappers:
	$(MAKE) -C Wrappers clean

clean-visualizer:
	$(MAKE) -C Visualizer clean

.PHONY: wrappers visualizer clean clean-wrappers clean-visualizer
