#!/bin/sh
dotnet msbuild -property:Configuration="Debug With Modules Setup"
echo -----------------------------------------------
echo                 Build Completed
echo -----------------------------------------------
cd ./output/debug/AnyCPU/
dotnet Fluint.Editor.Runtime.dll --rebuild
