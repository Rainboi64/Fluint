// 
// ModulePair.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

namespace Fluint.Layer.DependencyInjection;

public record ModulePair(
    string Abstraction,
    string Implementation,
    string Assembly,
    string AssemblyPath,
    InitializationMethod InitializationMethod,
    bool Active = true);