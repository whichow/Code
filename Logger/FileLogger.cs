using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;

public class FileLogger : Logger
{

    private FileStream _fileStream;
    private StreamWriter _streamWriter;

    protected override void Awake()
    {
        base.Awake();
#if UNITY_EDITOR
        string path = "log/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".log";
#else
		string path = Application.persistentDataPath + "/log/" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".log";
#endif
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        _fileStream = new FileStream(path, FileMode.OpenOrCreate);
        _streamWriter = new StreamWriter(_fileStream);
    }

    void OnApplicationQuit()
    {
        _streamWriter.Flush();
        _streamWriter.Close();
        _fileStream.Close();
    }

    public override void PrintLog(Log log)
    {
		StringBuilder sb = new StringBuilder();
        if (log.logType == LogType.Assert || log.logType == LogType.Error || log.logType == LogType.Exception)
        {
            sb.Append("[Error]: ");
        }
        else if (log.logType == LogType.Warning)
        {
            sb.Append("[Warning]: ");
        }
		else
		{
			sb.Append("[Info]: ");
		}
		sb.Append(log.condition).AppendLine().Append(log.stacktrace);
        _streamWriter.WriteLine(sb.ToString());
    }
}
