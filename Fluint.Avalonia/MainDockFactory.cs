using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Data;
using Dock.Avalonia.Controls;
using Dock.Model;
using Dock.Model.Controls;
using Fluint.Avalonia.ViewModels;
using Fluint.Avalonia.Models;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics;
using Fluint.Avalonia.Views;
using Dock.Model.ReactiveUI;
using Dock.Model.Core;
using Dock.Model.ReactiveUI.Controls;

namespace Fluint.Avalonia
{
    public class MainDockFactory : Factory
    {
        private object _context;
        private ModulePacket _packet;

        public MainDockFactory(object context)
        {
            _context = context;
        }

        public MainDockFactory(object context, ModulePacket packet) : this(context)
        {
            _packet = packet;
        }

        public override IRootDock CreateLayout()
        {

            var a = new RendererDocumentViewModel(_packet)
            {
                Id = "Document1",
                Title = "Document1"
            };

            var b =  new RendererDocumentViewModel(_packet)
            {
                Id = "Document2",
                Title = "Document2"
            };

            var c = new RendererDocumentViewModel(_packet)
            {
                Id = "Document3",
                Title = "Document3"
            };


            var mainLayout = new ProportionalDock
            {
                Id = "MainLayout",
                Title = "MainLayout",
                Proportion = double.NaN,
                Orientation = Orientation.Horizontal,
                ActiveDockable = null,
                VisibleDockables = CreateList<IDockable>
                (
                    new DocumentDock
                    {
                        Id = "DocumentsPane",
                        Title = "DocumentsPane",
                        Proportion = double.NaN,
                        ActiveDockable = (IDockable)a,
                        VisibleDockables = CreateList<IDockable>
                        (
                            a, b, c
                        )
                    }
                )
            };

            var mainView = new MainViewModel
            {
                Id = "Main",
                Title = "Main",
                ActiveDockable = mainLayout,
                VisibleDockables = CreateList<IDockable>(mainLayout)
            };

            var root = CreateRootDock();

            root.Id = "Root";
            root.Title = "Root";
            root.ActiveDockable = mainView;
            root.DefaultDockable = mainView;
            root.VisibleDockables = CreateList<IDockable>(mainView);

            return root;
        }
    }
}
