using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Batchmode
{
    
    public static void BuildAndroid()
    {
        var config = BuildPipline.BuildConfig.LoadConfig("BuildConfig.xlsx", new BuildPipline.ExcelConfigParser());
        BuildPipline.Builder.DefaultBuilder.Build(config, System.Environment.GetCommandLineArgs());
        
    }

    [MenuItem("Build/Build Android")]
    public static void OpenBuildPanel()
    {
        var config = BuildPipline.BuildConfig.LoadConfig("BuildConfig.xlsx", new BuildPipline.ExcelConfigParser());
        BuildPipline.Builder.DefaultBuilder.Build(config, new[] { "-channel", "ANHUI_YIDONG" });
    }
}
