[![CodeFactor](https://www.codefactor.io/repository/github/rainboi64/fluint/badge)](https://www.codefactor.io/repository/github/rainboi64/fluint)
[![.NET Core Desktop](https://github.com/Rainboi64/Fluint/actions/workflows/dotnet-desktop.yml/badge.svg)](https://github.com/Rainboi64/Fluint/actions/workflows/dotnet-desktop.yml)

# Fluint

## What is Fluint

This project was born out of the frustrations of the cumbersome, non-intuitive, level design process that modern 3D tools provide. our mission with this project is to build a utility that's aimed at making the level design process as efficient and mindless as possible, we want to provide powerful yet fluent tools for the designer.

## The Fluint paradigm
Fluint is created around the philosophy of superflexiblity. at it's core ``Fluint.Layer`` has an interface specification that specifies all the functionality of fluint, in it is also built a math library, some useful helper functions and a dynamic dependency injection container which loads all the classes, and modules at runtime, allowing for the superflexible nature of fluint. in fluint if you can see it, you can mod it.

## Project Status
work on this project is very slow since am doing other stuff. and it's honestly not a top priority.
coding on this is generally for fun. feel free to contirbute.

## Building
Fluint has scripts to automatically build and setup the file structure, mainly fluint has two important build configurations ``Release With Modules Setup`` and ``Debug With Modules Setup`` these two build configuration will automatically copy the modules into the defualt ``(SlnDir)/output/base/`` directory.

included is also a shell script for building and running fluint in the output directory.

## Running 
Starting fluint for the first time will probably popup an issue about missing runtimes, these could be located in the ``/base/runtimes`` directory, you will need to copy the files contained depending on your system into the ``output`` and ``base`` folder.

Copyright (C) Yaman Alhalabi 2022
