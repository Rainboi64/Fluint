using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Engine.GL46
{
    /// <summary>
    /// Converts some math stuff.
    /// </summary>
    internal class OpenTKHelper
    {
        public static unsafe OpenTK.Mathematics.Matrix4 Matrix4(Layer.Mathematics.Matrix matrix)
        {
            return *(OpenTK.Mathematics.Matrix4*)&matrix;
        }
        public static unsafe OpenTK.Mathematics.Matrix3 Matrix3(Layer.Mathematics.Matrix3x3 matrix)
        {
            return *(OpenTK.Mathematics.Matrix3*)&matrix;
        }
        public static unsafe OpenTK.Mathematics.Vector3 Vector3(Layer.Mathematics.Vector3 vector3)
        {
            return *(OpenTK.Mathematics.Vector3*)&vector3;
        }
        public static unsafe OpenTK.Mathematics.Vector4 Vector4(Layer.Mathematics.Vector4 vector4)
        {
            return *(OpenTK.Mathematics.Vector4*)&vector4;
        }
        public static unsafe OpenTK.Mathematics.Vector2 Vector2(Layer.Mathematics.Vector2 vector2)
        {
            return *(OpenTK.Mathematics.Vector2*)&vector2;
        }
    }
}
