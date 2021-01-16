using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Dock.Model;
using Fluint.Avalonia.ViewModels;
using Fluint.Layer.DependencyInjection;

namespace Fluint.Avalonia
{
    public class ViewLocator : IDataTemplate
    {
        private readonly ModulePacket _packet;
        public ViewLocator(ModulePacket packet)
        {
            _packet = packet;
        }

        public bool SupportsRecycling => false;

        public IControl Build(object data)
        {
            var name = data.GetType().FullName.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                return (IControl)_packet.CreateInstance(type);
            }
            else
            {
                return new TextBlock { Text = "Not Found: " + name };
            }
        }
        public bool Match(object data)
        {
            return data is ViewModelBase || data is IDockable;
        }
    }
}