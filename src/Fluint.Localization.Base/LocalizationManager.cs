﻿//
// LocalizationManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fluint.Layer.Localization;
using Newtonsoft.Json;

namespace Fluint.Localization.Base;

public class LocalizationManager : ILocalizationManager
{
    private const string LanguagesFolder = "langs";

    private string _activeLanguage;

    private Dictionary<string, string> _records = new();

    public LocalizationManager()
    {
        ActiveLanguage = FetchLanguages()
            .FirstOrDefault("en_us");
    }

    public string ActiveLanguage
    {
        get => _activeLanguage;
        set
        {
            _activeLanguage = value;
            if (File.Exists($"{LanguagesFolder}/{ActiveLanguage}.json"))
            {
                _records = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                    File.ReadAllText($"{LanguagesFolder}/{ActiveLanguage}.json"));
            }
        }
    }

    public void CreateRecord(string recordName, string recordData)
    {
        if (!_records.ContainsKey(recordName))
        {
            _records.Add(recordName, recordData);
        }
        else
        {
            _records[recordName] = recordData;
        }

        Directory.CreateDirectory(@$"./{LanguagesFolder}/");
        File.WriteAllText($"./{LanguagesFolder}/{ActiveLanguage}.json",
            JsonConvert.SerializeObject(_records, Formatting.Indented));
    }

    public string Fetch(string recordName)
    {
        return _records.ContainsKey(recordName) ? _records[recordName] : recordName;
    }

    public string[] FetchLanguages()
    {
        if (!Directory.Exists(LanguagesFolder))
        {
            Directory.CreateDirectory(LanguagesFolder);
        }

        var start = LanguagesFolder.Length + 1;
        return Directory.GetFiles(LanguagesFolder)
            .Where(x => x.EndsWith(".json"))
            .Select(x => x[start..^5]).ToArray();
    }

    public void CreateLanguage(string languageAbbreviation)
    {
        ActiveLanguage = languageAbbreviation;
    }
}