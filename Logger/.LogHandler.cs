using System;
using System.Text;
using UnityEngine;

#if false
public class LogHandler : ILogHandler
{
    protected ILogHandler _unityLogHandler = Debug.logger.logHandler;

    private LogLevel _logLevel;

    public LogHandler(LogLevel level)
    {
        _logLevel = level;
    }

    public virtual void LogException(Exception exception, UnityEngine.Object context)
    {
        _unityLogHandler.LogException(exception, context);
    }

    public virtual void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        _unityLogHandler.LogFormat(logType, context, format, args);
    }
}

public class GUILogHandler : LogHandler
{
    private GUILogger _logger;
    public GUILogHandler(LogLevel level) : base(level)
    {
        _logger = Logger.GetLogger<GUILogger>();
    }

    public override void LogException(Exception exception, UnityEngine.Object context)
    {
        base.LogException(exception, context);
    }

    public override void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        base.LogFormat(logType, context, format, args);
        _logger.printLog(new Logger.Log() { condition = String.Format(format, args) });
    }
}

public class FileLogHandler : LogHandler
{
    private FileLogger _logger;
    public FileLogHandler(LogLevel level) : base(level)
    {
        _logger = Logger.GetLogger<FileLogger>();
    }

    public override void LogException(Exception exception, UnityEngine.Object context)
    {
        base.LogException(exception, context);
    }

    public override void LogFormat(UnityEngine.LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        base.LogFormat(logType, context, format, args);
        _logger.Log(String.Format(format, args));
    }
}

public class WebLogHandler : ILogHandler
{
    public void LogException(Exception exception, UnityEngine.Object context)
    {
        throw new NotImplementedException();
    }

    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        throw new NotImplementedException();
    }
}

public class LogglyLogHandler : ILogHandler
{
    public void LogException(Exception exception, UnityEngine.Object context)
    {
        throw new NotImplementedException();
    }

    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        throw new NotImplementedException();
    }
}

public class SocketLogHandler : ILogHandler
{
    public void LogException(Exception exception, UnityEngine.Object context)
    {
        throw new NotImplementedException();
    }

    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        throw new NotImplementedException();
    }
}
#endif