// 
// LambdaError.cs
// 
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.SDK;

public class LambdaObject
{
    public readonly object Data;
    public readonly LambdaStatus Status = LambdaStatus.Success;

    public LambdaObject(object data, LambdaStatus status)
    {
        Data = data;
        Status = status;
    }

    public LambdaObject(LambdaStatus status)
    {
        Status = status;
    }


    public LambdaObject(object data)
    {
        Data = data;
    }

    public static LambdaObject Success => new(LambdaStatus.Success);
    public static LambdaObject Failure => new(LambdaStatus.Failure);
    public static LambdaObject Unknown => new(LambdaStatus.Unknown);

    public static LambdaObject Error(string message)
    {
        return new LambdaObject(message, LambdaStatus.Failure);
    }

    public override string ToString()
    {
        return $"[{Status}] : [{Data}]";
    }
}