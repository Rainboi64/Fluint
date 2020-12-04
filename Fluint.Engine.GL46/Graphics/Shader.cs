//
// Shader.cs
//
// Copyright (C) 2020 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Fluint.Layer.Debugging;
using Fluint.Layer.Graphics;
using Fluint.Layer.Mathematics;
using OpenTK.Graphics.OpenGL4;

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

            //Read shaders from file.

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

        public int GetUniformLocation(string Name)
        {
            if (!_nameLocationCache.ContainsKey(Name)) return GL.GetUniformLocation(_handle, Name);

            var value = _nameLocationCache[Name];
            _nameLocationCache.Add(Name, value);
            return value;
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


        #region Uniform1
        public void Uniform1(string Name, int A)
        {
            GL.Uniform1(GetUniformLocation(Name), A);
        }

        public void Uniform1(string Name, float A)
        {
            GL.Uniform1(GetUniformLocation(Name), A);
        }

        public void Uniform1(string Name, double A)
        {
            GL.Uniform1(GetUniformLocation(Name), A);
        }
        #endregion
        #region Uniform2
        public void Uniform2(string name, double a, double b)
        {
            GL.Uniform2(GetUniformLocation(name), a, b);
        }

        public void Uniform2(string name, int a, int b)
        {
            GL.Uniform2(GetUniformLocation(name), a, b);
        }

        public void Uniform2(string name, Vector2 a)
        {
            GL.Uniform2(GetUniformLocation(name), OpenTKHelper.Vector2(a));
        }
        #endregion
        #region Uniform3
        public void Uniform3(string name, int a, int b, int c)
        {
            GL.Uniform3(GetUniformLocation(name), a, b, c);
        }

        public void Uniform3(string name, double a, double b, double c)
        {
            GL.Uniform3(GetUniformLocation(name), a, b, c);
        }

        public void Uniform3(string name, Vector3 a)
        {
            GL.Uniform3(GetUniformLocation(name), OpenTKHelper.Vector3(a));
        }
        #endregion
        #region Uniform4
        public void Uniform4(string name, int a, int b, int c, int d)
        {
            GL.Uniform4(GetUniformLocation(name), a, b, c, d);
        }

        public void Uniform4(string name, double a, double b, double c, double d)
        {
            GL.Uniform4(GetUniformLocation(name), a, b, c, d);
        }

        public void Uniform4(string name, Vector4 a)
        {
            GL.Uniform4(GetUniformLocation(name), OpenTKHelper.Vector4(a));
        }
        #endregion

        public void UniformMat3(string Name, Matrix3x3 a, bool Transpose = true)
        {
            var matrix = OpenTKHelper.Matrix3(a);
            GL.UniformMatrix3(GetUniformLocation(Name), Transpose, ref matrix);
        }

        public void SetModelMatrix(Matrix matrix)
        {
            UniformMat4("ml_matrix", matrix);
        }

        public void UniformMat4(string Name, Matrix a, bool Transpose = true)
        {
            var matrix = OpenTKHelper.Matrix4(a);
            GL.UniformMatrix4(GetUniformLocation(Name), Transpose, ref matrix);
        }

        //Dispose Code
        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            GL.DeleteProgram(_handle);
            _disposedValue = true;
        }

        ~Shader()
        {
            GL.DeleteProgram(_handle);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
