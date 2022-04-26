//
// IOManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Graphics;
using Fluint.Layer.IO;

namespace Fluint.IO.Base
{
    public class IoManager : IIoManager
    {
        private readonly List<IExporter> _exporters;
        private readonly List<IImporter> _importers;
        private readonly ModulePacket _packet;

        public IoManager(ModulePacket packet)
        {
            _packet = packet;
            _exporters = (List<IExporter>)_packet.GetInstances().OfType<IExporter>();
            _importers = (List<IImporter>)_packet.GetInstances().OfType<IImporter>();
        }

        public void Export(string fileName, IMesh[] meshes, string format = "")
        {
            if (format == string.Empty)
            {
                format = Path.GetExtension(fileName);
            }

            foreach (var exporter in _exporters)
            {
                var isValid = exporter.FileExtenstions.Where(x => x.ToLower() == format.ToLower()).Any();
                if (isValid)
                {
                    exporter.Export(meshes, fileName);
                }
            }
        }

        public IMesh[] Import(string fileName)
        {
            var format = Path.GetExtension(fileName);
            foreach (var importer in _importers)
            {
                var isValid = importer.FileExtenstions.Where(x => x.ToLower() == format.ToLower()).Any();
                if (isValid)
                {
                    return importer.Import(fileName);
                }
            }

            throw new ArgumentException($"could not import file type: {format}");
        }

        public string[] QueryExportableFormats()
        {
            var formats = new List<string>();
            foreach (var exporter in _exporters)
            {
                formats.AddRange(exporter.FileExtenstions);
            }

            return formats.ToArray();
        }

        public string[] QueryImportableFormats()
        {
            var formats = new List<string>();
            foreach (var importer in _importers)
            {
                formats.AddRange(importer.FileExtenstions);
            }

            return formats.ToArray();
        }
    }
}