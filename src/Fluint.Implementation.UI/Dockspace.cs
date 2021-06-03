//
// Dockspace.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fluint.Layer.UI;

namespace Fluint.Implementation.UI
{
    public class Dockspace : IDockspace
    {
        public string Name { get; }
        public ICollection<IGuiComponent> Children { get; }

        public void Begin(string name)
        {
            throw new NotImplementedException();
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }
    }
}
