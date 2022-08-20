//
// Vector2i.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluint.Layer.Mathematics
{
    public struct Vector2i : IEquatable<Vector2i>
    {
        public int X;
        public int Y;

        public Vector2i(int value)
        {
            X = value;
            Y = value;
        }

        public Vector2i(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Vector2i other)
        {
            return other.X == X && other.Y == Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2i && Equals((Vector2i)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(Vector2i left, Vector2i right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2i left, Vector2i right)
        {
            return !(left == right);
        }
    }
}
