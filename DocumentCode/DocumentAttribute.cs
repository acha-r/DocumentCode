using System.Reflection;

namespace DocumentCode
{
    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    internal class DocumentAttribute : Attribute
    {
        public string Description { get; set; }
        public string Input { get; set; }
        public string Output { get; set; }

        public DocumentAttribute(string description)
        {
            Description = description;
        }

        public DocumentAttribute(string description, string input = "Input", string output = "Output")
        {
            Description = description;
            Input = input;
            Output = output;
        }

        public static void DisplayAttributes(Type classtype)
        {
            Console.WriteLine("Assembly: {0}", Assembly.GetExecutingAssembly());
            Console.WriteLine("\nClass: \n{0}", classtype.Name);

            object[] classAttr = classtype.GetCustomAttributes(true);

            foreach (Attribute item in classAttr)
            {
                if (item is DocumentAttribute)
                {
                    DocumentAttribute doc = (DocumentAttribute)item;
                    Console.WriteLine(@"
    Description:
        {0}", doc.Description);
                }
            }

            Console.WriteLine("\nProperties: ");
            Console.WriteLine();

            PropertyInfo[] properties = classtype.GetProperties();

            for (int i = 0; i < properties.GetLength(0); i++)
            {
                object[] propAttr = properties[i].GetCustomAttributes(true);

                foreach (Attribute item in propAttr)
                {
                    if (item is DocumentAttribute)
                    {
                        DocumentAttribute doc = (DocumentAttribute)item;
                        Console.WriteLine(@"{0}
    Description:
        {1}
    Input:
        {2}", properties[i].Name, doc.Description, doc.Input);
                    }
                }
            }

            MethodInfo[] methods = classtype.GetMethods();

            Console.WriteLine("\nMethods:\n");

            for (int i = 0; i < methods.GetLength(0); i++)
            {
                object[] methAttr = methods[i].GetCustomAttributes(true);

                foreach (Attribute item in methAttr)
                {
                    if (item is DocumentAttribute)
                    {
                        DocumentAttribute doc = (DocumentAttribute)item;
                        Console.WriteLine(@"{0}
    Description:
        {1}
    Input:
        {2}
    Output:
        {3}", methods[i].Name, doc.Description, doc.Input, doc.Output);
                    }
                }
            }

        }
    }
}
