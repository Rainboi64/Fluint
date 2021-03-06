using Fluint.Layer.Engine;

namespace Fluint.Layer.IO
{
    [Initialization(InitializationMethod.Scoped)]
    public interface IIOManager : IModule
    {
        IMesh[] Import(string fileName);
        void Export(string fileName, IMesh[] meshes, string format = "");
        string[] QueryImportableFormats();
        string[] QueryExportableFormats();
    }
}
