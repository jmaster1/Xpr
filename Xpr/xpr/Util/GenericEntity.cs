namespace Xpr.xpr.Util;

public class GenericEntity
{
    public static void Assert(bool condition)
    {
        if (!condition)
        {
            throw new Exception();
        }
    }
    
    public void Log(string line)
    {
        Console.WriteLine(line);
    }
    
    public void Log(string line, params object[] args)
    {
        var lf = string.Format(line, args);
        Log(lf);
    }
    
    public T? Cast<T>()
    {
        if (typeof(T).IsAssignableFrom(GetType()))
        {
            return (T)(object)this;
        }

        return default;
    }
}