//
// MouseButton.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Input
{
    /// <summary>
    ///     Specifies the buttons of a mouse.
    /// </summary>
    public enum MouseButton
    {
        /// <summary>
        ///     The first button.
        /// </summary>
        Button1 = 0,

        /// <summary>
        ///     The second button.
        /// </summary>
        Button2 = 1,

        /// <summary>
        ///     The third button.
        /// </summary>
        Button3 = 2,

        /// <summary>
        ///     The fourth button.
        /// </summary>
        Button4 = 3,

        /// <summary>
        ///     The fifth button.
        /// </summary>
        Button5 = 4,

        /// <summary>
        ///     The sixth button.
        /// </summary>
        Button6 = 5,

        /// <summary>
        ///     The seventh button.
        /// </summary>
        Button7 = 6,

        /// <summary>
        ///     The eighth button.
        /// </summary>
        Button8 = 7,

        /// <summary>
        ///     The ninth button.
        /// </summary>
        Button9 = 8,

        /// <summary>
        ///     The left mouse button. This corresponds to <see cref="Button1"/>.
        /// </summary>
        Left = Button1,

        /// <summary>
        ///     The right mouse button. This corresponds to <see cref="Button2"/>.
        /// </summary>
        Right = Button2,

        /// <summary>
        ///     The middle mouse button. This corresponds to <see cref="Button3"/>.
        /// </summary>
        Middle = Button3,

        /// <summary>
        ///     The highest mouse button available.
        /// </summary>
        Last = Button8,
    }
}
