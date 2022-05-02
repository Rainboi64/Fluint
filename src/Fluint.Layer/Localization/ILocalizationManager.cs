//
// ILocalizationManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

namespace Fluint.Layer.Localization
{
    [Initialization(InitializationMethod.Singleton)]
    public interface ILocalizationManager : IModule
    {
        string ActiveLanguage
        {
            get;
            set;
        }

        string[] FetchLanguages();
        string Fetch(string recordName);
        void CreateRecord(string recordName, string recordData);
    }
}