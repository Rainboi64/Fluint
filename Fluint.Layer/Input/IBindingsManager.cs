//
// BindingsManager.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//

namespace Fluint.Layer.Input
{
    /// <summary>
    /// an interface for managing keybinds.
    /// </summary>
    [Initialization(InitializationMethod.Scoped)]
    public interface IBindingsManager : IModule
    {
        bool IsBindingPressed(string bindName);
        bool IsBindingBound(string bindName);
        void SetBinding(string bindName, Key bindValue);
    }
}
