using System.IO;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class PathUtil
{
#if UNITY_EDITOR
    /// <summary>
    /// 分平台打包输出AB包文件夹
    /// </summary>
    /// <returns></returns>
    public static string PlatformBuildPath
    {
        get
        {
            string path = "AssetBundles/Other";
            if(EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows)
                path = "AssetBundles/Windows";
            else if(EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
                path = "AssetBundles/iOS";
            else if(EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
                path = "AssetBundles/Android";
            return CombinePath(ProjectPath, path);
        }
    }
#endif
    /// <summary>
    /// 需要打包的资源文件夹
    /// </summary>
    /// <returns></returns>
    public static string BuildAssetsPath
    {
        get
        {
            return CombinePath(AssetsPath, "BuildAssets");
        }
    }
    public static string ResourcesBuildPath
    {
        get
        {
            return CombinePath(ResourcesPath, "BuildAssets");
        }
    }
    /// <summary>
    /// AB包输出文件夹
    /// </summary>
    /// <returns></returns>
    public static string AssetBundleBuildPath
    {
        get
        {
            return CombinePath(ProjectPath, "AssetBundles", PlatformUtil.GetPlatformName());
        }
    }

    public static string PlatformPerfix
    {
        get
        {
            if(Application.platform == RuntimePlatform.Android)
                return "";
            else if(Application.platform == RuntimePlatform.WindowsEditor)
                return "file:///";
            else
                return "file://";
        }
    }
    /// <summary>
    /// 随APP打包的AB包文件夹
    /// </summary>
    /// <returns></returns>
    public static string RawAssetBundlesPath
    {
        get
        {
            return CombinePath(StreamingAssetsPath, "AssetBundles", PlatformUtil.GetPlatformName());                
        }
    }

    //运行时加载AB包文件夹
    public static string AssetBundlesPath
    {
        get
        {
// #if UNITY_EDITOR
//             return AssetBundleBuildPath;
// #else
            return CombinePath(PersistentDataPath, "AssetBundles", PlatformUtil.GetPlatformName());
// #endif
        }
    }

    public static string LocalRawPath
    {
        get
        {
            return CombinePath(StreamingAssetsPath, "RawAssets");
        }
    }

    public static string RawAssetsPath
    {
        get
        {
            return CombinePath(PersistentDataPath, "RawAssets");
        }
    }

    public static string AssetsPath
    {
        get
        {
            return Application.dataPath;
        }
    }

    public static string ResourcesPath
    {
        get
        {
            return CombinePath(Application.dataPath, "Resources");
        }
    }

    public static string PersistentDataPath
    {
        get
        {
            return Application.persistentDataPath;
        }
    }

    public static string StreamingAssetsPath
    {
        get
        {
            return Application.streamingAssetsPath;
        }
    }

    public static string ProjectPath
    {
        get
        {
            return Application.dataPath.Substring(0, Application.dataPath.Length - "Assets".Length);
        }
    }

    public static string DatabasePath
    {
        get
        {
            return CombinePath(PersistentDataPath, "app.db");
        }
    }

    public static string PathToABName(string path)
    {
        return path.Replace('\\', '/').Replace('/', '_').ToLower();
    }

    public static Resource PathToResource(string path)
    {
        string packName = path.Substring(0, path.IndexOf('/'));
        string resName = path.Substring(path.LastIndexOf('/') + 1, path.Length - (path.LastIndexOf('/') + 1));
        return new Resource(packName, path.Replace(packName + '/', "").Replace('/' + resName, ""), resName);
    }

    public static string CombinePath(string path1, string path2)
    {
        return Path.Combine(path1, path2).Replace('\\', '/');
    }

    public static string CombinePath(params string[] paths)
    {
        string path = paths[0];
        for(int i = 1; i < paths.Length; i++)
        {
            path = CombinePath(path, paths[i]);
        }
        return path;
    }

    public static string RelativePath(string dir, string subDir)
    {
        return subDir.Replace('\\', '/').Replace(dir.Replace('\\', '/'), "").Trim('/');
    }

    public static string GetDirectoryName(string path)
    {
        return Path.GetDirectoryName(path);
    }

    public static string GetFileName(string path)
    {
        return Path.GetFileName(path);
    }

    public static string GetFilePath(string path)
    {
        return path.Replace (Path.GetFileName(path), "");
    }

    public static bool CheckFileInPath(string file, string path)
    {
        if(file.Replace('\\', '/').StartsWith(path.Replace('\\', '/')))
            return true;
        else
            return false;
    }
}