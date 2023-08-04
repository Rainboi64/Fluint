#!/bin/sh
dotnet msbuild -property:Configuration="Release With Modules Setup"
cd ./output/release/AnyCPU/
dotnet Fluint.Editor.Runtime.dll