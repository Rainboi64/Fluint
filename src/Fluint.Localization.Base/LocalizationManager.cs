//
// LocalizationManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Collections.Generic;
using System.IO;
using Fluint.Layer.Localization;
using Newtonsoft.Json;

namespace Fluint.Localization.Base
{
    public class LocalizationManager : ILocalizationManager
    {
        public const string LanguagesFolder = "langs";

        private string _activeLanguage;

        private Dictionary<string, string> _records = new Dictionary<string, string>();

        public string ActiveLanguage
        {
            get => _activeLanguage;
            set
            {
                _activeLanguage = value;
                if (File.Exists(@$"{LanguagesFolder}\{ActiveLanguage}.json"))
                {
                    _records = JsonConvert.DeserializeObject<Dictionary<string, string>>(
                        File.ReadAllText(@$"{LanguagesFolder}\{ActiveLanguage}.json"));
                }
            }
        }

        public void CreateRecord(string recordName, string recordData)
        {
            if (!_records.ContainsKey(recordName))
                _records.Add(recordName, recordData);
            else
                _records[recordName] = recordData;
            Directory.CreateDirectory(@$"{LanguagesFolder}\");
            File.WriteAllText(@$"{LanguagesFolder}\{ActiveLanguage}.json",
                JsonConvert.SerializeObject(_records, Formatting.Indented));
        }

        public string Fetch(string recordName)
        {
            if (_records.ContainsKey(recordName)) return _records[recordName];
            return recordName;
        }

        public string[] FetchLanguages()
        {
            // TODO: make this.
            return null;
        }

        public void CreateLanguage(string languageAbbreviation)
        {
            ActiveLanguage = languageAbbreviation;
        }
    }
}