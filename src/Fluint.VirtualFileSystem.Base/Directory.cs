// 
// Directory.cs
// 
// Copyright (C) 2022 Yaman Alhalabi

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.VirtualFileSystem;

namespace Fluint.VirtualFileSystem.Base;

public class Directory : IDirectory
{
    private readonly Dictionary<string, IDirectory> _directories = new();
    private readonly Dictionary<string, IFile> _files = new();
    private readonly ModulePacket _packet;

    public Directory(ModulePacket packet)
    {
        _packet = packet;
    }

    public event EventHandler<DirectoryChangedEvent> OnChange;

    public bool TryGetFile(string name, out IFile file)
    {
        if (_files.ContainsKey(name))
        {
            file = _files[name];
            return true;
        }

        file = null;
        return false;
    }

    public bool TryGetDirectory(string name, out IDirectory directory)
    {
        if (_directories.ContainsKey(name))
        {
            directory = _directories[name];
            return true;
        }

        directory = null;
        return false;
    }

    public IFile GetFile(string name)
    {
        return _files[name];
    }

    public IDirectory GetDirectory(string name)
    {
        return _directories[name];
    }

    public bool HasFile(string name)
    {
        return _files.ContainsKey(name);
    }

    public bool HasDirectory(string name)
    {
        return _directories.ContainsKey(name);
    }

    public void AddDirectory(string name, IDirectory directory)
    {
        OnChange?.Invoke(this, new DirectoryChangedEvent());
        _directories.Add(name, directory);
    }

    public IDirectory AddDirectory(string name, string physicalPath)
    {
        OnChange?.Invoke(this, new DirectoryChangedEvent());
        var directory = _packet.CreateScoped<IDirectory>();
        directory.UpdateDirectory(physicalPath);
        AddDirectory(name, directory);

        return directory;
    }

    public void AddFile(string name, IFile file)
    {
        _files.Add(name, file);
    }

    public IFile AddFile(string name, string physicalPath)
    {
        var file = _packet.CreateScoped<IFile>();
        file.Update(physicalPath);
        _files[name] = file;

        return file;
    }

    public void UpdateDirectory(string physicalPath)
    {
        var dirs = System.IO.Directory.GetDirectories(physicalPath);
        var files = System.IO.Directory.GetFiles(physicalPath);

        foreach (var dir in dirs)
        {
            AddDirectory(Path.GetFileName(dir), dir);
        }

        foreach (var file in files)
        {
            AddFile(Path.GetFileName(file), file);
        }
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        foreach (var directory in _directories)
        {
            stringBuilder.Append($"- {directory.Key}");
            stringBuilder.AppendLine(directory.Value.ToString());
        }

        foreach (var file in _files)
        {
            stringBuilder.AppendLine(file.Key);
        }

        return stringBuilder.ToString();
    }
}