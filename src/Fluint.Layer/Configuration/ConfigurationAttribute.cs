using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fluint.Layer.Configuration
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class ConfigurationAttribute : Attribute
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ConfigurationVisibility Visibility { get; set; }
        public ConfigurationAttribute(string title, string description, ConfigurationVisibility visibility)
        {
            Title = title;
            Description = description;
            Visibility = visibility;
        }
        public ConfigurationAttribute(string title, string description)
        {
            Title = title;
            Description = description;
            Visibility = ConfigurationVisibility.Visible;
        }
    }
}
