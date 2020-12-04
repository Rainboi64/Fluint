//
// IInputManager.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//

using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Input
{
    /// <summary>
    /// An Interface for managing the input.
    /// </summary>
    [Initialization(InitializationMethod.Scoped)]
    public interface IInputManager : IModule
    {
        /// <summary>
        /// Returns the if the specified key is pressed. 
        /// </summary>
        /// <param name="key">The key to test for.</param>
        /// <returns>true if the key is pressed, otherwise else.</returns>
        bool IsKeyDown(Key key);

        /// <summary>
        /// Returns the if the specified key is pressed. 
        /// </summary>
        /// <param name="key">The key to test for.</param>
        /// <returns>true if the key is pressed, otherwise else.</returns>
        bool IsButtonDown(MouseButton key);

        /// <summary>
        /// Gets the location of the mouse.
        /// </summary>
        /// <returns>the location of the mouse.</returns>
        Point GetMouseLocation();
    }
}
