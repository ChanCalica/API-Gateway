namespace API_Gateway.Models.Request
{
    public class MSG3RequestObjects
    {
        public List<MSG3RequestObject> RequestObjects { get; set; }

        public MSG3RequestObjects()
        {
            RequestObjects = new List<MSG3RequestObject>();
        }
    }

    public class MSG3RequestObject
    {
        public string MessageData { get; set; }
        public string Chain { get; set; }
        public string Branch { get; set; }
    }
}
