cmake_minimum_required(VERSION 3.14 FATAL_ERROR)

if(POLICY CMP0092)
    cmake_policy(SET CMP0092 NEW) # Don't add -W3 warning level by default.
endif()


file(READ "${CMAKE_CURRENT_SOURCE_DIR}/version.txt" VERSION_FILE_CONTENTS)
string(STRIP "${VERSION_FILE_CONTENTS}" VERSION_FILE_CONTENTS)
set(NETLM_VERSION "${VERSION_FILE_CONTENTS}" CACHE STRING "Set version name")
set(NETLM_PACKAGE "plugify-module-dotnet" CACHE STRING "Set package name")
string(REPLACE "v" "" NETLM_VERSION "${NETLM_VERSION}")
string(REGEX REPLACE "[.+-]" ";" NETLM_VERSION_LIST ${NETLM_VERSION})
list(GET NETLM_VERSION_LIST 0 NETLM_VERSION_MAJOR)
list(GET NETLM_VERSION_LIST 1 NETLM_VERSION_MINOR)
list(GET NETLM_VERSION_LIST 2 NETLM_VERSION_PATCH)

project(plugify-module-dotnet 
        VERSION "${NETLM_VERSION_MAJOR}.${NETLM_VERSION_MINOR}.${NETLM_VERSION_PATCH}"
        DESCRIPTION "C# (.NET) Language Module"
        HOMEPAGE_URL "https://github.com/untrustedmodders/plugify-module-dotnet" 
        LANGUAGES C CXX)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)
set(CMAKE_C_STANDARD 11)
set(CMAKE_C_STANDARD_REQUIRED ON)
set(CMAKE_C_EXTENSIONS OFF)
set(CMAKE_POSITION_INDEPENDENT_CODE ON)

if(NOT CMAKE_BUILD_TYPE MATCHES "Debug|Devel|MinSizeRel|RelWithDebInfo|Release")
    message(STATUS "CMAKE_BUILD_TYPE not set, defaulting to Debug.")
    set(CMAKE_BUILD_TYPE Debug)
endif()

if(UNIX AND NOT APPLE)
    set(LINUX TRUE)
endif()

#
# Plugify
#
set(PLUGIFY_BUILD_SHARED_LIB ON CACHE INTERNAL "")
set(PLUGIFY_BUILD_JIT ON CACHE INTERNAL "")
set(PLUGIFY_BUILD_ASSEMBLY ON CACHE INTERNAL "")
set(PLUGIFY_BUILD_TESTS OFF CACHE INTERNAL "")
set(PLUGIFY_INTERFACE ON CACHE INTERNAL "")
set(PLUGIFY_DOWNLOADER OFF CACHE INTERNAL "")
if(LINUX)
    set(PLUGIFY_USE_STATIC_STDLIB ON CACHE INTERNAL "")
endif()
add_subdirectory(external/plugify)

#
# C# Language Module for Plugify
#
file(GLOB_RECURSE NETLM_SOURCES RELATIVE ${CMAKE_CURRENT_SOURCE_DIR} "src/*.cpp")
set(NETLM_PCH_FILE "src/pch.hpp")

add_library(${PROJECT_NAME} SHARED ${NETLM_SOURCES})

set(NETLM_LINK_LIBRARIES plugify::plugify plugify::plugify-jit plugify::plugify-assembly asmjit::asmjit)

if(NOT COMPILER_SUPPORTS_FORMAT)
    set(NETLM_LINK_LIBRARIES ${NETLM_LINK_LIBRARIES} fmt::fmt-header-only)
endif()

if(NOT COMPILER_SUPPORTS_STACKTRACE)
    set(NETLM_LINK_LIBRARIES ${NETLM_LINK_LIBRARIES} cpptrace::cpptrace)
endif()

target_include_directories(${PROJECT_NAME} PRIVATE ${CMAKE_CURRENT_SOURCE_DIR}/external/dotnet/include)

target_link_libraries(${PROJECT_NAME} PRIVATE ${NETLM_LINK_LIBRARIES})
target_precompile_headers(${PROJECT_NAME} PRIVATE ${NETLM_PCH_FILE})

if(MSVC)
    target_compile_options(${PROJECT_NAME} PRIVATE /W4 /WX /Wv:18)
else()
    target_compile_options(${PROJECT_NAME} PRIVATE -Wextra -Wconversion -Werror) # -Wshadow -Wpedantic
endif()

if(APPLE)
    target_link_options(${PROJECT_NAME} PRIVATE "-Wl,-exported_symbols_list,${CMAKE_CURRENT_SOURCE_DIR}/sym/exported_symbols.lds")
elseif(UNIX)
    target_link_options(${PROJECT_NAME} PRIVATE "-Wl,--version-script,${CMAKE_CURRENT_SOURCE_DIR}/sym/version_script.lds")
endif()

include(GenerateExportHeader)
generate_export_header(${PROJECT_NAME} EXPORT_MACRO_NAME NETLM_EXPORT EXPORT_FILE_NAME ${CMAKE_BINARY_DIR}/exports/module_export.h)
target_include_directories(${PROJECT_NAME} PRIVATE ${CMAKE_BINARY_DIR}/exports)

target_compile_definitions(${PROJECT_NAME} PRIVATE
        NETLM_PLATFORM_WINDOWS=$<BOOL:${WIN32}>
        NETLM_PLATFORM_APPLE=$<BOOL:${APPLE}>
        NETLM_PLATFORM_LINUX=$<BOOL:${LINUX}>
        NETLM_ARCH_ARM=$<BOOL:${PLUGIFY_USE_ARM}>
        NETLM_IS_DEBUG=$<STREQUAL:${CMAKE_BUILD_TYPE},Debug>
        NETLM_LIBRARY_SUFFIX="${CMAKE_SHARED_LIBRARY_SUFFIX}"
        NETLM_LIBRARY_PREFIX="${CMAKE_SHARED_LIBRARY_PREFIX}"
)

configure_file(
    ${CMAKE_CURRENT_SOURCE_DIR}/${PROJECT_NAME}.pmodule.in
    ${CMAKE_CURRENT_BINARY_DIR}/${PROJECT_NAME}.pmodule
)