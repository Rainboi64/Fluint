//
// IInputManager.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//

using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;
using System;

namespace Fluint.Layer.Input
{
    /// <summary>
    /// An Interface for managing the input.
    /// </summary>
    [Initialization(InitializationMethod.Scoped)]
    public interface IInputManager : IModule
    {
        void Load(IBindingContext bindingContext);
        InputState State(Key key);

        event Action<InputState, Key> Keyboard;

        event Action<InputState, MouseButton> MouseButton;        

        /// <summary>
        /// Gets the location of the mouse.
        /// </summary>
        /// <returns>the location of the mouse.</returns>
        Point GetMouseLocation { get; set; }
    }
}   
