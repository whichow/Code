using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using ExcelDataReader;
using UnityEngine;

namespace BuildPipline
{
    public class ExcelConfigParser : IConfigParser
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
            using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet();
                    ParseExcel(result);
                }
            }
        }

        private void ParseExcel(DataSet dataSet)
        {
            ParseConfig(dataSet);
            ParseConfigVariables(dataSet);
        }

        private void ParseConfig(DataSet dataSet)
        {
            var cols = dataSet.Tables[0].Columns;
            var rows = dataSet.Tables[0].Rows;
            var configData = new ConfigData();
            configData.SetDataType(DataType.List);
            for (int i = 3; i < rows.Count; i++)
            {
                var rawData = new ConfigData();
                rawData.SetDataType(DataType.Dictionary);
                for (int j = 0; j < cols.Count; j++)
                {
                    var data = GetConfigDataByType(rows[i][j].ToString(), rows[2][j].ToString());
                    rawData.Add(rows[1][j].ToString(), data);
                }
                configData.Add(rawData);
            }
            this.ConfigData = configData;
        }

        private void ParseConfigVariables(DataSet dataSet)
        {
            var rows = dataSet.Tables[1].Rows;
            var configData = new ConfigData();
            configData.SetDataType(DataType.Dictionary);
            for (int i = 0; i < rows.Count; i++)
            {
                configData.Add(rows[i][0].ToString(), new ConfigData(rows[i][1].ToString()));
            }
            this.ConfigVariables = configData;
        }

        private static ConfigData GetConfigDataByType(string val, string type)
        {
            switch (type)
            {
                case "bool":
                    return new ConfigData(Boolean.Parse(val));
                case "int":
                    return new ConfigData(Int32.Parse(val));
                case "long":
                    return new ConfigData(Int64.Parse(val));
                case "float":
                    return new ConfigData(Single.Parse(val));
                case "double":
                    return new ConfigData(Double.Parse(val));
                case "string":
                    return new ConfigData(val);
                default:
                    return null;
            }
        }
    }
}