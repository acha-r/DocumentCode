using System.Reflection;
namespace DocumentCode
{
    internal class Program
    {      

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            DocumentAttribute.DisplayAttributes(typeof(Person));
        }
    }
}