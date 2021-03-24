// -------------------------------------------------------------------------
// ShaderGenerator.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//
// Description:
// This is the implementation for the Shader Generator which is an object,
// that uses IShaderGenerationModule for generating custom shader code.
//
// References:
// 1. https://ep.liu.se/ecp/013/005/ecp01305.pdf
//
// Note: 
// Consider using an array for data locality.
//
// -------------------------------------------------------------------------

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Engine.GL46.Graphics
{
    public class ShaderGenerator : IShaderGenerator
    {
        private readonly ModulePacket _packet;
        private readonly List<IShaderGenerationModule> _localVertexModules = new();
        private readonly List<IShaderGenerationModule> _localFragmentModules = new();
        private bool _needsUpdate = true;
        private IShader _cached;

        public ShaderGenerator(ModulePacket packet)
        {
            _packet = packet;
        }

        public IShader Generate()
        {
            if (_needsUpdate)
            {
                _cached = CreateShader();
                _needsUpdate = false;
            }
            return _cached;
        }

        private IShader CreateShader()
        {
            var shader = _packet.GetScoped<IShader>();

            // Vertex Shader stuff.

            var vertexShaderSource = new StringBuilder();

            var header = @"";

            var vertexModulesLength = _localVertexModules.Count;
            for (var i = 0; i < vertexModulesLength; i++)
            {
                vertexShaderSource.Append(_localVertexModules[i].Generate());
            }

            // Fragment Shader stuff.

            var fragmentShaderSource = new StringBuilder();

            var fragmentModulesLength = _localFragmentModules.Count;
            for (var i = 0; i < fragmentModulesLength; i++)
            {
                fragmentShaderSource.Append(_localFragmentModules[i].Generate());
            }

            shader.LoadSource(vertexShaderSource.ToString(), fragmentShaderSource.ToString());

            return shader;
        }

        public void Add(IShaderGenerationModule module)
        {
            switch (module.Type)
            {
                case ShaderType.VertexShader:
                    _localVertexModules.Insert(GetVertexSortedIndex(module.Priority), module);
                    break;
                case ShaderType.PixelShader:
                    _localFragmentModules.Insert(GetFragmentSortedIndex(module.Priority), module);
                    break;
                default:
                    throw new NotImplementedException();
            }
            _needsUpdate = true;


        }

        // Scuffed Sorting Algos lmao

        private int GetVertexSortedIndex(int priority)
        {
            if (priority >= _localVertexModules.Count)
            {
                return _localVertexModules.Count;
            }
            else
            {
                for (var i = 0; i < _localVertexModules.Count; i++)
                {
                    if (i == priority)
                    {
                        return i;
                    }
                }
                return _localVertexModules.Count;
            }
        }

        private int GetFragmentSortedIndex(int priority)
        {
            if (priority >= _localFragmentModules.Count)
            {
                return _localFragmentModules.Count;
            }
            else
            {
                for (var i = 0; i < _localFragmentModules.Count; i++)
                {
                    if (i == priority)
                    {
                        return i;
                    }
                }
                return _localFragmentModules.Count;
            }
        }
    }
}
