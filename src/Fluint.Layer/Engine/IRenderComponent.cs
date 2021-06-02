using Fluint.Layer.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Engine
{
    /// <summary>
    /// Contains data to be able to render stuff.
    /// </summary>
    public interface IRenderComponent
    {
        /// <summary>
        /// This functions is used to render the component.
        /// please don't call outside the renderer....
        /// </summary>
        IRenderable3D<PositionNormalUVTIDVertex> Load();

        /// <summary>
        /// the defualt material for the component.
        /// </summary>
        Material Material { get; set; }
    }
}
