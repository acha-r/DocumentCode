using System.Reflection;
using DocumentTool;

namespace DocumentCode
{
    internal class Program
    {      
        static void Main(string[] args)
        {
            DocumentAttribute.GetDocs(typeof(SamplePerson));
        }
    }
}