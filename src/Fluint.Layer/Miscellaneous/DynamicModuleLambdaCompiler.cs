//
// DynamicModuleLambdaCompiler.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Text;

namespace Fluint.Layer.Miscellaneous
{
    public static class DynamicModuleLambdaCompiler
    {
        // this is extremely cursed.
        // https://devblogs.microsoft.com/premier-developer/dissecting-the-new-constraint-in-c-a-perfect-example-of-a-leaky-abstraction/
        public static Func<T> GenerateFactory<T>() where T : new()
        {
            Expression<Func<T>> expr = () => new T();
            NewExpression newExpr = (NewExpression)expr.Body;

            var method = new DynamicMethod(
                name: "lambda",
                returnType: newExpr.Type,
                parameterTypes: new Type[0],
                m: typeof(DynamicModuleLambdaCompiler).Module,
                skipVisibility: true);

            ILGenerator ilGen = method.GetILGenerator();
            // Constructor for value types could be null
            if (newExpr.Constructor != null)
            {
                ilGen.Emit(OpCodes.Newobj, newExpr.Constructor);
            }
            else
            {
                LocalBuilder temp = ilGen.DeclareLocal(newExpr.Type);
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
            NewExpression newExpr = (NewExpression)expr.Body;

            var method = new DynamicMethod(
                name: "lambda",
                returnType: newExpr.Type,
                parameterTypes: new Type[parameters.Length],
                m: typeof(DynamicModuleLambdaCompiler).Module,
                skipVisibility: true);

            ILGenerator ilGen = method.GetILGenerator();
            // Constructor for value types could be null
            if (newExpr.Constructor != null)
            {
                ilGen.Emit(OpCodes.Newobj, newExpr.Constructor);
            }
            else
            {
                LocalBuilder temp = ilGen.DeclareLocal(newExpr.Type);
                ilGen.Emit(OpCodes.Ldloca, temp);
                ilGen.Emit(OpCodes.Initobj, newExpr.Type);
                ilGen.Emit(OpCodes.Ldloc, temp);
            }

            ilGen.Emit(OpCodes.Ret);

            return (Func<T>)method.CreateDelegate(typeof(Func<T>));
        }
    }
}
