using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public enum LogLevel
{
    All,
    None,
    Info,
    Warning,
    Error
}

public class Logger : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Logger/Create File Logger")]
    static void CreateFileLog()
    {
        instance.gameObject.AddComponent<FileLogger>();
    }

    [MenuItem("Logger/Create GUI Logger")]
    static void CreateGUILog()
    {
        instance.gameObject.AddComponent<GUILogger>();
    }
#endif
	public class Log
	{
		public int count = 1;
		public UnityEngine.LogType logType;
		public string condition;
		public string stacktrace;
	}

	public LogLevel logLevel;

    private static GameObject instance
    {
        get
        {
            GameObject go;
            if((go = GameObject.Find("Logger")) == null)
            {
                go = new GameObject("Logger");
            }
            return go;
        }
    }

    protected virtual void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        Application.logMessageReceived += OnLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= OnLog;
    }

    private void OnLog(string condition, string stackTrace, LogType type)
    {
		if(logLevel == LogLevel.None)
		{
			return;
		}
		else if(logLevel == LogLevel.Warning)
		{
			if(type == LogType.Log) return;
		}
		else if(logLevel == LogLevel.Error)
		{
			if(type == LogType.Log || type == LogType.Warning) return;
		}
		Logger.Log log = new Logger.Log() {condition = condition, stacktrace = stackTrace, logType = type};
        PrintLog(log);
    }

	public virtual void PrintLog(Logger.Log log)
	{

	}
}
