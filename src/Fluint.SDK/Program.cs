//
// Program.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.SDK;

internal static class FluintSdkRuntime
{
    private static readonly Sdk Sdk = new();

    private static void Main(string[] args)
    {
        Sdk.Start();
    }
}