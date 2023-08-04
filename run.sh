#!/bin/sh
dotnet msbuild -property:Configuration="Release With Modules Setup"
echo -----------------------------------------------
echo                 Build Completed                
echo -----------------------------------------------
cd ./output/release/AnyCPU/
dotnet Fluint.Editor.Runtime.dll