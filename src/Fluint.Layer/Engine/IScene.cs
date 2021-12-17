//
// IScene.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Engine
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IScene : IList<ISceneObject>, IModule
    {
    }
}
