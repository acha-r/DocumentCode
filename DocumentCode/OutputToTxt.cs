using DocumentTool;
using System.Reflection;

namespace DocumentCode
{
    internal class OutputToTxt
    {
        static string fileName = @"C:\Users\USER\Desktop\GetDocs.txt";

        internal static void GetDocs()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            FileCheck();
            Console.WriteLine("Assembly name: {0}", assembly.FullName);

            Type[] types = assembly.GetTypes();

            foreach (Type t in types)
            {
                DisplayTypeAttr(t);
                DisplayConstructorAttr(t);
                DisplayPropAttr(t);
                DisplayMethodAttr(t);
            }
            Console.WriteLine(File.ReadAllText(fileName));
        }

        public static void DisplayTypeAttr(Type type)
        {
            DocumentAttribute documentAttribute = (DocumentAttribute)type.GetCustomAttribute(typeof(DocumentAttribute))!;

            if (documentAttribute != null && type.IsClass)
            {
                File.AppendAllText(fileName, $"\nClass: {type.Name}\n\tDescription: {documentAttribute.Description}\n");
            }
            else if (documentAttribute != null && type.IsEnum)
            {
                File.AppendAllText(fileName, $"\nEnum: {type.Name}\n\tDescription: {documentAttribute.Description}\n");
            }
            else if (documentAttribute != null && type.IsInterface)
            {
                File.AppendAllText(fileName, $"\nInterface: {type.Name}\n\tDescription: {documentAttribute.Description}\n");
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
                    File.AppendAllText(fileName, $"\tConstructor:\n\t\t{constructor.Name}\n\t\tDescription: {documentAttribute.Description}\n");
                }
                if (!string.IsNullOrEmpty(documentAttribute?.Input))
                {
                    File.AppendAllText(fileName, $"\t\tInput: {documentAttribute.Input}");
                }

                if (!string.IsNullOrEmpty(documentAttribute?.Output))
                {
                    File.AppendAllText(fileName, $"\n\t\tOutput: {documentAttribute.Output}");
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
                    File.AppendAllText(fileName, $"\tProperty:\n\t\t{property.Name}\n\t\tDescription: {documentAttribute.Description}\n");
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
                    File.AppendAllText(fileName, $"\tMethod:\n\t\t{method.Name}\n\t\tDescription: {documentAttribute.Description}\n");
                }
                if (!string.IsNullOrEmpty(documentAttribute?.Input))
                {
                    File.AppendAllText(fileName, $"\t\tInput: {documentAttribute.Input}");
                }

                if (!string.IsNullOrEmpty(documentAttribute?.Output))
                {
                    File.AppendAllText(fileName, $"\n\t\tOutput: {documentAttribute.Output}");
                }
            }
        }

        internal static void FileCheck()
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
    }
}

