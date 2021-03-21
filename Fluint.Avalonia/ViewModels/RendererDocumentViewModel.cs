using System;
using System.Collections.Generic;
using System.Text;
using Dock.Model.Controls;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics;
using Fluint.Layer.Input;
using Fluint.Layer.Tasks;

namespace Fluint.Avalonia.ViewModels
{
    class RendererDocumentViewModel : Document
    {
        private readonly ModulePacket _packet;
        public RendererDocumentViewModel(ModulePacket packet)
        {
            _packet = packet;
            BindingContext = packet.GetScoped<IBindingContext>();
            TaskManager = packet.GetScoped<ITaskManager>();
            BindingsManager = packet.GetScoped<IBindingsManager>();
        }

        public IBindingsManager BindingsManager { get; }
        public IBindingContext BindingContext { get; private set; }
        public ITaskManager TaskManager { get; private set; }

        public void Ready()
        {
            var inputManager = _packet.GetScoped<IInputManager>();
            inputManager.Load(BindingContext);
            BindingsManager.Load(inputManager);
            BindingsManager.BindingStateUpdated += BindingsManager_BindingStateUpdated;
        }

        private void BindingsManager_BindingStateUpdated(InputState state, Binding binding)
        {
            switch (binding.Tag)
            {
                case "SAVE_CONFIG":
                    if (state == InputState.Press)
                    {
                        BindingsManager.SaveCurrentCollection("default");
                    }
                    break;
            }
            Console.WriteLine($"Binding: {binding}, State: {state}");
        }

        public void Render()
        {

        }
    }
}
