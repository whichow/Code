using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public class ResourceInfo
{
    public string name;
    public int size;
    public string hash;

    public override bool Equals(System.Object obj)
    {
        if(obj == null)
            return false;
        var res = obj as ResourceInfo;
        if(res == null)
            return false;
        return res.name == name && res.hash == hash;
    }
}

public class ResourceUtil
{
    public static ResourceInfo[] CreateResourceList(string[] paths)
    {
        List<ResourceInfo> list = new List<ResourceInfo>();
        foreach(var path in paths)
        {
            ResourceInfo data = new ResourceInfo();
            data.name = PathUtil.RelativePath(PathUtil.AssetBundleBuildPath, path);
            data.size = FileUtil.GetFileSize(path);
            data.hash = ComputeHash(path);

            list.Add(data);
        }
        return list.ToArray();
    }

    public static string ComputeHash(string path)
    {
        StringBuilder hash = new StringBuilder();

        using (MD5 md5 = MD5.Create())
        {
            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                foreach (var b in md5.ComputeHash(fs))
                {
                    hash.Append(b.ToString("x2").ToLower());
                }
            }
        }
        return hash.ToString();
    }
}