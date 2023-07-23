//
// ConsoleTableOptions.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.IO;

namespace Fluint.Layer.Miscellaneous;

public class ConsoleTableOptions
{
    public IEnumerable<string> Columns
    {
        get;
        set;
    } = new List<string>();

    public bool EnableCount
    {
        get;
        set;
    } = true;

    /// <summary>
    ///     Enable only from a list of objects
    /// </summary>
    public Alignment NumberAlignment
    {
        get;
        set;
    } = Alignment.Left;

    /// <summary>
    ///     The <see cref="TextWriter" /> to write to. Defaults to <see cref="Console.Out" />.
    /// </summary>
    public TextWriter OutputTo
    {
        get;
        set;
    } = Console.Out;
}