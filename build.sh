#!/bin/sh
dotnet msbuild -property:Configuration="Release With Modules Setup"
echo -----------------------------------------------
echo                 Build Completed                
echo -----------------------------------------------