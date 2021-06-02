using System;
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
        event Action<InputState, Binding> BindingStateUpdated;

        /// <summary>
        /// Initializes the bindings manager.
        /// </summary>
        /// <param name="inputManager">an input manager to use for checking input.</param>
        void Load(IInputManager inputManager);

        /// <summary>
        /// Gets the state of the binding.
        /// </summary>
        /// <param name="bindName">String name of binding, gets translated into binding later.</param>
        /// <returns>The state of keys in the binding.</returns>
        InputState GetState(string bindName);

        /// <summary>
        /// Gets the state of the binding.
        /// </summary>
        /// <param name="binding">The provided binding.</param>
        /// <returns>The state of provided binding.</returns>
        InputState GetState(Binding binding);

        /// <summary>
        /// Gets the binding provided it's string tag.
        /// </summary>
        /// <param name="bindName">The actual binding tag.</param>
        /// <returns></returns>
        Binding GetBinding(string bindName);

        /// <summary>
        /// Gets a list of all bindings bound.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Binding> GetBindings();

        /// <summary>
        /// Gets the state of the binding.
        /// </summary>
        /// <param name="binding">the actual binding</param>
        /// <returns>the input binding state</returns>
        InputState Get(Binding binding);

        /// <summary>
        /// Gets the state of the binding.
        /// </summary>
        /// <param name="binding">the string represting the binding's tag.</param>
        /// <returns>the input binding state</returns>
        InputState Get(string binding);

        /// <summary>
        /// Loads a binding collection with specified tag.
        /// </summary>
        /// <param name="collectionTag">a tag to get the collection by.</param>
        void LoadCollection(string collectionTag);

        /// <summary>
        /// Gets a binding collection with specified tag.
        /// </summary>
        /// <param name="tag">a tag to get the collection by.</param>
        /// <returns></returns>
        IEnumerable<Binding> GetCollection(string tag);

        /// <summary>
        /// Saves the current collection.
        /// </summary>
        /// <param name="tag">the tag to save collection by.</param>
        void SaveCurrentCollection(string tag);

        /// <summary>
        /// Loads a specific binding.
        /// </summary>
        /// <param name="binding">the singular binding to load.</param>
        void LoadBinding(Binding binding);

        /// <summary>
        /// Loads a collection of bindings.
        /// </summary>
        /// <param name="bindings">the collection of bindings to load.</param>
        void LoadBindings(IEnumerable<Binding> bindings);
    }
}
