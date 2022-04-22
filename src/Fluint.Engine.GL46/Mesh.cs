//
// Mesh.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;

namespace Fluint.Engine.GL46
{
    /// <summary>
    /// Me in the future please add caching manually
    /// </summary>
    public class Mesh
    {
        private readonly ModulePacket _packet;

        private IRenderable3D<PositionNormalUvtidVertex> _cachedRenderable3D;
        private uint[] _indexArray;
        private Material _material;
        private PositionNormalUvtidVertex[] _vertexArray;

        public Mesh(ModulePacket packet)
        {
            _packet = packet;
            _material = new Material();
            Cache();
        }

        public IEnumerable<PositionNormalUvtidVertex> VertexArray
        {
            get => _vertexArray;
            set
            {
                _vertexArray = (PositionNormalUvtidVertex[])value;
                Cache();
            }
        }

        public IEnumerable<uint> IndexArray
        {
            get => _indexArray;
            set
            {
                _indexArray = (uint[])value;
                Cache();
            }
        }

        public Material Material
        {
            get => _material;
            set
            {
                _material = value;
                Cache();
            }
        }

        private ShaderPacket CreatePacket(Material material)
        {
            // this is one thicc boi
            var packet = new List<ShaderObject> {
                new ShaderObject(ShaderObjectType.Texture, material.Diffuse, "diffuse_map"),
                new ShaderObject(ShaderObjectType.Texture, material.Specular, "specular_map"),
                new ShaderObject(ShaderObjectType.Texture, material.Ambient, "ambient_map"),
                new ShaderObject(ShaderObjectType.Texture, material.Emissive, "emissive_map"),
                new ShaderObject(ShaderObjectType.Texture, material.NormalMap, "normal_map"),
                new ShaderObject(ShaderObjectType.Texture, material.OpacityMap, "opacity_map"),
                new ShaderObject(ShaderObjectType.Texture, material.DisplacementMap, "displacement_map"),
                new ShaderObject(ShaderObjectType.Int, (int)material.ShadingMode, "smooth_shading"),
                new ShaderObject(ShaderObjectType.Float, material.BumpScaling, "bump_scaling"),
                new ShaderObject(ShaderObjectType.Float, material.Opacity, "opacity"),
                new ShaderObject(ShaderObjectType.Float, material.Reflectivity, "reflective"),
                new ShaderObject(ShaderObjectType.Float, material.Shininess, "shininess"),
                new ShaderObject(ShaderObjectType.Vector4, material.DiffuseColor, "diffuse"),
                new ShaderObject(ShaderObjectType.Vector4, material.AmbientColor, "ambient"),
                new ShaderObject(ShaderObjectType.Vector4, material.SpecularColor, "specular"),
                new ShaderObject(ShaderObjectType.Vector4, material.EmissiveColor, "emissive"),
            };

            // I think I might be considering doing a shaderpack manifest. 🤔

            return new ShaderPacket(packet, "material");
        }

        private void Cache()
        {
            _cachedRenderable3D = _packet.CreateScoped<IRenderable3D<PositionNormalUvtidVertex>>();
            _cachedRenderable3D.Packet = CreatePacket(_material);
            _cachedRenderable3D.Indices = _indexArray;
            _cachedRenderable3D.Vertices = _vertexArray;
            _cachedRenderable3D.ModelMatrix = Matrix.Identity;
        }

        public IRenderable3D<PositionNormalUvtidVertex> Load()
        {
            return _cachedRenderable3D;
        }
    }
}