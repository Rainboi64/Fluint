//
// DynamicModuleLambdaCompiler.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace Fluint.Layer.Miscellaneous;

public static class DynamicModuleLambdaCompiler
{
    // this is extremely cursed.
    // https://devblogs.microsoft.com/premier-developer/dissecting-the-new-constraint-in-c-a-perfect-example-of-a-leaky-abstraction/
    public static Func<T> GenerateFactory<T>() where T : new()
    {
        Expression<Func<T>> expr = () => new T();
        var newExpr = (NewExpression)expr.Body;

        var method = new DynamicMethod(
            "lambda",
            newExpr.Type,
            new Type[0],
            typeof(DynamicModuleLambdaCompiler).Module,
            true);

        var ilGen = method.GetILGenerator();
        // Constructor for value types could be null
        if (newExpr.Constructor != null)
        {
            ilGen.Emit(OpCodes.Newobj, newExpr.Constructor);
        }
        else
        {
            var temp = ilGen.DeclareLocal(newExpr.Type);
            ilGen.Emit(OpCodes.Ldloca, temp);
            ilGen.Emit(OpCodes.Initobj, newExpr.Type);
            ilGen.Emit(OpCodes.Ldloc, temp);
        }

        ilGen.Emit(OpCodes.Ret);

        return (Func<T>)method.CreateDelegate(typeof(Func<T>));
    }

    public static Func<T> GenerateFactory<T>(params object[] parameters) where T : new()
    {
        Expression<Func<T>> expr = () => new T();
        var newExpr = (NewExpression)expr.Body;

        var method = new DynamicMethod(
            "lambda",
            newExpr.Type,
            new Type[parameters.Length],
            typeof(DynamicModuleLambdaCompiler).Module,
            true);

        var ilGen = method.GetILGenerator();
        // Constructor for value types could be null
        if (newExpr.Constructor != null)
        {
            ilGen.Emit(OpCodes.Newobj, newExpr.Constructor);
        }
        else
        {
            var temp = ilGen.DeclareLocal(newExpr.Type);
            ilGen.Emit(OpCodes.Ldloca, temp);
            ilGen.Emit(OpCodes.Initobj, newExpr.Type);
            ilGen.Emit(OpCodes.Ldloc, temp);
        }

        ilGen.Emit(OpCodes.Ret);

        return (Func<T>)method.CreateDelegate(typeof(Func<T>));
    }
}