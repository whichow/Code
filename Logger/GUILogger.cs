using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;

public class GUILogger : Logger
{
    private List<Log> logs = new List<Log>();
    private bool enable = false;
	private bool scroll = true;
    private Vector2 scrollPosition;
    private string tracke = "";
    private string filter = "";
    private bool info = true;
    private bool warning = true;
    private bool error = true;

    void OnGUI()
    {
        enable = GUILayout.Toggle(enable, "Debug Console");
        if (!enable) return;
		GUILayout.BeginHorizontal();
		scroll = GUILayout.Toggle(scroll, "Auto Scroll");
		if (GUILayout.Button("Clear", GUILayout.Width(50)))
        {
            logs.Clear();
        }
		GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        GUILayout.Label("Filter", GUILayout.Width(30));
        filter = GUILayout.TextField(filter);
        List<Log> filterLog = logs;

        if (GUILayout.Button("x", GUILayout.Width(20)))
        {
            filter = "";
        }
        info = GUILayout.Toggle(info, "I", GUILayout.Width(30));
        warning = GUILayout.Toggle(warning, "W", GUILayout.Width(30));
        error = GUILayout.Toggle(error, "E", GUILayout.Width(30));
        GUILayout.EndHorizontal();

        if (!info)
        {
            filterLog = filterLog.FindAll((s) => { return s.logType != LogType.Log; });
        }
        if (!warning)
        {
            filterLog = filterLog.FindAll((s) => { return s.logType != LogType.Warning; });
        }
        if (!error)
        {
            filterLog = filterLog.FindAll((s) => { return (s.logType != LogType.Error && s.logType != LogType.Assert && s.logType != LogType.Exception); });
        }

        if (!string.IsNullOrEmpty(filter))
        {
            filterLog = filterLog.FindAll((s) => { return s.condition.Contains(filter); });
        }
		if(scroll) scrollPosition = new Vector2(0, Mathf.Infinity);
        scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Width(Screen.width * 0.5f), GUILayout.Height(Screen.height * 0.5f));
        for (int i = 0; i < filterLog.Count; i++)
        {
            Log log = filterLog[i];
            string format = "{0}";
            if (log.logType == LogType.Assert || log.logType == LogType.Error || log.logType == LogType.Exception)
            {
                format = "<color=red>{0}</color>";
            }
            else if (log.logType == LogType.Warning)
            {
                format = "<color=yellow>{0}</color>";
            }
            GUIStyle guiStyle = GUI.skin.GetStyle("Box");
            guiStyle.alignment = TextAnchor.UpperLeft;
            if (GUILayout.Button(string.Format(format, log.condition), guiStyle))
            {
                tracke = filterLog[i].stacktrace;
            }
        }
        GUILayout.EndScrollView();

        if (!string.IsNullOrEmpty(tracke))
        {
            GUILayout.TextArea(tracke, GUILayout.MaxWidth(Screen.width * 0.5f));
        }
    }

    public override void PrintLog(Log log)
    {
        logs.Add(log);
    }

    public void ClearLog()
    {
        logs.Clear();
    }
}
