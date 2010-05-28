cmake_minimum_required(VERSION 2.6)

project(streamvis-wrappers-yarp)

find_package(YARP REQUIRED)
include_directories(${YARP_INCLUDE_DIRS})

include_directories(include)

add_library(${PROJECT_NAME} SHARED source/streamvis-wrappers-yarp.cpp)

target_link_libraries(${PROJECT_NAME} ${YARP_LIBRARIES})
