//
// IFontManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.UI;

[Initialization(InitializationMethod.Scoped)]
public interface IFontManager : IModule
{
    Font LoadFont(string fontName, float fontSize = 16.0f);
}