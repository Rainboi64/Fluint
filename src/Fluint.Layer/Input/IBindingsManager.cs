//
// IBindingsManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;

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
        IInputManager InputManager
        {
            get;
            set;
        }

        InputState GetState(string bindName);
        InputState GetState(Binding binding);
        Binding GetBinding(string bindName);
        IEnumerable<Binding> GetBindings();
        InputState Get(Binding binding);
        InputState Get(string binding);
        void LoadCollection(string collectionTag);
        IEnumerable<Binding> GetCollection(string tag);
        void SaveCurrentCollection(string tag);
        void LoadBinding(Binding binding);
        void LoadBindings(IEnumerable<Binding> bindings);
    }
}