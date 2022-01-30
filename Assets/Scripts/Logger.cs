using System.IO;

public static class Logger
{

    private static string path = "Log.txt";

    private static StreamWriter writer = null;

    public static void Log(string message)
    {
#if MY_DEBUG
        if (writer == null)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
            }
            writer = File.AppendText(path);
        }

        writer.WriteLine(message);
#endif
    }
}