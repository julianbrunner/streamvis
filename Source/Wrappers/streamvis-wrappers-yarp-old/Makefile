binary = libstreamvis-wrappers-yarp.so
includeDirectories = 
libraryDirectories = 
libraries = YARP_OS
classes = 
CXXFLAGS += -fPIC
LDFLAGS += -shared

CPPFLAGS += $(addprefix -I,$(includeDirectories))
LDFLAGS += $(addprefix -l,$(libraries)) $(addprefix -L,$(libraryDirectories))

objectFiles = $(addsuffix .o,$(classes))
dependencyFiles = $(addsuffix .d,$(classes))

bin/$(binary): $(binary)
	mkdir -p bin
	mv $(binary) bin

$(binary): $(objectFiles)

-include $(dependencyFiles)

clean:
	rm $(objectFiles) $(dependencyFiles) bin/$(binary)

.SECONDARY: $(binary)
.PHONY: clean

%.o: %.cpp
	$(CXX) -c $(CPPFLAGS) $(CXXFLAGS) $*.cpp -o $*.o
	$(CXX) -MM $(CPPFLAGS) $(CXXFLAGS) $*.cpp > $*.d
%: %.cpp
	$(CXX) $(CPPFLAGS) $(CXXFLAGS) $*.cpp $(LDFLAGS) -o $*
lib%.so: %.cpp
	$(CXX) $(CPPFLAGS) $(CXXFLAGS) $*.cpp $(LDFLAGS) -o lib$*.so
