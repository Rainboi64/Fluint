//
// IRenderComponent.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Graphics.API;

namespace Fluint.Layer.Graphics
{
    /// <summary>
    /// Contains data to be able to render stuff.
    /// </summary>
    public interface IRenderComponent
    {
        /// <summary>
        /// the defualt material for the component.
        /// </summary>
        Material Material
        {
            get;
            set;
        }

        /// <summary>
        /// This functions is used to render the component.
        /// please don't call outside the renderer....
        /// </summary>
        IRenderable3D<PositionNormalUvtidVertex> Load();
    }
}