// 
// IGracefulExit.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

namespace Fluint.Layer.Functionality.Common;

[Initialization(InitializationMethod.Instanced)]
public interface IGracefulExit : ILogicModule
{
    void Exit();
}