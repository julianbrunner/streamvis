binary = streamvis-wrappers-yarp
binarySuffix = .so
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

$(binary): $(objectFiles)

-include $(dependencyFiles)

clean:
	rm $(objectFiles) $(dependencyFiles) $(binary)$(binarySuffix)

.PHONY: clean

%.o: %.cpp
	$(CXX) -c $(CPPFLAGS) $(CXXFLAGS) $*.cpp -o $*.o
	$(CXX) -MM $(CPPFLAGS) $(CXXFLAGS) $*.cpp > $*.d
%: %.cpp
	$(CXX) $(CPPFLAGS) $(CXXFLAGS) $*.cpp $(LDFLAGS) -o $*$(binarySuffix)
