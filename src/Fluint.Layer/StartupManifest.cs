//
// StartupManifest.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer
{
    public readonly record struct StartupManifest(string[] CommandLineArguments, string ModuleManifest,
        string VersionDetails);
}