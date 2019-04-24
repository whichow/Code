namespace BuildPipline
{
    public class JsonConfigParser : IConfigParser
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