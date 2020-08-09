using System;

namespace Utility
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AttributeName : Attribute
    {
        
        public string Name { get; set; }

        public AttributeName(string name)
        {
            Name = name;
        }
            
    }
}