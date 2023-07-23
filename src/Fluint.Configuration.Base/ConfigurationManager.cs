//
// ConfigurationManager.cs
//
// Copyright (C) 2022 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Fluint.Layer.Configuration;
using Fluint.Layer.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Fluint.Configuration.Base;

public class ConfigurationManager : IConfigurationManager
{
    private readonly List<IConfiguration> _configs = new();

    private readonly ModulePacket _packet;

    public ConfigurationManager(ModulePacket packet)
    {
        _packet = packet;
    }

    public T Get<T>() where T : IConfiguration
    {
        // no caching
        // if (_configs.OfType<T>().Any())
        // {
        //     return _configs.OfType<T>().FirstOrDefault();
        // }

        if (FileExists(typeof(T)))
        {
            var jsonData = File.ReadAllText(GenerateTypeLocation(typeof(T)));
            var obj = JsonConvert.DeserializeObject<T>(jsonData,
                new StringEnumConverter());
            _configs.Add(obj);
            return obj;
        }

        Add(Activator.CreateInstance<T>());
        return _configs.OfType<T>().FirstOrDefault();
    }

    public void Add(IConfiguration configuration)
    {
        var fileName = GenerateTypeLocation(configuration.GetType());
        GenerateJsonFile(configuration);
        _configs.Add(configuration);
    }

    public void Save()
    {
        foreach (var config in _configs)
        {
            GenerateJsonFile(config);
        }
    }

    public bool Contains<T>() where T : IConfiguration
    {
        var length = _configs.Count;
        for (var i = 0; i < length; i++)
        {
            var current = _configs[i];
            if (current.GetType() == typeof(T))
            {
                return true;
            }
        }

        return FileExists(typeof(T));
    }

    private void GenerateJsonFile(IConfiguration config)
    {
        var jsonData = JsonConvert.SerializeObject(config, Formatting.Indented,
            new StringEnumConverter());

        GetLocation(config.GetType(), out var location);
        Directory.CreateDirectory(@$"./configs/{location}");

        File.WriteAllText(GenerateTypeLocation(config.GetType()), jsonData);
    }

    private bool FileExists(Type config)
    {
        return File.Exists(GenerateTypeLocation(config));
    }

    private string GenerateTypeLocation(Type type)
    {
        if (GetLocation(type, out var location))
        {
            return $"./configs/{location}/{type.Name}.json";
        }

        return $"./configs/{type.Name}.json";
    }

    private static bool GetLocation(ICustomAttributeProvider type, out string location)
    {
        var attributes = type.GetCustomAttributes(false).OfType<ConfigurationAttribute>();

        var configurationAttributes = attributes as ConfigurationAttribute[] ?? attributes.ToArray();
        if (configurationAttributes.Any())
        {
            location = configurationAttributes.FirstOrDefault()?.Location;
            return true;
        }

        location = string.Empty;
        return false;
    }
}