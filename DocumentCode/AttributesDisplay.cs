using DocumentTool;
using System.Reflection;

namespace DocumentCode
{
    internal class AttributesDisplay
    {
        internal static void GetDocs()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Console.WriteLine("Assembly name: {0}", assembly.FullName);

            Type[] types = assembly.GetTypes();

            foreach (Type t in types)
            {
                DisplayTypeAttr(t);
                DisplayConstructorAttr(t);
                DisplayPropAttr(t);
                DisplayMethodAttr(t);
                Console.WriteLine();
            }
        }

        public static void DisplayTypeAttr(Type type)
        {
            DocumentAttribute documentAttribute = (DocumentAttribute)type.GetCustomAttribute(typeof(DocumentAttribute))!;
            if (documentAttribute != null && type.IsClass)
            {
                Console.WriteLine($"Class: {type.Name}\n\tDescription: {documentAttribute.Description}\n");
            }
            else if (documentAttribute != null && type.IsEnum)
            {
                Console.WriteLine($"Enum: {type.Name}\n\tDescription: {documentAttribute.Description}\n");
            }
            else if (documentAttribute != null && type.IsInterface)
            {
                Console.WriteLine($"Interface: {type.Name}\n\tDescription: {documentAttribute.Description}\n");
            }
        }

        public static void DisplayConstructorAttr(Type type)
        {
            var constructors = type.GetConstructors();

            foreach (var constructor in constructors)
            {
                var documentAttribute = (DocumentAttribute)constructor.GetCustomAttribute(typeof(DocumentAttribute));

                if (documentAttribute != null)
                {
                    Console.WriteLine($"\tConstructor:\n\t\t{constructor.Name}\n\t\tDescription: {documentAttribute.Description}\n");
                }
                if (!string.IsNullOrEmpty(documentAttribute?.Input))
                {
                    Console.WriteLine($"\t\tInput: {documentAttribute.Input}");
                }

                if (!string.IsNullOrEmpty(documentAttribute?.Output))
                {
                    Console.WriteLine($"\t\tInput: {documentAttribute.Output}");
                }
            }
        }

        public static void DisplayPropAttr(Type classtype)
        {
            var properties = classtype.GetProperties();

            foreach (var property in properties)
            {
                var documentAttribute = (DocumentAttribute)property.GetCustomAttribute(typeof(DocumentAttribute));

                if (documentAttribute != null)
                {
                    Console.WriteLine($"\tProperty:\n\t\t{property.Name}\n\t\tDescription: {documentAttribute.Description}\n");
                }
            }
        }

        public static void DisplayMethodAttr(Type classtype)
        {
            var methods = classtype.GetMethods();

            foreach (var method in methods)
            {
                var documentAttribute = (DocumentAttribute)method.GetCustomAttribute(typeof(DocumentAttribute));

                if (documentAttribute != null)
                {
                    Console.WriteLine($"\tMethod:\n\t\t{method.Name}\n\t\tDescription: {documentAttribute.Description}\n");
                }
                if (!string.IsNullOrEmpty(documentAttribute?.Input))
                {
                    Console.WriteLine($"\t\tInput: {documentAttribute.Input}");
                }

                if (!string.IsNullOrEmpty(documentAttribute?.Output))
                {
                    Console.WriteLine($"\t\tOutput: {documentAttribute.Output}");
                }
            }
        }
    }
}
