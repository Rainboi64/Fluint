// 
// IXmlNode.cs
// 
// Copyright (C) 2023 Yaman Alhalabi

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface IXmlNode : IGuiComponent, IModule
{
    string Xml
    {
        set;
    }

    bool Live
    {
        get;
    }
}