namespace Xpr.xpr;

public class Logger
{
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