//
// Bindings.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//

namespace Fluint.Layer.Input
{
    [Initialization(InitializationMethod.Instanced)]
    public interface IBinding : IModule
    {
        string Tag { get; set; }
        Key MainKey { get; set; }
        Key SecondaryKey { get; set; }
    }
}
