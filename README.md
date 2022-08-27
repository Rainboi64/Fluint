# Fluint [![CodeFactor](https://www.codefactor.io/repository/github/rainboi64/fluint/badge)](https://www.codefactor.io/repository/github/rainboi64/fluint)
[![ForTheBadge built-with-love](http://ForTheBadge.com/images/badges/built-with-love.svg)](https://GitHub.com/Naereen/)
## Build Status
| Platform        | Status                                                                                                                                                                                              |
|-----------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Ubuntu Desktop  | [![.NET Core Ubuntu Desktop](https://github.com/Rainboi64/Fluint/actions/workflows/dotnet-ubuntu-desktop.yml/badge.svg)](https://github.com/Rainboi64/Fluint/actions/workflows/dotnet-ubuntu-desktop.yml) |
| Windows Desktop | [![.NET Core Windows Desktop](https://github.com/Rainboi64/Fluint/actions/workflows/dotnet-windows-desktop.yml/badge.svg)](https://github.com/Rainboi64/Fluint/actions/workflows/dotnet-windows-desktop.yml) |
| MacOS Desktop   | [![.NET Core MacOS Desktop](https://github.com/Rainboi64/Fluint/actions/workflows/dotnet-macos-desktop.yml/badge.svg)](https://github.com/Rainboi64/Fluint/actions/workflows/dotnet-macos-desktop.yml)    |

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

## Modules

you can access the ``Fluint.Layer`` documentation by clicking [here](.).

Currently these modules include :
* [``Fluint.Configuration.Base``](.) is a module that implements tools for storing, reading, and writing configurations, and themes in real-time.
* [``Fluint.Diagnostics.Base``](.) is a module that implements tools for logging, and debugging.
* [``Fluint.Input.Base``](.) is a module that implements mouse, and keyboard input into Fluint.
* [``Fluint.IO.Base``](.) is a module that implements file IO tools into Fluint, such as model importers, and exporters.
* [``Fluint.Localization.Base``](.) is a module that implements a localization/globalization system into fluint, with expandability in mind.
* [``Fluint.Networking.Base``](.) is a module that implements a server, and client networking features for cooperative work.
* [``Fluint.SDK.Base``](.) is a module that provides a toolset to make the debugging and module creation process for developers and third-parties easier.
* [``Fluint.Tasks.Base``](.) is a module for creating scheduled tasks that run at Fluint's runtime.
* [``Fluint.Graphics.Base``](.) is a module responsible for rendering in Fluint which is abstracted over the individual graphics APIs.
* [``Fluint.UI.Base``](.) contains an ImGui implementation of the UI library provided by Fluint.
* [``Fluint.UI.Layout.Base``](.) is the module which contains the base layout of the application.
* [``Fluint.Logic.Base``](.) is a module which contains separate pieces of logic which are reusable across the entirety of the project.
* [``Fluint.StateManagement.Base``](.) is a module which contains separate pieces of logic which are reusable across the entirety of the project.

There are also engine modules, which house components for rendering in different APIs, and these include :
* [``Fluint.Engine.GL46``](.) is a module that implements the OpenGL 4.6 graphics API into fluint.
* [``Fluint.Engine.GL33``](.) **[INCOMPLETE]** is a module that implements the OpenGL 3.3 graphics API into fluint.
* [``Fluint.Engine.D3D11``](.) **[INCOMPLETE]** is a module that implements the DirectX 11 graphics API into fluint.
* [``Fluint.Engine.D3D12``](.) **[INCOMPLETE]** is a module that implements the DirectX 12 graphics API into fluint.
* [``Fluint.Engine.VK``](.) **[INCOMPLETE]** is a module that implements the Vulkan graphics API into fluint.

Copyright (C) Yaman Alhalabi 2022
