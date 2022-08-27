// 
// ToolAttribute.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using System;

namespace Fluint.Layer.Editor.Tools;

public class ToolAttribute : Attribute
{
    public ToolAttribute(string displayName, string iconPath)
    {
        DisplayName = displayName;
        IconPath = iconPath;
    }

    public string DisplayName
    {
        get;
    }

    public string IconPath
    {
        get;
    }
}