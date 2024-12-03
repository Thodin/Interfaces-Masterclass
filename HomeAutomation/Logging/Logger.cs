namespace HomeAutomation.Logging;

public interface ILogger
{
    void Log(string message);
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
    }
}

public class FileLogger : ILogger, IDisposable
{
    private readonly StreamWriter _writer;

    public FileLogger(string filePath)
    {
        // object initializer syntax.
        // would be the same as _writer.AutoFlush = true;
        _writer = new StreamWriter(filePath, append: true) { AutoFlush = true };
    }

    public void Log(string message)
    {
        _writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}");
    }

    public void Dispose()
    {
        // Make sure we close the file.
        _writer.Dispose();
    }
}