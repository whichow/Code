using System.IO;

public class FileUtil
{
    public static byte[] ReadAllBytes(string path)
    {
        return File.ReadAllBytes(path);
    }

    public static string ReadAllText(string path)
    {
        return File.ReadAllText(path);
    }

    public static string[] ReadAllLines(string path)
    {
        return File.ReadAllLines(path);
    }

    public static void WriteAllBytes(string path, byte[] bytes)
    {
        File.WriteAllBytes(path, bytes);
    }

    public static void WriteAllText(string path, string text)
    {
        File.WriteAllText(path, text);
    }

    public static void WriteLines(string path, string[] lines)
    {
        File.WriteAllLines(path, lines);
    }

    public static void MoveFile(string src, string dest)
    {
        File.Move(src, dest);
    }

    public static void CopyFile(string src, string dest)
    {
        File.Copy(src, dest);
    }

    public static void CopyFiles(string[] files, string path)
    {
        foreach(var file in files)
        {
            FileUtil.CopyFile(file, PathUtil.CombinePath(path, PathUtil.GetFileName(file)));
        }
    }

    public static void DeleteFile(string path)
    {
        File.Delete(path);
    }

    public static bool FileExists(string path)
    {
        return File.Exists(path);
    }

    public static int GetFileSize(string path)
    {
        FileInfo info = new FileInfo(path);
        return (int)info.Length;
    }
}