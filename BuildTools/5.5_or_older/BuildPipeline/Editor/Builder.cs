namespace BuildPipline
{
    public abstract class Builder
    {
        protected string[] args;
        protected ConfigData configData;
        protected ConfigData variables;
        private static Builder defaultBuilder;

        public static Builder DefaultBuilder
        {
            get
            {
                if (defaultBuilder == null)
                {
                    defaultBuilder = new AndroidChannelBuilder();
                }
                return defaultBuilder;
            }
        }

        public void Build(BuildConfig config, string[] args)
        {
            this.args = args;
            this.configData = config.GetConfigData();
            this.variables = config.GetConfigVariables();
            
            Build();
        }

        protected virtual void Build() { }

        protected virtual void PreBuild() { }

        protected virtual void PostBuild() { }

        public string GetArg(string key)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == key)
                {
                    return args[i + 1];
                }
            }
            return "";
        }

        public bool HasArg(string key)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == key)
                {
                    return true;
                }
            }
            return false;
        }
    }
}