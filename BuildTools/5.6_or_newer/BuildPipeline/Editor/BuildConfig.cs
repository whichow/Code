using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using UnityEngine;

namespace BuildPipline
{
    public class BuildConfig
    {
        private ConfigData configData;
        private ConfigData configVariables;

        public static BuildConfig LoadConfig(string path, IConfigParser parser)
        {
            BuildConfig config = new BuildConfig();
            parser.Parse(path);
            config.configData = parser.ConfigData;
            config.configVariables = parser.ConfigVariables;
            return config;
        }

        public ConfigData GetConfigData()
        {
            return configData;
        }

        public ConfigData GetConfigVariables()
        {
            return configVariables;
        }
    }
}