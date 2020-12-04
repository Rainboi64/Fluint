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
    public interface IBindingsManager
    {

        /// <summary>
        /// Checks if the binding is pressed.
        /// </summary>
        /// <typeparam name="TBinding">The type of bindings to return</typeparam>
        /// <returns>returns if the binding is pressed.</returns>
        bool TestForBinding<TBinding>() where TBinding : IBinding;

        /// <summary>
        /// Gets the instance of the specified binding.
        /// </summary>
        /// <typeparam name="TBinding">The type the binding to return.</typeparam>
        /// <returns>returns the instance of specified binding.</returns>
        IBinding GetBinding<TBinding>() where TBinding : IBinding;

        /// <summary>
        /// Edits the instance of the specified type.
        /// </summary>
        /// <typeparam name="TBinding">The type of the instance.</typeparam>
        /// <param name="newBinding">The new instance.</param>
        void Edit<TBinding>(TBinding newBinding);

        /// <summary>
        /// Edits the instance of the item with specified type
        /// </summary>
        /// <param name="tag">The tag of the item to change</param>
        /// <param name="newBinding">The new edited instance.</param>
        void Edit(string tag, IBinding newBinding);


        void Add(IBinding item);
        void Save();
        void Load();
    }
}
