using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Data;
using Dock.Avalonia.Controls;
using Dock.Model;
using Dock.Model.Controls;
using Fluint.Avalonia.Models.Documents;
using Fluint.Avalonia.ViewModels;

namespace Fluint.Avalonia
{
    public class MainDockFactory : Factory
    {
        private object _context;

        public MainDockFactory(object context)
        {
            _context = context;
        }

        public override IDock CreateLayout()
        {
            var document1 = new RendererDocumentViewModel
            {
                Id = "Document1",
                Title = "Document1"
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
                        ActiveDockable = document1,
                        VisibleDockables = CreateList<IDockable>
                        (
                            document1
                        )
                    },
                    new DocumentDock
                    {
                        Id = "DocumentsPane",
                        Title = "DocumentsPane",
                        Proportion = double.NaN,
                        ActiveDockable = document1,
                        VisibleDockables = CreateList<IDockable>
                        (
                            document1
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
        public override void InitLayout(IDockable layout)
        {
            this.ContextLocator = new Dictionary<string, Func<object>>
            {
                [nameof(IRootDock)] = () => _context,
                [nameof(IProportionalDock)] = () => _context,
                [nameof(IDocumentDock)] = () => _context,
                [nameof(IToolDock)] = () => _context,
                [nameof(ISplitterDock)] = () => _context,
                [nameof(IDockWindow)] = () => _context,
                [nameof(IDocument)] = () => _context,
                [nameof(ITool)] = () => _context,
                ["Document1"] = () => new RendererDocument(),
                ["LeftPane"] = () => _context,
                ["LeftPaneTop"] = () => _context,
                ["LeftPaneTopSplitter"] = () => _context,
                ["LeftPaneBottom"] = () => _context,
                ["RightPane"] = () => _context,
                ["RightPaneTop"] = () => _context,
                ["RightPaneTopSplitter"] = () => _context,
                ["RightPaneBottom"] = () => _context,
                ["DocumentsPane"] = () => _context,
                ["MainLayout"] = () => _context,
                ["LeftSplitter"] = () => _context,
                ["RightSplitter"] = () => _context,
                ["MainLayout"] = () => _context,
                ["Main"] = () => _context,
            };

            this.HostWindowLocator = new Dictionary<string, Func<IHostWindow>>
            {
                [nameof(IDockWindow)] = () =>
                {
                    var hostWindow = new HostWindow()
                    {
                        [!HostWindow.TitleProperty] = new Binding("ActiveDockable.Title")
                    };
                    return hostWindow;
                }
            };

            this.DockableLocator = new Dictionary<string, Func<IDockable>>
            {
            };

            base.InitLayout(layout);
        }
    }

}
