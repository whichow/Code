using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildPipline
{
    public enum DataType
    {
        None,
        Bool,
        Float,
        Double,
        Int,
        Long,
        String,
        List,
        Dictionary
    }

    public class ConfigData : IList<ConfigData>, IDictionary<string, ConfigData>
    {
        private DataType type;
        private bool bool_val;
        private float float_val;
        private double double_val;
        private int int_val;
        private long long_val;
        private string string_val;
        private IList<ConfigData> list_val;
        private IDictionary<string, ConfigData> dic_val;

        public bool IsBool
        {
            get
            {
                return type == DataType.Bool;
            }
        }

        public bool IsFloat
        {
            get
            {
                return type == DataType.Float;
            }
        }

        public bool IsDouble
        {
            get
            {
                return type == DataType.Double;
            }
        }

        public bool IsInt
        {
            get
            {
                return type == DataType.Int;
            }
        }

        public bool IsLong
        {
            get
            {
                return type == DataType.Long;
            }
        }

        public bool IsString
        {
            get
            {
                return type == DataType.String;
            }
        }

        public bool IsList
        {
            get
            {
                return type == DataType.List;
            }
        }

        public bool IsDictionary
        {
            get
            {
                return type == DataType.Dictionary;
            }
        }

        public ConfigData()
        {

        }

        public ConfigData(bool val)
        {
            type = DataType.Bool;
            bool_val = val;
        }

        public ConfigData(float val)
        {
            type = DataType.Float;
            float_val = val;
        }

        public ConfigData(double val)
        {
            type = DataType.Double;
            double_val = val;
        }

        public ConfigData(int val)
        {
            type = DataType.Int;
            int_val = val;
        }

        public ConfigData(long val)
        {
            type = DataType.Long;
            long_val = val;
        }

        public ConfigData(string val)
        {
            type = DataType.String;
            string_val = val;
        }

        public bool AsBool()
        {
            if (type != DataType.Bool)
            {
                throw new InvalidCastException("Instance of ConfigData doesn't hold a bool");
            }
            return bool_val;
        }

        public float AsFloat()
        {
            if (type != DataType.Float)
            {
                throw new InvalidCastException("Instance of ConfigData doesn't hold a float");
            }
            return float_val;
        }

        public double AsDouble()
        {
            if (type != DataType.Double)
            {
                throw new InvalidCastException("Instance of ConfigData doesn't hold a double");
            }
            return double_val;
        }

        public int AsInt()
        {
            if (type != DataType.Int)
            {
                throw new InvalidCastException("Instance of ConfigData doesn't hold a int");
            }
            return int_val;
        }

        public long AsLong()
        {
            if (type != DataType.Long)
            {
                throw new InvalidCastException("Instance of ConfigData doesn't hold a long");
            }
            return long_val;
        }

        public string AsString()
        {
            if (type != DataType.String)
            {
                throw new InvalidCastException("Instance of ConfigData doesn't hold a string");
            }
            return string_val;
        }

        public IList<ConfigData> AsList()
        {
            if (type != DataType.List)
            {
                throw new InvalidCastException("Instance of ConfigData doesn't hold a list");
            }
            if (list_val == null)
            {
                list_val = new List<ConfigData>();
            }
            return list_val;
        }

        public IDictionary<string, ConfigData> AsDictionary()
        {
            if (type != DataType.Dictionary)
            {
                throw new InvalidCastException("Instance of ConfigData doesn't hold a dictionary");
            }
            if (dic_val == null)
            {
                dic_val = new Dictionary<string, ConfigData>();
            }
            return dic_val;
        }

        public DataType GetDataType()
        {
            return type;
        }

        public void SetDataType(DataType type)
        {
            if (this.type == type)
                return;

            switch (type)
            {
                case DataType.None:
                    break;

                case DataType.Bool:
                    bool_val = default(Boolean);
                    break;

                case DataType.Int:
                    int_val = default(Int32);
                    break;

                case DataType.Long:
                    long_val = default(Int64);
                    break;

                case DataType.Float:
                    float_val = default(Single);
                    break;

                case DataType.Double:
                    double_val = default(Double);
                    break;

                case DataType.String:
                    string_val = default(String);
                    break;

                case DataType.List:
                    list_val = new List<ConfigData>();
                    break;

                case DataType.Dictionary:
                    dic_val = new Dictionary<string, ConfigData>();
                    break;
            }

            this.type = type;
        }

        public override string ToString()
        {
            switch (type)
            {
                case DataType.Bool:
                    return bool_val.ToString();

                case DataType.Int:
                    return int_val.ToString();

                case DataType.Long:
                    return long_val.ToString();

                case DataType.Double:
                    return double_val.ToString();

                case DataType.Float:
                    return float_val.ToString();

                case DataType.String:
                    return string_val;

                case DataType.List:
                    return "ConfigData list";

                case DataType.Dictionary:
                    return "ConfigData dictionary";
            }

            return "Uninitialized ConfigData";
        }

        #region IList and IDictionary implementation
        public int Count
        {
            get
            {
                return AsList().Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return AsList().IsReadOnly;
            }
        }

        public ICollection<string> Keys
        {
            get
            {
                return AsDictionary().Keys;
            }
        }

        public ICollection<ConfigData> Values
        {
            get
            {
                return AsDictionary().Values;
            }
        }

        public ConfigData this[string key]
        {
            get
            {
                return AsDictionary()[key];
            }
            set
            {
                AsDictionary()[key] = value;
            }
        }

        public ConfigData this[int index]
        {
            get
            {
                return AsList()[index];
            }
            set
            {
                AsList()[index] = value;
            }
        }

        public int IndexOf(ConfigData item)
        {
            return AsList().IndexOf(item);
        }

        public void Insert(int index, ConfigData item)
        {
            AsList().Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            AsList().RemoveAt(index);
        }

        public void Add(ConfigData item)
        {
            AsList().Add(item);
        }

        public void Clear()
        {
            AsList().Clear();
        }

        public bool Contains(ConfigData item)
        {
            return AsList().Contains(item);
        }

        public void CopyTo(ConfigData[] array, int arrayIndex)
        {
            AsList().CopyTo(array, arrayIndex);
        }

        public bool Remove(ConfigData item)
        {
            return AsList().Remove(item);
        }

        public IEnumerator<ConfigData> GetEnumerator()
        {
            return AsList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return AsList().GetEnumerator();
        }

        public bool ContainsKey(string key)
        {
            return AsDictionary().ContainsKey(key);
        }

        public void Add(string key, ConfigData value)
        {
            AsDictionary().Add(key, value);
        }

        public bool Remove(string key)
        {
            return AsDictionary().Remove(key);
        }

        public bool TryGetValue(string key, out ConfigData value)
        {
            return AsDictionary().TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<string, ConfigData> item)
        {
            AsDictionary().Add(item);
        }

        public bool Contains(KeyValuePair<string, ConfigData> item)
        {
            return AsDictionary().Contains(item);
        }

        public void CopyTo(KeyValuePair<string, ConfigData>[] array, int arrayIndex)
        {
            AsDictionary().CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<string, ConfigData> item)
        {
            return AsDictionary().Remove(item);
        }

        IEnumerator<KeyValuePair<string, ConfigData>> IEnumerable<KeyValuePair<string, ConfigData>>.GetEnumerator()
        {
            return AsDictionary().GetEnumerator();
        }
        #endregion
    }
}