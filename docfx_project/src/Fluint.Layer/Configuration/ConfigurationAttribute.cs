//
// ConfigurationAttribute.cs
//
// Copyright (C) 2021 Yaman Alhalabi
//

using System;

namespace Fluint.Layer.Configuration
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationAttribute : Attribute
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }

        public ConfigurationAttribute(string title, string description, string location)
        {
            Title = title;
            Description = description;
            Location = location;
        }
        public ConfigurationAttribute(string title, string description) : this(title, description, "") { }
        public ConfigurationAttribute(string location) : this("", "", location) { }
    }
}
