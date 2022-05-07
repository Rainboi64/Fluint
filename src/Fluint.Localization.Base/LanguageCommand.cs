// 
// Language.cs
// 
// Copyright (C) 2021 Yaman Alhalabi

using Fluint.Layer;
using Fluint.Layer.Localization;
using Fluint.Layer.SDK;

namespace Fluint.Localization.Base;

[Module("Language Command", "An interface for communicating with the localization API",
    "An interface for communicating with the localization API\n" +
    "Arguments: \n" +
    "[record x y] places a record where 'x' is the record name, and 'y' is the record data. (needs to be loaded)\n" +
    "[load x] loads the language with the abbreviation 'x'. (note: loading a non-existing language will create one with the corresponding abbreviation)\n" +
    "[fetch x] prints the corresponding record data for the loaded language")]
public class Language : ILambda
{
    private readonly ILocalizationManager _localizationManager;

    public Language(ILocalizationManager localizationManager)
    {
        _localizationManager = localizationManager;
    }

    public string Command => "language";

    public LambdaObject Run(string[] args)
    {
        if (args is null)
        {
            return LambdaObject.Error("invalid argument try running 'help language'");
        }

        if (args.Length < 1)
        {
            return LambdaObject.Error("invalid argument try running 'help language'");
        }

        switch (args[0].ToLower())
        {
            case "record":
                if (args.Length < 3)
                {
                    return LambdaObject.Error("invalid argument try running 'help language'");
                }

                Record(args[1], args[2]);
                break;
            case "load":
                Load(args[1]);
                break;
            case "fetch":
                if (args.Length < 2)
                {
                    return LambdaObject.Error("invalid argument try running 'help language'");
                }

                return new LambdaObject(Fetch(args[1]));
            default:
                return LambdaObject.Error("invalid argument try running 'help language'");
        }

        return LambdaObject.Failure;
    }

    private string Fetch(string key)
    {
        return _localizationManager.Fetch(key);
    }

    private void Load(string abbrevieation)
    {
        _localizationManager.ActiveLanguage = abbrevieation;
    }

    private void Record(string recordName, string recordData)
    {
        _localizationManager.CreateRecord(recordName, recordData);
    }
}