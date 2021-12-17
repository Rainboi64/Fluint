//
// ILocalizationManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.Text;

namespace Fluint.Layer.Localization
{
    [Initialization(InitializationMethod.Singleton)]
    public interface ILocalizationManager : IModule
    {
        /// <summary>
        /// Contains an abbreviation for the language where fetches will be executed
        /// </summary>
        string ActiveLanguage { get; set; }
        string[] FetchLanguages();
        string Fetch(string recordName);
        void CreateRecord(string recordName, string recordData);
    }
}
