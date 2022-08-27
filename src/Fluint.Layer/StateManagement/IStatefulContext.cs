// 
// IStatefulContext.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

namespace Fluint.Layer.StateManagement;

[Initialization(InitializationMethod.Scoped)]
public interface IStatefulContext : IModule
{
}