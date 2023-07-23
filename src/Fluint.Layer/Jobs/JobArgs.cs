//
// JobArgs.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Jobs;

public class JobArgs
{
    public JobArgs()
    {
    }

    public JobArgs(object[] args)
    {
        Args = args;
    }

    public JobArgs(object invoker)
    {
        Invoker = invoker;
    }

    public JobArgs(object[] args, object invoker)
    {
        Args = args;
        Invoker = invoker;
    }

    public object Invoker
    {
        get;
    }

    public object[] Args
    {
        get;
    }
}