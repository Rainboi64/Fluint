//
// About.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Reflection;
using Fluint.Layer;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Commands;

[Module("About Command", "Returns info about the loaded assembly.",
    "Returns info about the loaded assembly, doesn't support arguments")]
public class About : ILambda
{
    public string Command => "about";

    public LambdaObject Run(string[] args)
    {
        return new LambdaObject($"Working Assembly: {Assembly.GetExecutingAssembly().GetName()}");
    }
}