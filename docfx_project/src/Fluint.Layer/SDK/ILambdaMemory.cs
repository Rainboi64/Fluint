// 
// ILambdaMemory.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Collections.Generic;

namespace Fluint.Layer.SDK;

[Initialization(InitializationMethod.Singleton)]
public interface ILambdaMemory : IModule
{
    void Add(string name, object value);
    object Get(string name);
    IReadOnlyDictionary<string, object> AsDictionary();
}