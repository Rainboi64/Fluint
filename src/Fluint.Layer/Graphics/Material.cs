//
// Material.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Graphics.API;
using Fluint.Layer.Mathematics;

namespace Fluint.Layer.Graphics
{
    /// <summary>
    /// Contains Settings and data needed for drawing the object correctly.
    /// </summary>
    public class Material
    {
        /// <summary>
        /// The diffuse texture of the material
        /// </summary>
        public ITexture Diffuse
        {
            get;
            set;
        }

        /// <summary>
        /// The specular texture of the material
        /// </summary>
        public ITexture Specular
        {
            get;
            set;
        }

        /// <summary>
        /// The ambient texture of the material.
        /// </summary>
        public ITexture Ambient
        {
            get;
            set;
        }

        /// <summary>
        /// The emmissive texture of the material.
        /// </summary>
        public ITexture Emissive
        {
            get;
            set;
        }

        /// <summary>
        /// The normal map of the material.
        /// </summary>
        public ITexture NormalMap
        {
            get;
            set;
        }

        /// <summary>
        /// The Opacity map of the material.
        /// </summary>
        public ITexture OpacityMap
        {
            get;
            set;
        }

        /// <summary>
        /// The displacment map for the material.
        /// </summary>
        public ITexture DisplacementMap
        {
            get;
            set;
        }

        /// <summary>
        /// The shading mode of the material.
        /// is either shaded smooth or flat, and is flat by default
        /// </summary>
        public ShadingMode ShadingMode
        {
            get;
            set;
        } = ShadingMode.Flat;

        /// <summary>
        /// if true the object will be displayed in a wireframe manner.
        /// </summary>
        public bool IsWireframe
        {
            get;
            set;
        } = false;

        /// <summary>
        /// Scales bump map, is one by defualt ie. no scaling done.
        /// </summary>
        public float BumpScaling
        {
            get;
            set;
        } = 1;

        /// <summary>
        /// The opacity of the overall object. is one by default ie. completely opaque.
        /// </summary>
        public float Opacity
        {
            get;
            set;
        } = 1;

        /// <summary>
        /// Decides the overall reflectivity of the object, is overriden by the Reflection map if existant.
        /// </summary>
        public float Reflectivity
        {
            get;
            set;
        }

        /// <summary>
        /// Decides the overall shininess of the object.
        /// </summary>
        public float Shininess
        {
            get;
            set;
        }

        /// <summary>
        /// The diffuse color of the object, is overriden by the diffuse texture if existant.
        /// </summary>
        public Color DiffuseColor
        {
            get;
            set;
        }

        /// <summary>
        /// The ambient color of the object, is overriden by the ambient texture if existant.
        /// </summary>
        public Color AmbientColor
        {
            get;
            set;
        }

        /// <summary>
        /// The specular color of the object, is overriden by the specular texture if existant.
        /// </summary>
        public Color SpecularColor
        {
            get;
            set;
        }

        /// <summary>
        /// The emmisive color of the object, is overriden by the emmisive texture if existant.
        /// </summary>
        public Color EmissiveColor
        {
            get;
            set;
        }
    }
}