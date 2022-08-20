// 
// LambdaMemory.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System.Collections.Generic;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base;

public class LambdaMemory : ILambdaMemory
{
    private readonly Dictionary<string, object> _objects = new();

    public void Add(string name, object value)
    {
        _objects[name] = value;
    }

    public object Get(string name)
    {
        return _objects[name];
    }

    public IReadOnlyDictionary<string, object> AsDictionary()
    {
        return _objects;
    }
}