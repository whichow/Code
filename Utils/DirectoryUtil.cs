
using System.IO;

public class DirectoryUtils
{
    public static string[] GetDirectoryFiles(string dirName)
    {
        return Directory.GetFiles(dirName);
    }

    public static string[] GetAllDirectoryFiles(string dirName)
    {
        return Directory.GetFiles(dirName, "*", SearchOption.AllDirectories);
    }

    public static void CreateDirectory(string dirName)
    {
        if (!Directory.Exists(dirName))
        {
            Directory.CreateDirectory(dirName);
        }
    }

    public static void DeleteDirectory(string dirName)
    {
        if (Directory.Exists(dirName))
        {
            Directory.Delete(dirName, true);
        }
    }
    public static void CopyDirectory(string sourceDirName, string destDirName, bool copySubDirs = true)
    {
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);

        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }

        DirectoryInfo[] dirs = dir.GetDirectories();
        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }
        
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string temppath = Path.Combine(destDirName, file.Name);
            file.CopyTo(temppath, true);
        }

        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                CopyDirectory(subdir.FullName, temppath, copySubDirs);
            }
        }
    }

    public static bool DirectoryExists(string path)
    {
        return Directory.Exists(path);
    }
    
    public static string[] GetSubDirectories(string path)
    {
        return Directory.GetDirectories(path);
    }
}