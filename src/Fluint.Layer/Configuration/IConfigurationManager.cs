//
// IConfigurationManager.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System.Runtime.InteropServices;
namespace Fluint.Layer.Configuration
{
    /// <summary>
    /// An interface for managing configuration classes, and serializing them.
    /// </summary>
    [Initialization(InitializationMethod.Singleton)]
    public interface IConfigurationManager : IModule
    {
        /// <summary>
        /// For getting the data of the specified configuration class.
        /// </summary>
        /// <typeparam name="T">The type of data you want</typeparam>
        /// <returns>The deserialized data.</returns>
        T Get<T>() where T : IConfiguration;

        /// <summary>
        /// Saves the class in the configurations container
        /// </summary>
        /// <param name="configuration">The data to be saved.</param>
        void Add(IConfiguration configuration);

        /// <summary>
        /// Saves all the data to the long-term container 'ex: hard-drive'
        /// </summary>
        void Save();

        /// <summary>
        /// Checks if the configuration manager has type.
        /// </summary>
        /// <typeparam name="T">the type to check for.</typeparam>
        /// <returns>if the type is queryable, thus exists.</returns>
        bool Contains<T>() where T : IConfiguration;
    }
}
