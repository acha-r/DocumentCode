using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DocumentTool
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public class DocumentAttribute : Attribute
    {
        public DocumentAttribute(string description)
        {
            Description = description;
        }

        public DocumentAttribute(string description, string input = "", string output = "")
        {
            Description = description;
            Input = input;
            Output = output;
        }

        public string Description { get; }
        public string Input { get; }
        public string Output { get; }      
    }
}
