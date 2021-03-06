using Fluint.Engine.GL46.Graphics;
using Fluint.Layer.Debugging;
using Fluint.Layer.DependencyInjection;
using Fluint.Layer.Engine;

namespace Fluint.Engine.GL46
{
    public class Engine : IEngine
    {
        private Debug _debug;
        private readonly ModulePacket _packet;
        private readonly ILogger _logger;

        public Engine(ModulePacket packet)
        {
            _packet = packet;
            _logger = _packet.GetSingleton<ILogger>();
        }

        public void Start(EngineMode mode, string[] arguments)
        {
            Texture.ConfigureTextures();
            switch (mode)
            {
                case EngineMode.Defualt:
                    _logger.Information("Starting Engine in default mode.");
                    break;
                case EngineMode.Debug:
                    _logger.Information("Starting Engine in debug mode.");
                    _debug = new Debug();
                    break;
            }
        }

        public void Stop()
        {
            _logger.Information("Stopping Engine.");
        }
    }
}
