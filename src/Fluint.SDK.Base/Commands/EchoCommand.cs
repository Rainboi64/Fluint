// 
// EchoCommand.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;
using Fluint.Layer.SDK;

namespace Fluint.SDK.Base.Commands;

public class EchoCommand : ICommand
{
    public string Command => "echo";

    public void Do(string[] args)
    {
        foreach (var item in args)
        {
            Console.WriteLine(item);
        }
    }
}