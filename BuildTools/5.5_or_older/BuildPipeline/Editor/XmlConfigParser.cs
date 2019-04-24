namespace BuildPipline
{
    public class XmlConfigParser : IConfigParser
    {
        public ConfigData ConfigData
        {
            get;
            private set;
        }

        public ConfigData ConfigVariables
        {
            get;
            private set;
        }

        public void Parse(string filename)
        {
            throw new System.NotImplementedException();
        }
    }
}