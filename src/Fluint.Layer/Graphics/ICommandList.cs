//
// ICommandList.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

/**
 * @author Yaman Alhalabi <yamanalhalabi2@gmail.com>
 * @file A class for proccessing graphics API related commands.
 * @desc Created on 2020-12-11 7:38:08 pm
 * @copyright Panic Factory (C) 2020
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Graphics
{
    [Initialization(InitializationMethod.Scoped)]
    public interface ICommandList : ICollection<Command>
    {
        void Flush();
    }
}
