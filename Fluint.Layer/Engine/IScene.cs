﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Engine
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IScene : ICollection<ISceneObject>, IModule
    {
    }
}