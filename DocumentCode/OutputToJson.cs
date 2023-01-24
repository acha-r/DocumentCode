using DocumentTool;
using System.Reflection;

namespace DocumentCode
{
    internal class OutputToJson
    {
        private static ResponseClassJson response = new();
        private static List<ResponseClassJson> responses = new();

        static string fileName = @"C:\Users\USER\Desktop\GetDocs.json";

        internal static void GetDocs()
        {
            if (File.Exists(fileName)) File.Delete(fileName);

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

                responses.Add(response);
                response = new();
            }
            JsonFileUtils.WriteToJsonFile(responses, fileName);
        }

        public static void DisplayTypeAttr(Type type)
        {
            DocumentAttribute documentAttribute = (DocumentAttribute)type.GetCustomAttribute(typeof(DocumentAttribute))!;

            if (documentAttribute is not null)
            {
                response.Name = type.Name;
                response.Description = documentAttribute.Description;
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
                    response.Constructor = new()
                    {
                        Name = constructor.Name,
                        Description = documentAttribute.Description
                    };
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
                    response.Properties.Add(new()
                    {
                        Name = property?.Name,
                        Description = documentAttribute?.Description

                    });
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
                    response.Methods.Add(new()
                    {
                        Name = method.Name,
                        Description = documentAttribute.Description,
                        Input = string.IsNullOrEmpty(documentAttribute?.Input) ? string.Empty: documentAttribute?.Input,
                        Output = string.IsNullOrEmpty(documentAttribute?.Output) ? string.Empty : documentAttribute?.Output,
                    });
                }
            }
        }
    }
}

