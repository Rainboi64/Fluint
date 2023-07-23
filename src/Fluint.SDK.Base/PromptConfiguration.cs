// 
// PromptConfiguration.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using Fluint.Layer.Configuration;

namespace Fluint.SDK.Base;

public class PromptConfiguration : IConfiguration
{
    public string DefaultPrompt
    {
        get;
        set;
    } = "[magenta]λ[/magenta] ";

    public string Prompt
    {
        get;
        set;
    } = "[{0}]λ[/{0}] [took [yellow]{1}s[/yellow]] ";
}