// 
// SceneHierarchyControl.cs
// 
// Copyright (C) 2023 Yaman Alhalabi
//

using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.Editor;
using Fluint.Layer.EntityComponentSystem;
using Fluint.Layer.Localization;
using Fluint.Layer.UI;

namespace Fluint.Editor.Layout.Base.Controls;

public class SceneHierarchyControl : Control
{
    private readonly ITreeView _tree;
    private readonly IContainer _container;
    private readonly ModulePacket _packet;
    private readonly INodeSystem _system;

    public SceneHierarchyControl(ModulePacket packet)
    {
        _packet = packet;

        var localizationManager = packet.GetSingleton<ILocalizationManager>();

        _container = packet.CreateScoped<IContainer>();
        _container.Title = localizationManager.Fetch("scene_heirarchy");
        _container.ScrollBar = false;
        Children.Add(_container);

        _tree = packet.CreateScoped<ITreeView>();
        _container.Add("Tree", _tree);

        var world = packet.GetSingleton<IWorld>();
        _system = world.GetSystem<INodeSystem, INode>();        
    }

    private void Add(object sender, NodeAddedEventArgs args)
    {
        var item = _packet.CreateScoped<ITreeNode>();
        item.Text = args.Node.Name;
        _tree.Add(item.GetHashCode().ToString() + args.Node.Name, item);
    }

    public override void Tick()
    {
        _tree.Size = _container.Size;
        
        _tree.Clear();
        foreach(var node in _system.Nodes)
        {
            Add(this, new NodeAddedEventArgs(node));
        }

        base.Tick();
    }
}