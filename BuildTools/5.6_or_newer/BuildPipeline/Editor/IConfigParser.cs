namespace BuildPipline
{
    public interface IConfigParser
    {
        ConfigData ConfigData
        {
            get;
        }
        ConfigData ConfigVariables
        {
            get;
        }
        void Parse(string filename);
    }
}