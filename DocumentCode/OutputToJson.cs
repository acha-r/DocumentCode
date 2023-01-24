using DocumentTool;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace DocumentCode
{
    internal class OutputToJson
    {
        static List<Response> response = new();
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
            }
           JsonFileUtils.WriteToJsonFile(response, fileName);
        }


        public static void DisplayTypeAttr(Type type)
        {
            DocumentAttribute documentAttribute = (DocumentAttribute)type.GetCustomAttribute(typeof(DocumentAttribute))!;

            if (documentAttribute != null && type.IsClass)
            {
                var res = new Response(documentAttribute.Description, type.Name);                
                response.Add(res);
            }
            else if (documentAttribute != null && type.IsEnum)
            {
                var res = new Response(documentAttribute.Description, type.Name);
                response.Add(res);
            }
            else if (documentAttribute != null && type.IsInterface)
            {
                var res = new Response(documentAttribute.Description, type.Name);
                response.Add(res);
            }
        }

        public static void DisplayConstructorAttr(Type type)
        {
            var constructors = type.GetConstructors();

            foreach (var constructor in constructors)
            {
                var documentAttribute = (DocumentAttribute)constructor.GetCustomAttribute(typeof(DocumentAttribute));

                if (documentAttribute != null )
                {
                    var res = new Response(documentAttribute.Description, constructor.Name);
                    response.Add(res);
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
                    var res = new Response(documentAttribute.Description, property.Name);
                    response.Add(res);
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
                    var res = new Response()
                    {
                        Description = documentAttribute.Description,
                        Name = method.Name
                    };
                    response.Add(res);
                    JsonSerializer.Serialize(response);
                }
                if (!string.IsNullOrEmpty(documentAttribute?.Input))
                {
                    var res = new Response()
                    {
                        Input = documentAttribute.Input
                    };
                    response.Add(res);
                    JsonSerializer.Serialize(response);
                }
                if (!string.IsNullOrEmpty(documentAttribute?.Output))
                {
                    var res = new Response()
                    {
                        Output = documentAttribute.Output
                    };
                    response.Add(res);
                    JsonSerializer.Serialize(response);
                }
            }
        }

        public class Response
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Input { get; set; }
            public string Output { get; set; }

            public Response()
            {

            }
            public Response(string name, string description, string input = "", string output ="")
            {
                Name = name;
                Description = description;
                Input = input;
                Output = output;
            }
        }
    }
    
}
