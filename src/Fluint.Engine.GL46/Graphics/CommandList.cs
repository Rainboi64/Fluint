#define Target_Windows

using Fluint.Layer.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using Fluint.Engine.GL46.Graphics.Native;

namespace Fluint.Engine.GL46.Graphics
{
    public class CommandList : ICommandList
    {
        private List<Command> _commands = new List<Command>();
        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public void Add(Command item)
        {
            _commands.Add(item);        
        }

        public void Clear()
        {
            _commands.Clear();
        }

        public bool Contains(Command item)
        {
            return _commands.Contains(item);
        }

        public void CopyTo(Command[] array, int arrayIndex)
        {
            _commands.CopyTo(array, arrayIndex);
        }
        public void Flush()
        {
            foreach (var command in _commands)
            {
                switch (command.CommandIdentifier)
                {
 
                    default:
                        throw new NotImplementedException();
                }
            }
            _commands.Clear();
        }

        public IEnumerator<Command> GetEnumerator()
        {
            return _commands.GetEnumerator();
        }

        public bool Remove(Command item)
        {
            return _commands.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _commands.GetEnumerator();
        }
    }
}
