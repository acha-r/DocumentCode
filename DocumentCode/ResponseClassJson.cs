namespace DocumentCode
{

    public class ResponseClassJson
    {
        public ResponseClassJson()
        {
            Properties = new();
            Methods = new();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public PropertyResp Constructor { get; set; }
        public List<PropertyResp> Properties { get; set; }
        public List<MethodResp> Methods { get; set; }
    }

    public class PropertyResp
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class MethodResp : PropertyResp
    {
        public string Input { get; set; }
        public string Output { get; set; }
    }
}

