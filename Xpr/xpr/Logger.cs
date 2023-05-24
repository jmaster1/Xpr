namespace Xpr.xpr;

public class Logger
{
    public static void Assert(bool condition)
    {
        if (!condition)
        {
            throw new Exception();
        }
    }
    
    public void log(string line)
    {
        Console.WriteLine(line);
    }
    
    public void log(string line, params object[] args)
    {
        var lf = string.Format(line, args);
        log(lf);
    }
}