using Avalonia;
using Avalonia.Logging.Serilog;
using Fluint.Layer.Debugging;
using Fluint.Layer.SDK;
using Fluint.SDK.Commands.Splitter.GUI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Fluint.SDK.Commands.Splitter
{
    public class SplitterCommand : ICommand
    {
        public string Command => "splitter";

        public void Do(string[] args)
        {
            // TODO: Finish this one day :D
            switch (args[0])
            {
                case "-w":
                    BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
                    break;
                case "-g":
                    GenerateConfiguration(args[1], args[2], args[3]);
                    break;
                default:
                    Split(args[0], args[1], false);
                    break;
            }
        }

        public static IEnumerable<Split> SplitDirectory(string splitDirectory, string baseDirectory = null, bool isFirstCall = true)
        {
            var directoryName = Path.GetFileName(splitDirectory);

            var splits = new List<Split>();

            if (directoryName == "bin" || directoryName == "obj") return splits;

            var absloutePathFiles = Directory.GetFiles(splitDirectory);

            // Get the relative path files.
            // for example:
            // makes C://SomeDirectory/xd to /xd

            var relativePathFiles = new List<string>();

            foreach (var absloutePath in absloutePathFiles)
            {
                // Skips file is it's the project file.
                if (Path.GetExtension(absloutePath) == ".csproj")
                    continue;

                // Checks if this is the main call
                if (isFirstCall)
                {
                    // if it is the main call then it's relative to the current directory.
                    relativePathFiles.Add(Path.GetRelativePath(splitDirectory, absloutePath));
                }
                else
                {
                    // Otherwise it's relative to the base directory.
                    relativePathFiles.Add(Path.GetRelativePath(baseDirectory, absloutePath));
                }
            }

            var mainSplit = new Split
            {
                Name = directoryName,
                Files = relativePathFiles
            };

            splits.Add(mainSplit);

            foreach (var directory in Directory.GetDirectories(splitDirectory))
            {
                if (isFirstCall) baseDirectory = splitDirectory;
                splits.AddRange(SplitDirectory(directory, baseDirectory, false));
            }

            return splits;
        }

        public static void GenerateConfiguration(string modulePath, string layerLocation, string outputPath)
        {
            var splitConfiguration = new SplitConfigurations
            {
                LayerLocation = layerLocation
            };

            splitConfiguration.Splits = SplitDirectory(modulePath);

            Directory.CreateDirectory(outputPath);

            var outputJson = outputPath + @"\config.json";

            File.WriteAllText(outputJson, JsonConvert.SerializeObject(splitConfiguration, Formatting.Indented));
        }

        public static void Split(string modulePath, string outputPath, bool copyLayer = false)
        {
            // TODO: Add Multithreading.
            var configPath = (outputPath + @"\config.json");
            if (!File.Exists(configPath))
            {
                Console.WriteLine("config file not found, expected at location {0}", configPath);
                return;
            }
            var jsonData = File.ReadAllText(configPath);
            var configuration = JsonConvert.DeserializeObject<SplitConfigurations>(jsonData);
            var splitCount = configuration.Splits.Count();

            Directory.CreateDirectory(outputPath);
            Console.WriteLine("Beginning file generation 0/{0}", splitCount + 1);

            for (int i = 0; i < splitCount; i++)
            {
                var split = configuration.Splits.ToArray()[i];
                var splitDirectory = outputPath + @"\" + split.Name;
                Directory.CreateDirectory(splitDirectory);

                // Start building csproj
                StringBuilder csprojData = new StringBuilder();

                csprojData.AppendLine("<Project Sdk=\"Microsoft.NET.Sdk\">");

                // copying global project configs
                if(!(configuration.ProjectConfigurations is null))
                {
                    foreach (var projectConfig in configuration.ProjectConfigurations)
                    {
                        csprojData.AppendLine(projectConfig);
                    }
                }

                // copying local project configs
                if (!(configuration.ProjectConfigurations is null))
                {
                    foreach (var projectConfig in split.ProjectConfigurations)
                    {
                        csprojData.AppendLine(projectConfig);
                    }
                }

                // implement dependencies
                csprojData.AppendLine("  <ItemGroup>");

                // Add layer as a dependency.
                if (copyLayer)
                {
                    var destination = outputPath + @"\Fluint.Layer";
                    Console.WriteLine("Copying {0} to {1}", configuration.LayerLocation, destination);
                    File.Copy(configuration.LayerLocation, destination);
                    csprojData.AppendLine($"    <ProjectReference Include=\"..\\Fluint.Layer\\Fluint.Layer.csproj/>");
                }
                else
                {
                    csprojData.AppendLine($"    <ProjectReference Include=\"{configuration.LayerLocation}\" />");
                }

                // Add user dependencies.
                if (!(split.Dependencies is null))
                {
                    foreach (var dependency in split.Dependencies)
                    {
                        csprojData.AppendLine($"    <ProjectReference Include=\"..\\{dependency}\\{dependency}.csproj\" />");
                    }
                }

                csprojData.AppendLine("  </ItemGroup>");


                csprojData.AppendLine("</Project>");

                // csproj done!

                File.WriteAllText(@$"{splitDirectory}\{ split.Name }.csproj", csprojData.ToString());

                // now copy project files
                foreach (var file in split.Files)
                {
                    var source = $@"{modulePath}\{file}";
                    var destination = $@"{splitDirectory}\{file}";
                    Console.WriteLine("Copying {0} to {1}", source, destination);
                    Directory.CreateDirectory(Path.GetDirectoryName(destination));
                    File.Copy(source, destination);
                }

                Console.WriteLine("Finished splitting {0}/{1}.", i + 1, splitCount + 1);
            }
            Console.WriteLine("Done.");
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        private static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect();
    }
}
