// 
// DOMRenderer.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Diagnostics;
using Fluint.Layer.UI;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Fluint.UI.Base;

public class XmlContainerNode : IXmlNode
{
    private static readonly ScriptOptions CompileOptions = ScriptOptions.Default
        .WithReferences(typeof(string).Assembly, typeof(Thread).Assembly, typeof(IGuiComponent).Assembly)
        .WithImports("System")
        .WithImports("System.Threading")
        .WithImports("Fluint.Layer")
        .WithImports("Fluint.Layer.Mathematics");
    
    public XmlContainerNode(ModulePacket packet)
    {
        _packet = packet;
        _logger = _packet.GetSingleton<ILogger>();
        _profiler = _packet.CreateScoped<IFrameProfiler>();
    }
    
    private readonly ILogger _logger;
    private readonly ModulePacket _packet;
    private readonly IFrameProfiler _profiler;
    private IList<IGuiComponent> _children = new List<IGuiComponent>();
    

    public string Name
    {
        get;
        private set;
    }

    public void Begin(string name)
    {
        Name = name;
        try
        {
            _profiler.Begin("xml_build");

            _children = new List<IGuiComponent>();

            var doc = new XmlDocument();
            doc.LoadXml(Xml);

            for (var i = 0; i < doc.ChildNodes.Count; i++)
            {
                var node = doc.ChildNodes[i];
                if (node is null)
                {
                    continue;
                }

                if (node.Name != "layout")
                {
                    continue;
                }

                if (node.Attributes is not null)
                {
                    for (var j = 0; j < node.Attributes.Count; j++)
                    {
                        var attribute = node.Attributes[j];
                        if (string.Equals(attribute.Name, "live", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Live = bool.Parse(attribute.InnerText);
                        }
                    }
                }

                for (var j = 0; j < node.ChildNodes.Count; j++)
                {
                    var child = ParseXml(node.ChildNodes[j]);
                    _children.Add(child);
                    child.Begin($"{name}.{j}");
                }
            }
            _profiler.End("xml_build");
        }
        catch (XmlException exception)
        {
            _logger.Error("[XMLContainerNode:{0}] : {1}", name, exception.Message);
        }
        catch (Exception exception)
        {
            _logger.Error("[XMLContainerNode:{0}] : {1}", name, exception);
        }
        finally
        {
            _profiler.End("xml_build");
            _logger.Information("build took {0}", _profiler.GetDetails()["xml_build"]);
        }
    }

    public void Tick()
    {
        foreach (var child in _children)
        {
            child.Tick();
        }
    }

    private IGuiComponent ParseXml(XmlNode node)
    {
        IGuiComponent component;

        if (node is null)
        {
            return null;
        }
        
        switch (node.Name)
        {
            case "container":
                component = _packet.CreateScoped<IContainer>(); 
                break;
            case "button":
                var button = _packet.CreateScoped<IButton>();
                button.Text = node.InnerText;
                component = button;
                break;
            case "imageButton":
                var imageButton = _packet.CreateScoped<IImageButton>();
                imageButton.Text = node.InnerText;
                component = imageButton;
                break;
            case "label":
                var label = _packet.CreateScoped<ITextLabel>();
                label.Text = node.InnerText;
                component = label;
                break;
            case "image":
                var image = _packet.CreateScoped<IImage>();
                image.Path = node.InnerText;
                component = image;
                break;
            case "textbox":
                var textbox = _packet.CreateScoped<ITextBox>();
                textbox.SideText = node.InnerText;
                component = textbox;
                break;
            case "demo":
                var demo = _packet.CreateScoped<IDemo>();
                component = demo;
                break;
            case "list":
                var list = _packet.CreateScoped<IListbox>();
                component = list;
                break;
            case "tree":
                var tree = _packet.CreateScoped<ITreeView>();
                component = tree;
                break;
            case "node":
                var treeNode = _packet.CreateScoped<ITreeNode>();
                treeNode.Text = node.InnerText;
                component = treeNode;
                break;
                
            default:
                throw new UIXmlException("unknown component tag");
        }

        var type = component.GetType();
        var properties = type.GetProperties();

        if (node.Attributes is not null)
        {
            for (var i = 0; i < node.Attributes.Count; i++)
            {
                var attribute = node.Attributes[i];
                foreach (var property in properties)
                {
                    if (!string.Equals(attribute.Name, property.Name, StringComparison.CurrentCultureIgnoreCase))
                    {
                        continue;
                    }

                    var script = attribute.InnerText;
                    if (script.StartsWith("$"))
                    {
                        var state = CSharpScript.RunAsync(script.Substring(1), CompileOptions, component, component.GetType()).Result;
                        property.SetValue(component, state.ReturnValue);
                    }
                    else
                    {
                        property.SetValue(component, script);
                    }

                }
            }
            
        }
        
        if (component is not IGuiContainer<IGuiComponent> container)
        {
            return component;
        }
        
        for (var i = 0; i < node.ChildNodes.Count; i++)
        {
            var child = node.ChildNodes[i];
            if (child is null)
            {
                continue;
            }

            var childComponent = ParseXml(child);
            container.Add($"{Name}.{node.BaseURI}.{i}", childComponent);
        }
        container.Begin($"{Name}.{node.BaseURI}");
        return container;
    }

    public string Xml
    {
        get;
        set;
    }

    public bool Live
    {
        get;
        private set;
    }
}