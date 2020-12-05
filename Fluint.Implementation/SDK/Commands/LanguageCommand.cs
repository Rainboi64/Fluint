using Fluint.Layer.Debugging;
using Fluint.Layer.Localization;
using Fluint.Layer.SDK;
using System;

namespace Fluint.Implementation.SDK.Commands
{
    [Layer.Module("Language Command", "An interface for communicating with the localization API",
        "An interface for communicating with the localization API\n" +
        "Arguments: \n" +
        "[record x y] places a record where 'x' is the record name, and 'y' is the record data. (needs to be loaded)\n" +
        "[load x] loads the language with the abbreviation 'x'. (note: loading a non-existing language will create one with the corresponding abbreviation)\n" +
        "[fetch x] prints the corresponding record data for the loaded language")]
    public class LanguageCommand : ICommand
    {
        private readonly ILocalizationManager _localizationManager;
        public LanguageCommand(ILocalizationManager localizationManager) 
        {
            _localizationManager = localizationManager;
        }

        public string Command => "language";

        public void Do(string[] args)
        {
            // idiot proofing
            if (args is null)
            {
                Console.WriteLine("invalid argument try running 'help language'");
                return;
            }

            if (args.Length < 1)
            {
                Console.WriteLine("invalid argument try running 'help language'");
                return;
            }

            switch (args[0].ToLower())
            {
                case "record":
                    if (args.Length < 3)
                    {
                        Console.WriteLine("invalid argument try running 'help language'");
                        return;
                    }
                    Record(args[1], args[2]);
                    break;
                case "load":
                    Load(args[1]);
                    break;
                case "fetch":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("invalid argument try running 'help language'");
                        return;
                    }
                    Console.WriteLine(Fetch(args[1]));
                    break;
                default:
                    Console.WriteLine("invalid argument try running 'help language'");
                    break;
            }
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
}
