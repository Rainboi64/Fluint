//
// EnableFluincyConfiguration.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using Fluint.Layer.Configuration;

namespace Fluint.Fluincy
{
    public class EnableFluincyConfiguration : IConfiguration
    {
        public bool EnableFluincyServices
        {
            get;
            set;
        }
    }
}