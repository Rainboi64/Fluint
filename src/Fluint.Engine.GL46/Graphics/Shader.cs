//
// Shader.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;
using OpenTK.Graphics.OpenGL4;
using ShaderType = OpenTK.Graphics.OpenGL4.ShaderType;

namespace Fluint.Engine.GL46.Graphics
{
    public partial class Shader : IShader
    {
        private readonly Dictionary<string, int> _nameLocationCache;

        private int _handle;

        public Shader()
        {
            _nameLocationCache = new Dictionary<string, int>();
        }

        public Shader(string vertexPath, string fragmentPath)
        {
            _nameLocationCache = new Dictionary<string, int>();

            string vertexShaderSource;

            // Read shaders from file.

            using (var reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                vertexShaderSource = reader.ReadToEnd();
            }

            string fragmentShaderSource;

            using (var reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                fragmentShaderSource = reader.ReadToEnd();
            }

            LoadSource(vertexShaderSource, fragmentShaderSource);
        }

        public void Enable()
        {
            GL.UseProgram(_handle);
        }

        public void Disable()
        {   
            GL.UseProgram(0);
        }

        // last implementation was cursed. I seroiusly don't know what the fuccc
        public int GetUniformLocation(string Name)
        {
            // If address isn't in cache. add it and return.
            if (!_nameLocationCache.ContainsKey(Name))
            {
                var value = GL.GetUniformLocation(_handle, Name);
                _nameLocationCache.Add(Name, value);
                return value;
            } 
            // otherwise just return cached
            return _nameLocationCache[Name];
        }

        public void LoadSource(string vertexShaderSource, string pixelShaderSource)
        {
            //Create shaders

            var vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);

            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, pixelShaderSource);

            GL.CompileShader(vertexShader);

            //error checking
            var shaderInfoLogVertex = GL.GetShaderInfoLog(vertexShader);
            if (shaderInfoLogVertex != string.Empty)
                Console.WriteLine($"SHADER COMPILATION ERROR:{shaderInfoLogVertex}");

            GL.CompileShader(fragmentShader);
            //error checking
            var shaderInfoLogFrag = GL.GetShaderInfoLog(fragmentShader);
            if (shaderInfoLogFrag != string.Empty)
                Console.WriteLine($"SHADER COMPILATION ERROR:{shaderInfoLogFrag}");

            _handle = GL.CreateProgram();

            GL.AttachShader(_handle, vertexShader);
            GL.AttachShader(_handle, fragmentShader);

            GL.LinkProgram(_handle);

            GL.DetachShader(_handle, vertexShader);
            GL.DetachShader(_handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);
        }

        public void Uniform1(string Name, int a)
        {
            GL.Uniform1(GetUniformLocation(Name), a);
        }

        public void Uniform1(string Name, float a)
        {
            GL.Uniform1(GetUniformLocation(Name), a);
        }

        public void Uniform1(string Name, double a)
        {
            GL.Uniform1(GetUniformLocation(Name), a);
        }

        public void Uniform2(string name, double a, double b)
        {
            GL.Uniform2(GetUniformLocation(name), a, b);
        }

        public void Uniform2(string name, int a, int b)
        {
            GL.Uniform2(GetUniformLocation(name), a, b);
        }

        public void Uniform2(string name, float a, float b)
        {
            GL.Uniform2(GetUniformLocation(name), a, b);
        }

        public void Uniform2(string name, Vector2 a)
        {
            GL.Uniform2(GetUniformLocation(name), OpenTKHelper.Vector2(a));
        }

        public void Uniform3(string name, int a, int b, int c)
        {
            GL.Uniform3(GetUniformLocation(name), a, b, c);
        }

        public void Uniform3(string name, double a, double b, double c)
        {
            GL.Uniform3(GetUniformLocation(name), a, b, c);
        }

        public void Uniform3(string name, float a, float b, float c)
        {
            GL.Uniform3(GetUniformLocation(name), a, b, c);
        }

        public void Uniform3(string name, Vector3 a)
        {
            GL.Uniform3(GetUniformLocation(name), OpenTKHelper.Vector3(a));
        }

        public void Uniform4(string name, int a, int b, int c, int d)
        {
            GL.Uniform4(GetUniformLocation(name), a, b, c, d);
        }

        public void Uniform4(string name, double a, double b, double c, double d)
        {
            GL.Uniform4(GetUniformLocation(name), a, b, c, d);
        }

        public void Uniform4(string name, float a, float b, float c, float d)
        {
            GL.Uniform4(GetUniformLocation(name), a, b, c, d);
        }

        public void Uniform4(string name, Vector4 a)
        {
            GL.Uniform4(GetUniformLocation(name), OpenTKHelper.Vector4(a));
        }

        public void UniformMat3(string Name, Matrix3x3 a, bool Transpose = true)
        {
            var matrix = OpenTKHelper.Matrix3(a);
            GL.UniformMatrix3(GetUniformLocation(Name), Transpose, ref matrix);
        }

        public void UniformMat4(string Name, Matrix a, bool Transpose = true)
        {
            var matrix = OpenTKHelper.Matrix4(a);
            GL.UniformMatrix4(GetUniformLocation(Name), Transpose, ref matrix);
        }

        public void SetModelMatrix(Matrix matrix)
        {
            UniformMat4("ml_matrix", matrix);
        }

        public void SetViewMatrix(Matrix matrix)
        {
            UniformMat4("vw_matrix", matrix);
        }

        public void SetProjectionMatrix(Matrix matrix)
        {
            UniformMat4("pn_matrix", matrix);
        }

        public void LoadPacket(ShaderPacket packet)
        {
            // Here's a reference https://www.lighthouse3d.com/tutorials/glsl-tutorial/uniform-variables/
            var length = packet.Count;

            // we use this in glActiveTexture, and is incremented when new textures are added.
            // value starts at 33984 || 0x84C0 and increments by 1.
            var textureCount = 33984;

            for (int i = 0; i < length; i++)
            {
                var current = packet[i];
                switch (current.Type)
                {
                    case ShaderObjectType.Texture:
                        var value = (ITexture)current.Value;
                        Uniform1($"{packet.Tag}.{current.Tag}", textureCount);
                        GL.ActiveTexture((TextureUnit)textureCount);
                        value.Bind();

                        textureCount++;
                        break;
                    case ShaderObjectType.Double:
                        Uniform1($"{packet.Tag}.{current.Tag}", (double)current.Value);
                        break;
                    case ShaderObjectType.Float:
                        Uniform1($"{packet.Tag}.{current.Tag}", (float)current.Value);
                        break;
                    case ShaderObjectType.Int:
                        Uniform1($"{packet.Tag}.{current.Tag}", (int)current.Value);
                        break;
                    case ShaderObjectType.Vector2:
                        Uniform2($"{packet.Tag}.{current.Tag}", (Vector2)current.Value);
                        break;
                    case ShaderObjectType.Vector3:
                        Uniform3($"{packet.Tag}.{current.Tag}", (Vector3)current.Value);
                        break;
                    case ShaderObjectType.Vector4:
                        Uniform4($"{packet.Tag}.{current.Tag}", (Vector4)current.Value);
                        break;
                    default:
                        throw new NotImplementedException($"type {current.Type} is not implemented in this module");
                }
            }
        }

        // Dispose Code
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            GL.DeleteProgram(_handle);
            _disposedValue = true;
        }

        public void Dispose()
        {
            GL.DeleteProgram(_handle);

            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
