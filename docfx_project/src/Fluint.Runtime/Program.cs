//
// Program.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Runtime
{
    internal static class Program
    {
        private static readonly Fluint Fluint = new();

        private static void Main(string[] args)
        {
            Fluint.Start();
        }
    }
}