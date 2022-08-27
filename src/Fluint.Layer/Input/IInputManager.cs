//
// IInputManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Mathematics;
using Fluint.Layer.Windowing;

namespace Fluint.Layer.Input
{
    /// <summary>
    /// An Interface for managing the input.
    /// </summary>
    [Initialization(InitializationMethod.Scoped)]
    public interface IInputManager : IModule
    {
        Vector2 MouseScroll
        {
            get;
        }

        Vector2 MouseScrollDelta
        {
            get;
        }

        /// <summary>
        /// Gets the location of the mouse.
        /// </summary>
        /// <returns>the location of the mouse.</returns>
        Vector2 MouseLocation
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        Vector2 LastMouseLocation
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        Vector2 MouseMovementDelta
        {
            get;
        }

        void Load(IWindowProvider bindingContext);
        InputState State(Key key);
        InputState State(MouseButton button);

        bool IsKeyPressed(Key key);
        bool IsKeyReleased(Key key);
        bool WasKeyPressed(Key key);

        bool IsMouseButtonPressed(MouseButton button);
        bool IsMouseButtonReleased(MouseButton button);
        bool WasMouseButtonPressed(MouseButton button);
    }
}